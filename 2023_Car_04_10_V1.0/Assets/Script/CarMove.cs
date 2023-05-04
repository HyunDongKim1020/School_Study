using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float RunSpeed = 20.0f;
    public float rotSpeed = 60f;    //ȸ���ӵ� �ʼ� 60��

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        //���� �����ӿ��� �̵��� �Ÿ�
        float amount = moveSpeed * Time.deltaTime;
        
        //transform.Translate(Vector3.forward * amount);

        //���� �����ӿ��� �̵��� �Ÿ�
        float amountRot = rotSpeed * Time.deltaTime;

        //������ �̵�Ű
        float vert = Input.GetAxis("Vertical");

        //�¿� �̵�Ű
        float horz = Input.GetAxis("Horizontal");
        
        //Ű���� �������� �̵�
        transform.Translate(Vector3.forward * amount * vert);

        //����� ȸ��
        transform.Translate(Vector3.up * amount * horz);

        //ShiftŰ�� ��������
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
