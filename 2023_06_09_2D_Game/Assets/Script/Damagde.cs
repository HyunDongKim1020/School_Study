using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagde : MonoBehaviour
{
    
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    CircleCollider2D circle;  

    // Start is called before the first frame update
    void Start()
    {     
        circle = GetComponent<CircleCollider2D>();      
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
 
    public void OnDamaged()
    {
        
        //��������Ʈ
        sprite.color = new Color(1, 1, 1, 0.4f);

        //����
        sprite.flipY = true;

        //�ݶ��̴� ��Ȱ��ȭ
        circle.enabled = false;

        //���� ����
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //����
        Invoke("Destory", 5);
    }

    void Destory()
    {
        gameObject.SetActive(false);
    }

}
