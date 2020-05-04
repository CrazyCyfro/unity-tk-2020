using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameButton : MonoBehaviour
{
	public GameDataScriptableObject gameData;
	private Button button;
	
	void Awake() {
		Cursor.lockState = CursorLockMode.None;
	}
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(ChangeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void ChangeScene() {
		gameData.score = 0;
		SceneManager.LoadScene("StartMenuScene");
	}
}
