using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.Monetization;

public class Propagandas : MonoBehaviour {

    public string placementId_Completo = "rewardedVideo";
    public string placementId_Skip = "video";
    public string tipoRecompensa;
    string gameId = "3262530";
    bool testMode = false;
    private UiControle controleUi;
    private LojaController controleLoja;
    private void Awake() {
        Monetization.Initialize(gameId, testMode);
    }
    void Start() {
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        controleLoja = GameObject.FindGameObjectWithTag("Loja").GetComponent<LojaController>();
    }
    public void ShowAd_Completo (string tipo) {
        tipoRecompensa = tipo;
        StartCoroutine (WaitForAd ());
    }
    public void ShowAd_Skip () {
        StartCoroutine (ShowAdWhenReady ());
    }
    IEnumerator WaitForAd () {
        while (!Monetization.IsReady (placementId_Completo)) {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (placementId_Completo) as ShowAdPlacementContent;

        if (ad != null) {
            ad.Show (AdFinished);
        }
    }

    void AdFinished (ShowResult result) {
        if (result == ShowResult.Finished) {
            if (tipoRecompensa == "vida")
            {
                controleUi.Continue();
                GameController.CONTROLE_DE_JOGO.continueADS = true;

            }else if(tipoRecompensa == "dinheiro")
            {
                
            }
            
        }
    }

    private IEnumerator ShowAdWhenReady () {
        while (!Monetization.IsReady (placementId_Skip)) {
            yield return new WaitForSeconds(0.25f);
        }
    
        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (placementId_Skip) as ShowAdPlacementContent;

        if(ad != null) {
            ad.Show ();
        }
    }

}