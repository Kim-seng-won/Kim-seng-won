using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator anim;
    private GameObject cursor;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        cursor = transform.Find("cursor").gameObject;
    }
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Run");
            gameObject.layer = 16;
            rigid.AddForce(Vector2.right * 4 * -1, ForceMode2D.Impulse);
            Invoke("thank", 3);
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
            cursor.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            cursor.SetActive(false);
    }
    void thank()
    {
        Destroy(gameObject);
    }
}
