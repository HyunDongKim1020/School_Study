using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip audioItem;
    public AudioClip audioFinish;
    public AudioClip audioAttack;
    public AudioClip audioJump;
    public AudioClip audioDamaged;
    public AudioClip audioDie;
    public Audio AUD;
    

    public GameManager gameManager;
    public float Player_Speed = 3.0f;
    public float JumpPower = 1.0f;
    public bool isJump;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer SpriteRenderer;
    CircleCollider2D circle;
    AudioSource audioSource;

    Vector2 movement = new Vector2();
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        isJump = false;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        //false 는 점프중이지 않을때
    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
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
                PlaySound("JUMP");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isJump = false;
        }
        if (collision.collider.tag == "Enemy")
        {
            isJump = false;
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            PlaySound("ITEM");
            gameManager.StagePoint += 100;

            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            PlaySound("FINISH");
            gameManager.NextStage();
        }
       

    }
    void OnDamaged(Vector2 targetPos)
    {
        PlaySound("DAMAGED");
        gameManager.HpDown();

        //player  damaged
        gameObject.layer = 11;

        SpriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //reaction forc
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse);

        //animation
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 2);
    }

    void OnAttack(Transform enemy)
    {
        PlaySound("ATTACK");
        gameManager.StagePoint += 100;

        Enemy enemy1 = enemy.GetComponent<Enemy>();
        enemy1.OnDamaged();
    }
    void OffDamaged()
    {
        gameObject.layer = 10;
        SpriteRenderer.color = new Color(1, 1, 1, 1);

    }

    public void OnDie()
    {
        PlaySound("DIE");
        //스프라이트
        SpriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //반전
        SpriteRenderer.flipY = true;

        //콜라이더 비활성화
        circle.enabled = false;

        //위로 점프
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        Time.timeScale = 0;

        //AUD.StopAUD();

    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
