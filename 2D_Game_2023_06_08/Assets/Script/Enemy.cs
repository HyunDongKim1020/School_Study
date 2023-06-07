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
        //�� �������θ� �˾Ƽ� �����̰�
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);//�������� ���ϱ� -1, y���� 0�� ������ ū�ϳ�!

        //����üũ
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
        nextMove = Random.Range(-1, 2);   //���������� �ִ밪�� ���� ���� �ʱ� ������ �ִ밪 1�� �ֱ� ���� 1���� ���� 2�� �ش�
                                          //�������� -1�̸� ����,0�̸� ������,1�϶� ����

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

        anim.SetInteger("WalkSpeed", nextMove);
        //ĳ���� ������ȯ
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
