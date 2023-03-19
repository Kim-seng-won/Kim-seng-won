using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy_애기용 : Enemy
{
    public enum State
    {
        Idle,
        Run,
        Attack
    };
    public State currentState = State.Idle;
    public ObjectManager objectManager;
    public Transform[] wallcheck;
    public Transform genPoint; // 총알생성위치
    public string Bullet1;
    WaitForSeconds Delay1000 = new WaitForSeconds(2f);
    public Item item;

    void Awake()
    {
        base.Awake();
        moveSpeed = 1f;
        jumpPower = 3f;
        Hp = 10;
        atkCoolTime = 3f;
        atkCoolTimeCal = atkCoolTime;
        Bullet1 = Bullet[0];

        StartCoroutine(FSM());
    }
    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }
    IEnumerator Idle()
    {
        yield return null;
        MyAnimSetTrigger("Idle");
        yield return Delay1000;
        currentState = State.Run;
    }
    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 3f); //걷는 시간
        while (runTime >= 0f)
        {
            runTime-= Time.deltaTime;
            MyAnimSetTrigger("Run");
            if (!isHit)
            {
                rigid.velocity = new Vector2(-transform.localScale.x * moveSpeed, rigid.velocity.y);
                if (Physics2D.OverlapCircle(wallcheck[0].position,0.01f, 1 << LayerMask.NameToLayer("wall")))
                {
                    MonsterFlip();
                }
                if (canAtk && IsPlayerDir())
                {
                    if(Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position)<3f)
                    {
                        currentState = State.Attack;
                        break;
                    }
                }
            }
            yield return null;
        }
        if(currentState != State.Attack)
        {
            if(Random.value>0.5f)
            {
            }
            else
            {
                currentState = State.Idle;
            }
        }
        
    }
    IEnumerator Attack()
    {
        yield return null;
        canAtk = false;
        rigid.velocity = new Vector2(0, jumpPower);//조우 효과
        MyAnimSetTrigger("Attack");
        yield return Delay1000;
        currentState = State.Idle;
    }
    void Fire()
    {
        GameObject bullet = objectManager.MakeObj(Bullet1);
        bullet.transform.position = genPoint.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
        bullet.transform.localScale = new Vector2(transform.localScale.x, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
            if(Hp <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        spriterenderer.color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = 16;
        item.drop();
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
