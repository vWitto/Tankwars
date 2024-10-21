using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoManager : MonoBehaviour
{
    // Adiciona a pontua��o ao jogador
    public void AdicionarPontuacao(Player player)
    {
        //Inicializa a pontua��o como zero e obtem a pontua��o do jogador
        int pontuacaoAtual = 0;
        if (player.CustomProperties.ContainsKey("Pontuacao"))
        {
            pontuacaoAtual = (int)player.CustomProperties["Pontuacao"];
        }

        //Adiciona a pontua��o em 1
        pontuacaoAtual += 1;

        //Atualiza a pontua��o no PhotonPun e notifica todos jogadores, fazendo isso, o Photon Pun executar� o m�todo OnPlayerPropertiesUpdate da classe PontuacaoUIController
        Hashtable propriedadePontuacao = new Hashtable();
        propriedadePontuacao["Pontuacao"] = pontuacaoAtual;
        player.SetCustomProperties(propriedadePontuacao);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
