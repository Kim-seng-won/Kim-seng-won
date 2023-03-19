using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp;
    public float moveSpeed;
    public float jumpPower;
    public float atkCoolTime = 3f;
    public float atkCoolTimeCal;
    public string[] Bullet;

    public bool isHit = false;
    public bool isGround = true;
    public bool canAtk = true;
    public bool MonsterDirRight;

    protected Rigidbody2D rigid;
    protected BoxCollider2D col;
    public GameObject hitBoxCollider;
    public Animator Anim;
    public LayerMask layerMask;
    public SpriteRenderer spriterenderer;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        Anim= GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        Bullet = new string[] { "EnemyBullet" };

        StartCoroutine(CalcCoolTime());
        StartCoroutine(ResetCollider());
    }
    IEnumerator ResetCollider()
    //IEnumerator 열거자 함수의 권한을 넘겨도 상태를 기억함 덕분에 return 이후 문장이 실행되는듯?
    {
        while ( true )
        {
            yield return null;
            if(!hitBoxCollider.activeInHierarchy)
            //activeInHierarchy 부모 오브젝트에 의한 비활성화
            {
                yield return new WaitForSeconds(0.5f);
                hitBoxCollider.SetActive(true);
                isHit = false;
            }
        }
    }
    IEnumerator CalcCoolTime()
    {
        while ( true )
        {
            yield return null;
            if (!canAtk)
            {
                atkCoolTimeCal -= Time.deltaTime;
                if(atkCoolTimeCal <= 0)
                {
                    atkCoolTimeCal = atkCoolTime;
                    canAtk = true;
                }
            }
        }
    }
    public bool IsPlayingAnim(string AnimName)
    {
        if (Anim.GetCurrentAnimatorStateInfo( 0 ).IsName(AnimName))
            //GetCurrentAnimatorStateInfo -> 현재 실행중인 애니메이션 확인
        {
            return true;
        }
        return false;
    }
    public void MyAnimSetTrigger (string AnimName)
    {
        if (!IsPlayingAnim(AnimName))
        {
            Anim.SetTrigger(AnimName);
        }
    }
    protected void MonsterFlip()
    {
        MonsterDirRight = !MonsterDirRight;
        Vector3 thisScale = transform.localScale;
        if (MonsterDirRight)
        {
            thisScale.x = -Mathf.Abs(thisScale.x);
        }
        else
        {
            thisScale.x = Mathf.Abs(thisScale.x);
        }
        transform.localScale = thisScale;
        rigid.velocity = Vector2.zero;
    }
    protected bool IsPlayerDir()
    {
        if(transform.position.x < PlayerData.Instance.Player.transform.position.x ? MonsterDirRight : !MonsterDirRight)
        {
            return true;
        }
        return false;
    }
    protected void GroundCheck()
    {
        if(Physics2D.BoxCast (col.bounds.center, col.size, 0, Vector2.down,0.05f,layerMask))
        {
            isGround= true;
        }
        else
        {
            isGround= false;
        }
    }
    public void TakeDamage(int dam)
    {
        Hp -= dam;
        isHit = true;
        spriterenderer.color = new Color(1, 0, 0, 0.5f);
        Invoke("OffDamage", 0.2f);
    }
    
    
    
    public void OffDamage()
    {
        spriterenderer.color = new Color(1, 1, 1, 1);
        isHit = false;
    }

}
