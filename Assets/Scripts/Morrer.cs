using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morrer : MonoBehaviour
{
    private UiControle controleUi;
    private PropagandaFree controlePropagandas;
    void Start()
    {
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        controlePropagandas = GameObject.FindGameObjectWithTag("Propaganda").GetComponent<PropagandaFree>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Morte(){
        GameController.CONTROLE_DE_JOGO.jogoOn = false;
        // Descer tela de menu game over
        controleUi.GameOver();
        // Parar jogo
        int random = Random.Range(0, 11);
        if (random % 3 == 0)
        {
            controlePropagandas.ShowAd();    
        }
        
    }
}
