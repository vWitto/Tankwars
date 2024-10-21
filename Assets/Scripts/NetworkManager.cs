using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    void Start()
    {
        //Conecta no servidor Photon que foi configurado com o AppId do Photon Pun
        PhotonNetwork.ConnectUsingSettings();
    }

    // Callback de quando houve conex�o no servidor, este m�todo ser� chamado.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado no servidor photon.");

        PhotonNetwork.JoinLobby();
    }

    //M�todo chamado quando entrou no lobby, ocorre ap�s o OnConnectedToMaster
    public override void OnJoinedLobby()
    {
        Debug.Log("Executou OnJoinedLobby");

        // Cria ou entra em uma sala chamada "PanzerWars"
        PhotonNetwork.JoinOrCreateRoom("PanzerWars", new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
