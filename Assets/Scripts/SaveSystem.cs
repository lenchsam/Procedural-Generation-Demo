using UnityEngine;
using System.IO;

//helped by https://www.youtube.com/watch?v=6uMFEM-napE
public class SaveSystem : MonoBehaviour
{
    public static readonly string SaveFolder = Application.dataPath + "/saves/";

    public void Initialise(){
        if(!Directory.Exists(SaveFolder)){
            Directory.CreateDirectory(SaveFolder);
        }
    }
    public void Save(string saveString){
        //alows for multiple save files.
        int saveNumber = 1;
        while (File.Exists("save_" + saveNumber + ".txt")){
            saveNumber++;
        }
        File.WriteAllText(SaveFolder + "/save" + saveNumber + ".txt", saveString);
    }
    public string Load(){
        if(File.Exists(SaveFolder + "/save.txt")){
            string saveString = File.ReadAllText(SaveFolder + "/save.txt");
            return saveString;
        }else{
            return null;
        }
    }
}
