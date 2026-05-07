using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cajaenvio : MonoBehaviour
{
    public float dineroTotal = 0;
    public TextMeshProUGUI textoDinero;

    private void OnTriggerEnter(Collider other)
    {
        
        ObjetoAgarrable objeto = other.GetComponentInParent<ObjetoAgarrable>();

        if (objeto != null)
        {
            EntregarPedido(objeto);
        }
    }

    void EntregarPedido(ObjetoAgarrable objeto)
    {
        
        float pago = objeto.puntaje;
        dineroTotal += pago;

        if (textoDinero != null) textoDinero.text = "$" + dineroTotal;

        Debug.Log("Pedido entregado. Recibiste: $" + pago);

        
        Destroy(objeto.gameObject);
        FindObjectOfType<GameManager>().SiguientePedido();
    }
}
