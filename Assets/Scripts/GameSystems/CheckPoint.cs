using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform RespawnLocationLight;
    public Transform RespawnLocationDark;

    private void OnTriggerEnter(Collider other)
    {
        DarkPlayer darkPlayer = other.GetComponent<DarkPlayer>();
        LightPlayer lightPlayer = other.GetComponent<LightPlayer>();

        if(darkPlayer != null)
        {
            GameManager.Instance.ShadowPlayerCheckPoint = this;
        }

        if(lightPlayer != null)
        {
            GameManager.Instance.LightPlayerCheckPoint = this;
        }
    }
}
