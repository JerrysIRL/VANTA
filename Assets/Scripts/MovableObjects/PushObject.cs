using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushPower = 2.0f;
    public float interactDistance = 1.0f;

    private Transform cubeTransform;

    public void PushCube(Rigidbody cubeRb, Vector3 dir)
    {
        dir = cubeTransform.TransformDirection(dir);
        cubeRb.AddForce(dir * pushPower * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("PushableObject"))
        {
            cubeTransform = hit.transform;
            Vector3 localDarkPos = cubeTransform.InverseTransformPoint(transform.position);
            localDarkPos.Normalize();
            Vector3 dir = Vector3.zero;

            Rigidbody cubeRb = hit.gameObject.GetComponent<Rigidbody>();
            //cubeRb.constraints = RigidbodyConstraints.FreezeRotation;
            dir = CalculatePushDirection(localDarkPos, dir, cubeRb);
            PushCube(cubeRb, dir);
        }
    }

    private static Vector3 CalculatePushDirection(Vector3 localDarkPos, Vector3 dir, Rigidbody rb)
    {
        if (Mathf.Abs(localDarkPos.x) > Mathf.Abs(localDarkPos.z))
        {
            if (localDarkPos.x > 0)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.right;
            }
        }

        if (Mathf.Abs(localDarkPos.z) > Mathf.Abs(localDarkPos.x))
        {
            if (localDarkPos.z > 0)
            {
                dir = Vector3.back;
            }
            else
            {
                dir = Vector3.forward;
            }
        }

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        if (MathF.Abs(localDarkPos.y) > 0.6f)
        {
            if (Mathf.Abs(localDarkPos.y) > Mathf.Abs(localDarkPos.x) ||
                (Mathf.Abs(localDarkPos.y) > Mathf.Abs(localDarkPos.z)))
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
        }


        return dir;
    }
}