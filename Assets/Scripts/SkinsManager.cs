using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable] public class Skin{
    
    public Sprite imagemSkin;
    public InfoSkin informacao;
}
[Serializable] public class InfoSkin{
    public int valorSkin;
    public bool adquirida;
}

[Serializable] public class SkinsGame{
    public InfoSkin[] skins;
}
public class SkinsManager : MonoBehaviour
{
    public List<Skin> listaSkins;
    public List<Skin> listaSkinsArquivo;
    private string caminhoArquivoDadosJogo;

    private void Awake() {
        
        caminhoArquivoDadosJogo = Application.persistentDataPath + "/SkinsInfo.dat";
        if(File.Exists(caminhoArquivoDadosJogo)){
            CarregarDadosJogo();
            if (listaSkins.Count != listaSkinsArquivo.Count)
            {
                DeletarArquivo();
                GerarArquivo();
                CarregarDadosJogo();
            }

        }else{

            GerarArquivo();
            CarregarDadosJogo();
        }
    }

    void GerarArquivo(){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream arquivoSave = File.Create(caminhoArquivoDadosJogo);
        SkinsGame dadosJogo = new SkinsGame();
        
        dadosJogo.skins = new InfoSkin[listaSkins.Count];

        for (int i = 0; i < listaSkins.Count; i++)
        {
            dadosJogo.skins[i] = listaSkins[i].informacao;
        }
        

        bf.Serialize(arquivoSave, dadosJogo);
        arquivoSave.Close();

    }
    void CarregarDadosJogo(){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream arquivoSave = File.Open(caminhoArquivoDadosJogo, FileMode.Open);
        SkinsGame dadosJogo = (SkinsGame) bf.Deserialize(arquivoSave);
        arquivoSave.Close();
        listaSkinsArquivo.Clear();
        //for (int i = 0; i < dadosJogo.skins.Length; i++)
        for (int i = 0; i < listaSkins.Count; i++)
        {
            Skin novaSkin = new Skin();
            novaSkin.informacao = dadosJogo.skins[i];
            novaSkin.imagemSkin = listaSkins[i].imagemSkin;
            listaSkinsArquivo.Add(novaSkin);
        }        

    }
    public void SalvarDados(){

        BinaryFormatter bf = new BinaryFormatter();
        FileStream arquivoSave = File.Create(caminhoArquivoDadosJogo);
        SkinsGame dadosJogo = new SkinsGame();

        dadosJogo.skins = new InfoSkin[listaSkinsArquivo.Count];

        for (int i = 0; i < listaSkinsArquivo.Count; i++)
        {
            dadosJogo.skins[i] = listaSkinsArquivo[i].informacao;
        }

        bf.Serialize(arquivoSave, dadosJogo);
        arquivoSave.Close();
    }

    public void DeletarArquivo(){
        
        File.Delete(caminhoArquivoDadosJogo);

    }
}
