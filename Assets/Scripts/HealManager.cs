using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        setHealth();
        setText();
        stringList.Add(s1);
        stringList.Add(s2);

        InvokeRepeating("showText", 0f, 0.1f);
	}

    public Slider healthSlider;

	private void setHealth()
    {
        healthSlider.value = PlayerPrefs.GetInt("health");

    }

    public void sliderMoved()
    {
        if (healthSlider.value != PlayerPrefs.GetInt("health"))
        {
            healthSlider.value = PlayerPrefs.GetInt("health");
        }
    }

    public Text fText;
    public Text bText;


    public void setText()
    {
        fText.text = PlayerPrefs.GetInt("food").ToString();
        bText.text = PlayerPrefs.GetInt("bandages").ToString();
    }

    private bool moveBandages = false;
    private bool moveFood = false;
    public Button food;
    public Button bandages;
    public Button man;



    private void checkDrag()
    {
        Vector3 pos = Input.mousePosition;
        Vector3 pickPos1 = food.transform.position;
        Vector3 pickPos2 = bandages.transform.position;

        Debug.Log(pos);

        if (pos.x >= pickPos1.x && pos.x <= pickPos1.x + 275 && pos.y >= pickPos1.y && pos.y <= pickPos1.y + 275 && PlayerPrefs.GetInt("food") >= 1 && PlayerPrefs.GetInt("health") != 100)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moveFood = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                moveFood = false;
                food.transform.position = new Vector3(976, 344, 0);

            }
        }

        if (pos.x >= pickPos2.x && pos.x <= pickPos2.x + 275 && pos.y >= pickPos2.y && pos.y <= pickPos2.y + 275 && PlayerPrefs.GetInt("bandages") >= 1 && PlayerPrefs.GetInt("health") != 100)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moveBandages = true;

            }

            if (Input.GetMouseButtonUp(0))
            {
                moveBandages = false;
                bandages.transform.position = new Vector3(976, 74, 0);
            }
        }



    }

    public void dragBandages()
    {
        if (moveBandages)
        {
            bandages.transform.position = Input.mousePosition;
        }
    }

    public void dragFood()
    {
        if (moveFood)
        {
            food.transform.position = Input.mousePosition;
        }
    }

    public void minerContact()
    {
        Vector2 foodPos = food.transform.position;
        Vector2 bandagesPos = bandages.transform.position;

        if (foodPos.x <= 510 && moveFood == true)
        {
            moveFood = false;

            food.transform.position = new Vector3(976, 344, 0);
            PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") + 10);
            PlayerPrefs.SetInt("food", PlayerPrefs.GetInt("food") - 1);
            setHealth();
            setText();
        }
        else if(bandagesPos.x <= 510 && moveBandages == true)
        {
            moveBandages = false;

            bandages.transform.position = new Vector3(976, 74, 0);
            PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") + 90);
            if(PlayerPrefs.GetInt("health") > 100)
            {
                PlayerPrefs.SetInt("health", 100);
            }
            PlayerPrefs.SetInt("bandages", PlayerPrefs.GetInt("bandages") - 1);
            setHealth();
            setText();
        }
    }


    void Update()
    {
        checkDrag();
        dragBandages();
        dragFood();
        minerContact();
        checkMax();
    }

    private bool canCheck = true;

    private void checkMax()
    {
        if(canCheck && (PlayerPrefs.GetInt("health") == 100 || (PlayerPrefs.GetInt("bandages") == 0 && PlayerPrefs.GetInt("food") == 0)))
        {
            canCheck = false;
            CancelInvoke("showText");
            infoText.text = "";
            i = 0;
            count = 1;
            InvokeRepeating("showText", 0f, .1f);
        }
    }


    private string s1 = "Drag the food and bandages to the miner";
    private string s2 = "You cannot heal yourself anymore";

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
        }
        else
        {
            infoText.text += stringList[count].Substring(i, 1);
            i += 1;
            type.Play();

        }
    }



}
