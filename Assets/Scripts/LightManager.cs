using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : Singleton<LightManager>
{
    public List<GameObject> _litObjects;

    protected override void Awake()
    {
        base.Awake();
        _litObjects = new List<GameObject>();
    }


}
