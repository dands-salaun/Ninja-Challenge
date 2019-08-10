using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho : MonoBehaviour
{
    private Rigidbody2D rbEspinho;
    private int atrito;
    public int atritoMax;
    private Vector3 posicao;
    public GameObject espinhoPrefab;
    public float tempoEspera;


    void Start()
    {
        rbEspinho = GetComponent<Rigidbody2D>();

        rbEspinho.gravityScale = 0;

        StartCoroutine("Espera");

        posicao = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible() {
        Instantiate(espinhoPrefab, posicao, transform.localRotation);
        Destroy(gameObject);
    }

    IEnumerator Espera(){

        yield return new WaitForSeconds(tempoEspera);
        rbEspinho.gravityScale = 1;
        atrito = Random.Range(1, atritoMax);
        rbEspinho.drag = atrito;
        
    }
}
