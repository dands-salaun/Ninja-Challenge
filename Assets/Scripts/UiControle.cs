using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiControle : MonoBehaviour
{
    [Header("Compra de Moedas")]
    public GameObject panelCoins;
    public Text moedas200Dinheiro;
    public Text moedas500Dinheiro;
    public Text moedas800Dinheiro;
    public Text moedas1200Dinheiro;
    public Text moedasCoinText;



    [Header("Menu")]
    public GameObject menu;
    public CanvasGroup menuCanvas;
    public GameObject playBotao;
    public Text textoStart;

    [Header("Loja")]
    public CanvasGroup loja;
    public GameObject botoes;
    public GameObject skins;
    public GameObject itens;
    public GameObject coins;
    public Text moedasLoja;
    public Text moedasSkin;
    public Text moedasItens;
    private LojaController lojaController;

    [Header("Jogo")]
    public Text pontosTxt;
    public GameObject barraPontos;
    public Text moedasTxt;    
    public CanvasGroup jogo;    
    public GameObject barraMoedas;
    public RectTransform chao;


    [Header("Game Over")]
    public RectTransform gameOver;
    public GameObject painelGameOver;
    public Text pontosTxtGameOver;
    public Text pontosMaxGameOver;
    public CanvasGroup canvasGameOver;
    public Player player;    
    public GameObject botaoContinueADS;
    public GameObject botaoContinueDinheiro;
    public Text valorContinue;
    private UiControle controleUi;
    [Header("Creitos")]
    public CanvasGroup creditosCanvas;
    public GameObject creditos;
    [Header("Em Breve")]
    public GameObject emBreve;
    public CanvasGroup emBreveCanvas;
    void Start()
    {
        pontosTxt.text = GameController.CONTROLE_DE_JOGO.pontos.ToString();
        moedasTxt.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
        moedasLoja.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        lojaController = GameObject.FindGameObjectWithTag("Loja").GetComponent<LojaController>();
        MenuDescer();
        ChaoSubir();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizarMoedas(){
        moedasTxt.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
        moedasLoja.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
        moedasSkin.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
        moedasItens.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
        moedasCoinText.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
    }
    public void AtualizarPontos(){
        pontosTxt.text = GameController.CONTROLE_DE_JOGO.pontos.ToString();
    }
    public void AtualizarMoedasLoja(){
        moedasLoja.text = GameController.CONTROLE_DE_JOGO.moedas.ToString();
    }

    public void MenuSubir(){
        menu.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 510), 1f);
        playBotao.SetActive(false);
        textoStart.gameObject.SetActive(false);
    }
    public void MenuDescer(){
        menu.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 1f);
        playBotao.SetActive(true);
        textoStart.gameObject.SetActive(true);
    }
    public void GameOverDescer(){

        if (GameController.CONTROLE_DE_JOGO.continueADS)
        {
            botaoContinueADS.SetActive(false);
        }else
        {
            botaoContinueADS.SetActive(true);
        }
        if (GameController.CONTROLE_DE_JOGO.continueDinheiro)
        {
            botaoContinueDinheiro.SetActive(false);
        }else
        {
            if (GameController.CONTROLE_DE_JOGO.moedas >= GameController.CONTROLE_DE_JOGO.valorContinue)
            {
                valorContinue.text = GameController.CONTROLE_DE_JOGO.valorContinue.ToString();
                botaoContinueDinheiro.SetActive(true);    
            }
            
        }
        canvasGameOver.DOFade(1f, 0.01f);
        painelGameOver.SetActive(false);
        gameOver.DOAnchorPos(new Vector2(0, 0f), 1f);
    }
    public void GameOverEsconder(){
        canvasGameOver.DOFade(0f, 0.5f);
        painelGameOver.SetActive(false);
        StartCoroutine("DelayGameOverSubir");
        
    }
    public void GameOverSubir(){
        gameOver.DOAnchorPos(new Vector2(0, 760f), 0.01f);
    }

    IEnumerator DelayGameOverSubir(){
        yield return new WaitForSeconds(0.6f);
        GameOverSubir();
    }

    public void GameOver(){
        painelGameOver.SetActive(true);
        pontosTxtGameOver.text = GameController.CONTROLE_DE_JOGO.pontos.ToString();
        pontosMaxGameOver.text = GameController.CONTROLE_DE_JOGO.pontosMax.ToString();
        GameOverDescer();
    }

    public void Reiniciar(){
        GameOverEsconder();
        GameController.CONTROLE_DE_JOGO.Iniciar();
        GameController.CONTROLE_DE_JOGO.somGeral.Play();
        player.StartCoroutine("FadeOut");
        jogo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        StartCoroutine("DelayFadeIn");
        GameController.CONTROLE_DE_JOGO.ReiniciarEstrelas();
        controleUi.AtualizarPontos();        
    }
    public void Continue(){
        GameOverEsconder();
        GameController.CONTROLE_DE_JOGO.Continue();
        GameController.CONTROLE_DE_JOGO.somGeral.Play();
        player.StartCoroutine("FadeOut");
        jogo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        StartCoroutine("DelayFadeIn");
        GameController.CONTROLE_DE_JOGO.ReiniciarEstrelas();
        controleUi.AtualizarPontos();        
    }
    public void VoltarMenu(){

        GameOverEsconder();
        player.StartCoroutine("FadeOut");
        jogo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        StartCoroutine("DelayFadeInMenu");
        controleUi.AtualizarPontos();  
        playBotao.SetActive(true);

    }
    IEnumerator DelayFadeIn(){
        yield return new WaitForSeconds(0.5f);
        player.Reiniciar();
        jogo.DOFade(1f, 0.5f);
        player.StartCoroutine("FadeIn");
        DesativarBarraSuperior();
    }

    IEnumerator DelayFadeInMenu(){
        yield return new WaitForSeconds(0.5f);
        player.Reiniciar();
        jogo.DOFade(1f, 0.5f);
        player.StartCoroutine("FadeIn");
        DesativarBarraSuperior();
        MenuDescer();
        GameController.CONTROLE_DE_JOGO.somGeral.Play();
    }

    public void AtivarBarraSuperior(){
        barraMoedas.SetActive(true);
        barraPontos.SetActive(true);
    }
    public void DesativarBarraSuperior(){
        barraMoedas.SetActive(false);
        barraPontos.SetActive(false);
    }
    public void ChaoSubir(){
        chao.DOAnchorPos(new Vector2(0, 0), 1f);
    }

    public void LojaMostrar(){ // esconder player
        jogo.DOFade(0f, 0.5f);
        menu.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        player.StartCoroutine("FadeOut");
        loja.gameObject.SetActive(true);
        StartCoroutine("DelayMostrarLoja");

    }
    IEnumerator DelayMostrarLoja(){

        yield return new WaitForSeconds(0.5f);
        player.gameObject.SetActive(false);
        AtualizarMoedasLoja();
        menu.SetActive(false);
        // Desativar botão play
        playBotao.SetActive(false);
        loja.DOFade(1f, 0.5f);
    }
    public void LojaEsconder(){
        loja.DOFade(0f, 0.5f);
        menu.SetActive(true);
        StartCoroutine("DelayEsconderLoja");
    }
    IEnumerator DelayEsconderLoja(){

        yield return new WaitForSeconds(0.5f);
        jogo.DOFade(1f, 0.5f);
         menu.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        player.gameObject.SetActive(true);
        player.StartCoroutine("FadeIn");
        loja.gameObject.SetActive(false);
        AtualizarMoedas();
        playBotao.SetActive(true);
        AtualizarMoedas();
    }
    

    public void CreditosMostrar(){
        
        creditos.SetActive(true);
        jogo.DOFade(0f, 0.5f);
        player.StartCoroutine("FadeOut");
        menuCanvas.DOFade(0f, 0.5f);
        StartCoroutine("DelayCreditosMostrar");
    }

    IEnumerator DelayCreditosMostrar(){

        yield return new WaitForSeconds(0.5f);
        creditosCanvas.DOFade(1f, 0.5f);
        menu.SetActive(false);
    }
    public void CreditosEsconder(){

        creditosCanvas.DOFade(0f, 0.5f);
        StartCoroutine("DelayCreditosEsconder");
    }

    IEnumerator DelayCreditosEsconder(){

        yield return new WaitForSeconds(0.5f);
        menu.SetActive(true);
        menuCanvas.DOFade(1f, 0.5f);
        jogo.DOFade(1f, 0.5f);
        player.StartCoroutine("FadeIn");
        creditos.SetActive(false);
        
    }
    public void EmBreveMostrar(){
        
        emBreve.SetActive(true);
        jogo.DOFade(0f, 0.5f);
        player.StartCoroutine("FadeOut");
        menuCanvas.DOFade(0f, 0.5f);
        StartCoroutine("DelayEmBreveMostrar");
    }

    IEnumerator DelayEmBreveMostrar(){

        yield return new WaitForSeconds(0.5f);
        emBreveCanvas.DOFade(1f, 0.5f);
        menu.SetActive(false);
    }

    public void EmBreveEsconder(){

        emBreveCanvas.DOFade(0f, 0.5f);
        StartCoroutine("DelayEmBreveEsconder");
    }

    IEnumerator DelayEmBreveEsconder(){

        yield return new WaitForSeconds(0.5f);
        menu.SetActive(true);
        menuCanvas.DOFade(1f, 0.5f);
        jogo.DOFade(1f, 0.5f);
        player.StartCoroutine("FadeIn");
        emBreve.SetActive(false);
        
    }

    public void MostrarRanking(){
        PlayServices.ShowLeaderBoard(NinjaChallengeServices.leaderboard_ranking);
    }

    public void LojaSkinsMostrar(){
        // Desativar botões
        botoes.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);

        // Ativar Skins
        StartCoroutine("DelayMostrarSkins");

    }
    IEnumerator DelayMostrarSkins(){

        yield return new WaitForSeconds(0.5f);
        botoes.SetActive(false);
        skins.SetActive(true);
        skins.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        AtualizarMoedas();
    }

    public void LojaSkinEsconder(){

        skins.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        StartCoroutine("DelayEsconderSkins");

    }

    IEnumerator DelayEsconderSkins(){

        yield return new WaitForSeconds(0.5f);
        botoes.SetActive(true);
        skins.SetActive(false);
        skins.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);

        botoes.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }

    public void LojaItensMostrar(){
        // Desativar botões
        botoes.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        
        StartCoroutine("DelayMostrarItens");

    }
    IEnumerator DelayMostrarItens(){

        yield return new WaitForSeconds(0.5f);
        botoes.SetActive(false);
        itens.SetActive(true);
        itens.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        AtualizarMoedas();
    }

    public void LojaItensEsconder(){

        itens.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        StartCoroutine("DelayEsconderItens");

    }

    IEnumerator DelayEsconderItens(){

        yield return new WaitForSeconds(0.5f);
        botoes.SetActive(true);
        itens.SetActive(false);
        //itens.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);

        botoes.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }

    public void LojaMoedasMostrar()
    {
        botoes.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);

        StartCoroutine("LojaMoedasMostrarDelay");
    }

    IEnumerator LojaMoedasMostrarDelay()
    {
        yield return new WaitForSeconds(0.5f);
        botoes.SetActive(false);
        panelCoins.SetActive(true);
        panelCoins.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        AtualizarMoedas();
    }

    public void LojaMoedasEsconder()
    {
        panelCoins.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        StartCoroutine("LojaMoedasEsconderDelay");
    }

    IEnumerator LojaMoedasEsconderDelay()
    {
        yield return new WaitForSeconds(0.5f);
        botoes.SetActive(true);
        panelCoins.SetActive(false);
        //itens.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);

        botoes.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);

    }
}
