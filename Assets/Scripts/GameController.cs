using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.Audio;

[Serializable] public class DadosJogo{
    public int dinheiro;
    public int dadosPontuacao;
    public int vidas;
    public int skinAtual;
    public bool somLigado;
    public bool propagandasAtivadas;
    public bool ima;
    public bool relogio;
    public bool shield;
}
public class GameController : MonoBehaviour
{
    public static GameController CONTROLE_DE_JOGO;
    private string caminhoArquivoDadosJogo;
    public int moedas;
    public int pontos;
    public int pontosMax;
    public int vidas;
    public int skinAtual;
    public bool jogoOn = false;
    public bool ima;
    public bool relogio;
    public bool shield;
    GameObject[] estrelasArray;
    public List<Estrela> estrelasLista;
    private UiControle controleUi;
    [Header("Som")]
    public Sprite[] iconesSom;
    public Image iconeBotaoSom;
    private bool somLigado;
    public AudioSource somGeral;
    public AudioMixer somGeralMixer;

    public bool continueADS = false;
    public bool continueDinheiro = false;
    public int valorContinue;
    public int contagemMortes;
    public bool propagandasAtivadas;

    private void Awake() {
        
        caminhoArquivoDadosJogo = Application.persistentDataPath + "/dataGame.dat";

        if (CONTROLE_DE_JOGO == null)
        {
            CONTROLE_DE_JOGO = this;
        }
        else if (CONTROLE_DE_JOGO != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if(File.Exists(caminhoArquivoDadosJogo)){
            CarregarDadosJogo();
        }else{

            moedas = 0;
            pontosMax = 0;
            vidas = 1;
            skinAtual = 0;
            somLigado = true;
            propagandasAtivadas = true; // mudar quando implementar compra
            ima = false;
            relogio = false;
            shield = false;
            SalvarDados();
        }

        somGeral = GetComponent<AudioSource>();


    }

    void Start()
    {
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        estrelasArray = GameObject.FindGameObjectsWithTag("Estrela");
        for (int i = 0; i < estrelasArray.Length; i++)
        {
            estrelasLista.Add(estrelasArray[i].GetComponent<Estrela>());
        }
        ControleDeSom();
        
        contagemMortes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Iniciar(){
        continueADS = false;
        continueDinheiro = false;
        pontos = 0;
        controleUi.AtualizarMoedas();
        controleUi.AtualizarPontos();
        StartCoroutine("DelayIniciar");
    }
    public void Continue(){
        StartCoroutine("DelayIniciar");
    }
    IEnumerator DelayIniciar(){
        yield return new WaitForSeconds(1f);
        jogoOn = true;
        controleUi.AtivarBarraSuperior();
        ReiniciarEstrelas();
        
    }

    public void Pontuar(){
        pontos++;
    }
    public void FicarRico(){
        moedas++;
    }

    void CarregarDadosJogo(){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream arquivoSave = File.Open(caminhoArquivoDadosJogo, FileMode.Open);
        DadosJogo dadosJogo = (DadosJogo) bf.Deserialize(arquivoSave);
        arquivoSave.Close();

        moedas = dadosJogo.dinheiro;
        pontosMax = dadosJogo.dadosPontuacao;
        skinAtual = dadosJogo.skinAtual;
        //skinsCompradas = dadosJogo.skinsCompradas;
        somLigado = dadosJogo.somLigado;
        propagandasAtivadas = true; // Alterar depois
        ima = dadosJogo.ima;
        relogio = dadosJogo.relogio;
        shield = dadosJogo.shield;

        moedas = 1000;


    }
    public void SalvarDados(){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream arquivoSave = File.Create(caminhoArquivoDadosJogo);
        DadosJogo dadosJogo = new DadosJogo();

        dadosJogo.dinheiro = moedas;
        dadosJogo.dadosPontuacao = pontosMax;
        dadosJogo.skinAtual = skinAtual;
        //dadosJogo.skinsCompradas = skinsCompradas;
        dadosJogo.somLigado = somLigado;
        dadosJogo.propagandasAtivadas = propagandasAtivadas;
        dadosJogo.ima = ima;
        dadosJogo.relogio = relogio;
        dadosJogo.shield = shield;
        
        bf.Serialize(arquivoSave, dadosJogo);
        arquivoSave.Close();
    }

    public void VerificarPontuacaoMax(){

        if (pontos > pontosMax)
        {
            pontosMax = pontos;
        }
    }
    public void DesabilitarObjetosCena(){

        
        foreach (GameObject item in estrelasArray) 
        {
            item.SetActive(false);
        }
        GameObject[] moedas = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject item in moedas) 
        {
            Destroy(item);
        }
        GameObject[] relogios = GameObject.FindGameObjectsWithTag("Relogio");
        foreach (GameObject item in relogios) 
        {
            Destroy(item);
        }

        GameObject[] imas = GameObject.FindGameObjectsWithTag("Ima");
        foreach (GameObject item in imas) 
        {
            Destroy(item);
        }
        GameObject[] escudos = GameObject.FindGameObjectsWithTag("Escudo");
        foreach (GameObject item in escudos) 
        {
            Destroy(item);
        }
    }

    public void ReiniciarEstrelas(){

        for (int i = 0; i < estrelasLista.Count; i++)
        {
            estrelasLista[i].Reiniciar();
        }
    }
    public void ControleDeSom(){

        if (somLigado){
            somGeralMixer.SetFloat("SomGeral", 0f);
            iconeBotaoSom.sprite = iconesSom[1];
        }else{
            somGeralMixer.SetFloat("SomGeral", -80f);
            iconeBotaoSom.sprite = iconesSom[0];
        }        
    }

    public void MudarEstadoSom(){
        if (somLigado)
        {
            somLigado = false;
        }else
        {
            somLigado = true;
        }

        ControleDeSom();
        SalvarDados();
    }

    public void OpenLink(string link){
        Application.OpenURL(link);
    }

    public void ComprarContinue(){
        moedas -= valorContinue;
        controleUi.AtualizarMoedas();
        controleUi.AtualizarMoedasLoja();
        continueDinheiro = true;
        SalvarDados();
        controleUi.Continue();
        
    }
}
