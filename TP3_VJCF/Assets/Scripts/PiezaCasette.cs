using UnityEngine;

public class PiezaCasette : MonoBehaviour
{
    public Transform objetivo;
    public float distanciaParaEncajar = 0.5f;

    [Header("Estado de la Pieza")]
    public string nombrePieza;
    public float integridad = 100f; 
    public bool rota = false;

    [Header("Configuración de Emoción")]
    public bool esFragil;
    public bool requiereFuerza;

    private GestorEmociones gestor;
    private bool encajada = false;
    private PuzzleManager puzzleManager;
    void Start()
    {
        gestor = Object.FindFirstObjectByType<GestorEmociones>();
        puzzleManager = Object.FindFirstObjectByType<PuzzleManager>();
    }

    void OnMouseUp()
    {
        if (encajada || rota) return;

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
            RecibirDanio(40f, "¡Demasiada fuerza! La pieza se está agrietando.");
        }
        
        else if (requiereFuerza && emocion != EstadoEmocional.Enojo)
        {
            RecibirDanio(20f, "La pieza no encaja y se desgasta al forzarla.");
        }
        
        else
        {
            Encajar();
        }
    }

    void RecibirDanio(float cantidad, string mensaje)
    {
        integridad -= cantidad;
        Debug.Log(mensaje + " Integridad: " + integridad + "%");

        if (integridad <= 0)
        {
            Debug.Log("PIEZA ROTA: Puzzle Perdido.");

            if (integridad <= 0 && !rota)
            {
                RomperObjeto();
            }
        }
    }

    void RomperObjeto()
    {
        integridad = 0;
        rota = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        puzzleManager.RegistrarPiezaTerminada(false);
    }


        void Encajar()
    {
        encajada = true;
        transform.position = objetivo.position;
        transform.rotation = objetivo.rotation;
        if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MoverPieza>().BloquearMovimiento();
        puzzleManager.RegistrarPiezaTerminada(true);
    }
}