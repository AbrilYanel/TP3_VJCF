using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Esto crea una instancia global a la que cualquier script puede acceder fácilmente
    public static GameManager Instance;

    [Header("Referencias Globales de la Escena")]
    public Camera camaraPrincipal;
    public Camera camaraMesa;
    public MonoBehaviour playerMovement;
    public GameObject botonFinalizar;
    public TextMeshProUGUI textoPuntaje;
    public ControladorUI controladorUI;

    [Header("Gestión de Puzzles")]
    public GameObject[] puzzles; // 0: Casete, 1: Reloj
    public Transform puntoAparicionMesa;
    private int nivelActual = 0;

    void Awake()
    {
        // Configuramos el Singleton
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

   
    public void SiguientePedido()
    {
        nivelActual++;
        if (nivelActual < puzzles.Length)
        {
            InstanciarPuzzleActual();
        }
        else
        {
            Debug.Log("¡Todos los pedidos completados!");
        }
    }

    private void InstanciarPuzzleActual()
    {
        if (puzzles.Length > 0 && nivelActual < puzzles.Length)
        {
            Instantiate(puzzles[nivelActual], puntoAparicionMesa.position, puntoAparicionMesa.rotation);

            // Re-habilita el área para el nuevo puzzle
            ActivadorPuzzle activador = FindFirstObjectByType<ActivadorPuzzle>();
            if (activador != null) activador.HabilitarArea();
        }
    }
}