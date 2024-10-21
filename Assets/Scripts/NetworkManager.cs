using Photon.Pun;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
