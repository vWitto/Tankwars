using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
