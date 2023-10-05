using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class LightPlayer : Characters
{
    [Header("Main info")]
    [SerializeField] private float _energy;
    [SerializeField] private float _maxEnergy;

    [SerializeField] private float _maxRange;
    [SerializeField] private float _minRange;

    private Light _light;
    private bool _isDead;
    private float _deathSpeed = 10;
    private LightSource _source;
    private bool _tookDamage;
    private float _damageTimer;
    [SerializeField] private float _healDelay;

    [Header("Energy Decrease")]
    [SerializeField] private float _frequency;
    [SerializeField] private float _energyLoss;
    [SerializeField] private float smoothSpeed = 0.05f;
    private float _timer;

    [Header("Burst")]
    [SerializeField] private float _boostedArea;
    [SerializeField] private float _energyCostBurst;
    private float _burst;
    private float _boost;
    private float _boostTarget;

    [Header("Light Droplett")]
    [SerializeField] private LightDroplett _lightDroplet;
    [SerializeField] private float _lightBoostCostModifier;
    private float _cooldownTimer;
    private float _cooldownLimit;
    private bool _spawnedLightDroplett;

    private float _targetEnergy = 0;

    [SerializeField] private UnityEvent _onDeath;

    private void Start()
    {
        IsDarkPlayer = false;
        _light = GetComponent<Light>();
        _targetEnergy = _energy;
        _source = GetComponent<LightSource>();
        
        //_boostedArea = _boostedArea * _light.range;
    }

    private void Update()
    {

        if (_spawnedLightDroplett)
        {
            _cooldownTimer += Time.deltaTime;

            if(_cooldownTimer >= _cooldownLimit)
            {
                _cooldownTimer = 0;
                _spawnedLightDroplett = false;
            }
        }

        if (_tookDamage)
        {
            _damageTimer += Time.deltaTime;

            if(_damageTimer >= _healDelay)
            {
                RestoreHealth();
                _damageTimer = 0;
                _tookDamage = false;
            }
        }

        _timer += Time.deltaTime;

        if (_timer >= _frequency)
        {
            ChangeEnergyLevel(_energyLoss + _burst);
            _timer = 0;
        }

        _boost = Mathf.Lerp(_boost, _boostTarget, smoothSpeed * Time.deltaTime);

        if (_isDead)
        {
            return;
        }
        _source.UpdateRange(_minRange + (_energy / _maxEnergy) * (_maxRange - _minRange) + _boost);
        _energy = Mathf.Lerp(_energy, _targetEnergy, smoothSpeed * Time.deltaTime);
    }

    public void LightDropletSpawn()
    {
        if (!_spawnedLightDroplett)
        {
            LightDroplett temp = Instantiate<LightDroplett>(_lightDroplet, this.transform.position, Quaternion.identity);
            _cooldownLimit = temp._timeLimit;
            _spawnedLightDroplett = true;
            ChangeEnergyLevel(_lightBoostCostModifier);
        }
    }

    public void ChangeEnergyLevel(float energy)
    {
        _targetEnergy += energy;

        if(energy < 0)
        {
            _tookDamage = true;
            _damageTimer = 0;
        }

        if (_energy <= 0)
        {
            KillPlayer(true);
        }
    }

    public override void KillPlayer(bool respawn)
    {
        StartCoroutine(DeathSequence(respawn));
    }

    private IEnumerator DeathSequence(bool respawn)
    {
        _isDead = true;

        while (_light.range > 0)
        {
            _light.range -= Time.deltaTime * _deathSpeed;
            yield return null;
        }
        base.KillPlayer(respawn);
        _onDeath.Invoke();
        RestoreHealth();

        //List<LightDroplett> lights = new List<LightDroplett>();
        //
        //lights = FindObjectsOfType<LightDroplett>().ToList();
        //
        //for (int i = 0; i < lights.Count; i++)
        //{
        //    Destroy(lights[i].gameObject);
        //}
        _isDead = false;
    }
    
    private void RestoreHealth()
    {
        _targetEnergy = _maxEnergy;
    }

    public void BurstLight(bool boosting)
    {
        if (boosting)
        {
            _burst = _lightBoostCostModifier;
            _boostTarget = _boostedArea;
        }
        else
        {
            _burst = 0f;
            _boostTarget = 0f;
        }

    }
}
