using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public int nextMove;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;
    CircleCollider2D circle;
    public Transform target;
    public float moveSpeed;

    Coroutine coroutine;

    public int state = 0;  // 1 Think, 2 Follow


    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coroutine = StartCoroutine(Think());
    }

    private void Update()
    {
        switch (state)
        {
            case 1:
                break;
            case 2:
                break;
        }
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
        float distance = Vector3.Distance(transform.position, target.position);

    }
    IEnumerator Think()
    {
        while (true)
        {
            state = 1;

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
            yield return new WaitForSeconds(5.0f);
        }
    }
    void Return()
    {
        nextMove = nextMove * -1;
        sprite.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }
    void Destory()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(MoveTarget());
            //MoveTarget();
            //FaceTarget();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(Think());
        }
    }

    IEnumerator MoveTarget()
    {
        while (true)
        {
            state = 2;
            float dir = target.position.x - transform.position.x;
            dir = (dir < 0) ? -1 : 1;
            transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);

            if (dir == -1)
            {
                Debug.Log("왼쪽");
                anim.SetInteger("WalkSpeed", 1);
                sprite.flipX = false;
            }
            else
            {
                Debug.Log("오쪽");
                anim.SetInteger("WalkSpeed", 1);
                sprite.flipX = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }
    
}