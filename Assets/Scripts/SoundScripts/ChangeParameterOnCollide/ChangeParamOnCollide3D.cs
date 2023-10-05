using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParamOnCollide3D : MonoBehaviour
{
    [SerializeField] private Default3dSoundScript soundToChange;
    [SerializeField] private float volumeChange;
    //[SerializeField] private int collisionLayerIntVal;



    void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.layer==collisionLayerIntVal)
        {
            soundToChange.volume=volumeChange;
        }
    }
}
