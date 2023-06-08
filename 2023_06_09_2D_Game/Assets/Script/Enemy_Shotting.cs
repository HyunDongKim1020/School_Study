using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shotting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletPos;
    public float moveSpeed;
    public Transform target;

    Animator anim;
    SpriteRenderer sprite;
    private GameObject player;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            if (timer > 5)
            {
                timer = 0;            
                shoot();
            }
        }
    }

    void shoot()
    {
        //anim.SetBool("Attack", true);
        Instantiate(Bullet, BulletPos.position, Quaternion.identity);
    }
}
