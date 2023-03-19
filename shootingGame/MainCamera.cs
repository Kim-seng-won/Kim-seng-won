using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public PlayerMove player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1, -10);
    }
}
