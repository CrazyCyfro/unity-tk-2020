using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsTracker : MonoBehaviour
{
	public GameDataScriptableObject gameData;
	private TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
		gameData.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Points: " + gameData.score.ToString();
    }
}
