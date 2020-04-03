using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//System.Serializable]
//public class stringEvent : UnityEvent<string> {}

public class textScript : MonoBehaviour
{
	public TextAsset gameText;
	public Text displayText;
	public Button choiceButton;
	private string story;
	private List<string[]> storyList = new List<string[]>();//list of string arrays - each array is [title, next title it should find. text]
	private int currentProgress = 0;
	
	private bool awaitingChoice = false;
	private bool gameEnd = false;
	
//	public stringEvent buttonClick = new stringEvent();

    // Start is called before the first frame update
    void Start()
    {
		story = gameText.ToString();
		
		//variables for compiler to go through text and arrange it into the storyList
		/*
		storyList.Add(new string[3]);
		Object[] compiler = new Object[] {"",0,0};
		
		Compile(compiler);
		*/
		
		
		string tempStoryCompiler = "";
		string tempStoryCompilerTitle = "";
		string tempStoryCompilerAfter = "";
		
		for (int i = 0; i<story.Length;i++)
		{
			if (story[i].ToString() == "\n" || story[i].ToString() == "\r") {
				if (!System.String.IsNullOrEmpty(tempStoryCompiler)) {
						storyList.Add(new string[]{tempStoryCompilerTitle, tempStoryCompilerAfter, tempStoryCompiler});//title of current text, text, and next title it should search for
						//print("\""+tempStoryCompiler+"\"");
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
		//print(tempStoryCompiler);
		storyList.Add(new string[]{tempStoryCompilerTitle, tempStoryCompilerAfter, tempStoryCompiler});//get the last line
		
		//buttonClick.AddListener(ChoiceOnClick);
    }
	
	/*Object[] Compile(Object[] compiler) {
		string tempCompiler = compiler[0];
		int index = compiler[1];
		int currentLine = compiler[2];
		if (story[index] == '|') {
			storyList[currentLine][0] = tempCompiler;
			tempCompiler = "";
		}
		else if (story[index] == '>') {
			storyList[currentLine][1] = tempCompiler;
			tempCompiler = "";
		}
		else if (story[index] == '\n' || story[index] == '\r') {
			if (tempCompiler != "") {
				storyList[currentLine][2] = tempCompiler;
				currentLine++;
				tempCompiler = "";
				storyList.Add(new string[3]);
			}
		}
		else {
			tempCompiler += story[index].ToString();
		}
		index++;
		if (index < story.Length) {
			Compile();
		}
	}*/
	
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space") && !awaitingChoice && !gameEnd) {
			ProgressStory("");
		}
    }
	
	void ProgressStory(string titleToSearchFor)
	{
		if (titleToSearchFor != "") {
			for (int i=0; i<storyList.Count; i++) {
				if (storyList[i][0] == titleToSearchFor) {
					print(i.ToString());
					currentProgress = i;
				}
			}
		}
		if (storyList[currentProgress][2].ToString()[0] == '*') {
				//displayText.text = FindOptions(storyList[currentProgress][2].ToString() + "\n\r");
				FindChoices(0);
				currentProgress++;
				awaitingChoice = true;
			}
		else if (currentProgress<(storyList.Count)) {
			displayText.text = storyList[currentProgress][2];
			currentProgress++;
			if (currentProgress == storyList.Count) {
				print("GAME END");
				gameEnd = true;
			}
		}
		/*
		if (titleToSearchFor == "") {
			//print(storyList[currentProgress][1].ToString()[0]);
			//print(storyList.Count.ToString());
			//print(currentProgress.ToString());
			if (storyList[currentProgress][2].ToString()[0] == '*') {
				//displayText.text = FindOptions(storyList[currentProgress][2].ToString() + "\n\r");
				FindChoices(0);
				currentProgress++;
				awaitingChoice = true;
			}
			else if (currentProgress<(storyList.Count)) {
				displayText.text = storyList[currentProgress][2];
				currentProgress++;
				if (currentProgress == storyList.Count) {
					print("GAME END");
					gameEnd = true;
				}
			}
		}
		else {
			for (int i=0; i<storyList.Count; i++) {
				if (storyList[i][1] == titleToSearchFor) {
					currentProgress = i;
				}
			}
			displayText
		}*/
	}
	
	void FindChoices(int choiceNumber) {
		//creating and positioning button
		Button choice = Instantiate(choiceButton, displayText.GetComponentInParent<Canvas>().transform);
		choice.GetComponentInChildren<Text>().text=storyList[currentProgress][2].ToString();
		Vector2 buttonPosition = choice.GetComponent<RectTransform>().anchoredPosition;
		buttonPosition.y -= choiceNumber*30;
		choice.GetComponent<RectTransform>().anchoredPosition = buttonPosition;
		
		//adding listener to button
		print("DELEGATING: " + storyList[currentProgress][1].ToString());
		string temp = storyList[currentProgress][1].ToString();
		choice.onClick.AddListener(delegate {ChoiceOnClick(temp);});
		//choice.onClick.AddListener(() => ChoiceOnClick(storyList[currentProgress][1].ToString()));
		//choice.onClick.AddListener(SomethingThatInvokesTheEvent);
		string nextLine = storyList[currentProgress+1][2].ToString();
		if (nextLine[0] == '*') {
			currentProgress++;
			FindChoices(choiceNumber+1);
		}
		else {
			return;
		}
	}
	
	/*void SomethingThatInvokesTheEvent(){
		print("ER: " + storyList[currentProgress][1].ToString());
		buttonClick.Invoke(storyList[currentProgress][1].ToString());
	}*/
	
	void ChoiceOnClick(string titleToSearchFor) {
		print(titleToSearchFor);
		GameObject[] buttonArray = GameObject.FindGameObjectsWithTag("choiceButton");
		for (int i=0; i<buttonArray.Length; i++) {
			Destroy(buttonArray[i]);
		}
		awaitingChoice = false;
		ProgressStory(titleToSearchFor);
	}
	
	
	/*
	string FindOptions(string toDisplay) {
		Button choice = Instantiate(choiceButton, displayText.GetComponentInParent<Canvas>().transform);
		choice.GetComponentInChildren<Text>().text=storyList[currentProgress][2].ToString();
		string nextLine = storyList[currentProgress+1][2].ToString();
		if (nextLine[0] == '*') {
			toDisplay += (nextLine + "\n\r");
			currentProgress++;
			return FindOptions(toDisplay);
		}
		else {
			return toDisplay;
		}
	}*/
	
	
}


