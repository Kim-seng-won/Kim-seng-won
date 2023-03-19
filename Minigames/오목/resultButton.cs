using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class resultButton : MonoBehaviour
{
    Button btn;
    public FiveMokManager manager;

    void Awake()
    {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(Onclick);
    }
    public void Onclick()
    {
        manager.chose(manager.focus);
        if (manager.Winning(manager.turn) == 1)
        {
            manager.End();
        }
        else
            manager.NextTurn();
    }
}
