using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFIre : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotSpeed = 60f;
    //private float DelayTime = 0.1f;
    private bool canFire = true;

    public Transform spPoint;
    public Transform Bullet;
    AudioSource[] GunSound;
    Rigidbody rigid;
    
    // Start is called before the first frame update
    void Start()
    {
        GunSound = GetComponents<AudioSource>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float amount = MoveSpeed * Time.deltaTime;
        float amountRot = RotSpeed * Time.deltaTime;

        float Vet = Input.GetAxis("Vertical");
        float Hoz = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * amount * Vet);
        transform.Rotate(Vector3.up * amountRot * Hoz);
        */
        if (Input.GetButtonDown("Fire1"))
        {
            SingleShoot();
        }
        if (Input.GetKey(KeyCode.LeftShift) && canFire == true)
        {
            StartCoroutine(AutoFire2());
        }
        
    }

    void FixedUpdate()
    {
        float Vet = Input.GetAxis("Vertical");
        float Hoz = Input.GetAxis("Horizontal");

        float amount = MoveSpeed * Vet * Time.deltaTime;
        float amountRot = RotSpeed * Hoz * Time.deltaTime;

        rigid.MovePosition(rigid.position + transform.forward * amount);
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(Vector3.up * amountRot));
    }

    /*void AutoFire()
    {
        DelayTime += Time.deltaTime;
        //������ Ÿ�ӿ��� ��Ÿ Ÿ���� ���Ѵ� 
        if(DelayTime >= 0.1)
            //��Ÿ Ÿ���� 0.05�� ������ ������ Ÿ���� 0�̶� �ϸ� 0+0.05�̹Ƿ� 0.1���� �۴�
        {
            DelayTime = 0f;
            Instantiate(Bullet, spPoint.position, spPoint.rotation);
        }
      //������ Ÿ���� 0.1���� �����Ƿ� ������ ������ ������ ��Ÿ Ÿ���� �ٽ� ���Ѵ�
      //0.05+0.05 �� 1�� �Ƿ� if���� �����ϸ� ������ �����Ѵ�
    }
    */

    IEnumerator AutoFire2()
    {
        GunSound[0].Play();
        Instantiate(Bullet, spPoint.position, spPoint.rotation);
        canFire = false;

        yield return new WaitForSeconds(0.1f);
        canFire = true;
        
    }

    void SingleShoot()
    {
        Instantiate(Bullet, spPoint.position, spPoint.rotation);
        //���� ������Ʈ,������ġ,���� ��ġ�� ȸ����
        GunSound[1].Play();
    }
}
