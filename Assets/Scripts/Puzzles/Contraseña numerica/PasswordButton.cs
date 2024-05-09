using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButton : MonoBehaviour
{
    [Header("script para los botones de la contraseña de la puerta")]
    public PasswordDoor passwordDoor;

    public void AddNumber(int digit)
    {
        passwordDoor.AddDigit(digit);
        //SoundManager.instance.PlaySound(0,false);
    }

    public void Exit()
    {
        passwordDoor.Exit();
    }
}
