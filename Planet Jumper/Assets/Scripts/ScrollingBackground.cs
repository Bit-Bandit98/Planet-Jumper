using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    
    [SerializeField]
    public float ScrollingSpeed, AdditionSpeed;
    Vector2 NewOffset = new Vector2();
    [SerializeField]
    Material BackgroundMat;
    public static ScrollingBackground Singleton;
    // Start is called before the first frame update
    void Awake()
    {
        Singleton = this;
        BackgroundMat.SetTextureOffset("_MainTex", Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        if (GM.Singleton.PlayerStats.PIsMoving)
        {
            NewOffset.y = (BackgroundMat.GetTextureOffset("_MainTex").y + -ScrollingSpeed * Time.deltaTime);
            BackgroundMat.SetTextureOffset("_MainTex", NewOffset);
            if (BackgroundMat.GetTextureOffset("_MainTex").y <= -1)
            {
                BackgroundMat.SetTextureOffset("_MainTex", new Vector2(0, BackgroundMat.GetTextureOffset("_MainTex").y - Mathf.Ceil(BackgroundMat.GetTextureOffset("_MainTex").y)));
            }
        }
    }

    public void IncreaseScrollingSpeed(float Increase) { ScrollingSpeed += Increase; }
    public void SetScrollSpeed(float Value) { ScrollingSpeed = Value; }
    public float ReturnScrollingSpeed() { return ScrollingSpeed; }
}
