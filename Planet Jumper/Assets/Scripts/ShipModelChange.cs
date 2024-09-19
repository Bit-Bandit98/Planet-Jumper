using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipModelChange : MonoBehaviour
{
    [SerializeField]
    Sprite[] PlayerModels;
    public Sprite[] PPlayerModels
    {
        get { return PlayerModels; }
    }
    [SerializeField]
    SpriteRenderer ShipSprite;
    public static ShipModelChange Singleton;

    private void Awake()
    {
        Singleton = this;
    }
    public void ChangeShipModel(int Num)
    {
        ShipSprite.sprite = PlayerModels[Num];
    }
}
