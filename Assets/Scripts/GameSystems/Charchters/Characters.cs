using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [HideInInspector]
    public bool IsDarkPlayer;



    public virtual void KillPlayer(bool Respawn)
    {
        gameObject.SetActive(false);

        if(Respawn && IsDarkPlayer && GameManager.Instance.ShadowPlayerCheckPoint != null)
        {
            gameObject.transform.position = GameManager.Instance.ShadowPlayerCheckPoint.RespawnLocationDark.position;
            gameObject.SetActive(true);
        }
        else if(Respawn && !IsDarkPlayer && GameManager.Instance.LightPlayerCheckPoint != null)
        {
            gameObject.transform.position = GameManager.Instance.LightPlayerCheckPoint.RespawnLocationLight.position;
            gameObject.SetActive(true);
        }
        else
        {
            print("Game over");
            GameManager.Instance.GameOver();
        }
    }
}
