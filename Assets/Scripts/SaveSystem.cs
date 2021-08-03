using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
   public static void savePlayer(LevelLoader ll){

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+ "/smth";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(ll);

        formatter.Serialize(stream, data);
        stream.Close();

   }

   public static PlayerData LoadPlayer(){
       string path = Application.persistentDataPath + "/smth";
       if(File.Exists(path)){
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);
           PlayerData data = formatter.Deserialize(stream) as PlayerData;
           stream.Close();
           
           return data;

       }
       else{
           Debug.LogError("Save File not found in "+ path);
           return null;
       }

   }

}
