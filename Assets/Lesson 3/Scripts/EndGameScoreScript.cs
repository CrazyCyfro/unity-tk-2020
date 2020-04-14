using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScoreScript : MonoBehaviour
{
	public GameDataScriptableObject gameData;
	
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
		text.text = "Score: " + gameData.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
