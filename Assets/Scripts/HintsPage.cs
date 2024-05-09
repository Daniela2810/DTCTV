using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Hint
{
    public string[] Hints;
}

public class HintsPage : MonoBehaviour
{
    public List<Hint> hints = new List<Hint>();

    public TextMeshProUGUI Hint1;
    public TextMeshProUGUI Hint2;
    public TextMeshProUGUI Hint3;

    public GameObject HintVoid1;
    public GameObject HintVoid2;
    public GameObject HintVoid3;

    public Button HintButton;
    private int hint = 1;
    private int puzzle = 1;

    public void HintHelp(int puzzle, int num)
    {
        switch (puzzle)
        {
            case 1:
                HintNumber(puzzle, num);
                break;
            case 2:
                HintNumber(puzzle, num);
                break;
            case 3:
                HintNumber(puzzle, num);
                break;
            default:
                break;
        }
    }

    public void HintNumber(int puzzle, int num)
    {
        switch (num)
        {
            case 1:
                Hint1.text = hints[puzzle - 1].Hints[num - 1];
                HintVoid1.gameObject.SetActive(false);
                break;
            case 2:
                Hint2.text = hints[puzzle - 1].Hints[num - 1];
                HintVoid2.gameObject.SetActive(false);
                break;
            case 3:
                Hint3.text = hints[puzzle - 1].Hints[num - 1];
                HintVoid3.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void UnlockHint()
    {
        HintHelp(puzzle, hint);
        hint++;

        if (hint > 3)
        {
            HintButton.interactable = false;
        }

        LevelManager.instance.AddPoints(-500);
    }

    public void NextPuzzleHint()
    {

        HintVoid1.gameObject.SetActive(true);
        HintVoid2.gameObject.SetActive(true);
        HintVoid3.gameObject.SetActive(true);

        puzzle++;
        hint = 1;
        HintButton.interactable = true;
    }
}
