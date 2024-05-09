using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonpahoy : MonoBehaviour
{
    [Header("script para los botones de la contraseña de la puerta")]
    public PadlockProvisionalparahoyHastaarreglarelotro passwordDoor;

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
