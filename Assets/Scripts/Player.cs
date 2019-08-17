using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    private Rigidbody2D meuCorpinho;
    public float velocidade;
    public bool direita;
    public bool esquerda;
    private bool isRight;
    [Header("Imagem")]
    private SpriteRenderer playerSprite;
    private Animator minhaAnimacao;
    public GameObject ninja;
    public GameObject naruto;
    
    [Header("Controles Externos")]
    private UiControle controleUi;
    private PowerUpController controlePu; 
    private Propagandas controlePropagandas;
    [Header("Som")]
    public AudioSource meuSom;

    private int vidas;
    public float tempoImortal;
    public bool imortal = false;

    void Start()
    {
        meuCorpinho = GetComponent<Rigidbody2D>();
        meuSom = GetComponent<AudioSource>();
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        controlePu = GameObject.FindGameObjectWithTag("ControlePU").GetComponent<PowerUpController>();
        controlePropagandas = GameObject.FindGameObjectWithTag("Propaganda").GetComponent<Propagandas>();
        //vidas = GameController.CONTROLE_DE_JOGO.vidas;
        vidas = 1;
        SelecionarPersonagem();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.CONTROLE_DE_JOGO.jogoOn)
        {
            //MovimentoAutonomo();
            MovimentoBotoes();
        }
        
    }
    
    public void SelecionarPersonagem(){
        if (GameController.CONTROLE_DE_JOGO.skinAtual == 0)
        {
            PersonagemNinja();
        }else if(GameController.CONTROLE_DE_JOGO.skinAtual == 1)
        {
            PersonagemNaruto();
        }
        GameController.CONTROLE_DE_JOGO.SalvarDados();
    }
    public void PersonagemNinja(){
        
        ninja.SetActive(true);
        naruto.SetActive(false);
        playerSprite = ninja.GetComponent<SpriteRenderer>();
        minhaAnimacao = ninja.GetComponent<Animator>();

    }
    public void PersonagemNaruto(){
        naruto.SetActive(true);
        ninja.SetActive(false);
        playerSprite = naruto.GetComponent<SpriteRenderer>();
        minhaAnimacao = naruto.GetComponent<Animator>();
    }

    void MovimentoAutonomo(){
        if (Input.GetMouseButtonDown(0))
            {
                MudarDirecao();
            }
            meuCorpinho.velocity = new Vector2(velocidade, meuCorpinho.velocity.y);    

    }
    public void MovimentoBotoes(){

        if (direita)
        {
            //playerSprite.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate (Vector2.right * velocidade * Time.deltaTime);
            esquerda = false;
            minhaAnimacao.SetBool("Correndo", true);
        }else if (esquerda)
        {
            //playerSprite.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate (Vector2.left * velocidade * Time.deltaTime);
            direita = false;
            minhaAnimacao.SetBool("Correndo", true);
        }else
        {
            minhaAnimacao.SetBool("Correndo", false);
        }
    }

    public void MoverDireita(bool ativo){
        direita = ativo;
    }
    public void MoverEsquerda(bool ativo){
        esquerda = ativo;
    }
    public void MudarDirecao(){
        velocidade = velocidade * -1;
        isRight = !isRight;
        playerSprite.flipX = isRight;
    }
    private void OnTriggerEnter2D(Collider2D objetoColidido) {

        if (objetoColidido.gameObject.tag == "Coin")
        {
            GameController.CONTROLE_DE_JOGO.FicarRico();
            controleUi.AtualizarMoedas();
            Destroy(objetoColidido.gameObject);

        }
    }
    private void OnCollisionEnter2D(Collision2D objetoColidido) {
        if (objetoColidido.gameObject.tag == "Estrela")
        {
            if (!imortal)
            {
                vidas -= 1;
                if (vidas < 1)
                {
                    GameController.CONTROLE_DE_JOGO.somGeral.Pause();
                    meuSom.Play();
                    GameController.CONTROLE_DE_JOGO.jogoOn = false;
                    minhaAnimacao.SetBool("Morto", true);
                    minhaAnimacao.SetBool("Correndo", false);
                    GameController.CONTROLE_DE_JOGO.VerificarPontuacaoMax();
                    GameController.CONTROLE_DE_JOGO.SalvarDados();
                    GameController.CONTROLE_DE_JOGO.DesabilitarObjetosCena();    
                }else
                {
                    objetoColidido.gameObject.SetActive(false);
                    objetoColidido.gameObject.GetComponent<Estrela>().Voltar();
                    StartCoroutine("Imortal");
                    
                }    
            }else
            {
                objetoColidido.gameObject.SetActive(false);
                objetoColidido.gameObject.GetComponent<Estrela>().Voltar();                    
            } 
        }
        if (objetoColidido.gameObject.tag == "Parede")
        {
            MudarDirecao();
        }
        if (objetoColidido.gameObject.tag == "Relogio")
        {
            controlePu.AtivarRelogio();
            Destroy(objetoColidido.gameObject);
        }
    }
    public void Morrer(){
        GameController.CONTROLE_DE_JOGO.jogoOn = false;
        //controlePropagandas.GanharDinheiro();
        controleUi.GameOver();
        
    }

    public void Reiniciar(){
        minhaAnimacao.SetBool("Morto", false);
        transform.position = new Vector3(0 , transform.position.y, transform.position.z);
    }
    IEnumerator FadeOut()
    {
        Color newColor = playerSprite.color;      
        for (float f = 1f; f >= 0; f -= 0.1f)
        {  
            newColor.a = f;
            playerSprite.color = newColor;
            yield return new WaitForSeconds(.05f);
        }
    }
    IEnumerator FadeIn()
    {
        Color newColor = playerSprite.color;      
        for (float f = 0f; f <= 1; f += 0.1f)
        {  
            newColor.a = f;
            playerSprite.color = newColor;
            yield return new WaitForSeconds(.05f);
        }
        newColor.a = 1f;
        playerSprite.color = newColor;
    }
    
    IEnumerator Imortal(){
        imortal = true;
        Physics2D.IgnoreLayerCollision(8, 10);
        for (float i = 0; i < tempoImortal; i += 0.2f)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);    
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        Physics2D.IgnoreLayerCollision(8, 10, false);
        imortal = false;
    }
    
}
