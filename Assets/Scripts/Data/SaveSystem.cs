using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
	public enum Result
	{
		None,
		FileNotFound,
		FileFound
	}

	public static Result LastResult = Result.None;

	public static void Save<T>(T saveData)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = new FileStream(Application.persistentDataPath + "/savegame.dat", FileMode.Create);
		bf.Serialize(file, saveData);
		file.Close();
	}

	public static T Load<T>()
	{
		if(File.Exists(Application.persistentDataPath + "/savegame.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = new FileStream(Application.persistentDataPath + "/savegame.dat", FileMode.Open);

			T loaded = (T)bf.Deserialize(file);
			file.Close();
			LastResult = Result.FileFound;
			return loaded;
		}
		else
		{
			LastResult = Result.FileNotFound;
		}

		return default(T);
	}
}
