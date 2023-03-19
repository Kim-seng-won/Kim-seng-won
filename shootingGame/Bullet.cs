using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(rigid.velocity.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipY = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border"|| collision.gameObject.tag == "Hitbox" || collision.gameObject.tag == "platform"|| collision.gameObject.tag == "Object")
        {
            gameObject.SetActive(false);
        }
    }
}
