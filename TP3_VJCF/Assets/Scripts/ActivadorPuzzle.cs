using UnityEngine;

public class ActivadorPuzzle : MonoBehaviour
{
    public Camera camaraPrincipal;
    public Camera camaraMesa;
    public GameObject piezasPuzzle;
    public MonoBehaviour playerMovement; // Usamos MonoBehaviour para que sea más flexible

    private bool jugadorCerca = false;
    private bool enPuzzle = false;

    void Update()
    {
        // Si el jugador está cerca y presiona la E
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (!enPuzzle)
            {
                EntrarAModoPuzzle();
            }
            else
            {
                SalirDeModoPuzzle();
            }
        }
    }

    public void EntrarAModoPuzzle()
    {
        enPuzzle = true;
       
        camaraPrincipal.gameObject.SetActive(false);
        camaraMesa.gameObject.SetActive(true);
        piezasPuzzle.SetActive(true);
        if (playerMovement != null) playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SalirDeModoPuzzle()
    {
        enPuzzle = false;
        camaraPrincipal.enabled = true;
        camaraMesa.enabled = false;
        piezasPuzzle.SetActive(false);
        if (playerMovement != null) playerMovement.enabled = true;

      
    }

    // Detectar cuando el jugador entra al rango
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Presiona E para reparar");
        }
    }

    // Detectar cuando el jugador se aleja
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}