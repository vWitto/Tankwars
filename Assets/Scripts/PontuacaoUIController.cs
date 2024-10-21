using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class PontuacaoUIController : MonoBehaviourPunCallbacks
{
    //Refer�ncia da UI de texto de pontua��o
    public Text textPontuacao;

    //N�mero do "Actor" do Photon PUN para ter a refer�ncia de quem pertence esta UI
    public int actorNumber;

    //M�todo executado automaticamente pelo PhotonPun quando � identificado que algum jogador teve uma propriedade alterada
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        // Verifica se a propriedade "Score" foi alterada e se deve atualizar esta UI
        if (changedProps.ContainsKey("Pontuacao") && targetPlayer.ActorNumber == actorNumber)
        {
            //Obt�m a pontua��o atualizada
            int newScore = (int)changedProps["Pontuacao"];

            //Altera a UI para o jogador
            textPontuacao.text = newScore.ToString();
        }
    }
}
