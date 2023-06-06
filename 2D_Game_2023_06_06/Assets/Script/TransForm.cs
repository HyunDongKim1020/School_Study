using UnityEngine;

public class TransForm : MonoBehaviour
{
    public Transform desPos;
    public Transform startPos;
    public Transform EndPos;
    public float speed;
    private void Start()
    {
        transform.position = startPos.position;
        desPos = EndPos;
    }

    private void Update()
    {
        transform.position =  Vector2.MoveTowards(transform.position, desPos.position,Time.deltaTime * speed);
      
        if(Vector2.Distance(transform.position,desPos.position) <= 0.05f)
        {
            if(desPos == EndPos)
            {
                desPos = startPos;
            }
            else
            {
                desPos = EndPos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            transform.localScale = new Vector3(1.1f, 1.1f, 1.2f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
