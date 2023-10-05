using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private NearingTheEnd105 _music;
    [SerializeField] private float _maxDistance;

    private GameObject _lightPlayer;
    private Vector3 _distanceToLight;
    private bool _endSequence;

    private void OnTriggerEnter(Collider other)
    {
        _music.StopSound();
        GameManager.Instance.GameOver();
    }

    private void FixedUpdate()
    {
        if (!_endSequence)
        {
            return;
        }
        _distanceToLight = this.transform.position -_lightPlayer.transform.position;

        _music.SetIntensity(1 - (_distanceToLight.magnitude / _maxDistance));
    }

    public void StartEndSequence()
    {
        _lightPlayer = GameManager.Instance.GetPlayer((typeof(LightPlayer)));
        _endSequence = true;
    }
}
