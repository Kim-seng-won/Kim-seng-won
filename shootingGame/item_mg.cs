using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_mg : MonoBehaviour
{
    public PlayerMove player;

    private void Update()
    {
        transform.position = player.transform.position;
    }
}

