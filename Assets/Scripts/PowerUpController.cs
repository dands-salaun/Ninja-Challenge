using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [Header("Relogio")]
    public bool relogio = false;
    public bool relogioAtivado = false;
    public float tempoRelogio;

    [Header("Shield")]
    public bool shield = false;
    public bool shieldAtivado = false;
    public float tempoShield;
    public GameObject shieldObject;

    [Header("Imã")]
    public bool ima = false;
    public bool imaAtivado = false;
    public float tempoIma;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (relogio && (!relogioAtivado))
        {
            StartCoroutine("ExecutantoRelogio");
        }
        if (shield && (!shieldAtivado))
        {
            StartCoroutine("ExecutantoShield");
        }
        if (ima && (!imaAtivado))
        {
            StartCoroutine("ExecutandoIma");
        }
    }

    public void AtivarRelogio(){
        relogio = true;
    }
    public void AtivarShield(){
        shield = true;
    }
    public void AtivarIma(){
        ima = true;
    }
    IEnumerator ExecutantoRelogio(){
        relogioAtivado = true;

        yield return new WaitForSeconds(tempoRelogio);
        relogio = false;
        relogioAtivado = false;
    }

    IEnumerator ExecutantoShield(){

        shieldAtivado = true;
        shieldObject.SetActive(true);
        yield return new WaitForSeconds(tempoShield);

        for (float i = 0; i < 1; i += 0.1f)
        {
            shieldObject.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            shieldObject.gameObject.GetComponent<SpriteRenderer>().enabled = enabled;
            yield return new WaitForSeconds(0.1f);
        }
        shieldObject.SetActive(false);
        shieldAtivado = false;
        shield = false;
    }
    IEnumerator ExecutandoIma(){
        imaAtivado = true;
        // fazer coin ir até player
        
        yield return new WaitForSeconds(tempoIma);
        
        imaAtivado = false;
        ima = false;
    }

    public void DesabilitarTodosPowerUps(){
        StopAllCoroutines();
        ima = false;
        shield = false;
        relogio = false;
        imaAtivado = false;
        relogioAtivado = false;
        shieldAtivado = false;
        shieldObject.SetActive(false);
    }
}
