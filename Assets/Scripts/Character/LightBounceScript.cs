using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBounceScript : MonoBehaviour
{
    [SerializeField] int rayCount = 4;
    private void Update()
    {
        //call function
        CastRay(transform.position, transform.forward);
        
    }
    void CastRay(Vector3 position, Vector3 direction)
    {
        for(int i = 0; i < rayCount; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 10, 1))
            {
                Debug.DrawLine(position,hit.point,Color.white);
                position = hit.point;
                direction = hit.normal;
            }
            else
            {
                Debug.DrawRay(position, direction * 10, Color.blue);
                break;
            }
        }
        
    }
}
