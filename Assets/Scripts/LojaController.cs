using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LojaController : MonoBehaviour
{
    private UiControle controleUi;
    public Text dinheiroTexto;
    //public int dinheiro;
    public Text valorSkinTxt;
    public GameObject campoValor;
    public List<Skin> listaSkins;
    public SkinsManager skinsManager;
    public GameObject botaoComprar;
    public GameObject botaoSelecionar;
    public Image imagemItem;
    private int indice;
    private int skinSelecionada;
    public GameObject selecionada;
    private Player player;
    [Header("Itens")]
    public Text mensagem;

    [Header("Ima")]
    public int valorIma;
    public Text valorImaTxt;
    [Header("Relogio")]
    public int valorRelogio;
    public Text valorRelogioTxt;
    [Header("Escudo")]
    public int valorEscudo;
    public Text valorEscudoTxt;

    [Header("Moedas")]
    public int moedas200;
    public int moedas500;
    public int moedas800;
    public int moedas1200;

    void Start()
    {
        skinsManager = GameObject.FindGameObjectWithTag("SkinsManager").GetComponent<SkinsManager>(); 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        listaSkins = skinsManager.listaSkinsArquivo;
        indice = 0;

        AtualizarImagem();
		VerificarPoderDeCompra();

        valorEscudoTxt.text = valorEscudo.ToString();
        valorImaTxt.text = valorIma.ToString();
        valorRelogioTxt.text = valorRelogio.ToString();
    }

    public void PassarItem(){

		int aux = indice + 1;
		if (aux >= listaSkins.Count)
		{
			indice = 0;
		}else{
			indice = aux;
		}

		AtualizarImagem();
	}
	
	public void VoltarItem(){

		int aux = indice - 1;
		if (aux < 0)
		{
			indice = listaSkins.Count - 1;
		}else{
			indice = aux;
		}

		AtualizarImagem();
	}
	void AtualizarImagem(){

		imagemItem.sprite = listaSkins[indice].imagemSkin;
        valorSkinTxt.text = listaSkins[indice].informacao.valorSkin.ToString();

        if (indice == GameController.CONTROLE_DE_JOGO.skinAtual)
        {
            selecionada.SetActive(true);
        }else
        {
            selecionada.SetActive(false);
        }
		VerificarPoderDeCompra();
	}
    
    public void VerificarPoderDeCompra(){

        if (!listaSkins[indice].informacao.adquirida) // Se não tiver sido adquirida
        {
            campoValor.SetActive(true);

            if (GameController.CONTROLE_DE_JOGO.moedas < listaSkins[indice].informacao.valorSkin)
		    {
			    botaoComprar.SetActive(false);
                botaoSelecionar.SetActive(false);
            }else{
    
                botaoComprar.SetActive(true);
                botaoSelecionar.SetActive(false);
            }    
        }else // Adiquirida
        {   
            // Desativar campo valor
            campoValor.SetActive(false);

            if (GameController.CONTROLE_DE_JOGO.skinAtual == indice)
            {
                botaoComprar.SetActive(false);
                botaoSelecionar.SetActive(false);    
            }else
            {
                botaoComprar.SetActive(false);
                botaoSelecionar.SetActive(true);
            }
            
                
        }
	}

    public void Comprar(){
		int valor = listaSkins[indice].informacao.valorSkin;
		GameController.CONTROLE_DE_JOGO.moedas -= valor;
        GameController.CONTROLE_DE_JOGO.SalvarDados();
        listaSkins[indice].informacao.adquirida = true;
        skinsManager.listaSkins = listaSkins;
        skinsManager.SalvarDados();
		VerificarPoderDeCompra();
        //AtualisarDinheiro();
        controleUi.AtualizarMoedas();

    }

    public void SelecionarSkin(){

        skinSelecionada = indice;
        GameController.CONTROLE_DE_JOGO.skinAtual = skinSelecionada;
        GameController.CONTROLE_DE_JOGO.SalvarDados();
        player.SelecionarPersonagem();
        AtualizarImagem();

    }
    public void AtualisarDinheiro(){
        //dinheiroTexto.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
        controleUi.AtualizarMoedas();   
    }

    public void MensagemDinheiro(){
        mensagem.text = "Insufficient money";
        StartCoroutine("MostrarMensagem");
    }
    public void MensagemItemComprado(){

        mensagem.text = "Purchased item";
        StartCoroutine("MostrarMensagem");
    }

    public void MensagemItemExistente(){
        mensagem.text = "you already have this item";
        StartCoroutine("MostrarMensagem");
    }

    IEnumerator MostrarMensagem(){
        mensagem.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        mensagem.gameObject.SetActive(false);
    }

    public void ComprarIma(){
        if (!GameController.CONTROLE_DE_JOGO.ima)
        {
            if (GameController.CONTROLE_DE_JOGO.moedas >= valorIma)
            {
                GameController.CONTROLE_DE_JOGO.ima = true;
                GameController.CONTROLE_DE_JOGO.moedas -= valorIma;
                GameController.CONTROLE_DE_JOGO.SalvarDados();
                //AtualisarDinheiro();
                controleUi.AtualizarMoedas();
                MensagemItemComprado();
            }else{
                MensagemDinheiro();
            }
        }else{
            MensagemItemExistente();
        }
    }

    public void ComprarRelogio(){
        if (!GameController.CONTROLE_DE_JOGO.relogio)
        {
            if (GameController.CONTROLE_DE_JOGO.moedas >= valorRelogio)
            {
                GameController.CONTROLE_DE_JOGO.relogio = true;
                GameController.CONTROLE_DE_JOGO.moedas -= valorRelogio;
                GameController.CONTROLE_DE_JOGO.SalvarDados();
                //AtualisarDinheiro();
                controleUi.AtualizarMoedas();
                MensagemItemComprado();
            }else{
                MensagemDinheiro();
            }
        }else{
            MensagemItemExistente();
        }
    }

    public void ComprarEscudo(){
        if (!GameController.CONTROLE_DE_JOGO.shield)
        {
            if (GameController.CONTROLE_DE_JOGO.moedas >= valorEscudo)
            {
                GameController.CONTROLE_DE_JOGO.shield = true;
                GameController.CONTROLE_DE_JOGO.moedas -= valorEscudo;
                GameController.CONTROLE_DE_JOGO.SalvarDados();
                //AtualisarDinheiro();
                controleUi.AtualizarMoedas();
                MensagemItemComprado();
            }else{
                MensagemDinheiro();
            }
        }else{
            MensagemItemExistente();
        }
    }
}
