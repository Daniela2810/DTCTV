using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoseManager : MonoBehaviour
{
    [Header("Ui de derrota")]
    public GameObject LoseUi;

    [Header("UI de victoria")]
    public GameObject WinUi;
    public GameObject Page6;

    private static WinOrLoseManager instance;
    public static WinOrLoseManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void lose()
    {
        LoseUi.SetActive(true);
        DetectiveManager.Instance.LockCamera();
        SoundManager.instance.PlaySound(4, false);
    }

    public void Win()
    {
        WinUi.SetActive(true);
        DetectiveManager.Instance.LockCamera();
        SoundManager.instance.PlaySound(11, true);
    }

    public void page6()
    {
        //WinUi.SetActive(false);
        Page6.SetActive(true);
        DetectiveManager.Instance.LockCamera();
    }
}
