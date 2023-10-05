using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private LightPlayer _lightPlayer;
    private float Distance;

    [SerializeField] private float _playerTakesDamage;
    [SerializeField] private float _killZone;
    [SerializeField] private float _damageDealt;

    private bool _lightPlayerClose;

    private float _timer;
    [SerializeField] private float _timerLimit;

    private void Start()
    {
        if(_timerLimit > 0.5f)
        {
            _timerLimit = 0.5f;
        }

        if(FindObjectsOfType<LightPlayer>().Length >= 1)
        {
            //Debug.LogError("More then 1 Light player found in scene");
        }

        _lightPlayer = FindObjectOfType<LightPlayer>();
    }

    private void Update()
    {
        if (_lightPlayerClose)
        {
            _timer += Time.deltaTime;

            if(_timer >= _timerLimit)
            {
                DistanceCheck();

                _timer = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerTakesDamage);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _killZone);
    }

    private void DistanceCheck()
    {
        Distance = Vector3.Distance(this.transform.position, _lightPlayer.transform.position);

        if(Distance <= _playerTakesDamage)
        {
            if(_lightPlayer != null)
            {
                _lightPlayer.ChangeEnergyLevel(_damageDealt);
            }
        }
        
        if(Distance <= _killZone)
        {
            if (_lightPlayer != null)
            {
                _lightPlayer.KillPlayer(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LightPlayer light = other.GetComponent<LightPlayer>();
        if(light != null)
        {
            print(light.name);
            _lightPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LightPlayer light = other.GetComponent<LightPlayer>();
        if (light != null)
        {
            _lightPlayerClose = false;
        }
    }
}

