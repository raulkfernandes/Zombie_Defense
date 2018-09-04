using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaMenu : MonoBehaviour {

    [SerializeField] private GameObject botaoSair;
    [SerializeField] private GameObject botaoFacil;
    [SerializeField] private GameObject botaoMedio;
    [SerializeField] private GameObject botaoDificil;
    [SerializeField] private Color corVerde;
    [SerializeField] private Color corVermelha;

    private float tempoDeEsperaBotoes = 0.1f;

    private void Start()
    {
        // Começar o Menu com dificuldade igual a selecionada anteriormente:
        StartCoroutine(SelecionarNivel(0, PlayerPrefs.GetInt(LiteralStrings.NivelDeDificuldade)));

        Time.timeScale = 1;

        #if UNITY_STANDALONE || UNITY_EDITOR
                botaoSair.SetActive(true);
        #endif
    }

    public void IniciarJogo ()
    {
        StartCoroutine(MudarCena(LiteralStrings.MainScene));
    }

    IEnumerator MudarCena (string momeDaCena)
    {
        yield return new WaitForSecondsRealtime(tempoDeEsperaBotoes);
        SceneManager.LoadScene(momeDaCena);
    }

    public void SairDoJogo ()
    {
        StartCoroutine(Sair());
    }

    IEnumerator Sair ()
    {
        yield return new WaitForSecondsRealtime(tempoDeEsperaBotoes);
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void JogarFacil()
    {
        StartCoroutine(SelecionarNivel(tempoDeEsperaBotoes, 0));
    }

    public void JogarMedio()
    {
        StartCoroutine(SelecionarNivel(tempoDeEsperaBotoes, 1));
    }

    public void JogarDificil()
    {
        StartCoroutine(SelecionarNivel(tempoDeEsperaBotoes, 2));
    }

    IEnumerator SelecionarNivel (float tempoDeEspera, int nivelDeDificuldade)
    {
        yield return new WaitForSeconds(tempoDeEspera);

        PlayerPrefs.SetInt(LiteralStrings.NivelDeDificuldade, nivelDeDificuldade);

        switch (nivelDeDificuldade)
        {
            case 0:
                botaoFacil.GetComponent<Image>().color = corVermelha;
                botaoMedio.GetComponent<Image>().color = corVerde;
                botaoDificil.GetComponent<Image>().color = corVerde;
                break;

            case 1:
                botaoFacil.GetComponent<Image>().color = corVerde;
                botaoMedio.GetComponent<Image>().color = corVermelha;
                botaoDificil.GetComponent<Image>().color = corVerde;
                break;

            case 2:
                botaoFacil.GetComponent<Image>().color = corVerde;
                botaoMedio.GetComponent<Image>().color = corVerde;
                botaoDificil.GetComponent<Image>().color = corVermelha;
                break;
        }
    }
}
