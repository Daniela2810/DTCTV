using System.Collections;
using UnityEngine;

public abstract class InteractionTimer : MonoBehaviour
{
    public float interactionCooldowns = 0;

    private bool interactionFinish;
    public bool InteractionFinish
    {
        get { return interactionFinish; }
        set { interactionFinish = value; }
    }

    private float time;

    private void Start()
    {
        interactionFinish = true;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > interactionCooldowns)
        {
            interactionFinish = true;
            time = 0;
        }
    }

    public virtual void Interact()
    {
        
    }
}
