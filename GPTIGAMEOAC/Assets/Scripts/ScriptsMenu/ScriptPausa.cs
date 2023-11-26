using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptPausa : MonoBehaviour
{
    [SerializeField] private GameObject MenuPausaUI;
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject ElementoDeshabilitar;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            isPaused = !isPaused;
        }

        if(isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DesactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        
        Time.timeScale = 0;
        CambiScript(false);
        MenuPausaUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None; 
    }

    public void DesactivateMenu()
    {
        Time.timeScale = 1;
        CambiScript(true);
        MenuPausaUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CambiScript(bool PrmBlScrit)
    {
        PruebaMov scriptToDisable = ElementoDeshabilitar.GetComponent<PruebaMov>();
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = PrmBlScrit; // Deshabilita el script.
        }
    }

    public void BotonRegresar()
    {
        isPaused = false;
    }
}
