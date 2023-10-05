using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWallOpacity : MonoBehaviour
{
    [SerializeField] private float raySphereRad = 1f;
    [SerializeField] private float rayDistanceModifier = 0.9f;
    public Transform lightP;
    public Transform darkP;
    private RaycastHit _hit;
    private HashSet<GameObject> _collisionStore = new HashSet<GameObject>();
    private RaycastHit[] _results = new RaycastHit[10];



    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, darkP.position);
    }

    private void FixedUpdate()
    {
        _collisionStore.Clear();
        PlayerSphereCastNonAlloc(darkP.position);
        PlayerSphereCastNonAlloc(lightP.position);
    }

    private void PlayerRaycast(Vector3 playerPos)
    {
        Vector3 dir = playerPos - transform.position;
        if (Physics.Raycast(transform.position, dir, out _hit, dir.magnitude))
        {
            var wall = _hit.transform.gameObject;
            if (wall.CompareTag("Wall"))
            {
                var wallFade = wall.GetComponent<WallFade>();
                wallFade.SetObstruction();
            }
        }
    }

    private void PlayerSphereCast(Vector3 playerPos)
    {
        Vector3 dir = playerPos - transform.position;
        var colliders = Physics.SphereCastAll(transform.position, raySphereRad, dir.normalized, dir.magnitude);
        {
            foreach (var raycastHit in colliders)
            {
                var wall = raycastHit.transform.gameObject;
                if (wall.CompareTag("Wall"))
                {
                    var wallFade = wall.GetComponent<WallFade>();
                    wallFade.SetObstruction();
                }
            }
        }
    }
    
    private void PlayerSphereCastNonAlloc(Vector3 playerPos)
    {
        Vector3 dir = playerPos - transform.position;
        dir.Normalize();
        
        var numberOfHits = Physics.SphereCastNonAlloc(transform.position, raySphereRad, dir, _results , Vector3.Distance(transform.position, playerPos) * rayDistanceModifier);
        {
            for (int i = 0; i < numberOfHits; i++)
            {
                _collisionStore.Add(_results[i].transform.gameObject);
            }
            foreach (var raycastHit in _collisionStore)
            {
                var wall = raycastHit.transform.gameObject;
                if (wall.CompareTag("Wall"))
                {
                    var wallFade = wall.GetComponent<WallFade>();
                    wallFade.SetObstruction();
                }
            }
        }
    }
}