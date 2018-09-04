using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

	[SerializeField] private GameObject zumbiPrefab;
	[SerializeField] private LayerMask mascaraZumbi;

    [HideInInspector] public int TempoAumentarNumeroZumbis = 30;
    private float contadorAumentarDificuldade;
    private float tempoGerarZumbi = 1;
    private int quantidadeMaximaZumbis = 2;
    private int quantidadeAtualZumbis; 

    private GameObject jogador;
	private float contadorTempo;
	[SerializeField] [Range(20, 50)] private float distanciaGerarZumbi = 30;
	private float alcanceDaGeracaoDeZumbi = 3;
	private float alcanceTesteColisao = 1;

	void Start () {

		jogador = GameObject.FindWithTag (LiteralStrings.Jogador);
        contadorAumentarDificuldade = TempoAumentarNumeroZumbis;

        for (int i = 0; i < quantidadeMaximaZumbis; i++) {

            StartCoroutine(GerarNovoZumbi ());
        }
	}

	void Update () {

        bool distanciaSuficiente = Vector3.Distance(transform.position, jogador.transform.position) > distanciaGerarZumbi;

        if (distanciaSuficiente && quantidadeAtualZumbis < quantidadeMaximaZumbis) {
			
			contadorTempo += Time.deltaTime;

			if (contadorTempo >= tempoGerarZumbi) {

				StartCoroutine(GerarNovoZumbi ());
				contadorTempo = 0;
			}
		}

        if (Time.timeSinceLevelLoad > contadorAumentarDificuldade) {

            contadorAumentarDificuldade = Time.timeSinceLevelLoad + TempoAumentarNumeroZumbis;
            quantidadeMaximaZumbis++;
        }
	}

	private IEnumerator GerarNovoZumbi () {

		Vector3 posicaoDeCriacao = RandomizarPosicao ();
		Collider[] zumbisColisores = Physics.OverlapSphere (posicaoDeCriacao, alcanceTesteColisao, mascaraZumbi);

		while (zumbisColisores.Length > 0) {

			posicaoDeCriacao = RandomizarPosicao ();
			zumbisColisores = Physics.OverlapSphere (posicaoDeCriacao, alcanceTesteColisao, mascaraZumbi);
			yield return null;
		}

		ControlaZumbi zumbiCriado = Instantiate (zumbiPrefab, posicaoDeCriacao, transform.rotation)
            .GetComponent<ControlaZumbi>();
        zumbiCriado.geradorZumbis = this;
        quantidadeAtualZumbis++;
	}

    public void AtualizarZumbisMortos() {

        quantidadeAtualZumbis--;
    }

	private Vector3 RandomizarPosicao () {

		Vector3 posicao = Random.insideUnitSphere * alcanceDaGeracaoDeZumbi;
		posicao += transform.position;
		posicao.y = transform.position.y;

		return posicao;
	}

	private void OnDrawGizmos () {

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, alcanceDaGeracaoDeZumbi);
	}
}
