using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] float speed = 1;
    [SerializeField] float threshold;
    public float defSpeed;
    [SerializeField] float slowness = .5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        defSpeed = speed;
    }

    private void FixedUpdate()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector2 movement = new Vector2(horizontal, vertical);

        if (Mathf.Abs(movement.x) > threshold || Mathf.Abs(movement.y) > threshold)
        {
            //if(Mathf.Abs(movement.x) > threshold && Mathf.Abs(movement.y) > threshold) { movement *= .7f; }

            rb.velocity = movement * speed;

            Flip(movement.x < 0);
            animator.SetBool("move", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("move", false);
        }
    }

    void Flip(bool _p)
    {
        if (_p) { transform.eulerAngles = Vector3.up * 180; }
        else { transform.eulerAngles = Vector3.zero; }
    }
    
    public void SetMovement(float _percentage = 0)
    {
        defSpeed += defSpeed * _percentage;
        speed = defSpeed;
    }

    public void SetSpeed()
    {
        speed *= slowness;
    }

    public void Halt(bool _p)
    {
        if (_p) { speed = 0; }
        else { speed = defSpeed; }
    }
}