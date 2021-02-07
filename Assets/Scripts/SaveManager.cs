using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private string filePath;

    private void Awake()
    {
        Vibration.Init();
        if (Instance == null)
        {
            Instance = this;
        }
        filePath = Application.persistentDataPath + "data.gamesave";

        LoadGame();
    }

    public void SaveGame()
    {
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            var bf = new BinaryFormatter();
            var save = new Save();
            save.OrderIndexLength = PlayerProgress.OrderIndexLength;
            save.OrderIndexStrength = PlayerProgress.OrderIndexStrength;
            save.Money = PlayerProgress.Money;

            bf.Serialize(fs, save);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        using (var fs = new FileStream(filePath, FileMode.Open))
        {
            var bf = new BinaryFormatter();
            var save = (Save)bf.Deserialize(fs);

            PlayerProgress.Money = save.Money;
            PlayerProgress.OrderIndexLength = save.OrderIndexLength;
            PlayerProgress.OrderIndexStrength = save.OrderIndexStrength;
        }
    }

    [System.Serializable]
    public class Save
    {
        public int OrderIndexLength;
        public int OrderIndexStrength;
        public int Money;
    }
}
