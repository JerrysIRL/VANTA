using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public Light _light;
    private SphereCollider _collider;
    [SerializeField] private LayerMask _raycastInclude;

    [SerializeField] float pivitOffset;

    private void Start()
    {
        _light = GetComponent<Light>();
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _light.range;        
    }

    public void UpdateRange(float Range)
    {
        _light.range = Range;
        _collider.radius = Range;
    }

    public bool CheckLineOfSight(LightReciever LR)
    {
        Vector3 position = LR.transform.position + Vector3.up * pivitOffset;
        Vector3 dir = (position - this.transform.position).normalized;

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, _light.range, _raycastInclude))
        {
            if (hit.transform.gameObject.GetComponent<LightReciever>() == LR)
            {

                Debug.DrawRay(this.transform.position, dir * hit.distance, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(this.transform.position, dir * hit.distance, Color.red);
            }
        }
        
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        LightReciever Reciever = other.GetComponent<LightReciever>();
        if(Reciever != null)
        {
            Reciever.AddLight(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LightReciever Reciever = other.GetComponent<LightReciever>();

        if (Reciever != null)
        {
            Reciever.RemoveLight(this);
        }
    }
}