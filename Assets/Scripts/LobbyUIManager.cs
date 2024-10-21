using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviourPunCallbacks
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
        //Inativa o bot�o pois o jogo acabou de come�ar
        buttonIniciarPartida.gameObject.SetActive(false);
        buttonRecomecarPartida.gameObject.SetActive(false);

        //Inicia o texto como Carregando enquanto o jogo est� carregando
        textStatus.text = "Carregando...";
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

    //M�tdo executado ao clicar no bot�o de iniciar a partida
    public void OnClickButtonIniciarPartida()
    {
        //Verifica se "eu sou" o host da sess�o e, caso for, inicia a partida com todos que est�o em sala
        if (PhotonNetwork.IsMasterClient)
        {
            //Envia uma mensagem via RPC avisando todos os jogadores que a partida deve come�ar
            photonView.RPC("IniciarPartidaParaTodos", RpcTarget.All);
        }
    }

    //M�tdo executado ao clicar no bot�o de iniciar a partida
    public void OnClickButtonRecomecarPartida()
    {
        //Verifica se "eu sou" o host da sess�o e, caso for, inicia a partida com todos que est�o em sala
        if (PhotonNetwork.IsMasterClient)
        {
            //Envia uma mensagem via RPC avisando todos os jogadores que a partida deve ser reiniciada
            photonView.RPC("RecomecarPartidaParaTodos", RpcTarget.All);
        }
    }


    [PunRPC]
    public void IniciarPartidaParaTodos()
    {
        //Esconde o texto e o bot�o pois a partida vai iniciar
        textStatus.gameObject.SetActive(false);
        buttonIniciarPartida.gameObject.SetActive(false);

        //Procura o objeto e classe GameManager e inicia a partida
        var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.IniciarPartida();
    }

    public void MostrarResultados()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            buttonRecomecarPartida.gameObject.SetActive(true);
        }
    }


    [PunRPC]
    public void RecomecarPartidaParaTodos()
    {
        //Esconde o texto e o bot�o pois a partida vai iniciar
        textStatus.gameObject.SetActive(false);
        buttonIniciarPartida.gameObject.SetActive(false);
        buttonRecomecarPartida.gameObject.SetActive(false);

        //Procura o objeto e classe GameManager e inicia a partida
        var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.IniciarPartida();
    }

    //M�todo executado automaticamente pelo PhotonPun quando o jogador dono da sess�o foi alterado (ex.: o criador saiu da sala)
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //Atualiza a interface
        AtualizarUI();
    }
}
