using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ValuePage : MonoBehaviour
{
    List<GameObject> ObjectToInteract = new List<GameObject>();
    List<GameObject> ObjectInteracted = new List<GameObject>();
    List<ObjectOpen> ObjectOpenList = new List<ObjectOpen>();
    List<ObjectMove> ObjectMoveList = new List<ObjectMove>();
    List<GameObject> ObjectToInvestigate = new List<GameObject>();
    List<GameObject> ObjectInvestigated = new List<GameObject>();
    List<DescriptionUpgrade> ObjectInvestigateList = new List<DescriptionUpgrade>();
    List<Item> ObjectInventoryList = new List<Item>();
    int finalPoints;

    [SerializeField] TextMeshProUGUI InteractedObject;
    [SerializeField] TextMeshProUGUI totalObjectInteracted;
    [SerializeField] TextMeshProUGUI InvestigatedObject;
    [SerializeField] TextMeshProUGUI totalObjectInvestigated;
    [SerializeField] TextMeshProUGUI FinalTime;
    [SerializeField] TextMeshProUGUI FinalPoints;


    private void Start()
    {
        ObjectOpen[] objectOpenScripts = FindObjectsOfType<ObjectOpen>();
        ObjectMove[] objectMoveScripts = FindObjectsOfType<ObjectMove>();
        DescriptionUpgrade[] objectInvestigateScripts = FindObjectsOfType<DescriptionUpgrade>();
        Item[] objectPickableScripts = FindObjectsOfType<Item>();

        ObjectOpenList.AddRange(objectOpenScripts);
        ObjectMoveList.AddRange(objectMoveScripts);
        ObjectInvestigateList.AddRange(objectInvestigateScripts);
        ObjectInventoryList.AddRange(objectPickableScripts);

        foreach (var obj in objectOpenScripts)
        {
            ObjectToInteract.Add(obj.gameObject);
        }

        foreach (var obj in objectMoveScripts)
        {
            ObjectToInteract.Add(obj.gameObject);
        }

        foreach (var obj in objectInvestigateScripts)
        {
            ObjectToInvestigate.Add(obj.gameObject);
        }

        FinalTime.text = Timer.Instance.timerText.text;
        FinalPoints.text = finalPoints.ToString();
    }

    public void FinalCheck()
    {
        ObjectInteracted.Clear();
        ObjectInvestigated.Clear();

        foreach (var obj in ObjectOpenList)
        {
            if (!obj.isOpen)
            {
                ObjectInteracted.Add(obj.gameObject);
            }
        }

        foreach (var obj in ObjectMoveList)
        {
            if (!obj.isOpen)
            {
                ObjectInteracted.Add(obj.gameObject);
            }
        }

        foreach (var obj in ObjectInvestigateList)
        {
            if (obj.analysisTime == 0)
            {
                ObjectInvestigated.Add(obj.gameObject);
            }
        }

        finalPoints = LevelManager.instance.currentPoints;

        InteractedObject.text = ObjectInteracted.Count.ToString();
        totalObjectInteracted.text = ObjectToInteract.Count.ToString();


        InvestigatedObject.text = ObjectInvestigated.Count.ToString();
        totalObjectInvestigated.text = ObjectToInvestigate.Count.ToString();
        FinalTime.text = Timer.Instance.timerText.text;
        FinalPoints.text = finalPoints.ToString();
    }
}
