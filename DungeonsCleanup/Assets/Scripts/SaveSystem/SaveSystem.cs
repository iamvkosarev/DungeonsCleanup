using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(string saveName, PlayerDataManager playerDataManager)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + saveName +  ".save";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerDataManager);

        binaryFormatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayer(string saveName)
    {
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData playerData = binaryFormatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return playerData;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    public static void SaveSession(bool[] sessionActivity, bool[] createdSessions)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + "sessions" + ".save";

        FileStream stream = new FileStream(path, FileMode.Create);

        SessionData data = new SessionData(sessionActivity, createdSessions);

        binaryFormatter.Serialize(stream, data);

        stream.Close();
    }

    public static SessionData LoadSession()
    {
        string path = Application.persistentDataPath + "/saves/" + "sessions" + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SessionData sessionData = binaryFormatter.Deserialize(stream) as SessionData;

            stream.Close();

            return sessionData;
        }
        else
        {
            SessionData data = new SessionData(new bool[3] { false, false, false }, new bool[3] { false, false, false });
            SaveSession(data.sessionActivity, data.createdSessions);
            return data;
        }
    }
}
