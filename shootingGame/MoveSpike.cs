using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpike : MonoBehaviour
{
    private Transform trans;
    private Rigidbody2D rigid;
    float startY;
    float y;
    void Awake()
    {
        startY= transform.position.y;
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }
    void Update()
    {
        y = transform.position.y;   
        if (y < startY - 10.0f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x ,30);
        }
        else if(y >= startY + 10.0f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x ,- 30);
        }
    }
}
