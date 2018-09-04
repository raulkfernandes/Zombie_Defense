using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour {

	private Animator animatorPersonagem;

	void Awake () {

		animatorPersonagem = GetComponent<Animator> ();
	}

	public void Atacar (bool atacando) {

		animatorPersonagem.SetBool (LiteralStrings.Atacando, atacando);
	}

	public void Movimentar (float valorDeMovimento) {

		animatorPersonagem.SetFloat (LiteralStrings.Movendo, valorDeMovimento);
	}

    public void Morrer () {

        animatorPersonagem.SetTrigger (LiteralStrings.Morrendo);
    }
}
