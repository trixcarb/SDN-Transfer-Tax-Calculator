using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;

    public void SettingButton()
    {
        canvas2.SetActive(true);
        canvas1.SetActive(false);
    }
    public void CloseButton()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }
}
