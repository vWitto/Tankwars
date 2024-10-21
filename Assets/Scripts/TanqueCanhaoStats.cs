using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TanqueCanhaoStats : MonoBehaviour, IShootable
{
    // Localização de onde vai sair a bala
    public Transform LocalizacaoSaidaBala;

    // Frequência que o tanque pode atirar uma bala de canhão, é o tempo que ele leva para carregar.
    public float _frequenciaBala = 1f;
    public float _frequenciaBalaAtual = 0f;

    public void Atirar()
    {
        // Reseta a frequência para 0, pois o tanque precisa recarregar a bala de canhão para o próximo tiro
        _frequenciaBalaAtual = 0f;

        //Dispara a bala e instância entre a rede
        var bala = PhotonNetwork.Instantiate("BalaPrefab", LocalizacaoSaidaBala.transform.position, LocalizacaoSaidaBala.transform.rotation);
        bala.GetComponent<BalaController>().Inicializar(GetComponentInParent<TanqueController>().gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Contabiliza a frequência da bala, pois o tanque está carregando a bala de canhão
        _frequenciaBalaAtual += Time.deltaTime;

        //Verifica se "sou eu" atirando
        if (photonView.IsMine)
        {
            //Verifica se o botão de espaço do teclado foi apertado e se o tanque já está carregado para atirar
            if (Input.GetKeyDown(KeyCode.Space) && _frequenciaBalaAtual > _frequenciaBala)
            {
                //Executa o método de atirar a bala de canhão
                Atirar();
            }
        }
    }
}
