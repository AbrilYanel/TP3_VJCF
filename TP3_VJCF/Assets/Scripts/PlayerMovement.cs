using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    private CharacterController controller;
    private Vector3 moveDirection;
    public bool bloqueado = false; // Nuevo

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (bloqueado) return; // Si está bloqueado no procesa input

        float x = Input.GetAxisRaw("Horizontal"); // Raw elimina la inercia
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);
    }
}
