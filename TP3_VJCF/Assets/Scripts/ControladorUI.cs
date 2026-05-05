using UnityEngine;
using TMPro; // Asegúrate de tener instalado TextMeshPro

public class ControladorUI : MonoBehaviour
{
    public TextMeshProUGUI textoNombrePieza;
    public TextMeshProUGUI textoEstadoPieza;
    public GameObject panelInfo; // El panel que contiene los textos

    void Start()
    {
        panelInfo.SetActive(false); // Oculto al inicio
    }

    public void MostrarInfo(string nombre, float vida)
    {
        panelInfo.SetActive(true);
        textoNombrePieza.text = "Objeto: " + nombre;
        textoEstadoPieza.text = "Estado: " + vida + "%";

        // Cambiar color según el daño
        if (vida < 40) textoEstadoPieza.color = Color.red;
        else textoEstadoPieza.color = Color.white;
    }

    public void OcultarInfo()
    {
        panelInfo.SetActive(false);
    }
}