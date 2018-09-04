using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem {

	private int comprimentoDoRaio = 100;
    [SerializeField] private GameObject alturaDaBala;
    
    //  [HideInInspector] public Vector3 trajetoriaDoTiro;  (Usava no método antigo, para pegar o impacto.point no chão)

    public void RotacionarJogador () {

		Ray raio = Camera.main.ScreenPointToRay (Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * comprimentoDoRaio, Color.red);

        Plane planoDeMira = new Plane(Vector3.up, alturaDaBala.transform.position);

        float distanciaDeColisao;

        if(planoDeMira.Raycast(raio, out distanciaDeColisao))
        {
            Vector3 localDeColisao = raio.GetPoint(distanciaDeColisao);

            Vector3 posicaoParaOlhar = localDeColisao - transform.position;
            posicaoParaOlhar.y = transform.position.y;
            Rotacionar(posicaoParaOlhar);
        }
    }

    /*
    public void RotacionarJogador(LayerMask mascaraChao)
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(raio.origin, raio.direction * comprimentoDoRaio, Color.red);

        // Ponto onde o raio colide com algo (Sem valor atribuído, logo requer uso do 'out' na chamada)
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, comprimentoDoRaio, mascaraChao))
        {

            trajetoriaDoTiro = impacto.point;
            //Debug.Log(trajetoriaDoTiro.y);

            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;
            Rotacionar(posicaoMiraJogador);
        }
    }*/
}
