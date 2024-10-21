using Photon.Pun;
using System.Data.Common;
using UnityEngine;

public class BalaController : MonoBehaviourPun
{
    //Velocidade da bala
    public float velocidade = 8f;

    //Tempo de vida at� a bala se auto destruir
    public float tempoDeVida = 3f;
    float tempoDeVidaAtual = 0f;

    //GameObject do tanque que disparou
    private GameObject atirador;

    //Inicializa a bala informando quem disparou ela
    public void Inicializar(GameObject atirador)
    {
        this.atirador = atirador;
    }

    // Update is called once per frame
    void Update()
    {
        //Faz a bala se movimentar 
        transform.Translate(Vector3.right * velocidade * Time.deltaTime);

        //Contabiliza o tempo para saber se deve destruir a bala
        tempoDeVidaAtual += Time.deltaTime;
        if (tempoDeVidaAtual > tempoDeVida)
        {
            //Destr�i a bala
            AutoDestruir();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se a bala est� colidindo com o atirador, neste caso, n�o deve fazer nada
        if (atirador == collision.gameObject)
        {
            Debug.Log("A bala passou pelo pr�prio atirador");
            return;
        }

        //Obtem e verifica se o objeto com quem a bala colidiu, pode receber dano
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            //Faz com que o objeto que recebeu o tiro e pode receber dano, receba o dano
            damageable.ReceberDano();

            //Verifica se a bala "� minha", se for e acertou o alvo, contabiliza um ponto
            if (photonView.IsMine)
            {
                //Adiciona a pontua��o para o jogador atual
                FindObjectOfType<PontuacaoManager>().AdicionarPontuacao(PhotonNetwork.LocalPlayer);
            }
        }

        AutoDestruir();
    }

    void AutoDestruir()
    {
        //Somente pode destruir o objeto quem � o dono dele ou o host da partida
        if (photonView.IsMine)
        {
            // Destr�i a bala ap�s a colis�o
            PhotonNetwork.Destroy(gameObject);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            // Destr�i a bala ap�s a colis�o
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
