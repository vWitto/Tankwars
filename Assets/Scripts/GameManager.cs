using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    //Referência as localizações que cada jogador começa
    public List<GameObject> localizacoesSpawn;

    //Referência ao texto de UI que tem o cronômetro
    public Text textTimer;

    //Tempo de partida em segundos
    public float tempoDePartida = 120f;
    private float tempoDePartidaAtual = 0f;

    //Boolean informando se o jogo finalizou
    public bool ehGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        //Obtém o índice do jogador para saber onde o tanque deve nascer
        //var indiceJogador = (PhotonNetwork.LocalPlayer.ActorNumber -1) % localizacoesSpawn.Count;
        //var go = localizacoesSpawn[indiceJogador];
        var go = ObterLocalizacaoSpawn(PhotonNetwork.LocalPlayer);

        //Cria o tanque no local onde ele deve ser criado
        var tanque = PhotonNetwork.Instantiate("TanquePrefab", go.transform.position, go.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
