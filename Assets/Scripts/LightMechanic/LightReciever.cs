using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightReciever : MonoBehaviour
{
    public int unlitLayer = 7;
    public int litLayer = 6;
    public bool Lit;
    public List<LightSource> LightList;
    private List<bool> seen = new List<bool>();


    private void Start()
    {
        LightList = new List<LightSource>();    
    }

    private void Update()
    {
        
        Lit = false;

        for (int i = 0; i < LightList.Count; i++)
        {
            bool temp = LightList[i].CheckLineOfSight(this);
            if (temp) 
            {
                Lit = true;
                break;
            }
        }

        if (Lit)
        {
            gameObject.layer = litLayer;
        }
        else
        {
            gameObject.layer = unlitLayer;
        }
    }

    public void AddLight(LightSource light)
    {
        LightList.Add(light);
    }

    public void RemoveLight(LightSource light)
    {
        LightList.Remove(light);
    }
}
