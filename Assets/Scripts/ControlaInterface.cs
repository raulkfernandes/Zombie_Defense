using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {

	[SerializeField] private Slider sliderVidaJogador;
	[SerializeField] private GameObject painelGameOver;
	[SerializeField] private Text textoGameOver;
    [SerializeField] private Text textoTempoMaximo;
    [SerializeField] private Text textoZumbisMortos;
    [SerializeField] private Text textoChefeCriado;
    private ControlaJogador controlaJogador;
	private float tempoMaximo;
    private int quantidadeZumbisMortos;

    // IEnumerator
    private float tempoEsperaSumirTexto = 1;
    private float tempoEsperaBotoes = 0.1f;

    // Use this for initialization
    void Start () {

		Time.timeScale = 1;
		controlaJogador = GameObject.FindWithTag (LiteralStrings.Jogador).GetComponent<ControlaJogador> ();

		sliderVidaJogador.maxValue = controlaJogador.StatusJogador.Vida;
		AtualizarSliderVidaJogador ();
		tempoMaximo = PlayerPrefs.GetFloat (LiteralStrings.TempoMaximoSalvo);
	}

	public void AtualizarSliderVidaJogador () {

		sliderVidaJogador.value = controlaJogador.StatusJogador.Vida;
	}

    public void AtualizaZumbisMortos () {

        quantidadeZumbisMortos++;
        textoZumbisMortos.text = string.Format(LiteralStrings.ZumbisMortos, quantidadeZumbisMortos);
    }

	public void GameOver () {

        Time.timeScale = 0;
		painelGameOver.SetActive (true);

		int minutos = (int) Time.timeSinceLevelLoad / 60;
		int segundos = (int) Time.timeSinceLevelLoad % 60;

		textoGameOver.text = string.Format (LiteralStrings.GameOver, minutos, segundos);
		AjustarTempoMaximo (minutos, segundos);
	}

	private void AjustarTempoMaximo (int min, int seg) {

		if (Time.timeSinceLevelLoad > tempoMaximo) {

			tempoMaximo = Time.timeSinceLevelLoad;
			textoTempoMaximo.text = string.Format (LiteralStrings.TempoMaximo, min, seg);
			PlayerPrefs.SetFloat (LiteralStrings.TempoMaximoSalvo, tempoMaximo);
		}

		if (textoTempoMaximo.text == "") {

			min = (int) tempoMaximo / 60;
			seg = (int) tempoMaximo % 60;
			textoTempoMaximo.text = string.Format (LiteralStrings.TempoMaximo, min, seg);
		}
	}

	public void Reiniciar ()
    {
        StartCoroutine(MudarCena(LiteralStrings.MainScene));
    }

    public void VoltarAoMenu ()
    {
        StartCoroutine(MudarCena(LiteralStrings.MenuScene));
    }

    IEnumerator MudarCena(string momeDaCena)
    {
        yield return new WaitForSecondsRealtime(tempoEsperaBotoes);
        SceneManager.LoadScene(momeDaCena);
    }


    public void MostrarTextoChefeCriado ()
    {
        StartCoroutine(SumirTexto(2, textoChefeCriado));
    }

    IEnumerator SumirTexto (float tempoParaSumir, Text textoParaSumir)
    {
        // Ativa o texto, garante que a transparencia é 100% e que a cor será renovada:
        textoParaSumir.gameObject.SetActive(true);
        Color corDoTexto = textoParaSumir.color;
        corDoTexto.a = 1;                               // Vai de 0 a 1 como uma porcentagem.
        textoParaSumir.color = corDoTexto;

        // Mantem o texto na tela pela quantidade de segundos da variável:
        yield return new WaitForSeconds(tempoEsperaSumirTexto);

        // Altera o 'alpha' da cor do texto de 100% a 0% de acordo com o tempoParaSumir passado:
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += (Time.deltaTime / tempoParaSumir);
            corDoTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corDoTexto;

            if (textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }

            yield return null;                          // Renova o loop a cada frame, evitande deadlock.
        }
    }
}
