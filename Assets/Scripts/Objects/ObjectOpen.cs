using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOpen : InteractionTimer, IInteractable
{
    [Header("Object")]
    public Transform doorTransform;

    [Header("Valores de movimiento")]
    public float rotationSpeed;
    public float openAngle = 90.0f;
    public RotationAxis rotationAxis = RotationAxis.Y;

    [Header("¿Esta cerrado?")]
    public bool Locked = false;

    [Header("¿Esta abierto?")]
    public bool isOpen = false;

    private bool first = true; // Agrega esta línea
    private Coroutine rotateCoroutine;
    private Ishake shakeObject;

    public event Action OnObjectOpened;

    private void Start()
    {
        shakeObject = GetComponent<Ishake>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        float choque = doorTransform.localEulerAngles.y;
        isOpen = Mathf.Abs(choque - openAngle) < Mathf.Abs(choque - 0);
    }

    public override void Interact()
    {
        if (!Locked)
        {
            if (rotateCoroutine == null)
            {
                float targetAngle = isOpen ? 0.0f : openAngle;
                rotateCoroutine = StartCoroutine(RotateDoor(targetAngle));
                isOpen = !isOpen;
                OnObjectOpened?.Invoke();
            }
        }
        else
        {
            StartCoroutine(ShakeCoroutine(0.03f, 0.1f));
            SoundManager.instance.PlaySound(6, false);
        }

        if (first)
        {
            LevelManager.instance.AddPoints(50);
            first = false;
        }
    }

    private IEnumerator RotateDoor(float targetAngle)
    {
        Quaternion startRotation = doorTransform.localRotation;
        Quaternion endRotation = Quaternion.Euler(rotationAxis == RotationAxis.X ? new Vector3(targetAngle, 0, 0) :
                            rotationAxis == RotationAxis.Y ? new Vector3(0, targetAngle, 0) :
                            new Vector3(0, 0, targetAngle));

        float timePassed = 0.0f;
        while (timePassed < 1.0f)
        {
            timePassed += Time.deltaTime * (rotationSpeed / 360.0f);
            doorTransform.localRotation = Quaternion.Lerp(startRotation, endRotation, timePassed);
            yield return null;
        }

        rotateCoroutine = null;
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position; // Guardar la posición original para restaurarla después.
        float elapsed = 0.0f; // Contador de tiempo transcurrido

        while (elapsed < duration)
        {
            float x = originalPosition.x + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float z = originalPosition.z + UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, z); // Asignar la nueva posición con un ligero desplazamiento aleatorio
            elapsed += Time.deltaTime; // Incrementar el contador de tiempo
            yield return null; // Esperar un frame antes de continuar
        }

        transform.position = originalPosition; // Restaurar la posición original
    }
}
