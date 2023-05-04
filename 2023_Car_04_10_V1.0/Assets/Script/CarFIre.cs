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
        //딜레이 타임에서 델타 타임을 더한다 
        if(DelayTime >= 0.1)
            //델타 타임을 0.05라 했을때 딜레이 타임을 0이라 하면 0+0.05이므로 0.1보다 작다
        {
            DelayTime = 0f;
            Instantiate(Bullet, spPoint.position, spPoint.rotation);
        }
      //딜레이 타임이 0.1보다 작으므로 밖으로 나가고 위에서 델타 타임을 다시 더한다
      //0.05+0.05 는 1이 므로 if문이 성립하며 구문을 실행한다
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
        //만들 오브젝트,만들위치,만든 위치의 회전값
        GunSound[1].Play();
    }
}
