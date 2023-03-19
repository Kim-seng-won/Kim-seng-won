using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    private RectTransform rectTransform;
    public float squareSize;

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

        rectTransform.sizeDelta = new Vector2(screenWidth*0.7f, screenHeight*0.7f);
    }
}
