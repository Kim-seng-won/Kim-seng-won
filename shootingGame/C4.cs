using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    public GameObject something;
    private void Over()
    {
        gameObject.SetActive(false);
        something.SetActive(false);
    }
}
