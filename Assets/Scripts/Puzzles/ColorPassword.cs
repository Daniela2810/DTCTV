using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ColorPassword : MonoBehaviour
{
    [Header("objeto que se movera")]
    public GameObject ObjectToMove;

    [Header("contrase�a de colores")]
    public List<colorValue> colorPassword = new List<colorValue>();

    public List<ColorValue> Libros = new List<ColorValue>();
    public List<colorValue> CheckPassword = new List<colorValue>();

    public void AddColor(colorValue color, ColorValue book)
    {
        CheckPassword.Add(color);
        Libros.Add(book);
        if (!book.Activated)
        {
            Libros.Remove(book);
        }
        SoundManager.instance.PlaySound(10,true);
        if (CheckPassword.Count >= 2)
        {
            StartCoroutine(check(book));
        }
    }

    IEnumerator check(ColorValue book)
    {
        bool areEqual = true;
        for (int i = 0; i < CheckPassword.Count; i++)
        {
            if (CheckPassword[i] != colorPassword[i])
            {
                areEqual = false;
                break;
            }
        }

        if (!areEqual)
        {
            for (int i = 0; i < Libros.Count; i++)
            {
                ColorValue item = Libros[i];
                item.move();
                item.Activated = false;
                yield return new WaitForSeconds(0.5f);
            }
            Libros.Clear();
            CheckPassword.Clear();
        }

        if (CheckPassword.SequenceEqual(colorPassword))
        {
            LevelManager.instance.AddPoints(700);
            yield return new WaitForSeconds(1);
            ObjectToMove.GetComponent<ObjectMove>().Interact();
        }
    }
}
