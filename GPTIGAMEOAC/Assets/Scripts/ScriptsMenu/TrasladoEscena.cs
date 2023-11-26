using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrasladoEscena : MonoBehaviour
{
    public void Cargar(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
