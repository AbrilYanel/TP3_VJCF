using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavabo : MonoBehaviour
{
    public Limpiador scriptJabon;
    private bool jugadorCerca = false;

    void Update()
    {
        if (!jugadorCerca || !Input.GetKeyDown(KeyCode.E)) return;

        // Buscamos la pieza en la mano del jugador específicamente
        GameObject mano = GameObject.Find("Mano");
        if (mano == null) { Debug.LogError("No se encontró el objeto Mano."); return; }

        PiezaCasette pieza = mano.GetComponentInChildren<PiezaCasette>();

        if (pieza == null)
        {
            Debug.Log("No tenés ninguna pieza en la mano.");
            return;
        }

        if (!pieza.estaSucia)
        {
            Debug.Log("Esta pieza no está sucia.");
            return;
        }

        if (!scriptJabon.tieneJabon)
        {
            Debug.Log("Necesitás el limpiador químico primero.");
            return;
        }

        // Limpiamos la pieza
        pieza.haSidoLavada = true;
        pieza.estaSucia = false;
        Debug.Log("La pieza " + pieza.nombrePieza + " ahora está limpia. Volvé a la mesa.");

        // Re-habilitamos MoverPieza para cuando la devuelvan al puzzle
        // (estará inactivo hasta que entre al modo puzzle de nuevo)
        MoverPieza mover = pieza.GetComponent<MoverPieza>();
        if (mover != null) mover.enabled = false; // Se reactiva al entrar al puzzle
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) jugadorCerca = true; }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) jugadorCerca = false; }
}
