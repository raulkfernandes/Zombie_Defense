using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {

	private Rigidbody rigidbodyPersonagem;
    private RigidbodyConstraints constraintsPersonagem;

	void Awake () {

		rigidbodyPersonagem = GetComponent<Rigidbody> ();
        constraintsPersonagem = rigidbodyPersonagem.constraints;
	}

	public void Movimentar (Vector3 direcao, float velocidade) {
        
            Vector3 direcaoCorrigida = direcao * velocidade * Time.deltaTime;
            rigidbodyPersonagem.MovePosition(rigidbodyPersonagem.position + direcaoCorrigida);
	}

	public void Rotacionar (Vector3 direcao) {

		Quaternion novaRotacao = Quaternion.LookRotation (direcao);
		rigidbodyPersonagem.MoveRotation (novaRotacao);
	}

    public void Morrer () {

        rigidbodyPersonagem.constraints = RigidbodyConstraints.FreezeAll;
        rigidbodyPersonagem.isKinematic = false;
        rigidbodyPersonagem.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }

    public void TravarPosicao ()
    {   
        rigidbodyPersonagem.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void DestravarPosicao ()
    {
        rigidbodyPersonagem.constraints = constraintsPersonagem;
    }

    public void ZerarVelocidade ()
    {
        rigidbodyPersonagem.velocity = Vector3.zero;
    }
}
