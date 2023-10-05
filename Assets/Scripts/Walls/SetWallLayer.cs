using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWallLayer : MonoBehaviour
{
    public string WallLayer;

    //change layer back to wall.
    private void Update()
    {
        gameObject.layer = LayerMask.NameToLayer(WallLayer);
    }
}
