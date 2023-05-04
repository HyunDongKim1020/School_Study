using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        float amount = Speed * Time.deltaTime;
                                    //0,0,1 
        transform.Translate(Vector3.forward * amount);
    }
}
