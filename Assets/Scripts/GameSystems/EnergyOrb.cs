using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    [SerializeField] private float _energyGain;

    private void OnTriggerEnter(Collider other)
    {
        DarkPlayer dark = other.GetComponent<DarkPlayer>();

        if(dark != null)
        {
            FindObjectOfType<LightPlayer>().ChangeEnergyLevel(_energyGain);
            Destroy(this.gameObject);
        }
    }
}
