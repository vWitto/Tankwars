using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TanqueCanhaoStats : MonoBehaviour, IShootable
{
    // Localiza��o de onde vai sair a bala
    public Transform LocalizacaoSaidaBala;

    // Frequ�ncia que o tanque pode atirar uma bala de canh�o, � o tempo que ele leva para carregar.
    public float _frequenciaBala = 1f;
    public float _frequenciaBalaAtual = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
