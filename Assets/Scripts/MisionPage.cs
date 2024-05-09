using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MisionPage : MonoBehaviour
{
    public TextMeshProUGUI ToDO1;
    public TextMeshProUGUI ToDO2;
    public TextMeshProUGUI ToDO3;
    public TextMeshProUGUI ToDO4;

    public void ObjectiveReach(int reach, bool done)
    {
        switch (reach)
        {
            case 1:
                ToDO1.gameObject.SetActive(true);
                if(done)
                    ToDO1.fontStyle = FontStyles.Strikethrough;
                break;
            case 2:
                ToDO2.gameObject.SetActive(true);
                if (done)
                    ToDO2.fontStyle = FontStyles.Strikethrough;
                break;
            case 3:
                ToDO3.gameObject.SetActive(true);
                if (done)
                    ToDO3.fontStyle = FontStyles.Strikethrough;
                break;
            case 4:
                ToDO4.gameObject.SetActive(true);
                if (done)
                    ToDO4.fontStyle = FontStyles.Strikethrough;
                break;
            default:
                break;
        }
    }
}
