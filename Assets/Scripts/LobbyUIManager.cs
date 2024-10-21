using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    //Refer�ncia ao bot�o de iniciar partida
    public Button buttonIniciarPartida;

    //Refer�ncia ao bot�o de recome�ar a partida
    public Button buttonRecomecarPartida;

    //Refer�ncia ao texto da UI de status
    public Text textStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //M�todo respons�vel por atualizar a interface
    public void AtualizarUI()
    {
        //Verifica se � o host da partida (geralmente o primeiro que entra ou cria a sala)
        if (PhotonNetwork.IsMasterClient)
        {
            //Mostra o bot�o de iniciar partida, pois somente o host pode fazer isso
            buttonIniciarPartida.gameObject.SetActive(true);

            //Esconde de status da partida
            textStatus.gameObject.SetActive(false);
        }
        else
        {
            //Esconde o bot�o de iniciar partida, pois somente o host pode fazer isso
            buttonIniciarPartida.gameObject.SetActive(false);

            //Mostra e define um texto do status da partida
            textStatus.gameObject.SetActive(true);
            textStatus.text = "Aguardando dono da sala iniciar a partida";
        }
    }
}
