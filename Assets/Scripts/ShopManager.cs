using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    public static bool done = false;


    public List<int> minerScores = new List<int>();
    void Start()
    {
        Population p = new Population();
        p.SetUp();

        int day = PlayerPrefs.GetInt("day");



        if(day < 10)
        {
            p.threshold = day * 10 + 10;
        } else if(day >= 10 && day < 20)
        {
            p.threshold = day * 20 + 10;
        } else if(day >= 20 && day < 25)
        {
            p.threshold = 500;
        } else if(day >= 25 && day < 50)
        {
            p.threshold = day * 25 + 10;
        } else if(day >= 50)
        {
            p.threshold = day * 50;
        }

        p.threshold = 900;

        Debug.Log(p.threshold);

        while (done == false)
        {
            p.run();
        }

        for(int i = 0; i < 9; i++)
        {
            p.members[i].calcScore();
            minerScores.Add(p.members[i].score);
            Debug.Log(p.members[i].score + " Knapsack: " + p.members[i].weight);
        }

        placeString.Add("1st");
        placeString.Add("2nd");
        placeString.Add("3rd");
        placeString.Add("4th");
        placeString.Add("5th");
        placeString.Add("6th");
        placeString.Add("7th");
        placeString.Add("8th");
        placeString.Add("9th");
        placeString.Add("10th");

        SetUpString();
        stringList.Add(s1);
        //InvokeRepeating("Population.p.run()", 0f, 1f);
        //Debug.Log(p.members.Count);
        InvokeRepeating("showText", 0f, .1f);
        InvokeRepeating("showSkip", 0f, .5f);
    }



    public Text skip;
    private int timerCount = 0;

    private void showSkip()
    {
        timerCount += 1;
        if(timerCount >= 4)
        {
            if(timerCount % 2 == 0)
            {
                skip.gameObject.active = true;
            } else
            {
                skip.gameObject.active = false;

            }
        }

    }





    private string s1 = "";

    private List<string> placeString = new List<string>();

    private void SetUpString()
    {
        s1 = "Congratulations \n you have made a profit of \n $" + PlayerPrefs.GetInt("profit") + ". \n" + "\n"
            + "Among the other miners \n you have placed ";


        minerScores.Add(PlayerPrefs.GetInt("profit"));

        minerScores.Sort();

        int place = 0;

        for(int i = 0; i < minerScores.Count; i++)
        {
            if(PlayerPrefs.GetInt("profit") == minerScores[i])
            {
                place = i;
            }
        }

        s1 += placeString[9 - place] + ". \n The shop keeper has decided to \n pay the top ";

        int day = PlayerPrefs.GetInt("day");
        int players = 0;
        if (day <= 10)
        {
            players = Random.Range(5, 11);
        } else if(day > 10 && day <= 25)
        {
            players = Random.Range(3, 6);
        } else if(day > 25 && day < 50)
        {
            players = Random.Range(1, 4);
        } else if(day >= 50)
        {
            players = 1;
        }

        s1 += players + " miners \n so you have made $";


        if(10 - place <= players) {
            s1 += PlayerPrefs.GetInt("profit") + ".";
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + PlayerPrefs.GetInt("profit"));
        } else
        {
            s1 += "0.";
        }

        s1 += " \n \n swipe up to visit the shop \n or \n swipe down to start the next day";
        PlayerPrefs.SetInt("day", PlayerPrefs.GetInt("day") + 1);
        PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") - 10);


    }


    public Text infoText;
    public AudioSource type;

    private List<string> stringList = new List<string>();
    int count = 0;
    int i = 0;

    private void showText()
    {
        if (stringList[count].Length == i)
        {
            CancelInvoke();
            skip.gameObject.active = false;

        }
        else
        {
            infoText.text += stringList[count].Substring(i, 1);
            i += 1;
            type.Play();

        }
    }

    public AudioSource music;

    void Update()
    {
        swipe();
        if(swipeDown)
        {
            PlayerPrefs.SetInt("order", 0);

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


            SceneManager.LoadScene(1);


        }
        else if(swipeUp)
        {
            music.Play();
            showBuy();
        }  
        
        if(Input.GetMouseButtonDown(0))
        {
            CancelInvoke();
            infoText.text = "";
            infoText.text = s1;
            skip.gameObject.active = false;

        }

    }


    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void swipe()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;


        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }

        #endregion


        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }


        #endregion


        // Calculate the distance

        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //Did we cross the deadzone?

        if (swipeDelta.magnitude > 125)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                //Up or down
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }



    public Button item1;
    public Button item2;
    public Button item3;
    public Button item4;


    public Text b1;
    public Text b2;
    public Text b3;
    public Text b4;


    public Sprite pick1;
    public Sprite pick2;
    public Sprite pick3;
    public Sprite pick4;
    public Sprite pick5;
    public Sprite pick6;

    public Text moneyText;

    private void showBuy()
    {
        infoText.gameObject.active = false;

        moneyText.gameObject.active = true;
        item1.gameObject.active = true;
        item2.gameObject.active = true;
        item3.gameObject.active = true;
        item4.gameObject.active = true;

        UpdateMoneyText();
        showPick();

        b1.text = "Food  \n \nIncreases health \nby 10% \n \nPrice: $100 \nAlready Have: " + PlayerPrefs.GetInt("food");
        b2.text = "Bandages\n \nIncreases health \nby 90% \n \nPrice: $600 \nAlready Have: " + PlayerPrefs.GetInt("bandages");

        

        int pick = PlayerPrefs.GetInt("pickaxe");

        int durability = PlayerPrefs.GetInt("durability");

        //pickaxe level ups -> 30, 36, 42, 48, 54, 60 / 15, 30, 45, 60, 75, 90
        //knapsack level ups -> 150, 180, 210, 240, 270, 300

        if (pick == 6)
        {
            b3.text = "Upgraded Fully";
        }
        else
        {
            b3.text = "Pickaxe \n \nAlready Have: \n";
            b3.text += ((pick - 1) * 20).ToString() + "% boost \n" + (durability) + "% durability \n\nWill Get: \n" + ((pick - 1 + 1) * 20).ToString() + "% boost \n" + (durability + 15) + "% durability";
            b3.text += "\n\nPrice: $" + (((pick + 1) * 1000)/2);

        }
        
        int knapsack = PlayerPrefs.GetInt("knapsack");

        if (knapsack == 300)
        {
            b4.text = "Upgraded Fully";
        }
        else
        {

            b4.text = "Knapsack \n \n Already Have: \n";
            b4.text += (knapsack).ToString() + " weight \n\nWill Get: \n" + (knapsack + 30).ToString() + " weight";
            b4.text += "\n \nPrice: $" + (knapsack * 10);
        }


    }

    private int i2 = 0;

    private void showMoney()
    {
        string m = "$" + PlayerPrefs.GetInt("money") + "\nSwipe Down to Exit";
        if (m.Length == i2)
        {
            i2 = 0;
            CancelInvoke("showMoney");

        }
        else
        {
            moneyText.text += m.Substring(i2, 1);
            i2 += 1;
            type.Play();

        }
    }



    private void UpdateMoneyText()
    {
        CancelInvoke("showMoney");
        i2 = 0;

        moneyText.text = "";
        InvokeRepeating("showMoney", 0f, 0.1f);
    }

    private void showPick()
    {
        int pick = PlayerPrefs.GetInt("pickaxe");
        Debug.Log(pick);
        if(pick == 1)
        {
            item3.image.sprite = pick2;
        } else if(pick == 2)
        {
            item3.image.sprite = pick3;
        }
        else if (pick == 3)
        {
            item3.image.sprite = pick4;
        }
        else if (pick == 4)
        {
            item3.image.sprite = pick5;
        }
        else if (pick == 5)
        {
            item3.image.sprite = pick6;
        }
        else if (pick == 6)
        {
            item3.image.sprite = pick6;
        }

    }

    private int noTimer = 0;

    private void timer()
    {
        if(noTimer == 3)
        {
            noTimer = 0;
            CancelInvoke("timer");
            UpdateMoneyText();
        }

        noTimer += 1;
    }


    public void buyFood()
    {
        int money = PlayerPrefs.GetInt("money");

        if(money < 100)
        {
            moneyText.text = "You don't have enough money";
            InvokeRepeating("timer", 0f, 1f);
        }
        else
        {
            PlayerPrefs.SetInt("money", money - 100);
            PlayerPrefs.SetInt("food", PlayerPrefs.GetInt("food") + 1);
            showBuy();
        }
    }

    public void buyBandages()
    {
        int money = PlayerPrefs.GetInt("money");

        if (money < 500)
        {
            moneyText.text = "You don't have enough money";
            InvokeRepeating("timer", 0f, 1f);
        }
        else
        {
            PlayerPrefs.SetInt("money", money - 500);
            PlayerPrefs.SetInt("bandages", PlayerPrefs.GetInt("bandages") + 1);
            showBuy();

        }




    }

    public void buyPickaxe()
    {
        int money = PlayerPrefs.GetInt("money");
        int needed = (((PlayerPrefs.GetInt("pickaxe") + 1) * 1000) / 2);

        if(PlayerPrefs.GetInt("pickaxe") == 6)
        {
            return;
        }

        if (money < needed)
        {
            moneyText.text = "You don't have enough money";
            InvokeRepeating("timer", 0f, 1f);
        } else
        {
            PlayerPrefs.SetInt("money", money - needed);
            PlayerPrefs.SetInt("pickaxe", PlayerPrefs.GetInt("pickaxe") + 1);
            PlayerPrefs.SetInt("durability", PlayerPrefs.GetInt("durability") + 15);

            showBuy();


        }
    }

    public void buyKnapsack()
    {
        int money = PlayerPrefs.GetInt("money");
        int needed = (PlayerPrefs.GetInt("knapsack") * 10);

        if (PlayerPrefs.GetInt("knapsack") == 300)
        {
            return;
        }


        if (money < needed)
        {
            moneyText.text = "You don't have enough money";
            InvokeRepeating("timer", 0f, 1f);
        }
        else
        {
            PlayerPrefs.SetInt("money", money - needed);
            PlayerPrefs.SetInt("knapsack", PlayerPrefs.GetInt("knapsack") + 30);

            showBuy();

        }
    }





    private class Chromosomes
    {
        public List<int> gemList = new List<int>();
        public int weight = 0;
        public int value = 0;
        public int maxWeight = 150;
        public float mutationRate = 70;
        public int score = 0;


        public void SetUp()
        {
            for (int i = 0; i < 4; i++)
            {
                string key = "q" + (i + 1);
                gemList.Add(Mathf.RoundToInt(Random.Range(0, PlayerPrefs.GetInt(key) + 1)));
            }
            mutate();
            calcScore();
        }

        public void mutate()
        {
            if (Random.Range(0, 101) <= 70)
            {
                int index = Random.Range(0, 4);
                int current = gemList[index];

                if (current == 0)
                {
                    current += 1;
                }
                else if (current == PlayerPrefs.GetInt("q" + (index + 1)))
                {
                    current -= 1;
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        current += 1;
                    }
                    else
                    {
                        current -= 1;
                    }
                }
                gemList[index] = current;
            }
        }

        public void calcScore()
        {
            value = 0;
            weight = 0;
            score = 0;

            for (int i = 0; i < 4; i++)
            {
                value += gemList[i] * PlayerPrefs.GetInt("v" + (i + 1));
                weight += gemList[i] * PlayerPrefs.GetInt("w" + (i + 1));
            }

            score = value;

            if (weight > maxWeight)
            {
                score -= (weight - maxWeight) * 10;
            }
        }

        public List<Chromosomes> mateWith(Chromosomes other)
        {
            List<int> child1 = new List<int>();
            List<int> child2 = new List<int>();

            int pivot = Random.Range(0, 4);
            int i = 0;

            for (int z = 0; z < 4; z++)
            {
                if (i < pivot)
                {
                    child1.Add(gemList[z]);
                    child2.Add(other.gemList[z]);
                }
                else
                {
                    child2.Add(gemList[z]);
                    child1.Add(other.gemList[z]);
                }
            }

            Chromosomes c1 = new Chromosomes();
            Chromosomes c2 = new Chromosomes();

            c1.gemList = child1;
            c2.gemList = child2;

            List<Chromosomes> chromList = new List<Chromosomes>();
            chromList.Add(c1);
            chromList.Add(c2);

            return chromList;


        }
    }


    private class Population
    {
        public List<Chromosomes> members = new List<Chromosomes>();
        public int size = 100;
        public float elitism = 0.2f;

        public void SetUp()
        {
            fill();
        }

        public void fill()
        {
            while (members.Count < size)
            {
                if (members.Count < (size / 3 - 1))
                {
                    Chromosomes n = new Chromosomes();
                    n.SetUp();
                    members.Add(n);
                }
                else
                {
                    mate();
                }
            }
        }

        public void kill()
        {
            int target = Mathf.FloorToInt(elitism * members.Count);
            while (members.Count > target)
            {
                members.RemoveAt(members.Count - 1);
            }
        }


        public void mate()
        {
            Chromosomes chrom1 = members[Random.Range(0, members.Count)];
            Chromosomes chrom2 = chrom1;

            while (chrom1 == chrom2)
            {
                chrom2 = members[Random.Range(0, members.Count)];
            }

            List<Chromosomes> children = chrom1.mateWith(chrom2);
            for (int i = 0; i < children.Count; i++)
            {
                members.Add(children[i]);
            }
        }

        public void generation()
        {
            members.Sort((a, b) => (b.score.CompareTo(a.score)));
            kill();
            mate();
            fill();
            members.Sort((a, b) => (b.score.CompareTo(a.score)));
        }


        public int threshold = 1000;
        public int noImprovement = 0;
        public int lastScore = 0;
        public int genNum = 0;

        public void run()
        {
            if (noImprovement < threshold)
            {
                members[0].calcScore();
                lastScore = members[0].score;
                generation();

                members[0].calcScore();
                int lastScore2 = members[0].score;

                if (lastScore >= lastScore2)
                {
                    noImprovement += 1;
                }
                else
                {
                    noImprovement = 0;
                }

                genNum += 1;


                if (genNum % 10 == 0)
                {
                    Debug.Log(genNum + " " + "Score: " + members[0].score + " Weight: " + members[0].weight + "    " + noImprovement);
                }


            }
            else
            {
                ShopManager.done = true;
            }
        }
    }
   
}
