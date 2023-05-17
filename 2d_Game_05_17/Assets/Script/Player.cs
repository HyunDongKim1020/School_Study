using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Player_Speed = 3.0f;
    public float JumpPower = 1.0f;
    Rigidbody2D rigid;
    bool isJump;
    Animator anim;

    Vector2 movement = new Vector2();
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        isJump = false;
        //false 는 점프중이지 않을때
    }

    void Update()
    {
        move();
        Jump();
    }
    
    void move()
    {
        Vector3 FiltMove = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            FiltMove = Vector3.left;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            anim.SetBool("Run", true);
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            FiltMove = Vector3.right;
            transform.localScale = new Vector3(1f, 1f, 1f);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("IsJump", false);
        }

        transform.position += FiltMove * Player_Speed * Time.deltaTime;
    }

   void Jump()
    {
        if (isJump == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                anim.SetBool("IsJump", true);
                isJump = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isJump = false;
        }

        if(collision.collider.tag == "Enemy")
        {
            anim.SetTrigger("IsHit");
        }
    }
}
