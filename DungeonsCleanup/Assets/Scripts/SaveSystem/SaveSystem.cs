using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    #region Player
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
    #endregion

    #region Sessions
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
    #endregion

    /*#region Settings
    public static void SaveSettings(bool useJoystick, float scaleParam, float posXParam, float posYParam, float alphaChannelParam)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + "settings" + ".save";

        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(useJoystick, scaleParam, posXParam, posYParam, alphaChannelParam);

        binaryFormatter.Serialize(stream, data);

        stream.Close();
    }
    public static SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/saves/" + "settings" + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SettingsData settingsData = binaryFormatter.Deserialize(stream) as SettingsData;
            stream.Close();
            return settingsData;
        }
        else
        {
            return SetDefaultSettings();
        }
    }
    public static SettingsData SetDefaultSettings()
    {
        SettingsData data = new SettingsData(false, 0.5f, 0.6f, 0.4f, 0.7f);
        SaveSettings(data.useJoystick, data.scaleParam, data.posXParam, data.posYParam, data.alphaChannelParam);
        return data;
    }
    #endregion*/

    #region Abilities
    public static void SaveShadowBorrleData(int shadowsBottleId, int[] listOfShadows, bool setNullShadows = false)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        int currentSessionNum = LoadSession().GetActiveSessionNum();
        if (!Directory.Exists(Application.persistentDataPath + "/saves" + $"/Session_{currentSessionNum}"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves" + $"/Session_{currentSessionNum}");
        }
        string path = Application.persistentDataPath + "/saves/" + $"/Session_{currentSessionNum}/" +
            $"shadowsBottle_{shadowsBottleId}" + ".save";

        FileStream stream = new FileStream(path, FileMode.Create);

        ShadowBorrleData data = new ShadowBorrleData(listOfShadows, setNullShadows);

        binaryFormatter.Serialize(stream, data);

        stream.Close();

    }
    public static ShadowBorrleData LoadShadowBorrleData(int shadowsBottleId)
    {
        int currentSessionNum = LoadSession().GetActiveSessionNum();
        string path = Application.persistentDataPath + "/saves/" + $"/Session_{currentSessionNum}/" +
            $"shadowsBottle_{shadowsBottleId}" + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ShadowBorrleData shadowBorrleData = binaryFormatter.Deserialize(stream) as ShadowBorrleData;

            stream.Close();

            return shadowBorrleData;
        }
        else
        {
            SaveShadowBorrleData(shadowsBottleId, new int[3], setNullShadows: true);
            return new ShadowBorrleData(new int[3]);
        }
    }
    #endregion

}
