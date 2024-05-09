using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    public float Timer;
    public bool isRinging = false;
    public bool textFinish;  // Asegúrate de definir o controlar esta variable apropiadamente
    public DetectiveDoor detectiveDoor;

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 10 && !isRinging)
        {
            StartRinging();
        }
    }

    private void StartRinging()
    {
        isRinging = true;
        SoundManager.instance.PlayLoopedSound(0);
    }

    public void Interact()
    {
        if (isRinging)
        {
            SoundManager.instance.StopLoopedSound();
            SoundManager.instance.PlayMusic(0);
            detectiveDoor.MisionLoad("Demo");
        }
        if (textFinish)
        {
            gameObject.tag = "Untagged";
        }
    }
}
