using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    [SerializeField] DetectiveCamera detectiveCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartAnimation(float animationDuration)
    {
        StartCoroutine(AnimateAndEnd(animationDuration));
    }

    private IEnumerator AnimateAndEnd(float animationDuration)
    {
        DetectiveManager.Instance.LockCamera();

        yield return new WaitForSeconds(animationDuration);

        DetectiveManager.Instance.UnlockCamera();
    }
}
