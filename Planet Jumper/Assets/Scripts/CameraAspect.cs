﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    [SerializeField]
    float WantedAspectRatio;
    Resolution CurrentRes;

    private void Start()
    {
        StartCoroutine("SetRatio");
    }
    void Update()
    {
        
       // CurrentRes = Screen.currentResolution;
       // if(CurrentRes == Screen.currentResolution)
       // SetAspectRatio();
    }

    IEnumerator SetRatio()
    {
        while (true) { 
        SetAspectRatio();
        yield return new WaitForSeconds(0.05f);
        }
    }

    //"SetAspectRatio()" method was copied from https://discussions.unity.com/t/how-do-i-set-the-aspect-ratio-of-the-viewport/9353. 
    //Author is user "Adrian_Lopez".
    private void SetAspectRatio()
    {
        // set the desired aspect ratio 
        float targetaspect = WantedAspectRatio;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
