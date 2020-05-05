using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Item
{
    public string ID;
    public Dictionary<string, string> intents = new Dictionary<string, string>();

    public void AddIntent(string tag, string value)
    {
        intents.Add(tag.ToLower(), value);
    }

    public void SaveItem()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + ID + ".txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        itemData data = new itemData(intents);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public bool LoadItem(string idToLoad)
    {
        string path = Application.persistentDataPath + "/" + idToLoad + ".txt";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            itemData data = formatter.Deserialize(stream) as itemData;

            stream.Close();

            ID = data.id;

            for (int i = 0; i < data.tags.Length; i++)
            {
                intents.Add(data.tags[i].ToLower(), data.awnsers[i]);
            }

            return true;
        }
        else
        {
            Debug.LogError("ID not found");
        }

        return false;
    }
}

[System.Serializable]
public class itemData
{
    public string id;
    public string[] tags;
    public string[] awnsers;

    public itemData(Dictionary<string, string> intents)
    {
        int index = 0;

        tags = new string[intents.Count];
        awnsers = new string[intents.Count];

        foreach (string key in intents.Keys)
        {
            if(index < tags.Length)
            {
                tags[index] = key;
                awnsers[index] = intents[key];
                index++;
            }
        }
    }
}
