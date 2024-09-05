using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Core.Singleton;

    public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();

        _saveSetup = new SaveSetup();

        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Joras";
        _saveSetup.lastCheckpoint = 0;

    }

    #region SAVE

    [NaughtyAttributes.Button]
    private void Save()
    {

        string setupToJson = JsonUtility.ToJson(_saveSetup, true); //biblioteca da unity que transforma a classe em um arquivo json
        Debug.Log(setupToJson);

        SaveFile(setupToJson);
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        Save();
    }

    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    public void SaveLastCheckpoint(int checkpoint)
    {
        _saveSetup.lastCheckpoint = checkpoint;
        Save();
    }


    #endregion

    private void SaveFile(string json)
    {
        string path = Application.dataPath + "/save.txt"; // a / eh importante para garantir que salva dentro da pasta de assets
        //salva dentro dos assets do projeto

        //string path = Application.persistentDataPath + "/save.txt";
        //guarda dentro de arquivos do servidor do computador -- dentro do usuario

        //string path = Application.streamingAssetsPath + "/save.txt";
        //guarda na pasta de streming assets dentro do projeto -- precisa ser criada manualmente

        string fileLoaded = "";

        if (File.Exists(path)) fileLoaded = File.ReadAllText(path);
        //le primeiro para garantir que nao tem nada salvo anteriormente

        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    #region DEBUG

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }    

    [NaughtyAttributes.Button]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }

    #endregion

}



[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int lastCheckpoint;
    public string playerName;




}
