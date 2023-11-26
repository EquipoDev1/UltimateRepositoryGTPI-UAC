using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    public VidaUsuario vidaUsuario;
    public bool Vida0 = false;
    [SerializeField] private Animator animadorPerder;
    // Start is called before the first frame update
    void Start()
    {
        vidaUsuario = GetComponent<VidaUsuario>();
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
    }

    void RevisarVida()
    {
        if (Vida0) return;
        if(vidaUsuario.valor <= 0)
        {
            Vida0 = true;
            Invoke("ReiniciarJuego", 1.5f);
        }
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
