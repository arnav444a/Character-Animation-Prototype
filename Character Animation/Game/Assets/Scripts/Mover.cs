using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Animator Anim;
    Vector2 movement;
    public Rigidbody2D rb;

    public float speed = 5f;
    private void Update()
    {
        movement.x= Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Anim.SetFloat("speedX", movement.x);
        Anim.SetFloat("speedY", movement.y);
        Anim.SetFloat("speed", movement.sqrMagnitude);
        if (movement.x==-1)
        {
            transform.localScale = new Vector3(-2.7179f, 2.7179f, 2.7179f);
        }
        else {
            transform.localScale = new Vector3(2.7179f, 2.7179f, 2.7179f);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
    }
}
