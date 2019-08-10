using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public bool relogio = false;
    public bool relogioAtivado = false;
    public float tempoRelogio;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (relogio && (!relogioAtivado))
        {
            tempoRelogio = GameController.CONTROLE_DE_JOGO.tempoRelogio;
            StartCoroutine("ExecutantoRelogio");
        }
    }

    public void AtivarRelogio(){
        relogio = true;
    }
    IEnumerator ExecutantoRelogio(){
        relogioAtivado = true;

        yield return new WaitForSeconds(tempoRelogio);
        relogio = false;
        relogioAtivado = false;
    }
}
