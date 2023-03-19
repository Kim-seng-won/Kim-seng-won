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
        // ȭ�� ���� ���ϱ�
        screenHeight = Screen.height;

        // UI�� ȭ�� �� �Ʒ��� �̵�
        uiObject.anchoredPosition = new Vector2(uiObject.anchoredPosition.x, -screenHeight / 2);

        // ��ǥ Y ��ǥ ���
        targetY = screenHeight / 2;

        // ��� �ð� �ʱ�ȭ
        elapsedTime = 0.0f;
    }

    void Update()
    {
        // ��� �ð� ����
        elapsedTime += Time.deltaTime;

        // UI �̵�
        float t = Mathf.Clamp01(elapsedTime / moveTime);
        float curveT = moveCurve.Evaluate(t);
        float newY = Mathf.Lerp(-screenHeight / 2, targetY, curveT);
        uiObject.anchoredPosition = new Vector2(uiObject.anchoredPosition.x, newY);
    }
}
