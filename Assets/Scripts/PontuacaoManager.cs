using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoManager : MonoBehaviour
{
    // Adiciona a pontuação ao jogador
    public void AdicionarPontuacao(Player player)
    {
        //Inicializa a pontuação como zero e obtem a pontuação do jogador
        int pontuacaoAtual = 0;
        if (player.CustomProperties.ContainsKey("Pontuacao"))
        {
            pontuacaoAtual = (int)player.CustomProperties["Pontuacao"];
        }

        //Adiciona a pontuação em 1
        pontuacaoAtual += 1;

        //Atualiza a pontuação no PhotonPun e notifica todos jogadores, fazendo isso, o Photon Pun executará o método OnPlayerPropertiesUpdate da classe PontuacaoUIController
        Hashtable propriedadePontuacao = new Hashtable();
        propriedadePontuacao["Pontuacao"] = pontuacaoAtual;
        player.SetCustomProperties(propriedadePontuacao);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
