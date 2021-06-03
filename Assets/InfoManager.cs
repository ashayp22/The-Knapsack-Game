using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        addText();
	}
	
	void addText()
    {
        text.text += "HIGHSCORE \nDay Streak:" + PlayerPrefs.GetInt("dayStreak") + "\nMoney:" + PlayerPrefs.GetInt("moneyStreak");
        text.text += "\n \nPlease give this game\na rating";
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}
