using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morrer : MonoBehaviour
{
    private UiControle controleUi;
    private Propagandas propagandasControle;

    void Start()
    {
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        propagandasControle = GameObject.FindGameObjectWithTag("Propaganda").GetComponent<Propagandas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Morte(){
        GameController.CONTROLE_DE_JOGO.jogoOn = false;
        GameController.CONTROLE_DE_JOGO.contagemMortes ++;
        PlayServices.PosScore((long) GameController.CONTROLE_DE_JOGO.pontos, NinjaChallengeServices.leaderboard_ranking);
        if (GameController.CONTROLE_DE_JOGO.contagemMortes == 3){
            if (GameController.CONTROLE_DE_JOGO.propagandasAtivadas)
            {
                GameController.CONTROLE_DE_JOGO.contagemMortes = 0;
                propagandasControle.ShowAd_Skip();    
            }
        }
        
        controleUi.GameOver();
    }
}
