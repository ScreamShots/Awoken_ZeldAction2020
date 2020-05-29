using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string pathSpecificPart = "/save.god";

    public static void SaveData(ProgressionManager proManagerToSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + pathSpecificPart;

        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressionFile data = new ProgressionFile(proManagerToSave);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static ProgressionFile LoadData()
    {
        string path = Application.persistentDataPath + pathSpecificPart;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressionFile data =  formatter.Deserialize(stream) as ProgressionFile;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static bool FilePresencePing()
    {
        string path = Application.persistentDataPath + pathSpecificPart;

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void DebugPath()
    {
        Debug.Log(Application.persistentDataPath + pathSpecificPart);
    }
}
