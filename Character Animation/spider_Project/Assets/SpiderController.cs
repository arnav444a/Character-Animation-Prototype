using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator> ();

    }

    private void Update()
    {
        if (myAnimator)
        {
            myAnimator.SetFloat("speed", Input.GetAxis("Vertical"));
        }
    }
}
