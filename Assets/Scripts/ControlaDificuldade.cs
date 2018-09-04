using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaDificuldade : MonoBehaviour {
    
    private GeradorZumbis[] geradorZumbis;
    private GeradorChefeDeFase geradorChefeDeFase;
    [SerializeField] private GameObject zumbi;
    [SerializeField] private GameObject chefeDeFase;
    
    private int nivelDeDificuldade = 0;
    private readonly int dificuldade = 10;

    private float velocidadeZumbi, velocidadeChefeDeFase;
    private int tempoAumentarNumeroDeZumbis, tempoGerarChefeDeFase;

    void Awake ()
    {
        geradorChefeDeFase = GameObject.FindObjectOfType(typeof(GeradorChefeDeFase)) as GeradorChefeDeFase;
        geradorZumbis = GameObject.FindObjectsOfType(typeof(GeradorZumbis)) as GeradorZumbis[];

        velocidadeZumbi = zumbi.GetComponent<Status>().Velocidade;
        velocidadeChefeDeFase = chefeDeFase.GetComponent<Status>().Velocidade;
        tempoGerarChefeDeFase = geradorChefeDeFase.TempoGerarChefeDeFase;
        tempoAumentarNumeroDeZumbis = geradorZumbis[0].TempoAumentarNumeroZumbis;

        nivelDeDificuldade = PlayerPrefs.GetInt(LiteralStrings.NivelDeDificuldade);
        DefinirDificuldade(nivelDeDificuldade);
    }

    private void OnApplicationQuit()
    {
        DefinirValoresIniciais();
    }

    private void DefinirDificuldade (int nivelDeDificuldade)
    {
        zumbi.GetComponent<Status>().Velocidade = velocidadeZumbi + nivelDeDificuldade;
        chefeDeFase.GetComponent<Status>().Velocidade = velocidadeChefeDeFase + nivelDeDificuldade;
        geradorChefeDeFase.TempoGerarChefeDeFase -= (nivelDeDificuldade * dificuldade);
        foreach (GeradorZumbis gerador in geradorZumbis)
        {
            gerador.TempoAumentarNumeroZumbis -= (nivelDeDificuldade * dificuldade);
        }
    }

    public void DefinirValoresIniciais ()
    {
        zumbi.GetComponent<Status>().Velocidade = velocidadeZumbi;
        chefeDeFase.GetComponent<Status>().Velocidade = velocidadeChefeDeFase;
        geradorChefeDeFase.TempoGerarChefeDeFase = tempoGerarChefeDeFase;
        foreach (GeradorZumbis gerador in geradorZumbis)
        {
            gerador.TempoAumentarNumeroZumbis = tempoAumentarNumeroDeZumbis;
        }
    }
}