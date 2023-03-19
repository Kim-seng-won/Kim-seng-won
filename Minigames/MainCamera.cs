using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        Rect rt = cam.rect;

        //세로모드로 가로모드를 원하면 16/9 입력.
        float scale_height = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        cam.rect = rt;

    }
}
