using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefeDeFase : MonoBehaviour {

    [Range(1, 60)] public int TempoGerarChefeDeFase = 30;
    [SerializeField] private GameObject jogador;
    [SerializeField] private GameObject chefeDeFasePrefab;
    [SerializeField] private Transform[] geradoresDeChefe;
    private ControlaInterface controlaInterface;
    private float contadorGerarChefe;
    private float alcanceDaGeracaoDeChefe = 4;

    private void Start()
    {
        contadorGerarChefe = TempoGerarChefeDeFase;
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > contadorGerarChefe)
        {
            Vector3 geradorMaisDistante = CalcularGeradorMaisDistante();
            Instantiate(chefeDeFasePrefab, geradorMaisDistante, Quaternion.identity);
            controlaInterface.MostrarTextoChefeCriado();
            contadorGerarChefe = Time.timeSinceLevelLoad + TempoGerarChefeDeFase;
        }
    }

    public Vector3 CalcularGeradorMaisDistante()
    {
        Vector3 posicaoMaisDistante = Vector3.zero;
        float maiorDistancia = 0;

        foreach (Transform gerador in geradoresDeChefe)
        {
            float distanciaDoJogador = Vector3.Distance(gerador.position, jogador.transform.position);
            if (distanciaDoJogador > maiorDistancia)
            {
                maiorDistancia = distanciaDoJogador;
                posicaoMaisDistante = gerador.position;
            }
        }

        return posicaoMaisDistante;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform gerador in geradoresDeChefe)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gerador.position, alcanceDaGeracaoDeChefe);
        }
    }
}
