using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SaveLoadController : MonoBehaviour
{
    public SaveManagementType type;
    public Text header;

    public Scrollbar sb;
    public Button[] saves;

    private PlayerData[] data;

    // Use this for initialization
    void Start()
    {
        header.text = type.ToString() + " Game";

        if (type == SaveManagementType.Load)
			GenerateLoadCanvas();
		else
			GenerateSaveCanvas();
    }

	private void GenerateLoadCanvas()
	{
		data = new PlayerData[saves.Length];
		for (int i = 0; i < saves.Length; i++){
			try
                {
                    data[i] = GameController.LoadPlayerData(i);
                    // Debug.Log(data[i].PlayerName);
                    SetSavedButtonText(data[i], saves[i]);
                }
                catch
                {
                    saves[i].interactable = false;
                }
		}
	}

    private void GenerateSaveCanvas()
    {
        for (int i = 0; i < saves.Length; i++)
        {
            try
            {
                SetSavedButtonText(GameController.LoadPlayerData(i), saves[i]);
            }
            catch
            {
                saves[i].GetComponentInChildren<Text>().text = "save" + i;
            }
        }
    }

    public void ScrollView(Canvas canvas)
    {
        canvas.transform.position = new Vector3(-gameObject.GetComponent<RectTransform>().sizeDelta.x * sb.value, canvas.transform.position.y);
    }

    private void SetSavedButtonText(PlayerData pd, Button button)
    {
        button.GetComponentInChildren<Text>().text = pd.PlayerName + "\n\nPlayer Level\n" + pd.Level + "\n\nGame Level\n" + pd.LatestGameLevel;
    }

	public void Close(){
		gameObject.SetActive(false);
	}

	public void ActivateButton(int saveNumber){
		if (type == SaveManagementType.Load){
			GameController.activatingPlayer = new PlayerData();
			GameController.activatingPlayer.SetAll(data[saveNumber]);
			GameController.LoadLevel(GameController.activatingPlayer.LatestGameLevel);
		}
		else {
			GameController.SavePlayerData(GameController.activatingPlayer, saveNumber);
		}
	}
}
