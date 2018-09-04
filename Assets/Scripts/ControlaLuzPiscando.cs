using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaLuzPiscando : MonoBehaviour {
    
	private float limite;
	private float contadorTempo;
	private float intensidade;
	private bool pisca;

	void Awake () {

		intensidade = GetComponent<Light> ().intensity;
	}

	void Update () {

		contadorTempo += Time.deltaTime;

		if (contadorTempo >= limite) {

			Piscar ();
			contadorTempo = 0;
		}
	}

	void Piscar () {

		if (pisca)
			gameObject.GetComponent<Light> ().intensity = intensidade;
		else
			gameObject.GetComponent<Light> ().intensity = 0;

		pisca = !pisca;
		limite = Random.value;
	}
}