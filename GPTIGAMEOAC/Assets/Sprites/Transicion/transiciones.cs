using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Transiciones : MonoBehaviour
{
    private Animator transicionAnim;
    public GameObject LoadingScreen;
    public Slider LoadingBar;
    public GameObject IntroScreen;
    public Camera CamaraMusica;

    void Start()
    {
        Time.timeScale = 1;
        LoadingScreen.SetActive(false);
        IntroScreen.SetActive(false);
        transicionAnim = GetComponent<Animator>();
        LoadingBar.value = 0;
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(PlayVideoAndTransition(scene));
    }

    IEnumerator PlayVideoAndTransition(string scene)
    {
        CamaraMusica.GetComponent<AudioListener>().enabled = false;
        IntroScreen.SetActive(true);
        yield return new WaitForSeconds(74);
        StartCoroutine(Transition(scene));
    }

    IEnumerator Transition(string scene)
    {
        IntroScreen.SetActive(false);
        LoadingScreen.SetActive(true);
        for (float i = 0.0f; i <= 1.1f; i += 0.1f)
        {
            yield return new WaitForSeconds(0.3f);
            LoadingBar.value = i;
        }
        Debug.Log("salidaanim");
        transicionAnim.SetTrigger("Salida");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);
    }
}