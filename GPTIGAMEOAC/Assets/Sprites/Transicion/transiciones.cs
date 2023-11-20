using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class transiciones : MonoBehaviour
{
    
    private Animator transicionAnim;
    public GameObject LoadingScreen;
    public Slider LoadingBar;
    void Start()
    {
        LoadingScreen.SetActive(false);
        transicionAnim = GetComponent<Animator>();
        LoadingBar.value = 0;
    }
    public void LoadScene(string scene)
    {
        StartCoroutine(Transiciona(scene));
       
        
    }
    IEnumerator Transiciona(string scene)
    {
        LoadingScreen.SetActive(true);

        Debug.Log("Inicio del juego");
        
        for (float i = 0.0f; i <= 1.1f; i += 0.1f)
        {
            yield return new WaitForSeconds(0.3f);
            LoadingBar.value = i;
        }
        
        Debug.Log("Fin del juego");
        
        transicionAnim.SetTrigger("Salida");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
       
    }


}
