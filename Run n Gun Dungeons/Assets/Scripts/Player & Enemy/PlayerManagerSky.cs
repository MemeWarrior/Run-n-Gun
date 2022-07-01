using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerSky : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    private float speed = 6.0f;
    private float moveLimiter = 0.7f;
    public GameObject currentweapon;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
    }

    void Run()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;
        bool playerHasYAxisSpeed = Mathf.Abs(body.velocity.y) > Mathf.Epsilon;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animator.SetBool("Running", playerHasXAxisSpeed || playerHasYAxisSpeed);
    }
    

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement and limits diagonal speed
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (body.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (body.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
