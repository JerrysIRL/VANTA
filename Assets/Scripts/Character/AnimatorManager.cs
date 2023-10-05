using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
   
    int speed; 

    private void Awake()
    {
        animator = GetComponent<Animator>();
       
        speed = Animator.StringToHash("Speed");
    }
    public void UpdateAnimatorValues()
    {
       
    }
}
