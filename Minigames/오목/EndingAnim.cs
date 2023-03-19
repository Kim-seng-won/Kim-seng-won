using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAnim : MonoBehaviour
{
    public RectTransform uiObject;
    public float moveTime = 1.0f;
    public AnimationCurve moveCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private float screenHeight;
    private float targetY;
    private float elapsedTime;

    void Start()
    {
        // 화면 높이 구하기
        screenHeight = Screen.height;

        // UI를 화면 맨 아래로 이동
        uiObject.anchoredPosition = new Vector2(uiObject.anchoredPosition.x, -screenHeight / 2);

        // 목표 Y 좌표 계산
        targetY = screenHeight / 2;

        // 경과 시간 초기화
        elapsedTime = 0.0f;
    }

    void Update()
    {
        // 경과 시간 증가
        elapsedTime += Time.deltaTime;

        // UI 이동
        float t = Mathf.Clamp01(elapsedTime / moveTime);
        float curveT = moveCurve.Evaluate(t);
        float newY = Mathf.Lerp(-screenHeight / 2, targetY, curveT);
        uiObject.anchoredPosition = new Vector2(uiObject.anchoredPosition.x, newY);
    }
}
