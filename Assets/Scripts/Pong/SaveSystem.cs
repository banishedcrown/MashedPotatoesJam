using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/game.data";
    public static void SaveData(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        Debug.Log("loading from path: " + path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Tried to load with no saveData");
            return null;
        }
    }

    public static bool SaveExists()
    {
        return File.Exists(path);
    }

    public static byte[] GetSaveData()
    {
        FileStream stream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[stream.Length];
        int num = (int)stream.Length;
        int numRead = 0;
        while(num > 0)
        {
            int n = stream.Read(data, numRead, num);
            if (n == 0) break;

            numRead += n;
            num -= n;
        }
        stream.Close();
        return data;
    }
}
