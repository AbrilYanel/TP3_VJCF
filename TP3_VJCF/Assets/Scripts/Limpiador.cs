using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limpiador : MonoBehaviour
{
    public bool tieneJabon = false;
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            tieneJabon = true;
            Debug.Log("Has recogido el limpiador químico.");
            // Opcional: desaparecer el frasco o cambiarle el color
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) jugadorCerca = true; }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) jugadorCerca = false; }
}
