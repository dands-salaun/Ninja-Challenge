using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrela : MonoBehaviour
{
    public Rigidbody2D rbEspinho;
    public Transform posInicial;
    public Transform posArremesso;
    public Animator myAmimator;
    //public float roracao;
    public float tempoMin;
    public float tempoMax;
    public float atritoMin;
    public float atritoMax;
    public bool caindo = false;
    private bool posicionar = true;
    public GameObject moeda;
    public GameObject shield;
    public GameObject ima;
    public GameObject relogioPU;
    public PowerUpController controlePU;
    public float gravidadeInicial = 0f;
    public float gravidadeCaindo = 1.5f;

    public UiControle controleUi;
    

    void Start()
    {
        controlePU = GameObject.FindGameObjectWithTag("ControlePU").GetComponent<PowerUpController>();
        controleUi = GameObject.FindGameObjectWithTag("ControleUI").GetComponent<UiControle>();
        rbEspinho = GetComponent<Rigidbody2D>();
        myAmimator = GetComponent<Animator>();
        transform.position = posInicial.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.CONTROLE_DE_JOGO.jogoOn)
        {
            if (posicionar)
            {

                float distancia = Vector3.Distance(transform.position, posArremesso.position);

                if (distancia < 0.01)
                {
                    posicionar = false;
                    StartCoroutine("Cair");
                }else
                {
                    transform.position = Vector3.MoveTowards(transform.position, posArremesso.position, 2f * Time.deltaTime);    
                }   
            }else
            {
                if (controlePU.relogio)
                {
                    AtivarPoewrUpRelogio();

                }else
                {
                    DesativarPowerUpRelogio();
                }
            }   
        }
        
         
    }

    void AtivarPoewrUpRelogio(){
        
        gravidadeCaindo = 0.4f;
                 
        if (caindo)
        {
            rbEspinho.gravityScale = gravidadeCaindo;
        }

    }
    void DesativarPowerUpRelogio(){
        gravidadeCaindo = 1.5f;
        if (caindo)
        {
            rbEspinho.gravityScale = gravidadeCaindo;
        }
    }

    IEnumerator Cair(){
        
        
        float tempo = Random.Range(tempoMin, tempoMax);
        yield return new WaitForSeconds(tempo);
        caindo = true;
        myAmimator.SetBool("Caindo", true);
        float atrito = Random.Range(atritoMin, atritoMax);
        rbEspinho.drag = atrito;
        //rbEspinho.AddTorque(roracao);
        rbEspinho.gravityScale = gravidadeCaindo;
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Chao")
        {
            GameController.CONTROLE_DE_JOGO.Pontuar();
            controleUi.AtualizarPontos();
            gameObject.SetActive(false);
            caindo = false;
            myAmimator.SetBool("Caindo", false);
            GerarItem();
            rbEspinho.gravityScale = gravidadeInicial;
            Voltar();
        }   
        if (other.gameObject.tag == "Shield")
        {
            
            gameObject.SetActive(false);
            caindo = false;
            rbEspinho.gravityScale = gravidadeInicial;
            Voltar();
        }
    }

    public void Voltar(){

        transform.position = posInicial.position;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(true);
        posicionar = true;
    }

    public void GerarMoeda(){
        
        int sorteio = Random.Range(1 , 11);

        if (sorteio % 4 == 0)
        {
            GameObject novaMoeda;
            novaMoeda = Instantiate(moeda, transform.position, Quaternion.identity);
            Destroy(novaMoeda, 3f);
        }
        if (sorteio % 7 == 0)
        {
            if (!controlePU.relogio)
            {
                GameObject novoPU;
                novoPU = Instantiate(relogioPU, transform.position, Quaternion.identity);
                Destroy(novoPU, 3f);    
            }
        }
        
    }

    public void GerarItem(){
        int sorteio = Random.Range(1, 11);
        
        if (sorteio % 4 == 0)
        {
            GameObject novaMoeda = Instantiate(moeda, transform.position, Quaternion.identity);
            Destroy(novaMoeda, 3f);
        }else
        {
            if (sorteio == 5)
            {
                if (GameController.CONTROLE_DE_JOGO.ima)
                {
                    if (!controlePU.ima)
                    {
                        GameObject novoIma = Instantiate(ima, transform.position, Quaternion.identity);
                        Destroy(novoIma, 3f);   
                    }
                }
            }else if (sorteio == 6)
            {
                if (GameController.CONTROLE_DE_JOGO.relogio)
                {
                    if (!controlePU.relogio)
                    {
                        GameObject novoRelogio = Instantiate(relogioPU, transform.position, Quaternion.identity);
                        Destroy(novoRelogio, 3f);    
                    }
                    
                }
            }else if (sorteio == 7)
            {
                if (GameController.CONTROLE_DE_JOGO.shield)
                {
                    if (!controlePU.shield)
                    {
                        GameObject novoShild = Instantiate(shield, transform.position, Quaternion.identity);
                        Destroy(novoShild, 3f);
                    }
                }
            }
        }
    }

    public void Reiniciar(){
        posicionar = true;
        rbEspinho.gravityScale = 0;
        caindo = false;
        transform.position = posInicial.position;
        gameObject.SetActive(true);
    }
}
