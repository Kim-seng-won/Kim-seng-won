using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public PlayerMove player;
    public GameManager gameManager;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hitbox")
        {
            CancelInvoke("BB");
            player.spriterenderer.color = new Color(1, 1, 1, 0.4f);
            gameManager.health--;
            if (gameManager.health <= 0)
            {
                player.Die();
            }

            if (player.maxspeed >= 1f)
                player.maxspeed -= 0.5f;
            Invoke("BB", 3);

        }
    }
    void BB()
    {
        player.spriterenderer.color = new Color(1, 1, 1, 1);
        player.maxspeed = 2;
    }
}
