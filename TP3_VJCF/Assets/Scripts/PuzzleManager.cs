using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    public int totalPiezas;
    private int piezasProcesadas = 0; 
    private int piezasEncajadasExitosamente = 0;

    public GameObject botonFinalizar; 
    public TextMeshProUGUI textoPuntaje;
    public ActivadorPuzzle activador;

    void Start()
    {
        botonFinalizar.SetActive(false);
    }

    // Este método lo llamará cada pieza al terminar su ciclo
    public void RegistrarPiezaTerminada(bool fueExito)
    {
        piezasProcesadas++;
        if (fueExito) piezasEncajadasExitosamente++;

        // Si ya no quedan piezas sueltas en la mesa
        if (piezasProcesadas >= totalPiezas)
        {
            MostrarBotonFinalizar();
        }
    }

    void MostrarBotonFinalizar()
    {
        botonFinalizar.SetActive(true);
    }

    public void FinalizarPuzzle()
    {
        // Cálculo de puntaje 
        int puntajeFinal = piezasEncajadasExitosamente * 25;
        textoPuntaje.text = "Puntaje: " + puntajeFinal;

        Debug.Log("Saliendo del puzzle...");

        
        if (activador != null)
        {
            activador.SalirDeModoPuzzle();
        }

        
        botonFinalizar.SetActive(false);

    }

}
