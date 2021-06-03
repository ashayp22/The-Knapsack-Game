using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenarioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        setUpString();
        stringList.Add(s1);
        InvokeRepeating("showText", 0f, .1f);
	}

    private void setUpString()
    {
        s1 = "You have ";
        if(PlayerPrefs.GetInt("scenario") == 1)
        {
            s1 += "broken your pickaxe \nSo you now have to buy a new one. \nClick below to buy the new model \n or the basic pickaxe.";
        } else if(PlayerPrefs.GetInt("scenario") == 2)
        {
            int s = Random.Range(0, 5);
            if(s == 0)
            {
                s1 += "fallen into a pit";
                PlayerPrefs.SetInt("scenario", 3);
            } else if(s == 1)
            {
                s1 += "gotten food poisoning";
                PlayerPrefs.SetInt("scenario", 4);

            }
            else if (s == 2)
            {
                s1 += "had rocks fallen on you";
                PlayerPrefs.SetInt("scenario", 5);


            }
            else if (s == 3)
            {
                s1 += "gotten rabies from a bat";
                PlayerPrefs.SetInt("scenario", 6);


            }
            else if (s == 4)
            {
                s1 += "gotten stuck in a flood";
                PlayerPrefs.SetInt("scenario", 7);

            }
            int health = Random.Range(1, 7);
            health *= 10;
            s1 += ". \n You have lost " + health + " health. \n Click below to heal yourself.";
            PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") - health);

            if(PlayerPrefs.GetInt("health") <= 0)
            {
                PlayerPrefs.SetInt("death", PlayerPrefs.GetInt("scenario") - 2);
                SceneManager.LoadScene(7);
            }

        }
    }

    private string s1 = "";

    private List<string> stringList = new List<string>();
    int count = 0;
    int i = 0;

    public Text infoText;
    public AudioSource type;

    private void showText()
    {
        if (stringList[count].Length == i)
        {
            CancelInvoke("showText");
            if(PlayerPrefs.GetInt("scenario") == 1)
            {
                oldP.gameObject.active = true;
                newP.gameObject.active = true;

            } else
            {
                heal.gameObject.active = true;
            }
        }
        else
        {
            infoText.text += stringList[count].Substring(i, 1);
            i += 1;
            type.Play();

        }
    }


    public Button heal;
    public Button oldP;
    public Button newP;

    public void BuyPick(int i)
    {
        if(i == 0) {
            PlayerPrefs.SetInt("pickaxe", 1);
            PlayerPrefs.SetInt("durability", 15);
        } else if(i == 1)
        {
            PlayerPrefs.SetInt("pickaxe", PlayerPrefs.GetInt("pickaxe") + 1);
            if(PlayerPrefs.GetInt("pickaxe") > 6) {
                PlayerPrefs.SetInt("pickaxe", 6);
            }
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - ((PlayerPrefs.GetInt("pickaxe") * 1000) / 2));


        }
        SceneManager.LoadScene(1);
    }


    public void Heal()
    {
        SceneManager.LoadScene(5);
    }

}
