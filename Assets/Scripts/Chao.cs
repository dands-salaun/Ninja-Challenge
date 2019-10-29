using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Bateu");
        if (other.gameObject.tag == "Estrela")
        {
            Debug.Log("Col");
            GameController.CONTROLE_DE_JOGO.Pontuar();
            other.gameObject.GetComponent<Estrela>().controleUi.AtualizarPontos();
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<Estrela>().caindo = false;
            other.gameObject.GetComponent<Estrela>().myAmimator.SetBool("Caindo", false);
            other.gameObject.GetComponent<Estrela>().GerarMoeda();
            other.gameObject.GetComponent<Estrela>().rbEspinho.gravityScale = other.gameObject.GetComponent<Estrela>().gravidadeInicial;
            other.gameObject.GetComponent<Estrela>().Voltar();
        }
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Encostou");
        if (other.gameObject.tag == "Estrela")
        {
            Debug.Log("Trig");
            GameController.CONTROLE_DE_JOGO.Pontuar();
            other.gameObject.GetComponent<Estrela>().controleUi.AtualizarPontos();
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<Estrela>().caindo = false;
            other.gameObject.GetComponent<Estrela>().myAmimator.SetBool("Caindo", false);
            other.gameObject.GetComponent<Estrela>().GerarMoeda();
            other.gameObject.GetComponent<Estrela>().rbEspinho.gravityScale = other.gameObject.GetComponent<Estrela>().gravidadeInicial;
            other.gameObject.GetComponent<Estrela>().Voltar();
        }
    }
}
