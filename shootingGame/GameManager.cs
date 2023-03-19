using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public TalkManager talkManager;
    public int DeathCount;
    public bool isAction=false;
    public TextMeshProUGUI UIPoint;
    public TextMeshProUGUI UIHealth;
    public TextMeshProUGUI UIDeathCount;
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI nameText;
    public GameObject talkPanel;
    public int talkIndex;
    public GameObject scanObject;
    public GameObject[] Stages;
    public GameObject[] Object;
    void Update()
    {
        UIPoint.text = "LIFE : " + (health).ToString();
        UIHealth.text = "LIFE : " + (health).ToString();
        UIDeathCount.text = "Death : " + (DeathCount).ToString();
    }
    public void NextStage()
    {
        if(stageIndex < Stages.Length-1) 
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            reload();

        }
        stageIndex++;
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            reload();
        }
        if (collision.gameObject.tag == "Enemy")
        {

        }
    }
    void reload()
    {
        player.rigid.velocity = Vector2.zero;
        player.transform.position = new Vector3(1.785003f, 2.125f, 0);
        player.spriterenderer.flipY = false;
        player.spriterenderer.color = new Color(1, 1, 1, 1f);
        player.gameObject.layer = 8;
        totalPoint = 0;
        stagePoint = 0;
        health = 5;
        DeathCount++;
    }
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }
    void talk(int id, bool isNpc)
    {
        string talkdata = talkManager.getTalk(id, talkIndex);
        string namedata = talkManager.getName(id);

        if (talkdata == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkdata;
            nameText.text = namedata;
        }
        else
        {
            talkText.text = talkdata;
            nameText.text = namedata;
        }
        isAction = true;
        talkIndex++;
    }
}
