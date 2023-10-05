using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class LightDroplett : MonoBehaviour
{
    [Header("Destroy Var")]
    public float _timeLimit;
    private float _timer;

    [Header("Decrease Light Var")]
    [SerializeField] private float _minRange;
    [SerializeField] private float _maxRange;
    private Light _light;
    private float _deathSpeed = 10;
    private bool _dead;
    private SphereCollider _sphereCollider;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _light = GetComponent<Light>();
        _maxRange = _light.range;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        if(_timer >= _timeLimit && !_dead)
        {
            _dead = true;
            StartCoroutine(DeathSequence());
        }

        if (_dead)
        {
            return;
        }

        _light.range = Mathf.Lerp(_maxRange, _minRange, _timer / _timeLimit);
        _sphereCollider.radius = _light.range;
    }

    private IEnumerator DeathSequence()
    {
        while (_light.range > 0)
        {
            _light.range -= Time.deltaTime * _deathSpeed;
            _sphereCollider.radius = _light.range;
            yield return null;
        }

        Destroy(gameObject);
    }
}
