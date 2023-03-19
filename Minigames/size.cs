using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class size : MonoBehaviour
{
    private RectTransform rectTransform;
    public float squareSize;
    public float squarehigh;
    public float sizeX=1f;
    public float sizeY=1f;
    public float positionY = 0f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ResizeAndPosition();
    }
    private void Update()
    {
        ResizeAndPosition();
    }

    private void ResizeAndPosition()
    {
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        squareSize = screenWidth;
        squarehigh = screenHeight;

        rectTransform.sizeDelta = new Vector2(squareSize*sizeX, squareSize*sizeY);
        rectTransform.anchoredPosition = new Vector2(squareSize/2 ,squarehigh*positionY) ;
    }
}
