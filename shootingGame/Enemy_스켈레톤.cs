using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_스켈레톤 : Enemy
{
    public Transform[] wallcheck;
    private Animator anim;
    private void Awake()
    {
        base.Awake();
        moveSpeed = 1f;
        jumpPower = 5;
        anim = GetComponent<Animator>();
        Hp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            move();
        }
    }
    private void move()
    {
        rigid.velocity = new Vector2(-transform.localScale.x*moveSpeed, rigid.velocity.y);
        if (Physics2D.OverlapCircle(wallcheck[0].position,0.1f,1 << LayerMask.NameToLayer("wall")))
        {
            MonsterFlip();
            anim.SetBool("IsWalking", true);
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
            if (Hp <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        spriterenderer.color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = 16;
        Invoke("extinction", 0.3f);
    }
   void extinction()
    {
        gameObject.SetActive(false);
        Invoke("respawn", 7);
    }
    void respawn()
    {
        spriterenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 7;
        gameObject.SetActive(true);
        Awake();
    }
}
