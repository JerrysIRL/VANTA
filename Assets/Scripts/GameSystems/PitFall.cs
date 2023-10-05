using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DarkPlayer dark = other.gameObject.GetComponent<DarkPlayer>();
        LightPlayer light = other.gameObject.GetComponent<LightPlayer>();

        if (dark != null)
        {
            print("kill" + dark);
            dark.KillPlayer(true);
        }
        if (light != null)
        {
            print("kill" + light);
            light.KillPlayer(true);
        }
    }
}
