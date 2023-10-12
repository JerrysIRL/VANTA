using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MultipeTargetCamera : MonoBehaviour
{
    [Header("Tunable variables")]
    [Range(0, 1)] [SerializeField] private float smoothTime;
    [SerializeField] private float minZoom = 30f; 
    [SerializeField] private float maxZoom = 100f;
    [SerializeField] private float zoomLimiter = 30f;
    [SerializeField] Vector3 cameraOffset;

    public Transform[] targets = new Transform[2];
    private Vector3 _velocity;      // reference variable, just something SmoothDamp needs, dont remove
    private Camera _cam;
    
    private void Start()
    {
        _cam = Camera.main;
    }

    void LateUpdate()
    {
        if (targets.Length == 0)     // if no players are in the game, return
        {
            return;
        }
        MoveCamera();
        Zoom();
    }

    Vector3 GetPlayerDistance()       // calculated the distance between players
    {
        Bounds bounds = EncapsulateTargets();
        return bounds.size;
    }

    private void MoveCamera()
    {
        Vector3 centerPosition = GetCenterPosition();
        Vector3 newPos = centerPosition + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref _velocity, smoothTime);
    }

    void Zoom()
    {
        float distance = (GetPlayerDistance().x > GetPlayerDistance().z) ? GetPlayerDistance().x : GetPlayerDistance().z;
        
        float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private Bounds EncapsulateTargets()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds;
    }
    
    
    /*protected bool IsOutsideThreshold()
    {
        if (targets.Length < 2)
        {
            Debug.Log("Set references to players");
            return false;
        }
        
        
        float dist = Vector3.Distance(targets[0].position, targets[1].position);
        if (dist >= distanceThreshold)
        {
            
        }
    }*/

    private Vector3 GetCenterPosition()
    {
        if (targets.Length == 1)
        {
            return targets[0].position;
        }
        Bounds bounds = EncapsulateTargets();

        return bounds.center;
    }
}