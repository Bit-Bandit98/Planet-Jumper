using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WindowScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] WindowsToLead;
    [SerializeField]
    Slider MasterVolume;
    [SerializeField]
    GameObject CurrentWindow;


    private void Awake()
    {
        CurrentWindow = this.gameObject;
    }


    public void LoadWindow(GameObject ChosenWindow)
    {
        ChosenWindow.gameObject.SetActive(true);
    }


    public void DeactivateWindow(GameObject ChosenWindow)
    {
        ChosenWindow.SetActive(false);
    }

    public void ChangeMasterVolume()
    {
        SoundManager.Singleton.ChangeMasterAudio(MasterVolume.value);
    }

}
