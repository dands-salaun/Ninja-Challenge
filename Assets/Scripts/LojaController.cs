using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LojaController : MonoBehaviour
{
    [Header("Valores")]
    public int precoNaruto;
    public int precoCincoSeg;
    public int precoSeteSeg;
    public int precoDezSeg;
    [Header("Skins")]
    public GameObject narutoBotaoComprar;
    public GameObject imagemNarutoValor;
    public GameObject imagemNarutoComprado;
    public Text ninjaTexto;
    public Text narutoTexto;
    public Text narutoValorTexto;
    [Header("Upgrade")]
    public GameObject cincoSecBt;
    public GameObject seteSecBt;
    public GameObject dezSecBt;

    [Header("Controles")]
    private UiControle controleUi;

    
    void Start()
    {
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        VerificarPoderDeCompraNaruto();
        VerificarPoderCompraUpgrade();
        narutoValorTexto.text = precoNaruto.ToString();
        TrocarTexto();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VerificarPoderDeCompraNaruto(){
        
        if (GameController.CONTROLE_DE_JOGO.skinsCompradas < 1)
        {
            if(GameController.CONTROLE_DE_JOGO.moedas >= precoNaruto){
                narutoBotaoComprar.SetActive(true);
            }    
        }else
        {
            imagemNarutoComprado.SetActive(true);
            imagemNarutoValor.SetActive(false);
            narutoTexto.text = "Choice";
        }
    }
    public void NarutoComprar(){
        GameController.CONTROLE_DE_JOGO.moedas -= precoNaruto;
        controleUi.AtualizarMoedasLoja();
        GameController.CONTROLE_DE_JOGO.skinsCompradas = 1;
        GameController.CONTROLE_DE_JOGO.SalvarDados();
        narutoBotaoComprar.SetActive(false);
        imagemNarutoComprado.SetActive(true);
        imagemNarutoValor.SetActive(false);
        narutoTexto.text = "Choice";
        
    }

    public void TrocarTexto(){
        if (GameController.CONTROLE_DE_JOGO.skinAtual == 0)
        {
            ninjaTexto.text = "Current";
            narutoTexto.text = "Choice";
        }else if(GameController.CONTROLE_DE_JOGO.skinAtual == 1)
        {
            ninjaTexto.text = "Choice";
            narutoTexto.text = "Current";
        }
    }

    public void VerificarPoderCompraUpgrade(){
        if(GameController.CONTROLE_DE_JOGO.tempoRelogio < 5){
            cincoSecBt.SetActive(true);
            seteSecBt.SetActive(false);
            dezSecBt.SetActive(false);
            if (GameController.CONTROLE_DE_JOGO.moedas >= precoCincoSeg)
            {
                cincoSecBt.GetComponent<Button>().enabled = true;
            }else
            {
                cincoSecBt.GetComponent<Button>().enabled = false;
            }
        }else if(GameController.CONTROLE_DE_JOGO.tempoRelogio < 7){
            cincoSecBt.SetActive(false);
            seteSecBt.SetActive(true);
            dezSecBt.SetActive(false);
            if (GameController.CONTROLE_DE_JOGO.moedas >= precoSeteSeg)
            {
                seteSecBt.GetComponent<Button>().enabled = true;
            }else
            {
                seteSecBt.GetComponent<Button>().enabled = false;
            }

        }else if(GameController.CONTROLE_DE_JOGO.tempoRelogio < 10){
            cincoSecBt.SetActive(false);
            seteSecBt.SetActive(false);
            dezSecBt.SetActive(true);
            if (GameController.CONTROLE_DE_JOGO.moedas >= precoDezSeg)
            {
                dezSecBt.GetComponent<Button>().enabled = true;
            }else
            {
                dezSecBt.GetComponent<Button>().enabled = false;
            }
        }else
        {
            cincoSecBt.SetActive(false);
            seteSecBt.SetActive(false);
            dezSecBt.SetActive(false);
        }
    }

    public void ComprarUpgrade(int indexSec){
        if(indexSec == 5){
            GameController.CONTROLE_DE_JOGO.moedas -= precoCincoSeg;
            GameController.CONTROLE_DE_JOGO.tempoRelogio = 5;
            GameController.CONTROLE_DE_JOGO.SalvarDados();
        }else if(indexSec == 7){
            GameController.CONTROLE_DE_JOGO.moedas -= precoSeteSeg;
            GameController.CONTROLE_DE_JOGO.tempoRelogio = 7;
            GameController.CONTROLE_DE_JOGO.SalvarDados();
        }else if(indexSec == 10){
            GameController.CONTROLE_DE_JOGO.moedas -= precoDezSeg;
            GameController.CONTROLE_DE_JOGO.tempoRelogio = 10;
            GameController.CONTROLE_DE_JOGO.SalvarDados();
        }
        controleUi.AtualizarMoedasLoja();
        VerificarPoderCompraUpgrade();
        VerificarPoderDeCompraNaruto();
    }
}
