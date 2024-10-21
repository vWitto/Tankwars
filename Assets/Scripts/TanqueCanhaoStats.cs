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

    public void Atirar()
    {
        // Reseta a frequ�ncia para 0, pois o tanque precisa recarregar a bala de canh�o para o pr�ximo tiro
        _frequenciaBalaAtual = 0f;

        //Dispara a bala e inst�ncia entre a rede
        var bala = PhotonNetwork.Instantiate("BalaPrefab", LocalizacaoSaidaBala.transform.position, LocalizacaoSaidaBala.transform.rotation);
        bala.GetComponent<BalaController>().Inicializar(GetComponentInParent<TanqueController>().gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Contabiliza a frequ�ncia da bala, pois o tanque est� carregando a bala de canh�o
        _frequenciaBalaAtual += Time.deltaTime;

        //Verifica se "sou eu" atirando
        if (photonView.IsMine)
        {
            //Verifica se o bot�o de espa�o do teclado foi apertado e se o tanque j� est� carregado para atirar
            if (Input.GetKeyDown(KeyCode.Space) && _frequenciaBalaAtual > _frequenciaBala)
            {
                //Executa o m�todo de atirar a bala de canh�o
                Atirar();
            }
        }
    }
}
