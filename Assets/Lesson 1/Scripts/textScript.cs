using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Animations;

//System.Serializable]
//public class stringEvent : UnityEvent<string> {}

public class textScript : MonoBehaviour
{
	public TextAsset gameText;
	public Text displayText;
	public Button choiceButton;
	public Image displayImage;
	public GameObject buttonPanel;
	
	private string story;
	private List<string[]> storyList = new List<string[]>();//list of string arrays - each array is [title, next title it should find. text]
	private int currentProgress = 0;
	
	private bool awaitingChoice = false;
	private bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
		story = gameText.ToString();
		
		//variables for compiler to go through text and arrange it into the storyList
		string tempStoryCompiler = "";
		string tempStoryCompilerTitle = "";
		string tempStoryCompilerAfter = "";
		
		for (int i = 0; i<story.Length;i++)
		{
			if (story[i].ToString() == "\n" || story[i].ToString() == "\r") {
				if (!System.String.IsNullOrEmpty(tempStoryCompiler)) {
						storyList.Add(new string[]{tempStoryCompilerTitle, tempStoryCompilerAfter, tempStoryCompiler});//title of current text, text, and next title it should search for
						tempStoryCompiler = "";
						tempStoryCompilerTitle = "";
						tempStoryCompilerAfter = "";
				}
			}
			else if (story[i].ToString() == "|"){
				tempStoryCompilerTitle = tempStoryCompiler;
				tempStoryCompiler = "";
			}
			else if (story[i].ToString() == ">") {
				tempStoryCompilerAfter = tempStoryCompiler;
				tempStoryCompiler = ""; //after this tempStoryCompiler is the pointer for the next part of story
			}
			else {
				tempStoryCompiler += story[i].ToString();
			}
		}
		storyList.Add(new string[]{tempStoryCompilerTitle, tempStoryCompilerAfter, tempStoryCompiler});//get the last line
    }
	
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space") && !awaitingChoice && !gameEnd) {
			ProgressStory(storyList[currentProgress][0]);
		}
    }
	
	void ProgressStory(string titleToSearchFor)
	{
		if (titleToSearchFor != "") {
			for (int i=0; i<storyList.Count; i++) {
				if (storyList[i][0] == titleToSearchFor) {
					currentProgress = i;
				}
			}
			if (Resources.Load<Sprite>(titleToSearchFor) != null) {
				Sprite sprite = Resources.Load<Sprite>(titleToSearchFor);
				displayImage.sprite = sprite;
				displayImage.preserveAspect = true;
				displayImage.color = Color.white;
			}
			else {
				displayImage.color = Color.clear;
			}
		}
		if (storyList[currentProgress][2].ToString()[0] == '*') {
				FindChoices(0);
				currentProgress++;
				awaitingChoice = true;
			}
		else if (currentProgress<(storyList.Count)) {
			displayText.text = storyList[currentProgress][2];
			currentProgress++;
			if (currentProgress == (storyList.Count-1)) {
				gameEnd = true;
				print("GAME ENDED");
			}
		}	
	}
	
	void FindChoices(int choiceNumber) {
		//creating and positioning button
		Button choice = Instantiate(choiceButton, buttonPanel.transform); //creates the button, makes the buttonPanel the parent
		choice.GetComponentInChildren<Text>().text=storyList[currentProgress][2].ToString(); //sets the text in the button
		
		RectTransform buttonRectTransform = choice.GetComponent<RectTransform>();
		buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonPanel.GetComponent<RectTransform>().rect.height/3);
		
		//choice.GetComponent<RectTransform>().rect.height
		Vector2 buttonPosition = choice.GetComponent<RectTransform>().anchoredPosition; 
		buttonPosition.y -= (choiceNumber+0.5f)*choice.GetComponent<RectTransform>().rect.height;
		choice.GetComponent<RectTransform>().anchoredPosition = buttonPosition; //adjusts the buttons y-position based on the number of buttons created so far
		
		//adding listener to button
		string temp = storyList[currentProgress][1].ToString();
		choice.onClick.AddListener(delegate {ChoiceOnClick(temp);});
		string nextLine = storyList[currentProgress+1][2].ToString();
		if (nextLine[0] == '*') {
			currentProgress++;
			FindChoices(choiceNumber+1);
		}
		else {
			return;
		}
	}
	
	void ChoiceOnClick(string titleToSearchFor) {
		GameObject[] buttonArray = GameObject.FindGameObjectsWithTag("choiceButton");
		for (int i=0; i<buttonArray.Length; i++) {
			Destroy(buttonArray[i]);
		}
		awaitingChoice = false;
		ProgressStory(titleToSearchFor);
	}	
}


