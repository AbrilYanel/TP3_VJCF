using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public int totalPiezas;
    private int piezasProcesadas = 0;
    private int piezasEncajadasExitosamente = 0;

    [HideInInspector] public GameObject botonFinalizar;
    [HideInInspector] public TextMeshProUGUI textoPuntaje;
     public ActivadorPuzzle activador;

    // ObjetoAgarrable ahora se auto-asigna al propio objeto padre
    [HideInInspector] public GameObject objetoAgarrable;

    public int puntajeObtenido;

    void Start()
    {
        activador = GetComponentInParent<ActivadorPuzzle>();
        if (activador == null)
            activador = Object.FindFirstObjectByType<ActivadorPuzzle>();

        if (activador == null)
            Debug.LogError("PuzzleManager no encontró ningún ActivadorPuzzle en la escena.");
        objetoAgarrable = this.gameObject;

        
        GameObject botonObj = GameObject.Find("BotonFinalizar");

        if (botonObj != null)
        {
            botonFinalizar = botonObj;

            // Obtenemos el componente Button
            Button btn = botonFinalizar.GetComponent<Button>();

            // Limpiamos funciones viejas (del puzzle anterior) y asignamos la nueva
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(FinalizarPuzzle);

            // Nos aseguramos de que empiece apagado
            botonFinalizar.SetActive(false);
        }
        else
        {
            Debug.LogError("Error de Arquitectura: No se encontró un objeto llamado BotonFinalizar en la escena.");
        }
    }

    public void RegistrarPiezaTerminada(bool fueExito)
    {
        piezasProcesadas++;
        if (fueExito) piezasEncajadasExitosamente++;

        if (piezasProcesadas >= totalPiezas)
        {
            botonFinalizar.SetActive(true);
        }
    }

    public void FinalizarPuzzle()
    {
        puntajeObtenido = piezasEncajadasExitosamente * 25;
        activador.SalirDeModoPuzzle();
        activador.DeshabilitarArea();

        if (objetoAgarrable != null)
        {
            Collider[] collidersHijos = objetoAgarrable.GetComponentsInChildren<Collider>();
            foreach (var c in collidersHijos)
            {
                if (c.gameObject != objetoAgarrable) c.enabled = false;
            }

            BoxCollider bc = objetoAgarrable.GetComponent<BoxCollider>();
            if (bc == null) bc = objetoAgarrable.AddComponent<BoxCollider>();
            bc.isTrigger = true;
            bc.size = new Vector3(1f, 1f, 1f);

            Rigidbody rb = objetoAgarrable.GetComponent<Rigidbody>();
            if (rb == null) rb = objetoAgarrable.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            ObjetoAgarrable recogible = objetoAgarrable.AddComponent<ObjetoAgarrable>();
            recogible.Configurar(this);
        }
    }
}