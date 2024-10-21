using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    //Velocidade da bala
    public float velocidade = 5f;

    //Tempo de vida at� a bala se auto destruir
    public float tempoDeVida = 3f;
    float tempoDeVidaAtual = 0f;

    //GameObject do tanque que disparou
    private GameObject atirador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
