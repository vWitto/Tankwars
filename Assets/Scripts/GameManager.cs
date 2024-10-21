using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    //Refer�ncia as localiza��es que cada jogador come�a
    public List<GameObject> localizacoesSpawn;

    //Refer�ncia ao texto de UI que tem o cron�metro
    public Text textTimer;

    //Tempo de partida em segundos
    public float tempoDePartida = 120f;
    private float tempoDePartidaAtual = 0f;

    //Boolean informando se o jogo finalizou
    public bool ehGameOver = false;

    void Start()
    {
        //Inicia como falso, pois antes de iniciar a partida, o contador n�o deve aparecer
        textTimer.gameObject.SetActive(false);
    }

    //M�todo respons�vel por iniciar a partida
    public void IniciarPartida()
    {
        ehGameOver = false;
        FindObjectOfType<PontuacaoManager>().ResetarPontuacao(PhotonNetwork.LocalPlayer);

        //Faz o cronometro aparecer
        tempoDePartidaAtual = tempoDePartida;

        textTimer.gameObject.SetActive(true);
        AtualizarTimerUI();

        //Inicia uma co-rotina que executa a cada 1 segundo para atualizar o tempo do cronometro
        StartCoroutine(TimerCoroutine());

        //Obt�m o �ndice do jogador para saber onde o tanque deve nascer
        //var indiceJogador = (PhotonNetwork.LocalPlayer.ActorNumber -1) % localizacoesSpawn.Count;
        //var go = localizacoesSpawn[indiceJogador];
        var go = ObterLocalizacaoSpawn(PhotonNetwork.LocalPlayer);

        //Cria o tanque no local onde ele deve ser criado
        var tanque = PhotonNetwork.Instantiate("TanquePrefab", go.transform.position, go.transform.rotation);
    }

    public GameObject ObterLocalizacaoSpawn(Player player)
    {
        var indice = (player.ActorNumber - 1) % localizacoesSpawn.Count;
        return localizacoesSpawn[indice];
    }

    //Co-rotina respons�vel por contar e atualizar em tela o cron�metro
    private IEnumerator TimerCoroutine()
    {
        //Enquanto o tempo da partida n�o acabou e o jogo n�o finalizou, aguarda 1 segundo e atualiza a interface
        while (tempoDePartidaAtual > 0 && !ehGameOver)
        {
            //Aguarda 1 segundo
            yield return new WaitForSeconds(1f);

            //Diminui o tempo em 1 segundo
            tempoDePartidaAtual -= 1f;

            //Atualiza o tempo
            AtualizarTimerUI();
        }

        //Se o tempo acabou e o jogo ainda n�o finalizou, finaliza
        if (tempoDePartidaAtual <= 0 && !ehGameOver)
        {
            //Se for o host, efetua o t�rmino de partida
            if (PhotonNetwork.IsMasterClient)
            {
                //Efetua o processo de finaliza��o do jogo, avisando todos no RPC que a partida finalizou
                photonView.RPC("TerminarJogo", RpcTarget.All);
            }

            //Para o contador de tempo
            StopCoroutine(TimerCoroutine());
        }
    }

    //M�todo respons�vel por finalizar o jogo
    [PunRPC]
    public void TerminarJogo()
    {
        //Marca o jogo como finalizado
        ehGameOver = true;

        FindObjectsByType<TanqueController>(FindObjectsSortMode.None).ToList().ForEach(tanque =>
        {
            if (tanque.photonView.IsMine)
            {
                //Destr�i o tanque
                PhotonNetwork.Destroy(tanque.gameObject);
            }
        });

        if (PhotonNetwork.IsMasterClient)
        {
            FindFirstObjectByType<LobbyUIManager>().MostrarResultados();
        }
    }

    //M�todo respons�vel por atualizar o tempo
    void AtualizarTimerUI()
    {
        int minutos = Mathf.FloorToInt(tempoDePartidaAtual / 60);
        int segundos = Mathf.FloorToInt(tempoDePartidaAtual % 60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
