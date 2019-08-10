﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable] public class DadosJogo{
    public int dinheiro;
    public int dadosPontuacao;
    public int tempoRelogio;
    public int skinAtual;
    public int skinsCompradas;
}
public class GameController : MonoBehaviour
{
    public static GameController CONTROLE_DE_JOGO;
    private string caminhoArquivoDadosJogo;
    public int moedas;
    public int pontos;
    public int pontosMax;
    public int tempoRelogio;
    public int skinAtual;
    public int skinsCompradas;
    public bool jogoOn = false;
    GameObject[] estrelasArray;
    public List<Estrela> estrelasLista;
    private UiControle controleUi;
    

    private void Awake() {
        
        caminhoArquivoDadosJogo = Application.persistentDataPath + "/jogoInfo.dat";

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
            tempoRelogio = 3;
            skinAtual = 0;
            skinsCompradas = 0;
        }

    }

    void Start()
    {
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        estrelasArray = GameObject.FindGameObjectsWithTag("Estrela");
        for (int i = 0; i < estrelasArray.Length; i++)
        {
            estrelasLista.Add(estrelasArray[i].GetComponent<Estrela>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Iniciar(){
        pontos = 0;
        controleUi.AtualizarMoedas();
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
        tempoRelogio = dadosJogo.tempoRelogio;
        pontosMax = dadosJogo.dadosPontuacao;
        skinAtual = dadosJogo.skinAtual;
        skinsCompradas = dadosJogo.skinsCompradas;
        
    }
    public void SalvarDados(){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream arquivoSave = File.Create(caminhoArquivoDadosJogo);
        DadosJogo dadosJogo = new DadosJogo();

        dadosJogo.dinheiro = moedas;
        dadosJogo.dadosPontuacao = pontosMax;
        dadosJogo.tempoRelogio = tempoRelogio;
        dadosJogo.skinAtual = skinAtual;
        dadosJogo.skinsCompradas = skinsCompradas;
        
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
    }

    public void ReiniciarEstrelas(){

        for (int i = 0; i < estrelasLista.Count; i++)
        {
            estrelasLista[i].Reiniciar();
        }
    }
}