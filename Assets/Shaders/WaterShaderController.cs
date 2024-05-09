using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShaderController : MonoBehaviour, IInteractable
{
    public GameObject AguaDelGrifo;
    public GameObject AguaDelLavado;
    public GameObject Espejo;
    public GameObject llave;
    public Material nuevoMaterial;
    public Material viejoMaterial;
    public ParticleSystem sistemaDeParticulas;
    public ParticleSystem sistemaDeParticulas2;

    public ObjectOpen closeDoor;
    public bool Funcionando = false;

    private bool Resolve = true;

    private void Start()
    {
        Renderer renderer = Espejo.GetComponent<Renderer>();
        viejoMaterial = renderer.material;
    }

    private void Update()
    {
        if (Funcionando && !closeDoor.isOpen)
        {
            if (!sistemaDeParticulas2.isPlaying)
            {
                sistemaDeParticulas2.Play();
            }

            if (Resolve)
            {
                StartCoroutine(ChangeMaterialAfterDelay(3.5f));
                Resolve = false; // Asegúrate de resetear este flag cuando sea necesario
            }
        }
        else
        {
            if (sistemaDeParticulas2.isPlaying)
            {
                sistemaDeParticulas2.Stop();
            }

            Renderer renderer = Espejo.GetComponent<Renderer>();
            renderer.material = viejoMaterial;
            Resolve = true; // Restablecer Resolve para la próxima vez que se cumplan las condiciones
        }
    }

    private IEnumerator ChangeMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Renderer renderer = Espejo.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = nuevoMaterial;
        }
    }

    public void Interact()
    {
        Funcionando = !Funcionando;
        if (Funcionando)
        {
            AguaDelGrifo.SetActive(true);
            AguaDelLavado.SetActive(true);
            sistemaDeParticulas.Play();
        }
        else
        {
            AguaDelGrifo.SetActive(false);
            AguaDelLavado.SetActive(false);
            sistemaDeParticulas.Stop();
        }
    }
}
