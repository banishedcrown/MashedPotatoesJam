using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    [DllImport("__Internal")]
    private static extern void syncSystem();

    static string path = Application.persistentDataPath + "/game.data";
    static string oldPath = "/idbfs/81d2821253ddbf7ddd185bb6991530b9/game.data";
    static bool inUse = false;
    public static void initSavePath()
    {
        
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            path = "/idbfs/jpgame.data";

            if (!File.Exists(path)) //let's try to replace the old save immediately.
            {
                if (File.Exists(oldPath))
                {
                    File.Copy(oldPath, path);
                    File.Delete(oldPath);
                }
            }
        }
        else
        {
            path = Application.persistentDataPath + "/game.data";
        }
        Debug.Log("Set save path to:" + path);
    }
    public static void SaveData(GameData data)
    {
        while (inUse) continue;
        inUse = true;
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        formatter.Serialize(stream, data);
        stream.Close();
        if (Application.platform == RuntimePlatform.WebGLPlayer) 
        { 
            syncSystem();
        }
        inUse = false;
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
