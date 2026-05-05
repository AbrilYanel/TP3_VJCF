using UnityEngine;

public class PiezaCasette : MonoBehaviour
{
    public Transform objetivo; // Arrastra aquí la pieza correspondiente del grupo "Ordenado"
    public float distanciaParaEncajar = 0.5f;

    [Header("Configuración de Emoción")]
    public bool esFragil;
    public bool requiereFuerza;

    private GestorEmociones gestor;
    private bool encajada = false;

    void Start()
    {
        gestor = Object.FindFirstObjectByType<GestorEmociones>();
        // Ocultamos el objetivo para que no se vea la pieza "fantasma" todavía
        if (objetivo != null) objetivo.gameObject.SetActive(false);
    }

    void OnMouseUp() // Cuando el jugador suelta la pieza
    {
        if (encajada) return;

        float distancia = Vector3.Distance(transform.position, objetivo.position);

        if (distancia < distanciaParaEncajar)
        {
            ComprobarEncaje();
        }
    }

    void ComprobarEncaje()
    {
        EstadoEmocional emocion = gestor.emocionActual;

        if (esFragil && emocion == EstadoEmocional.Enojo)
        {
            Debug.Log("¡CRACK! Rompiste la pieza por estar enojado.");
            this.enabled = false; // Desactiva el script, la pieza queda "muerta"
            // Aquí podrías cambiar el color a negro o destruirla
        }
        else if (requiereFuerza && emocion != EstadoEmocional.Enojo)
        {
            Debug.Log("Esta pieza está muy dura, necesitas más fuerza.");
        }
        else if (esFragil && emocion != EstadoEmocional.Relajado)
        {
            Debug.Log("Necesitas estar más Relajado para manipular esta pieza con cuidado.");
        }
        else
        {
            // Si pasa los filtros, se encaja
            Encajar();
        }
    }

    void Encajar()
    {
        encajada = true;
        transform.position = objetivo.position;
        transform.rotation = objetivo.rotation;

        // Bloqueamos el movimiento para que no se pueda volver a mover
        GetComponent<MoverPieza>().BloquearMovimiento();
        Debug.Log("Pieza encajada correctamente.");
    }
}