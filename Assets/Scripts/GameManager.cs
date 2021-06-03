using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text dayText;
    public Text moneyText;

    private string dT= "Day ";
    private string mT = "$";

  





    public AudioSource type;

    void Start()
    {
        
        InvokeRepeating("setDayText", 0f, 0.1f);
        InvokeRepeating("setMoneyText", 0f, 0.1f);
        setBorder();
        setHealth();
        checkDead();
    }

    private void checkDead()
    {
        if (PlayerPrefs.GetInt("health") <= 0)
        {
            PlayerPrefs.SetInt("death", 1);
            SceneManager.LoadScene(7);
        }
    }

    public Slider health;

   



    private void setHealth()
    {
        health.value = PlayerPrefs.GetInt("health");
        
        Debug.Log(PlayerPrefs.GetInt("health"));
    }

    public Button b1;
    public Button b2;
    public Button b3;

    public void setBorder()
    {
        int count = PlayerPrefs.GetInt("order");


        Debug.Log(count);
        if (count == 0)
        {
            b1.image.color = new Color32(243, 255, 100, 255);
            b2.image.color = new Color32(248, 219, 148, 255);
            b3.image.color = new Color32(248, 219, 148, 255);
            Debug.Log("set");
        } else if(count == 1)
        {
            b1.image.color = new Color32(248, 219, 148, 255);
            b2.image.color = new Color32(243, 255, 100, 255);
            b3.image.color = new Color32(248, 219, 148, 255);

        }
        else if(count == 2)
        {
            b1.image.color = new Color32(248, 219, 148, 255);
            b2.image.color = new Color32(248, 219, 148, 255);
            b3.image.color = new Color32(243, 255, 100, 255);
        }
    }



    int count1 = 0;

    private void setDayText()
    {
        if (count1 == dT.Length)
        {
            dayText.text += PlayerPrefs.GetInt("day");
            CancelInvoke("setDayText");
        }
        else
        {
            dayText.text += dT.Substring(count1, 1);
            count1 += 1;
            type.Play();
        }
    }


    int count2 = 0;
    private void setMoneyText()
    {
        if (count2 == mT.Length)
        {
            moneyText.text += PlayerPrefs.GetInt("money");
            CancelInvoke("setMoneyText");
        }
        else
        {
            moneyText.text += mT.Substring(count2, 1);
            count2 += 1;
        }
    }

    public void movedSlider()
    {
        if (health.value != PlayerPrefs.GetInt("health"))
        {
            health.value = PlayerPrefs.GetInt("health");
        }

    }

    public void gotoIntro()
    {
        PlayerPrefs.SetInt("canSkip", 0);

        SceneManager.LoadScene(0);
    }

    public void gotoInfo()
    {
        SceneManager.LoadScene(8);

    }




}
