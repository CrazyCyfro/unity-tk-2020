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
	
	//private string story;
	//private List<string[]> storyList = new List<string[]>();//list of string arrays - each array is [title, next title it should find. text]
	
	private StoryCompiler storyCompiler = new StoryCompiler(); //custom created StoryCompiler class
	
	private bool awaitingChoice = false;

    // Start is called before the first frame update
    void Start()
    {
		storyCompiler.Compile(gameText);
    }
	
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space") && !awaitingChoice && !storyCompiler.gameEnded()) {
			displayText.text = storyCompiler.ProgressStory("");//ProgressStory(storyCompiler.GetStoryList()[currentProgress][0]);
			if (Resources.Load<Sprite>(storyCompiler.getImageTitle()) != null) {//load the image at the top of the screen corresponding to the title
				Sprite sprite = Resources.Load<Sprite>(storyCompiler.getImageTitle());
				displayImage.sprite = sprite;
				displayImage.preserveAspect = true;
				displayImage.color = Color.white;
			}
			else {
				displayImage.color = Color.clear;
			}
			if (storyCompiler.getButtonTitles().Count != 0) {//create buttons based on choices player can make
				print("FOUND BUTTONS");
				createChoiceButtons(storyCompiler.getButtonTitles());
				awaitingChoice = true;
			}
		}
		
		if (storyCompiler.gameEnded()) {
			print("END");
		}
    }
	
	void createChoiceButtons(List<string> buttonTitles) {
		foreach (string buttonTitle in buttonTitles) {
			Button choice = Instantiate(choiceButton, buttonPanel.transform); //creates the button, makes the buttonPanel the parent
			choice.GetComponentInChildren<Text>().text=buttonTitle; //sets the text in the button
			
			RectTransform buttonRectTransform = choice.GetComponent<RectTransform>();
			buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonPanel.GetComponent<RectTransform>().rect.height/3);//set the height of the button, 1/3 of the panel height
			
			Vector2 buttonPosition = choice.GetComponent<RectTransform>().anchoredPosition; 
			buttonPosition.y -= (buttonTitles.FindIndex(x => x.StartsWith(buttonTitle))+0.5f)*choice.GetComponent<RectTransform>().rect.height;
			choice.GetComponent<RectTransform>().anchoredPosition = buttonPosition; //adjusts the buttons y-position based on the number of buttons created so far
			
			//adding listener to button
			string temp = storyCompiler.getAfterFromText(buttonTitle);
			choice.onClick.AddListener(delegate {ChoiceOnClick(temp);});
		}
	}
	
	void ChoiceOnClick(string titleToSearchFor) {//when button is clicked, destroy all buttons, progress story based on choice
		GameObject[] buttonArray = GameObject.FindGameObjectsWithTag("choiceButton");
		for (int i=0; i<buttonArray.Length; i++) {
			Destroy(buttonArray[i]);
		}
		awaitingChoice = false;
		storyCompiler.clearButtonTitles();
		displayText.text = storyCompiler.ProgressStory(titleToSearchFor);
	}
}


