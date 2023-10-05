using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GetMesh : MonoBehaviour
{
    [SerializeField] GameObject target;


    private MeshFilter myMesh;

    private void Start()
    {
        myMesh = GetComponent<MeshFilter>();
        myMesh.mesh = target.GetComponent<MeshFilter>().mesh;
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
        transform.localScale= target.transform.localScale;
    }
}
