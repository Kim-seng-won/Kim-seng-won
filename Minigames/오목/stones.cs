using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stones : MonoBehaviour
{
    private RectTransform rectTransform;
    public GameObject stageSize;
    public RectTransform gridTransform;
    public FiveMokManager manager;
    public stones_field field;
    public int boardSize = 19;
    public size lee;

    public bool ending = false;
    public int key;
    public int turn;
    public Sprite[] sprites;
    public Image myimage;
    public Color stone_blank;
    public Color stone_black;
    public Color stone_white;
    public bool active=false;
    public bool decision = false;
    public int x;
    public int y;
    Button btn;
    void Awake()
    {
        btn = this.transform.GetComponent<Button>();
        myimage = GetComponent<Image>();
        myimage.sprite = sprites[Random.Range(0,7)];
        myimage.color = stone_blank ;
        btn.onClick.AddListener(Onclick);
        gridTransform= GetComponent<RectTransform>();
    }
    public void Update()
    {
        turn = manager.turn;
    }
    public void retouch()
    {
        this.rectTransform.sizeDelta = new Vector2(stageSize.transform.localScale.x/20*lee.squareSize, stageSize.transform.localScale.y / 20 * lee.squareSize);
    }
    public void OutPocus()
    {
        myimage.color = stone_blank;
        active = false;
    }
    public void Onclick()
    {
        if (!ending)
        {
            manager.focus = key;
            manager.touch();
            if (!active && !decision)
            {
                if (turn == 1)
                {
                    myimage.color = stone_black;
                }
                else if (turn == 2)
                {
                    myimage.color = stone_white;
                }
            }
            active = true;
        }
    }
    

}
