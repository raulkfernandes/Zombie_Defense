using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {

	[SerializeField] private GameObject balaPrefab;
	[SerializeField] private GameObject canoDaArma;
	[SerializeField] private AudioClip somDeTiro;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown (LiteralStrings.Fire1)) {
            
			Instantiate (balaPrefab, canoDaArma.transform.position, canoDaArma.transform.rotation);
            if(Time.timeScale != 0)
            {
                ControlaAudio.instancia.PlayOneShot(somDeTiro);
            }
		}
	}
}
