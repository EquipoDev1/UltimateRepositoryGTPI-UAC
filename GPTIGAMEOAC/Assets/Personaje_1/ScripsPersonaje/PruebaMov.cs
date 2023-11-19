using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaMov : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    public float velocidadCorrer = 2.0f;
    public float tiempoRecuperacion = 2.0f; // Tiempo en segundos para recuperar la energía
    public float tiempoCansancio = 5.0f; // Tiempo en segundos antes de cansarse
    public float velocidadSalto = 8.0f;
    public float gravedad = 30.0f;
    public Transform camTransform;
    private Animator anim;
    private CharacterController characterController;
    private Vector3 movimiento;
    private bool cansado = false;
    private float tiempoCansado;
    private bool enElSuelo = true;
    private float velocidadVertical;

    [Header("Camera Controls")]
    public float mouseSensivity;
    public bool invertX;
    public bool invertY;

    private float pitch = 0f;

    private static readonly int SaltarHash = Animator.StringToHash("salte");


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Verifica si la tecla Left Shift está presionada para correr
        bool correr = Input.GetKey(KeyCode.LeftShift);

        // Determina la velocidad de movimiento actual
        float velocidadActual = correr && !cansado ? velocidadMovimiento * velocidadCorrer : velocidadMovimiento;

        // Calcular el movimiento en la dirección de la cámara
        Vector3 forward = camTransform.forward;
        forward.y = 0f; // No queremos movimiento vertical
        forward.Normalize();

        Vector3 right = camTransform.right;
        right.y = 0f; // No queremos movimiento vertical
        right.Normalize();

        movimiento = (forward * y + right * x) * velocidadActual;

        // Manejar el salto
        if (characterController.isGrounded)
        {
            enElSuelo = true;
            velocidadVertical = -0.5f; // Reiniciar la velocidad vertical cuando está en el suelo
            anim.SetBool(SaltarHash, false);
        }

        if (enElSuelo && Input.GetButtonDown("Jump"))
        {
            enElSuelo = false;
            velocidadVertical = Mathf.Sqrt(2 * gravedad * velocidadSalto);
            anim.SetBool(SaltarHash, true);
        }

        velocidadVertical -= gravedad * Time.deltaTime;
        movimiento.y = velocidadVertical;

        // Aplicar movimiento usando CharacterController
        characterController.Move(movimiento * Time.deltaTime);

        // Actualizar la animación
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        // Control Rotación Cuerpo del Personaje
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * velocidadRotacion, 0);

        // Control Rotación Cámara sin interrupción
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensivity;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }

        // Actualiza 'pitch' basado en el input del mouse, y luego usa eso para establecer la rotación de la cámara
        pitch -= mouseInput.y;
        pitch = Mathf.Clamp(pitch, -30f, 30f);

        camTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Manejar cansancio
        if (correr && !cansado)
        {
            tiempoCansado += Time.deltaTime;

            if (tiempoCansado >= tiempoCansancio)
            {
                cansado = true;
                tiempoCansado = 0f;
                StartCoroutine(RecuperarEnergia());
            }
        }

        anim.SetBool(SaltarHash, !characterController.isGrounded); // Restablecer el parámetro de animación de salto

    }

    IEnumerator RecuperarEnergia()
    {
        yield return new WaitForSeconds(tiempoRecuperacion);
        cansado = false;
    }
}
