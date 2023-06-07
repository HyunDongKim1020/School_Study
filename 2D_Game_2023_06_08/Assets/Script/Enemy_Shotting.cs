using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shotting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletPos;

    private GameObject player;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
       
        if(distance < 10)
        {
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        Instantiate(Bullet, BulletPos.position, Quaternion.identity);
    }
}
