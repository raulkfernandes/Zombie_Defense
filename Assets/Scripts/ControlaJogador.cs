using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel {

    // [SerializeField] private LayerMask mascaraChao;  (Usava para o RaycastHit de 'MovimentoJogador' bater no chão)
    private ControlaInterface controlaInterface;
    [SerializeField] private AudioClip somDeDano;
	private Vector3 direcao;

    private MovimentoJogador movimentoJogador;
	private AnimacaoPersonagem animacaoJogador;
	[HideInInspector] public Status StatusJogador;

	void Start () {
        
		movimentoJogador = GetComponent<MovimentoJogador> ();
		animacaoJogador = GetComponent<AnimacaoPersonagem> ();
		StatusJogador = GetComponent<Status> ();
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Parou de Colidir");
        //movimentoJogador.TravarPosicao();
        //movimentoJogador.DestravarPosicao();
        movimentoJogador.ZerarVelocidade();
    }

    // Update is called once per frame
    void Update () {

		// Guardando as teclas apertadas:
		float eixoX = Input.GetAxis (LiteralStrings.Horizontal);
		float eixoZ = Input.GetAxis (LiteralStrings.Vertical);

        Vector3 direcaoInput = new Vector3 (eixoX, 0, eixoZ);
        direcao = Vector3.ClampMagnitude(direcaoInput, 1);

		// Animação de movimento do Jogador (OBS: '.magnitude' para passar o valor 'float' referente ao tamanho do vetor)
		animacaoJogador.Movimentar (direcao.magnitude);
	}

    // Update utilizado para cálculos envolvendo física "Rigidbody"
    void FixedUpdate () {

		movimentoJogador.Movimentar (direcao, StatusJogador.Velocidade);
		movimentoJogador.RotacionarJogador ();
	}

	public void TomarDano (int dano) {

		StatusJogador.Vida -= dano;
		controlaInterface.AtualizarSliderVidaJogador();
		ControlaAudio.instancia.PlayOneShot (somDeDano);

		if (StatusJogador.Vida <= 0) {

			Morrer ();
		}
	}

	public void Morrer () {

		controlaInterface.GameOver ();
	}

    public void CurarVida (int quantidadeDeVida) {

        StatusJogador.Vida += quantidadeDeVida;
        if (StatusJogador.Vida > StatusJogador.VidaInicial) {

            StatusJogador.Vida = StatusJogador.VidaInicial;
        }
        controlaInterface.AtualizarSliderVidaJogador();
    }
}
