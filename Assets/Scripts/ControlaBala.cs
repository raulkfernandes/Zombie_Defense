using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBala : MonoBehaviour {

	[SerializeField] [Range(15, 30)] private float velocidade;
	private Rigidbody rigidbodyArma;
	private int danoDaBala = 100;
    //private MovimentoJogador movimentoJogador;

    void Start () {

        Destroy(gameObject, 1);

		rigidbodyArma = GetComponent<Rigidbody> ();
        // Olhar para o impacto.point do RaycastHit de 'MovimentoJogador':
        // movimentoJogador = GameObject.FindObjectOfType(typeof(MovimentoJogador)) as MovimentoJogador;
        // Quaternion direcaoDoTiro = Quaternion.LookRotation(movimentoJogador.trajetoriaDoTiro - transform.position);
        // rigidbodyArma.MoveRotation(direcaoDoTiro);
    }

    // Update is called once per frame
    void FixedUpdate () {

        Vector3 direcaoCorrigida = transform.forward * velocidade * Time.deltaTime;
		rigidbodyArma.MovePosition (rigidbodyArma.position + direcaoCorrigida);
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacaoDaBala = Quaternion.LookRotation(transform.forward);

        switch (objetoDeColisao.tag)
        {

            case LiteralStrings.Zumbi:
                ControlaZumbi zumbi = objetoDeColisao.GetComponent<ControlaZumbi>();
                zumbi.TomarDano(danoDaBala);
                zumbi.ParticulaSangue(transform.position, rotacaoDaBala);
                break;

            case LiteralStrings.ChefeDeFase:
                ControlaChefeDeFase chefe = objetoDeColisao.GetComponent<ControlaChefeDeFase>();
                chefe.TomarDano(danoDaBala);
                chefe.ParticulaSangue(transform.position, rotacaoDaBala);
                break;
        }

        Destroy (gameObject);
	}
}
