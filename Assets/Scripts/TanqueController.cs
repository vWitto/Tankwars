using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TanqueController : MonoBehaviour
{

    //Velocidade de rotação do tanque
    public float _velocidadeRotacao = 100f;

    //Velocidade de movimento do tanque
    public float _velocidadeMovimento = 5f;

    private Rigidbody2D rb;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoverTanque(float moverHorizonalmente, float moverVerticalmente)
    {
        // Movimento do tanque (Move o tanque na direção em que ele está apontado)
        Vector2 movimento = transform.right * moverVerticalmente * _velocidadeMovimento * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movimento);

        // Rotaciona o tanque (A ou D) - move no eixo Z para 2D
        float rotacao = -moverHorizonalmente * _velocidadeRotacao * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotacao);
    }

    public void ReceberDano()
    {
        //No caso deste jogo, ao receber um dano, o tanque é teleportado para a área de respawn
        //Por este motivo, envia uma mensagem ao dono deste tanque para que ele resete a posição pois só ele pode fazer isso
        photonView.RPC("ResetarPosicaoNoSpawn", photonView.Owner);
    }
}
