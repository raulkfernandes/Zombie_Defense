using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefeDeFase : MonoBehaviour, IMatavel {

    [SerializeField] [Range(25, 50)] private int danoMin;
    [SerializeField] [Range(50, 100)] private int danoMax;
    [SerializeField] private GameObject kitMedicoPrefab;
    [SerializeField] private GameObject particulaSangueZumbi;
    [SerializeField] private Slider sliderBarraDeVida;
    [SerializeField] private Image sliderImage;
    [SerializeField] private Color corVidaMinima, corVidaMaxima;

    private GameObject jogador;
    private ControlaJogador controlaJogador;
    private ControlaInterface controlaInterface;
    [HideInInspector] public Status StatusChefe;

    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;
    private NavMeshAgent agente;

    private void Start()
    {
        jogador = GameObject.FindWithTag(LiteralStrings.Jogador);
        controlaJogador = jogador.GetComponent<ControlaJogador>();
        controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

        agente = GetComponent<NavMeshAgent>();
        StatusChefe = GetComponent<Status>();
        agente.speed = StatusChefe.Velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();

        sliderBarraDeVida.maxValue = StatusChefe.VidaInicial;
        AtualizarBarraDeVida();
    }

    private void OnCollisionExit(Collision collision)
    {
        //movimentoChefe.TravarPosicao();
        //movimentoChefe.DestravarPosicao();
        movimentoChefe.ZerarVelocidade();
    }

    private void Update()
    {
        agente.SetDestination(jogador.transform.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if(agente.hasPath == true)
        {
            bool estaPertoParaAtacar = agente.remainingDistance <= agente.stoppingDistance;

            if(estaPertoParaAtacar == true)
            {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.transform.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            }
            else
            {
                animacaoChefe.Atacar(false);
            }
        }
    }

    private void AtualizarBarraDeVida()
    {
        sliderBarraDeVida.value = StatusChefe.Vida;
        float porcentagemDaVida = (float)StatusChefe.Vida / StatusChefe.VidaInicial;
        Color corDaVida = Color.Lerp(corVidaMinima, corVidaMaxima, porcentagemDaVida);
        sliderImage.color = corDaVida;
    }

    private void AtacarJogador ()
    {
        int dano = Random.Range(danoMin, danoMax);
        controlaJogador.TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        StatusChefe.Vida -= dano;
        AtualizarBarraDeVida();
        if (StatusChefe.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(particulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        Destroy(gameObject, 2);

        this.enabled = false;
        agente.enabled = false;
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();

        controlaInterface.AtualizaZumbisMortos();
        Instantiate(kitMedicoPrefab, transform.position, Quaternion.identity);
    }
}
