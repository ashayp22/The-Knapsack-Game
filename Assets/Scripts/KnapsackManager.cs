using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnapsackManager : MonoBehaviour {

    // Use this for initialization
    void Start() {

        gemList.Add(g1);
        gemList.Add(g2);
        gemList.Add(g3);
        gemList.Add(g4);
        gemList.Add(g5);
        gemList.Add(g6);
        gemList.Add(g7);
        gemList.Add(g8);



        stringList.Add(s1);
        stringList.Add(s2);
        stringList.Add(s3);

    


        InvokeRepeating("showText", 0f, 0.1f);
        InvokeRepeating("timer", 0f, 1f);

        showGems();
        changeSliderSize();
        setDisplay();
    }


    private string s1 = "Drag the sliders to add the gems to your knapsack";
    private string s2 = "Tap the knapsack once you are finished";
    private string s3 = "Your knapsack is too heavy, so remove some gems";

    public Text text;
    public AudioSource type;


    private List<string> stringList = new List<string>();
    int count = 0;
    int i = 0;
    int goto2 = 0;

    private void timer()
    {
        goto2 += 1;
        if(goto2 == 15)
        {
            CancelInvoke("showText");
            CancelInvoke("timer");
            count += 1;
            i = 0;
            text.text = "";
            InvokeRepeating("showText", 0f, 0.1f);
        }
    }

    private void showText()
    {
        if (stringList[count].Length == i)
        {
            CancelInvoke("showText");
        }
        else
        {
            text.text += stringList[count].Substring(i, 1);
            i += 1;
            type.Play();
        }
    }

    public Button gem1;
    public Button gem2;
    public Button gem3;
    public Button gem4;

    public Sprite g1;
    public Sprite g2;
    public Sprite g3;
    public Sprite g4;
    public Sprite g5;
    public Sprite g6;
    public Sprite g7;
    public Sprite g8;

    public List<Sprite> gemList = new List<Sprite>();


    private void showGems()
    {
        string gems = PlayerPrefs.GetString("gems");
        gem1.image.sprite = gemList[int.Parse(gems.Substring(0, 1)) - 1];
        gem2.image.sprite = gemList[int.Parse(gems.Substring(1, 1)) - 1];
        gem3.image.sprite = gemList[int.Parse(gems.Substring(2, 1)) - 1];
        gem4.image.sprite = gemList[int.Parse(gems.Substring(3, 1)) - 1];
    }

    public Slider sl1;
    public Slider sl2;
    public Slider sl3;
    public Slider sl4;


    private void changeSliderSize()
    {
        var transform1 = sl1.transform as RectTransform;
        transform1.sizeDelta = new Vector2((float)(PlayerPrefs.GetInt("q1") * 5), 20);

        var transform2 = sl2.transform as RectTransform;
        transform2.sizeDelta = new Vector2((float)(PlayerPrefs.GetInt("q2") * 5), 20);

        var transform3 = sl3.transform as RectTransform;
        transform3.sizeDelta = new Vector2((float)(PlayerPrefs.GetInt("q3") * 5), 20);

        var transform4 = sl4.transform as RectTransform;
        transform4.sizeDelta = new Vector2((float)(PlayerPrefs.GetInt("q4") * 5), 20);

        sl1.maxValue = PlayerPrefs.GetInt("q1");
        sl2.maxValue = PlayerPrefs.GetInt("q2");
        sl3.maxValue = PlayerPrefs.GetInt("q3");
        sl4.maxValue = PlayerPrefs.GetInt("q4");

    }


    private float profit = 0;
    private float weight = 0;


    public Text t1;
    public Text t2;
    public Text t3;
    public Text t4;

    public Text weightText;
    public Text valueText;


    public Text ct1;
    public Text ct2;
    public Text ct3;
    public Text ct4;

    public void setDisplay()
    {
        float w1 = (sl1.value * PlayerPrefs.GetInt("w1"));
        float w2 = (sl2.value * PlayerPrefs.GetInt("w2"));
        float w3 = (sl3.value * PlayerPrefs.GetInt("w3"));
        float w4 = (sl4.value * PlayerPrefs.GetInt("w4"));

        float v1 = (PlayerPrefs.GetInt("v1") * sl1.value);
        float v2 = (PlayerPrefs.GetInt("v2") * sl2.value);
        float v3 = (PlayerPrefs.GetInt("v3") * sl3.value);
        float v4 = (PlayerPrefs.GetInt("v4") * sl4.value);


        t1.text = "Weight: " + w1 + "\n" + "Profit: $ " + v1;
        t2.text = "Weight: " + w2 + "\n" + "Profit: $" + v2;
        t3.text = "Weight: " + w3 + "\n" + "Profit: $" + v3;
        t4.text = "Weight: " + w4 + "\n" + "Profit: $" + v4;

        weight = PlayerPrefs.GetInt("knapsack") - (w1 + w2 + w3 + w4);
        profit = v1 + v2 + v3 + v4;

        weightText.text = "Weight Left: " + weight;
        valueText.text = "Total Profit: $" + profit;


        ct1.text = sl1.value.ToString();
        ct2.text = sl2.value.ToString();
        ct3.text = sl3.value.ToString();
        ct4.text = sl4.value.ToString();

    }


    public void gotostore() {
        if(weight >= 0)
        {
            PlayerPrefs.SetInt("profit", Mathf.RoundToInt(profit));
            PlayerPrefs.SetInt("order", 2);
            SceneManager.LoadScene(1);
        } else
        {
            CancelInvoke("showText");
            CancelInvoke("timer");
            count = 2;
            i = 0;
            text.text = "";
            InvokeRepeating("showText", 0f, 0.1f);
        }
    }

}
