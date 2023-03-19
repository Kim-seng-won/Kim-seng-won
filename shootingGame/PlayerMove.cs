using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject C4;
    public GameObject scanObject;
    public item_mg mg;
    public float maxspeed;
    public float jumppower;
    public AudioClip audioFire;
    public AudioClip audioJump;
    public AudioClip audioFinish;
    public Vector3[] stages;

    public Rigidbody2D rigid;
    public SpriteRenderer spriterenderer;
    public Animator anim;
    public Transform position;
    public Collider2D col;

    private int dir;
    private bool CanDoit;
    private bool Cantalk;
    private bool Boom;
    private bool teleport;
    private int haveboom;
    private int stageLevel;
    private int direction;
    AudioSource audiosource;

    //Bullet file
    public string[] Bullet;
    public float power;
    public float maxshotDelay;
    public float curshotDelay;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        position = GetComponent<Transform>();
        audiosource= GetComponent<AudioSource>();
        Bullet = new string[] { "PlayerBullet" };
        Boom = true;
        teleport = false;
        haveboom = 0;
        stageLevel = 0;
    }

    void Update()
    {
        if (gameManager.health > 0)
        {
            if (!gameManager.isAction)
            {
                jump();
                fall();
                Move();
                fire();
                Reload();
                Doit();
                Teleport();
            }
        }
        talking();

    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxspeed)
        {
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
            spriterenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            dir = 1;
        }
        else if (rigid.velocity.x < maxspeed * (-1))
        {
            rigid.velocity = new Vector2(maxspeed * (-1), rigid.velocity.y);
            spriterenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            dir = -1;
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("IsWalking", false);
        else
            anim.SetBool("IsWalking", true);

    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping"))
        {
            PlaySound("Jump");
            rigid.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", true);
        }
    }
    void fall()
    {
        if (rigid.velocity.y < 0)
        {
            anim.SetBool("IsJumping", true);
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.95f)
                    anim.SetBool("IsJumping", false);
            }
        }
    }
    void fire()
    {
        if (!Input.GetButton("Attack"))
        {
            anim.SetBool("IsShooting", false);
            return;
        }

        if (curshotDelay < maxshotDelay)
            return;
        int indexBullet = 0;
        PlaySound("Fire");
        GameObject bullet = objectManager.MakeObj(Bullet[indexBullet]);
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.right * 10 * dir , ForceMode2D.Impulse);
        anim.SetBool("IsShooting", true);
        curshotDelay = 0;
    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "Fire":
                audiosource.clip = audioFire;
                break;
            case "Jump":
                audiosource.clip = audioJump;
                break;
            case "Finish":
                audiosource.clip = audioFinish;
                break;
        }
        audiosource.Play();
    }

    void Doit()
    {
        if(CanDoit==true&&haveboom!=0)
        {
            if (Input.GetKeyDown(KeyCode.G)&&Boom)
            {
                Debug.Log("GG");
                anim.SetBool("IsDoing", true);
                Boom = false;
                Invoke("setboom", 3);
            }
        }
    }
    void talking()
    {
        if(Cantalk==true&& Input.GetKeyDown(KeyCode.E))
        {
            gameManager.Action(scanObject);
        }
    }

    void Teleport()
    {
        if (Input.GetKeyDown(KeyCode.E) && teleport)
        {
            PlaySound("Finish");
            nextStage(direction);
        }
    }
    void Reload()
    {
        curshotDelay += Time.deltaTime;
    }
    void setboom()
    {
        anim.SetBool("IsDoing", false);
        C4.SetActive(true);
        CanDoit = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Hitbox")
        {
            HitEnemy(collision);
        }

        else if (collision.gameObject.tag == "spike")
            OnDamaged(collision.transform.position);
        else if (collision.gameObject.tag == "Item")
            haveboom++;

    }
    public void HitEnemy(Collision2D collision)
    {
        OnDamaged(collision.transform.position);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            gameManager.stagePoint += 100;

            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            teleport = true;
            direction = 1;
        }
        else if (collision.gameObject.tag == "Portal")
        {
            teleport = true;
            direction = -1;
        }
        else if (collision.gameObject.tag == "Object")
        {
            CanDoit = true;
        }
        else if (collision.gameObject.tag == "NPC")
        {
            Cantalk = true;
            scanObject = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            CanDoit = false;
        }
        else if (collision.gameObject.tag == "NPC")
        {
            Cantalk = false;
        }
        else if (collision.gameObject.tag == "Finish"|| collision.gameObject.tag == "Portal")
        {
            teleport = false;
        }
    }

    public void Die()
    {
        gameObject.layer = 18;
        spriterenderer.color = new Color(1, 0, 0, 0.5f);
        spriterenderer.flipY = true;
        Invoke("reload", 2);
    }
    void reload()
    {
        rigid.velocity = Vector2.zero;
        transform.position = new Vector3(1.785003f, 2.125f, 0);
        spriterenderer.flipY = false;
        spriterenderer.color = new Color(1, 1, 1, 1f);
        gameObject.layer = 8;
        gameManager.totalPoint = 0;
        gameManager.stagePoint = 0;
        gameManager.health = 5;
        gameManager.DeathCount++;
    }
    void OnDamaged(Vector2 targetPos) {
        gameObject.layer = 14;

        gameManager.health--;
        if (gameManager.health <= 0)
        {
            Die();
        }
        else
        {
            anim.SetBool("IsJumping", true);
            spriterenderer.color = new Color(1, 0, 0, 0.5f);
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 0.5f) * 7, ForceMode2D.Impulse);

            
            Invoke("OffDamaged", 2);
        }
    }
    void OffDamaged() // �ǰݽ� ���� ���� ����
    {
        gameObject.layer = 8;
        spriterenderer.color = new Color(1, 1, 1, 1);
    }
    
    void OnAttack(Transform enemy)
    {
        gameManager.stagePoint += 100;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
    void nextStage(int k)
    {
        stageLevel += k;
        gameObject.transform.position = stages[stageLevel];
        anim.SetBool("Finish", false);
    }

}
