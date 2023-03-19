using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float magnetStrength = 1f;
    public float distanceStretch = 1f;
    public float magnetDirection = 1f;
    public bool looseMagnet =true;
    public PlayerMove player;
    public Enemy enemy;

    private Transform trans;
    private int maxspeed = 3;
    private Transform magnetTrans;
    private bool magnetInZone;
    void Awake()
    {
        trans = transform;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = enemy.transform.position;
    }
    public void drop() 
    {
        gameObject.SetActive(true);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

}
