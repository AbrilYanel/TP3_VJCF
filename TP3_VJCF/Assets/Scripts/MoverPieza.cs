using UnityEngine;

public class MoverPieza : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool puedeMover = true;
    private Camera camaraActual;
    private ControladorUI ui;

    private void Start()
    {
        // Tomamos la UI del GameManager centralizado
        ui = GameManager.Instance.controladorUI;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GestorEmociones gestor = Object.FindFirstObjectByType<GestorEmociones>();
            PiezaCasette pieza = GetComponent<PiezaCasette>();

            if (gestor != null && gestor.emocionActual == EstadoEmocional.Desagrado && pieza.estaSucia)
            {
                Object.FindFirstObjectByType<ActivadorPuzzle>().SalirParaLavar(this.gameObject);
            }
        }
    }

    void OnMouseDown()
    {
        if (!puedeMover) return;

        // CRÍTICO: Usamos la cámara de la mesa para el cálculo del mouse
        camaraActual = GameManager.Instance.camaraMesa;

        zCoord = camaraActual.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();

        PiezaCasette pieza = GetComponent<PiezaCasette>();
        if (ui != null && pieza != null) ui.MostrarInfo(pieza.nombrePieza, pieza.integridad);
    }

    private void OnMouseUp()
    {
        if (ui != null) ui.OcultarInfo();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return camaraActual.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (!puedeMover) return;
        Vector3 nuevaPosicion = GetMouseWorldPos() + offset;
        nuevaPosicion.y = transform.position.y;
        transform.position = nuevaPosicion;
    }

    public void BloquearMovimiento()
    {
        puedeMover = false;
    }
}