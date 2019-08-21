using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrela : MonoBehaviour
{
    private Rigidbody2D rbEspinho;
    public Transform posInicial;
    public Transform posArremesso;
    public float roracao;
    public float tempoMin;
    public float tempoMax;
    public float atritoMin;
    public float atritoMax;
    private bool caindo = false;
    private bool posicionar = true;
    public GameObject moeda;
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
        float atrito = Random.Range(atritoMin, atritoMax);
        rbEspinho.drag = atrito;
        rbEspinho.AddTorque(roracao);
        rbEspinho.gravityScale = gravidadeCaindo;
        
    }
    private void OnCollisionEnter2D(Collision2D objetoColidido) {
         
        if (objetoColidido.gameObject.tag == "Chao")
        {
            GameController.CONTROLE_DE_JOGO.Pontuar();
            controleUi.AtualizarPontos();
            gameObject.SetActive(false);
            caindo = false;
            GerarMoeda();
            rbEspinho.gravityScale = gravidadeInicial;
            Voltar();
        }
    }

    public void Voltar(){

        transform.position = posInicial.position;
        gameObject.SetActive(true);
        posicionar = true;
    }

    void GerarMoeda(){
        
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

    public void Reiniciar(){
        posicionar = true;
        rbEspinho.gravityScale = 0;
        caindo = false;
        transform.position = posInicial.position;
        gameObject.SetActive(true);
    }
}
