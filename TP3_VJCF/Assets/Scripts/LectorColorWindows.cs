using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Win32;

public class LectorColorWindows : MonoBehaviour
{

    public GestorEmociones gestor;
    void Update()
    {
        // Esto busca la carpeta interna de Windows donde se guarda el color
        object colorRegistro = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", null);

        if (colorRegistro != null)
        {
            // Guardamos el número del color
            int valorColor = (int)colorRegistro;

            // Lo mostramos en la consola para comprobar que funciona
            gestor.TraducirColor(valorColor);
        }
    }
}
