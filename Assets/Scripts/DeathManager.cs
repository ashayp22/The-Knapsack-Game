using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("decreaseSlider", 0f, 0.02f);
        dying.Play();
	}

    public Slider slider;
    public Button image;
    public Sprite skull;

    private int Count = 0;

    private void decreaseSlider()
    {
        slider.value -= 1;
        Count += 1;
        if(slider.value == 0)
        {
            slider.transform.GetChild(1).gameObject.active = false;
            slider.transform.GetChild(2).gameObject.active = false;

        }

        if(Count == 103)
        {
            image.image.sprite = skull;
            slider.gameObject.active = false;
            CancelInvoke("decreaseSlider");
            stringSetUp();
            InvokeRepeating("showText", 0f, .1f);
            dead.Play();

        }
    }

    private string s1 = "";

    private void stringSetUp()
    {
        s1 = "You have died due to ";

        int way = PlayerPrefs.GetInt("death");

        if (way == 0)
        {
            s1 += "starvation. ";
        } else if (way == 1)
        {
            s1 += "falling into a pit. ";
        }
        else if (way == 2)
        {
            s1 += "getting food poisoning.";

        }
        else if (way == 3)
        {
            s1 += "rocks falling on you.";

        }
        else if (way == 4)
        {
            s1 += "getting rabies from a bat.";

        }
        else if (way == 5)
        {
            s1 += "getting stuck in a flood.";
        }

        s1 += "\n You survived till Day " + PlayerPrefs.GetInt("day") + " \n With $" + PlayerPrefs.GetInt("money") + ".";
        s1 += " \nTap the skull to play again.";

        if(PlayerPrefs.GetInt("dayStreak") < PlayerPrefs.GetInt("day"))
        {
            PlayerPrefs.SetInt("dayStreak", PlayerPrefs.GetInt("day"));
        }

        if (PlayerPrefs.GetInt("moneyStreak") < PlayerPrefs.GetInt("money"))
        {
            PlayerPrefs.SetInt("moneyStreak", PlayerPrefs.GetInt("money"));
        }

    }


    int i = 0;

    public Text infoText;
    public AudioSource type;
    public AudioSource dying;
    public AudioSource dead;

    private bool canTap = false;

    private void showText()
    {
        if (s1.Length == i)
        {
            CancelInvoke("showText");
            canTap = true;
        }
        else
        {
            infoText.text += s1.Substring(i, 1);
            i += 1;
            type.Play();

        }
    }


    public void playAgain()
    {
        if(canTap)
        {
            PlayerPrefs.SetInt("order", 0);
            PlayerPrefs.SetInt("money", 0);
            PlayerPrefs.SetInt("day", 1);
            PlayerPrefs.SetString("gems", "0000");

            PlayerPrefs.SetInt("q1", 0);
            PlayerPrefs.SetInt("q2", 0);
            PlayerPrefs.SetInt("q3", 0);
            PlayerPrefs.SetInt("q4", 0);

            PlayerPrefs.SetInt("w1", 0);
            PlayerPrefs.SetInt("w2", 0);
            PlayerPrefs.SetInt("w3", 0);
            PlayerPrefs.SetInt("w4", 0);

            PlayerPrefs.SetInt("v1", 0);
            PlayerPrefs.SetInt("v2", 0);
            PlayerPrefs.SetInt("v3", 0);
            PlayerPrefs.SetInt("v4", 0);

            PlayerPrefs.SetInt("profit", 0);

            PlayerPrefs.SetInt("health", 100);

            //upgradable variables
            PlayerPrefs.SetInt("food", 0);
            PlayerPrefs.SetInt("bandages", 0);

            PlayerPrefs.SetInt("pickaxe", 1);
            PlayerPrefs.SetInt("durability", 15);

            PlayerPrefs.SetInt("knapsack", 150);

            PlayerPrefs.SetInt("scenario", 0); //1 is pickaxe breaking, 2+ is other ways

            PlayerPrefs.SetInt("death", 0); //0 is starvation, 1+ is other ways

            SceneManager.LoadScene(1);
        }
    }

}
