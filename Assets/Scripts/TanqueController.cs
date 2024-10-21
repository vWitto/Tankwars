using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class TanqueController : MonoBehaviourPun, IDamageable
{
    //Velocidade de rota��o do tanque
    public float _velocidadeRotacao = 100f;

    //Velocidade de movimento do tanque
    public float _velocidadeMovimento = 5f;

    private Rigidbody2D rb;
    private GameManager gm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.ehGameOver)
        {
            return;
        }

        //Verifica se "sou eu"
        if (photonView.IsMine)
        {
            //Obt�m o comando de girar o tanque (A ou D)
            float moverHorizonalmente = Input.GetAxis("Horizontal");

            //Obt�m o comando de mover o tanque (W ou S)
            float moverVerticalmente = Input.GetAxis("Vertical");

            MoverTanque(moverHorizonalmente, moverVerticalmente);
        }
    }

    void MoverTanque(float moverHorizonalmente, float moverVerticalmente)
    {
        // Movimento do tanque (Move o tanque na dire��o em que ele est� apontado)
        Vector2 movimento = transform.right * moverVerticalmente * _velocidadeMovimento * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movimento);

        // Rotaciona o tanque (A ou D) - move no eixo Z para 2D
        float rotacao = -moverHorizonalmente * _velocidadeRotacao * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotacao);
    }

    //M�todo herdado da interface que trata o recebimento de dano
    public void ReceberDano()
    {
        //No caso deste jogo, ao receber um dano, o tanque � teleportado para a �rea de respawn
        //Por este motivo, envia uma mensagem ao dono deste tanque para que ele resete a posi��o pois s� ele pode fazer isso
        photonView.RPC("ResetarPosicaoNoSpawn", photonView.Owner);
    }

    //M�todo respons�vel por resetar a posi��o
    [PunRPC]
    public void ResetarPosicaoNoSpawn()
    {
        //Obt�m a posi��o com base no player
        var localizacaoSpawn1 = FindFirstObjectByType<GameManager>().ObterLocalizacaoSpawn(photonView.Owner);

        //Seta ao tanque, a posi��o do respawn
        transform.position = localizacaoSpawn1.transform.position;

        //Seta ao tanque, a rota��o do respawn
        transform.rotation = localizacaoSpawn1.transform.rotation;
    }
}
