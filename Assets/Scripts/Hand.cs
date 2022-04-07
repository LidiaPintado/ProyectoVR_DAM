using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float speed;
    Animator animator;
    private float gripTarget;
    private float gripCurrent;
    private string animatorGripParam = "Grip";


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        Debug.Log(v + "");
        gripTarget = v;
    }

    void AnimateHand()
    {
        Debug.Log(gripTarget + gripCurrent);
        Debug.Log(gripCurrent + " " + gripTarget);
        if (gripCurrent != gripTarget)
        {
            Debug.Log("Lanzando animación");
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
    }
}
