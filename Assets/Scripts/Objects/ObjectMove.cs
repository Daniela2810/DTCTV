using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : InteractionTimer
{
    [Header("Valores de movimiento")]
    public float moveSpeed = 2.0f;
    public float intensity = 1.0f;
    public MovementDirection moveDirection;

    [Header("¿Esta cerrado?")]
    public bool Locked = false;

    [Header("¿Esta abierto?")]
    public bool isOpen = false;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool first = true;

    public delegate void ObjectMovedEventHandler();
    public event ObjectMovedEventHandler OnObjectMoved;


    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    public override void Interact()
    {
        if (!Locked)
        {
            if (!isMoving)
            {
                if (isOpen)
                {
                    targetPosition = initialPosition;
                    isOpen = false;
                }
                else
                {
                    Vector3 moveVector = Vector3.zero;

                    switch (moveDirection)
                    {
                        case MovementDirection.Up:
                            moveVector = Vector3.up;
                            break;
                        case MovementDirection.Down:
                            moveVector = Vector3.down;
                            break;
                        case MovementDirection.Left:
                            moveVector = Vector3.left;
                            break;
                        case MovementDirection.Right:
                            moveVector = Vector3.right;
                            break;
                        case MovementDirection.Forward:
                            moveVector = Vector3.forward;
                            break;
                        case MovementDirection.Backward:
                            moveVector = Vector3.back;
                            break;
                        case MovementDirection.DiagonalUpLeft:
                            moveVector = Vector3.up + Vector3.left;
                            break;
                        case MovementDirection.DiagonalUpRight:
                            moveVector = Vector3.up + Vector3.right;
                            break;
                        case MovementDirection.DiagonalDownLeft:
                            moveVector = Vector3.down + Vector3.left;
                            break;
                        case MovementDirection.DiagonalDownRight:
                            moveVector = Vector3.down + Vector3.right;
                            break;
                    }

                    targetPosition = initialPosition + moveVector * intensity;
                    isOpen = true;
                }

                StartCoroutine(MoveCoroutine());
            }
        }
        else
        {
            StartCoroutine(ShakeCoroutine(0.005f, 0.1f));
            SoundManager.instance.PlaySound(6, false);
        }


        if (first)
        {
            LevelManager.instance.AddPoints(20);
            first = false;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < intensity / moveSpeed)
        {
            float t = (Time.time - startTime) / (intensity / moveSpeed);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        if (OnObjectMoved != null)
        {
            Debug.Log("evento");
            OnObjectMoved();
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 startPosition = this.gameObject.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = startPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
    }
}
