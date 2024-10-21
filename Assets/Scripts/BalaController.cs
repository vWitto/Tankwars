using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    //Velocidade da bala
    public float velocidade = 5f;

    //Tempo de vida até a bala se auto destruir
    public float tempoDeVida = 3f;
    float tempoDeVidaAtual = 0f;

    //GameObject do tanque que disparou
    private GameObject atirador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
