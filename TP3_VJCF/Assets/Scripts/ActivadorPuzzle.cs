using UnityEngine;
using System.Collections;
public class ActivadorPuzzle : MonoBehaviour
{
    [HideInInspector] public Camera camaraPrincipal;
    [HideInInspector] public Camera camaraMesa;
    [HideInInspector] public MonoBehaviour playerMovement;

    private bool jugadorCerca = false;
    private bool enPuzzle = false;

    private float tiempoUltimaInteraccion = 0f;
    private float cooldownInteraccion = 0.3f;
    void Start()
    {
        // Obtenemos las referencias globales al instanciarse el prefab
        camaraPrincipal = GameManager.Instance.camaraPrincipal;
        camaraMesa = GameManager.Instance.camaraMesa;
        playerMovement = GameManager.Instance.playerMovement;
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time - tiempoUltimaInteraccion < cooldownInteraccion) return;
            tiempoUltimaInteraccion = Time.time;

            if (!enPuzzle) EntrarAModoPuzzle();
            else SalirDeModoPuzzle();
        }
    }

    public void EntrarAModoPuzzle()
    {
        enPuzzle = true;
        camaraPrincipal.gameObject.SetActive(false);
        camaraMesa.gameObject.SetActive(true);

        Transform contenedor = transform.Find("PiezasPuzzle");
        if (contenedor != null) contenedor.gameObject.SetActive(true);

        // Bloqueamos el movimiento en lugar de deshabilitar el componente
       /* PlayerMovement pm = playerMovement as PlayerMovement;
        if (pm != null) pm.bloqueado = true;*/

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ReintegrarPiezaEnMano(contenedor);
    }

  
        private void ReintegrarPiezaEnMano(Transform contenedor)
    {
        GameObject mano = GameObject.Find("Mano");
        if (mano == null || contenedor == null) return;

        PiezaCasette pieza = mano.GetComponentInChildren<PiezaCasette>();
        if (pieza == null) return;

        pieza.transform.SetParent(contenedor);

        // Desactivamos todos los colliders para que no choque con nada
        foreach (Collider col in pieza.GetComponents<Collider>())
            col.enabled = false;

        // Reseteamos el Rigidbody si tiene uno
        Rigidbody rb = pieza.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        ObjetoAgarrable agarrable = pieza.GetComponent<ObjetoAgarrable>();
        if (agarrable != null) Destroy(agarrable);

        MoverPieza mover = pieza.GetComponent<MoverPieza>();
        if (mover != null) mover.enabled = true;

        Debug.Log("Pieza " + pieza.nombrePieza + " reintegrada al puzzle.");
    }
    public void SalirDeModoPuzzle()
    {
        enPuzzle = false;
        camaraPrincipal.gameObject.SetActive(true);
        camaraMesa.gameObject.SetActive(false);

        Transform contenedor = transform.Find("PiezasPuzzle");
        if (contenedor != null) contenedor.gameObject.SetActive(false);

        PlayerMovement pm = playerMovement as PlayerMovement;
        if (pm != null) pm.bloqueado = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = false;
    }

    public void SalirParaLavar(GameObject pieza)
    {
        SalirDeModoPuzzle();

        // Desactivamos el MoverPieza para que no interfiera fuera del puzzle
        MoverPieza mover = pieza.GetComponent<MoverPieza>();
        if (mover != null) mover.enabled = false;

        // Separamos la pieza del contenedor del puzzle
        pieza.transform.SetParent(null);

        // Aseguramos que tenga collider como trigger para detección
        Collider col = pieza.GetComponent<Collider>();
        if (col != null) col.isTrigger = true;

        // Agregamos ObjetoAgarrable si no lo tiene
        ObjetoAgarrable agarrable = pieza.GetComponent<ObjetoAgarrable>();
        if (agarrable == null) agarrable = pieza.AddComponent<ObjetoAgarrable>();

        agarrable.Agarrar(); // La pone directo en la mano
    }


    public void DeshabilitarArea()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    public void HabilitarArea()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;
        jugadorCerca = false; // Reseteamos por si el jugador quedó dentro
    }
}