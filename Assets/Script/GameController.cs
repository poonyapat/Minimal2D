using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

public class GameController : MonoBehaviour{

	public static PlayerData activatingPlayer;

	public static Vector3[] SpawnPosition = new Vector3 [] {
		new Vector3 (-7, -2.5f, 0),
		new Vector3 (0, 1, 0),
		new Vector3 (0, 1, 0),
		new Vector3 (0, 1.5f, 0),
		Vector3.zero,
		new Vector3 (0, 1, 0),
		Vector3.zero
	};

	public static void ChangeScene(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	public static void LoadLevel(int level){
		SceneManager.LoadScene("level " + level);
	}

	public static void SavePlayerData(PlayerData data){
		Debug.Log (Application.persistentDataPath + "PlayerData.dat");
		FileStream file = File.Create (Application.persistentDataPath + "PlayerData.dat");

		DataContractSerializer srl = new DataContractSerializer (data.GetType());
		MemoryStream streamer = new MemoryStream ();

		srl.WriteObject (streamer, data);
		streamer.Seek (0, SeekOrigin.Begin);
		file.Write (streamer.GetBuffer (), 0, streamer.GetBuffer ().Length);
		file.Close ();

		string result = XElement.Parse (Encoding.ASCII.GetString (streamer.GetBuffer ()).Replace ("\0", "")).ToString ();
		Debug.Log (result);
	}

	public static PlayerData LoadPlayerData(){
		FileStream file = File.OpenRead (Application.persistentDataPath + "PlayerData.dat");
		MemoryStream streamer = new MemoryStream ();
		DataContractSerializer srl = new DataContractSerializer (Type.GetType("PlayerData"));
		byte[] bytes = new byte[file.Length];
		file.Read (bytes, 0, (int)file.Length);
		streamer.Write (bytes, 0, (int)file.Length);
		streamer.Seek (0, SeekOrigin.Begin);
		Debug.Log (streamer.Position);
		PlayerData temp = (PlayerData) srl.ReadObject (streamer);
		file.Close ();
		return temp;
	}
}
