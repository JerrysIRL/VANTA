using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class DarkPlayer : Characters
{
    [SerializeField] private float _health;
    [SerializeField] private ShadowHealthIndicator _litItems;

    [SerializeField] private float _healthRegain;
    [SerializeField] private float _healthDrop;
    [SerializeField] private float _healthUpdateInterval;

    private float _targetHealth = 0;
    private LightReciever _lightReciever;

    [SerializeField] private float smoothSpeed = 0.05f;
    [SerializeField] private UnityEvent _onDeath;

    private float _timer;

    private void Start()
    {
        IsDarkPlayer = true;
        _targetHealth = _health;
        _lightReciever = GetComponent<LightReciever>();
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if(_timer >= _healthUpdateInterval)
        {
            UpdateHealth();
            _timer = 0;
        }
        _health = _targetHealth;

        //_health = Mathf.Lerp(_health, _targetHealth, smoothSpeed * Time.deltaTime);
        _litItems.SetEmissionLevel(_health / 100);
    }

    public void ChangeHealth(float change)
    {
        if(_targetHealth <= 100)
        {
            _targetHealth += change;
        }

        if(_targetHealth > 100)
        {
            _targetHealth = 100;
        }

        if(_health <= 0)
        {
            KillPlayer(true);
        } 


    }

    public override void KillPlayer(bool respawn)
    {
        _onDeath.Invoke();
        base.KillPlayer(respawn);
        if(_lightReciever.LightList.Count > 0)
        {
            _lightReciever.LightList.Clear();
        }
        _targetHealth = 100f;
    }

    private void UpdateHealth()
    {
        if (_lightReciever.Lit)
        {
            ChangeHealth(_healthRegain);
        }
        else
        {
            ChangeHealth(_healthDrop);
        }
    }
}
