using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;


public class Propagandas : MonoBehaviour {

	string gameId = "3249746";
    bool testMode = false;
    public string placementId = "rewardedVideo";
    private UiControle controleUi;
    private LojaController controleLoja;
    private string tipoRecompensa;

    void Start() {

        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        controleLoja = GameObject.FindGameObjectWithTag("Loja").GetComponent<LojaController>();
    }

    private void Awake(){
        Monetization.Initialize(gameId, testMode);
    }
    //public string placementId = "rewardedVideo";
    public void GanharDinheiro(string tipo){
        
        tipoRecompensa = tipo;
        ShowAd();
    }

    public void ShowAd () {
        StartCoroutine (WaitForAd ());
    }

    IEnumerator WaitForAd () {
        while (!Monetization.IsReady (placementId)) {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (placementId) as ShowAdPlacementContent;


        if (ad != null) {
            ad.Show (AdFinished);
        }

    }

    void AdFinished (ShowResult result) {

        
        
        if (result == ShowResult.Finished) {

            if (tipoRecompensa == "vida")
            {
                // chamar player com continue
                controleUi.Continue();
                GameController.CONTROLE_DE_JOGO.continueADS = true;
            }else if(tipoRecompensa == "dinheiro")
            {
                // Reward the player
                // DAR PREMIO
                //GameController.CONTROLE_DE_JOGO.moedas += 10;
                //controleUi.AtualizarMoedas();
                //controleUi.AtualizarMoedasLoja();
                //controleLoja.VerificarPoderCompraUpgrade();
                //controleLoja.VerificarPoderDeCompraNaruto();    
            }
            
        }
    }

    
}
