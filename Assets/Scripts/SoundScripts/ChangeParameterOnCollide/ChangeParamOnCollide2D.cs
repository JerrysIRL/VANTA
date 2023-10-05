using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParamOnCollide2D : MonoBehaviour
{
    [SerializeField] private Default2dSoundScript soundToChange;
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
