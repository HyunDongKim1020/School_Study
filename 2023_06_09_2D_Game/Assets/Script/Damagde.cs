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
        
        //스프라이트
        sprite.color = new Color(1, 1, 1, 0.4f);

        //반전
        sprite.flipY = true;

        //콜라이더 비활성화
        circle.enabled = false;

        //위로 점프
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //삭제
        Invoke("Destory", 5);
    }

    void Destory()
    {
        gameObject.SetActive(false);
    }

}
