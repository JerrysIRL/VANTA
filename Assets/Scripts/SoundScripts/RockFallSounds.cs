using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RockFallSounds : MonoBehaviour
{
    private ParticleSystem particle;
    [SerializeField] private UnityEvent rockFall;

    [SerializeField] private int _particleCount;
    [SerializeField] private float _soundCooldown;

    private float _cooldownTimer;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particle.particleCount > _particleCount && _soundCooldown > _cooldownTimer)
        {
            rockFall.Invoke();
            _cooldownTimer = 0;
        }

        _particleCount = particle.particleCount;
        _cooldownTimer += Time.deltaTime;
    }
}
