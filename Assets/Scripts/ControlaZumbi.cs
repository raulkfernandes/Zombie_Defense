using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel {

	[SerializeField] private AudioClip somDeMorte;
    [SerializeField] private GameObject kitMedicoPrefab;
    [SerializeField] private GameObject particulaSangueZumbi;
    [SerializeField] [Range(10, 25)] private int danoMin;
	[SerializeField] [Range(25, 40)] private int danoMax;

	// Direção e alcance:
	private Vector3 direcao;
	private float alcanceDoZumbi = 2.75f;
	private float alcanceDoGerador = 15;

	// Para vagar Aleatoriamente:
	private Vector3 posicaoAleatoria;
	private float alcancePosicaoAleatoria = 10;
	private float intervaloPosicaoAleatoria = 4;
	private float contadorPosicaoAleatoria;

	private GameObject jogador;
	private ControlaJogador controlaJogador;
    private ControlaInterface controlaInterface;
    [HideInInspector] public Status StatusZumbi;

    private MovimentoPersonagem movimentoZumbi;
	private AnimacaoPersonagem animacaoZumbi;
    private float porcentagemKitMedico = 0.1f;
    [HideInInspector] public GeradorZumbis geradorZumbis;

    // Use this for initialization
    void Start ()
    {
        jogador = GameObject.FindWithTag (LiteralStrings.Jogador);
		controlaJogador = jogador.GetComponent<ControlaJogador> ();
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

        RandomizarZumbi();

		movimentoZumbi = GetComponent<MovimentoPersonagem> ();
		animacaoZumbi = GetComponent<AnimacaoPersonagem> ();
		StatusZumbi = GetComponent<Status> ();
	}

    private void OnCollisionExit(Collision collision)
    {
        //movimentoZumbi.TravarPosicao();
        //movimentoZumbi.DestravarPosicao();
        movimentoZumbi.ZerarVelocidade();
    }

    void FixedUpdate () {

		movimentoZumbi.Rotacionar (direcao);
		animacaoZumbi.Movimentar (direcao.magnitude);

		float distancia = Vector3.Distance (transform.position, jogador.transform.position);

		if (distancia > alcanceDoGerador) {

			Vagar ();

		} else if (distancia > alcanceDoZumbi) {
            
            PerseguirJogador ();

		} else {
            
            movimentoZumbi.TravarPosicao();
            direcao = jogador.transform.position - transform.position;
			animacaoZumbi.Atacar (true);
		}
	}

	private void PerseguirJogador () {

        movimentoZumbi.DestravarPosicao();
        direcao = jogador.transform.position - transform.position;
        movimentoZumbi.Movimentar (direcao.normalized, StatusZumbi.Velocidade);
        animacaoZumbi.Atacar (false);
	}

	private void Vagar () {

		contadorPosicaoAleatoria -= Time.deltaTime;

		if (contadorPosicaoAleatoria <= 0) {

			posicaoAleatoria = RandomizarPosicao ();
			contadorPosicaoAleatoria += intervaloPosicaoAleatoria + Random.Range(-1f, 1f);
		}

		bool pertoDoDestino = Vector3.Distance (transform.position, posicaoAleatoria) <= 0.5;
		if (!pertoDoDestino) {

            direcao = posicaoAleatoria - transform.position;
            movimentoZumbi.Movimentar(direcao.normalized, StatusZumbi.Velocidade);
            animacaoZumbi.Atacar (false);
		}
        else
        {
            animacaoZumbi.Movimentar(0);
            animacaoZumbi.Atacar(false);
            movimentoZumbi.Movimentar(Vector3.zero, StatusZumbi.Velocidade);
            //movimentoZumbi.ZerarVelocidade();
            //movimentoZumbi.Rotacionar(Vector3.zero);
        }
    }

	private Vector3 RandomizarPosicao () {

		Vector3 posicao = Random.insideUnitSphere * alcancePosicaoAleatoria;
		posicao += transform.position;
		posicao.y = transform.position.y;

		return posicao;
	}

	private void RandomizarZumbi () {

		int zumbiAleatorio = Random.Range (1, transform.childCount);
		transform.GetChild (zumbiAleatorio).gameObject.SetActive (true);
	}

	private void AtacarJogador () {

		int dano = Random.Range (danoMin, danoMax);
		controlaJogador.TomarDano (dano);
	}

	public void TomarDano (int dano) {

		StatusZumbi.Vida -= dano;

		if (StatusZumbi.Vida <= 0) {
			
			Morrer ();
		}
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(particulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer () {

		Destroy (gameObject, 2);
        ControlaAudio.instancia.PlayOneShot(somDeMorte);

        this.enabled = false;
        animacaoZumbi.Morrer();
        movimentoZumbi.Morrer();

        controlaInterface.AtualizaZumbisMortos();
        GerarKitMedico(porcentagemKitMedico);
        geradorZumbis.AtualizarZumbisMortos();
	}

    private void GerarKitMedico (float porcentagemKitMedico) {

        if(Random.value <= porcentagemKitMedico)
        {
            Instantiate(kitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
