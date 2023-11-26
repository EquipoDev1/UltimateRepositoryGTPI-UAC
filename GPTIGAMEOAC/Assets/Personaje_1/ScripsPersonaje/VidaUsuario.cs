using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaUsuario : MonoBehaviour
{
    public float valor = 100;
    public Slider BarraVida;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecibirDaño(float daño)
    {
        valor -= daño;
        if (valor < 0)
        {
            valor = 0;
        }
        BarraVida.value = valor;
    }
}
