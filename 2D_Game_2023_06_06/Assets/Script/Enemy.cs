using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nextMove;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;
    CircleCollider2D circle;

    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        Think();
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //한 방향으로만 알아서 움직이게
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);//왼쪽으로 가니까 -1, y축은 0을 넣으면 큰일남!

        //지형체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platfrom"));
        if (rayHit.collider == null)
        {
            Return();
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);   //랜덤값에서 최대값은 포함 되지 않기 때문에 최대값 1을 주기 위해 1보다 높은 2를 준다
                                          //랜덤값이 -1이면 왼쪽,0이면 가만히,1일때 왼쪽

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

        anim.SetInteger("WalkSpeed", nextMove);
        //캐릭터 방향전환
        if (nextMove != 0)
        {
            sprite.flipX = nextMove == 1;
        }
    }
    void Return()
    {
        nextMove = nextMove * -1;
        sprite.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }

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
