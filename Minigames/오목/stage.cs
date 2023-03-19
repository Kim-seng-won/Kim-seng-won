using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage : MonoBehaviour
{
    public GameObject center;
    void Start()
    {
        this.transform.position = new Vector3(center.transform.position.x, center.transform.position.y, 0);
    }
}
