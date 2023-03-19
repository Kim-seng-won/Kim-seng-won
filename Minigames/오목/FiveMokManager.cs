using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FiveMokManager : MonoBehaviour
{
    public GameObject stages;
    public RectTransform gridtransform;
    public GameObject[] stones;
    public GameObject enddingImage;
    public stones[] stoneNum;
    public stones_field field;
    public int focus;
    float startX;
    float startY;
    int size = 3;
    int max = 19;
    float setting = Screen.width/720f;
    public int turn = 0;

    private void Awake()
    {
        startX = Screen.width / 19.1f;
        startY = Screen.width / 19.1f;
        stones[0].transform.position = new Vector2(stones[0].transform.position.x+startX*0.611f, stones[0].transform.position.y-startY*0.611f);

        CreateBoard();
        NextTurn();
    }
    public int Winning(int now)
    {
        for (int i = 0; i < max; i++)
        {
            for(int j=0; j<max; j++)
            {
                if (field.stones_position[i, j] != now) continue;

                if (InRange(j + 4) && field.stones_position[i, j + 1] == now && field.stones_position[i, j + 2] == now && field.stones_position[i, j + 3] == now && field.stones_position[i, j + 4] == now)
                    return (InRange(j + 5) && field.stones_position[i, j + 5] == now) ? 2 : 1;
                else if (InRange(i + 4) && field.stones_position[i+1, j] == now && field.stones_position[i+2, j] == now && field.stones_position[i+3, j] == now && field.stones_position[i+4, j] == now)
                    return (InRange(i + 5) && field.stones_position[i+5, j] == now) ? 2 : 1;
                else if (InRange(i + 4, j+4) && field.stones_position[i + 1, j+1] == now && field.stones_position[i + 2, j+2] == now && field.stones_position[i + 3, j+3] == now && field.stones_position[i + 4, j+4] == now)
                    return (InRange(i + 5,j+5) && field.stones_position[i + 5, j+5] == now) ? 2 : 1;
                else if (InRange(i + 4, j - 4) && field.stones_position[i + 1, j - 1] == now && field.stones_position[i + 2, j - 2] == now && field.stones_position[i + 3, j - 3] == now && field.stones_position[i + 4, j - 4] == now)
                    return (InRange(i + 5, j - 5) && field.stones_position[i + 5, j - 5] == now) ? 2 : 1;
            }
        }
        return 0;
    }
    bool InRange(params int[] v)
    {
        for(int i=0; i<v.Length; i++)
        {
            if (!(v[i] >= 0 && v[i]<max))
                return false;
        }
        return true;
    }
    public int NextTurn()
    {
        if (turn ==0 || turn==2)
            turn = 1;
        else
            turn = 2;
        return turn;
    }
    public void End()
    {
        for(int i=0; i<max; i++)
        {
            for(int j=0; j<max; j++)
            {
                stoneNum[j + i * max].ending = true;
                enddingImage.SetActive(true);
            }
        }
    }
    public void Reset()
    {
        for(int i=0; i<max; i++)
        {
            for(int j=0; j<max; j++)
            {
                stoneNum[j + i * max].OutPocus();
                stoneNum[j + i * max].decision = false;
                stoneNum[j + i * max].ending = false;
                field.stones_position[i, j] = 0;
                turn = 0;
            }
        }
        enddingImage.SetActive(false);
        stages.SetActive(false);
        NextTurn();
    }
    public void chose(int key)
    {
        if (turn==1)
        {
            field.stones_position[key / max, key % max] = 1;
            stoneNum[key].decision = true;
        }
        else if (turn==2)
        {
            field.stones_position[key / max, key % max] = 2;
            stoneNum[key].decision = true;
        }
    }
    public void touch()
    {
        for(int i=0; i<19; i++)
        {
            for(int j=0; j<19; j++)
            {
                if(!stoneNum[j + i * max].decision)
                {
                    stoneNum[j + i * max].OutPocus();
                }
            }
        }
    }
    void CreateBoard()
    {
        for (int i =0; i < max; i++)
        {
            for(int j=0; j< max; j++)
            {
                stones[j + i * max].gameObject.transform.position = new Vector2(stones[0].transform.position.x + (startX * (float)j),
                    stones[0].transform.position.y - (startY * (float)i));
                stones[j + i * max].gameObject.transform.localScale = new Vector2(size*setting, size*setting);
                stoneNum[j + i * max].key = j + i * max;
            }
        }
    }
}
