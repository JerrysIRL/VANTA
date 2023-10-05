using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightElement : MonoBehaviour
{
    public int unlitLayer;
    public int litLayer;

    public List<LightElement> _lightSources;

    void Start()
    {
        _lightSources = new List<LightElement>();
        gameObject.layer = unlitLayer;
    }

    private void Update()
    {
        if(_lightSources.Count > 0)
        {
            gameObject.layer = litLayer;
        }
        else
        {
            gameObject.layer = unlitLayer;
        }
    }

    public void AddToLight(LightElement lightSource)
    {
        _lightSources.Add(lightSource);
    }

    public void RemoveFromLight(LightElement lightSource)
    {
        if (_lightSources.Contains(lightSource))
        {
            _lightSources.Remove(lightSource);
        }
        else
        {
            print(lightSource + " was not found in the list of " + this.gameObject);
        }
    }

    public virtual bool EnterLight() // virtual so that inheriting classes can override to do any behaviour
    {
        //gameObject.layer = litLayer;
        return true;
    }

    public virtual void ExitLight() // virtual so that inheriting classes can override to do any behaviour
    {
        //gameObject.layer = unlitLayer;
    }



    public bool IsLit { get { return gameObject.layer == litLayer; } } // returns true if element is lit
}
