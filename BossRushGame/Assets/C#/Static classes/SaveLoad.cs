using UnityEngine;
// next line enables use of the operating system's serialization capabilities within the script
using System.Runtime.Serialization.Formatters.Binary;
// next line, IO stands for Input/Output, and is what allows us to write to and read from
// our computer or mobile device. Allowing to create unique files and then read them.
using System.IO;

public static class SaveLoad
{
    public const int SOUND_NOICE = 0;
    public const int MUSIC_NOICE = 1;
    public const string FILE_PATH = "/BossRushGame.gd";

    public static float[] Floats { get; set; }

    public static bool FindSaveFile()
    {
        return File.Exists(Application.persistentDataPath + FILE_PATH);
    }

    public static void MakeSaveFile()
    {
        Floats = new float[] { 0.6f, 0.4f };

        Save();
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_PATH);
        bf.Serialize(file, Floats);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + FILE_PATH))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FILE_PATH, FileMode.Open);
            Floats = (float[])bf.Deserialize(file);
            file.Close();
        }
    }

    public static void Delete()
    {
        if (File.Exists(Application.persistentDataPath + FILE_PATH))
            File.Delete(Application.persistentDataPath + FILE_PATH);
    }
}