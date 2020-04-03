using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class textScript : MonoBehaviour
{
	public TextAsset gameText;
	public Text displayText;
	private string story;
	private List<string> storyList = new List<string>();
	private int currentProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
		story = gameText.ToString();
		string tempStoryCompiler = "";
		for (int i = 0; i<story.Length;i++)
		{
		if (story[i].ToString() == "\n" || story[i].ToString() == "\r") {
				if (!System.String.IsNullOrEmpty(tempStoryCompiler)) {
					storyList.Add(tempStoryCompiler);
					print("\""+tempStoryCompiler+"\"");
					tempStoryCompiler = "";
				}
			}
			else {
				tempStoryCompiler += story[i].ToString();
			}
		}
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input. GetKeyDown("space"))
		{
			ProgressStory();
		}
    }
	
	void ProgressStory()
	{
		if (storyList[currentProgress].ToString() == "*") {
			print("HEY");
		}
		else if (currentProgress<(storyList.Count-1)) {
			displayText.text=storyList[currentProgress];
			currentProgress++;
		}
	}
}
