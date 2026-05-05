using UnityEngine;

public class MoverPieza : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool puedeMover = true;
    private Camera camaraActual;

    void OnMouseDown()
    {
        if (!puedeMover) return;

        // Buscamos cualquier cámara que esté encendida en la escena ahora mismo
        camaraActual = Camera.main;
        if (camaraActual == null) camaraActual = FindObjectOfType<Camera>();

        zCoord = camaraActual.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();
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