using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCollision : MonoBehaviour
{
    [Header("Light Variables")]
    public Transform LightCheck;
    [SerializeField] private float _checkLightRadius;
    public LayerMask whatIsWall;
    public string LightLayer;
    public string DarkLayer;


    //takes all colliders in sphere and and changes them to lightlayer.
    public void LateUpdate()
    {
        var foundColliders = Physics.OverlapSphere(LightCheck.position, _checkLightRadius, whatIsWall);
        foreach (var item in foundColliders)
        {
            item.gameObject.layer = LayerMask.NameToLayer(LightLayer);
        }
    }


    //Draws gizmo for visual clarity in Scene.
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(LightCheck.position, _checkLightRadius);
    }

}
