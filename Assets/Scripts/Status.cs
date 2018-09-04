using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    [Range(1, 15)] public float Velocidade;
    [Range (100, 1000)] public int VidaInicial;
	[HideInInspector] public int Vida;

	void Awake () {

		Vida = VidaInicial;
	}

}
