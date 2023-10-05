using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ShadowHealthIndicator : MonoBehaviour
{
    private SkinnedMeshRenderer _renderer;

    private void Awake()
    {
       _renderer = GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    ///     Sets the emission fade of the attached Material
    /// </summary>
    /// <param name="level"> The value to set to. Should be between 0 and 1</param>
    public void SetEmissionLevel(float level)
    {
        _renderer.material.SetFloat("_Emissive_Fade", level);
    }
}
