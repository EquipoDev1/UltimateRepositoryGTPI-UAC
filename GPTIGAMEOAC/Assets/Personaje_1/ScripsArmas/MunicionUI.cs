using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MunicionUI : MonoBehaviour
{
    public Text texto;
    public LogicaArma logicaArma;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = logicaArma.balasEnCartucho + "/" + logicaArma.tamañoDeCartucho
                             + "\n" + logicaArma.balasRestantes;
        
    }
}
