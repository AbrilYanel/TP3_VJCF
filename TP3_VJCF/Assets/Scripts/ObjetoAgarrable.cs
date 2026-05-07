using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoAgarrable : MonoBehaviour
{
    private bool jugadorCerca = false;
    private bool cargandoObjeto = false;
    private Transform puntoMano;
    public int puntaje;

    public void Configurar(PuzzleManager manager)
    {
        this.puntaje = manager.puntajeObtenido;

       
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player")) jugadorCerca = true;
        }
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !cargandoObjeto)
        {
            Agarrar();
        }
    }

    public void Agarrar()
    {
        cargandoObjeto = true;
        GameObject mano = GameObject.Find("Mano");

        if (mano != null)
        {
            puntoMano = mano.transform;
            transform.SetParent(puntoMano);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            
            if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().isKinematic = true;

            
            GetComponent<Collider>().isTrigger = false;

            Debug.Log("Objeto recogido con éxito.");
        }
        else
        {
            Debug.LogError("No se encontró el objeto PuntoMano en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Presiona E para recoger el objeto reparado");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = false;
    }
}
