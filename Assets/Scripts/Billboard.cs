using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour {

	void Update () {

        // Faz o canvas sempre olhar para a câmera:
        // (OBS: normalmente é feita uma subtração, nesse caso o sprite tem os dois lados visíveis)
        transform.LookAt(transform.position + Camera.main.transform.forward);
	}
}
