using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public GameObject door;

    public float Timer;
    private bool action = false;
    private bool changesSong = false;

    private void Start()
    {
        Timer = 5;
    }
    private void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer <= 12)
        {
            Song();
        }
        if (Timer <= 0)
        {
            Timer -= Time.deltaTime;
            Action();
        }
    }

    private void Action()
    {
        if (!action)
        {
            door.GetComponent<ObjectMove>().Interact();
            action = true;
            SoundManager.instance.StopSound();
            SoundManager.instance.PlayMusic(1);
        }
    }

    private void Song()
    {
        if (!changesSong)
        {
            SoundManager.instance.PlaySound(1, false);
            changesSong = true;
        }
    }
}
