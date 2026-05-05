using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoEmocional { Neutral, Enojo, Relajado, Tristeza, Desagrado }

public class GestorEmociones : MonoBehaviour
{
    public EstadoEmocional emocionActual;
    public Color colorDeWindows; 

    public void TraducirColor(int valorHex)
    {

        Color colorDetectado = ConvertirAColor(valorHex);
        colorDeWindows = colorDetectado;

        
        float h, s, v;
        Color.RGBToHSV(colorDetectado, out h, out s, out v);

        
        float matiz = h * 360;


        if (s < 0.10f || v < 0.10f)
        {
            emocionActual = EstadoEmocional.Neutral;
        }
        else
        {
            // clasifica por matiz
            if (matiz >= 330 || matiz < 15)
                emocionActual = EstadoEmocional.Enojo;        // Rojos
            else if (matiz >= 15 && matiz < 65)
                emocionActual = EstadoEmocional.Relajado;    // Amarillos
            else if (matiz >= 65 && matiz < 165)
                emocionActual = EstadoEmocional.Desagrado;   // Verdes
            else if (matiz >= 165 && matiz < 330)
                emocionActual = EstadoEmocional.Tristeza;    // Azules y Violetas
        }

       
    }

    // Método simple para convertir el número de registro a color de Unity
    Color ConvertirAColor(int colorBinario)
    {
        byte a = (byte)((colorBinario >> 24) & 0xFF);
        byte r = (byte)((colorBinario >> 16) & 0xFF);
        byte g = (byte)((colorBinario >> 8) & 0xFF);
        byte b = (byte)((colorBinario) & 0xFF);
        return new Color32(r, g, b, a);
    }
}
