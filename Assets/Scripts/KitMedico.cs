using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour {

    [SerializeField] private AudioClip somKitMedico;
    private int quantidadeDeCura = 15;
    private int tempoAutoDestruir = 5;

    private void Start()
    {
        Destroy(gameObject, tempoAutoDestruir);
    }

    private void OnTriggerEnter(Collider objetoDeColisao) {

        if (objetoDeColisao.tag == LiteralStrings.Jogador) {

            ControlaAudio.instancia.PlayOneShot(somKitMedico);
            objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
        
    }
}
