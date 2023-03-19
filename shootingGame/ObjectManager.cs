using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject PlayerBulletPrefab;
    GameObject[] PlayerBullet;
    public GameObject EnemyBulletPrefab;
    GameObject[] EnemyBullet;


    GameObject[] targetPool;

    private void Awake()
    {
        PlayerBullet = new GameObject[10];
        EnemyBullet = new GameObject[10];

        Generate();
    }
    void Generate()
    {
        for (int index = 0; index < PlayerBullet.Length; index++)
        {
            PlayerBullet[index] = Instantiate(PlayerBulletPrefab);
            PlayerBullet[index].SetActive(false);
        }

        for (int index = 0; index < EnemyBullet.Length; index++)
        {
            EnemyBullet[index] = Instantiate(EnemyBulletPrefab);
            EnemyBullet[index].SetActive(false);
        }
    }
    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "PlayerBullet":
                targetPool = PlayerBullet;
                break;
            case "EnemyBullet":
                targetPool = EnemyBullet;
                break;
        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].gameObject.activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
}
