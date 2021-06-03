using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public Text text;
    public Button button;
    private string introText = "";

    private string s1 = "Welcome to Miner's Knapsack.";
    private string s2 = "You and 9 other miners are tasked \n with venturing out into mines,";
    private string s3 = "And collecting different gems to sell.";
    private string s4 = "While there are a variety of gems \n to collect,";
    private string s5 = "Only some will fit in your knapsack \n which will carry the gems.";
    private string s6 = "The objective is to find \n the best combination of gems,";
    private string s7 = "And to make the most profit \n and beat the other miners.";
    private string s8 = "To do this, use the menu to \n navigate between the \n mine, knapsack and shop.";
    private string s9 = "You have a health bar which will \n decrease every day.";
    private string s10 = "Also, anything can happen to you \n from rocks falling on you \n to food poisoning,";
    private string s11 = "So be prepared. \n Also, the game gets harder \n as each day goes by.";
    private string s12 = "But this is so the game stays \n stimulating and satisfying.";
    private string s13 = "Thats the whole gist of the game \n so good luck and have fun!";

    public AudioSource audio;


    private List<string> intro = new List<string>();
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("canSkip"));
        if(PlayerPrefs.GetInt("canSkip") == 1) {
            SceneManager.LoadScene(1);
        }


        //diamond 1
        //gold 2
        //emerald 3
        //sapphire 4
        //amethyst 5
        //pearl 6
        //ruby 7
        //opal 8

        if (PlayerPrefs.HasKey("order") == false)
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

            PlayerPrefs.SetInt("dayStreak", 0);
            PlayerPrefs.SetInt("moneyStreak", 0);

        }

        PlayerPrefs.SetInt("canSkip", 1);


        intro.Add(s1);
        intro.Add(s2);
        intro.Add(s3);
        intro.Add(s4);
        intro.Add(s5);
        intro.Add(s6);
        intro.Add(s7);
        intro.Add(s8);
        intro.Add(s9);
        intro.Add(s10);
        intro.Add(s11);
        intro.Add(s12);
        intro.Add(s13);


        spriteList.Add(miner);
        spriteList.Add(mine);
        spriteList.Add(pickaxe);
        spriteList.Add(gem2);
        spriteList.Add(knapsack);
        spriteList.Add(black);
        spriteList.Add(black);
        spriteList.Add(black);
        spriteList.Add(food);
        spriteList.Add(rocks);
        spriteList.Add(day);
        spriteList.Add(black);
        spriteList.Add(black);



        InvokeRepeating("showText", 0f, 0.1f);

        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && continueIntro)
        {
            InvokeRepeating("showText", 0f, 0.1f);
            continueIntro = false;
            tapText.gameObject.active = false;
            imageButton.gameObject.active = false;

        }

        if(Input.GetKeyDown("s"))
        {
            SceneManager.LoadScene(1);
        }
    }


    private int count = 0;
    private int i = 0;
    private bool continueIntro = false;

    public Text tapText;
    private void showText()
    {
        if (count == 13)
        {
            button.gameObject.active = true;
            continueIntro = false;
            
            CancelInvoke("showText");
        }

        if (intro[count].Length == introText.Length)
        {
            changeSprite();

            count += 1;
            i = 0;
            introText = "";
            continueIntro = true;
            tapText.gameObject.active = true;
            CancelInvoke("showText");
        }
        else
        {

            

            introText += intro[count].Substring(i, 1);
            text.text = introText;
            i += 1;
            audio.Play();
        }
    }

    private List<Sprite> spriteList = new List<Sprite>();
    public Button imageButton;

    public Sprite miner;
    public Sprite mine;
    public Sprite pickaxe;
    public Sprite gem2;
    public Sprite knapsack;
    public Sprite black;
    public Sprite food;
    public Sprite rocks;
    public Sprite day;



    private void changeSprite()
    {
        imageButton.gameObject.active = true;
        imageButton.image.sprite = spriteList[count];
    }



}
