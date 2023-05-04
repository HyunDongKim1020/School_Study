using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float RunSpeed = 20.0f;
    public float rotSpeed = 60f;    //회전속도 초속 60초

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        //현재 프레임에서 이동할 거리
        float amount = moveSpeed * Time.deltaTime;
        
        //transform.Translate(Vector3.forward * amount);

        //현재 프레임에서 이동할 거리
        float amountRot = rotSpeed * Time.deltaTime;

        //전후진 이동키
        float vert = Input.GetAxis("Vertical");

        //좌우 이동키
        float horz = Input.GetAxis("Horizontal");
        
        //키보드 방향으로 이동
        transform.Translate(Vector3.forward * amount * vert);

        //조우로 회전
        transform.Translate(Vector3.up * amount * horz);

        //Shift키를 눌렀을때
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed * RunSpeed;
        }
        else
        {
            moveSpeed = 10.0f;
        }

    }


}
