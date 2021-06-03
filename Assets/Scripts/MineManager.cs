
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MineManager : MonoBehaviour {

    public Text infoText;
    public AudioSource type;
	// Use this for initialization
	void Start () {
        stringList.Add(s1);
        stringList.Add(s2);
        stringList.Add(s3);
        stringList.Add(s4);

        spriteList.Add(p1200);
        spriteList.Add(p900);
        spriteList.Add(p600);
        spriteList.Add(p300);

        fracList.Add(0.8);
        fracList.Add(0.6);
        fracList.Add(0.4);
        fracList.Add(0.2);

        gemList.Add(g1);
        gemList.Add(g2);
        gemList.Add(g3);
        gemList.Add(g4);
        gemList.Add(g5);
        gemList.Add(g6);
        gemList.Add(g7);
        gemList.Add(g8);

        setPickSprite();

        InvokeRepeating("showText", 0f, 0.1f);
	}


    public Sprite pick1;
    public Sprite pick2;
    public Sprite pick3;
    public Sprite pick4;
    public Sprite pick5;
    public Sprite pick6;

    private void setPickSprite()
    {
        int pickType = PlayerPrefs.GetInt("pickaxe");

        if(pickType == 1)
        {
            pickaxe.image.sprite = pick1;
        } else if(pickType == 2)
        {
            pickaxe.image.sprite = pick2;

        }
        else if (pickType == 3)
        {
            pickaxe.image.sprite = pick3;

        }
        else if (pickType == 4)
        {
            pickaxe.image.sprite = pick4;

        }
        else if (pickType == 5)
        {
            pickaxe.image.sprite = pick5;

        }
        else if (pickType == 6)
        {
            pickaxe.image.sprite = pick6;

        }
    }


    private string s1 = "Tap to enter the mine";
    private string s2 = "Hit the stone by dragging the pickaxe";
    private string s3 = "Gems collected from mining";
    private string s4 = "Tap to Continue";



    private List<string> stringList = new List<string>();
    int count = 0;
    int i = 0;

    private void showText()
    {
        if (stringList[count].Length == i)
        {
            CancelInvoke("showText");
        }
        else
        {
            infoText.text += stringList[count].Substring(i, 1);
            i += 1;
            type.Play();
            
        }
    }



    public Button mine;
    public Button stone;
    public Button pickaxe;

    public void goToMining()
    {
        mine.gameObject.active = false;
        stone.gameObject.active = true;
        pickaxe.gameObject.active = true;

        infoText.text = "";
        CancelInvoke();
        count += 1;
        i = 0;
        InvokeRepeating("showText", 0f, .1f);
    }

    private bool movePick = false;
    private void checkDrag()
    {
        Vector3 pos = Input.mousePosition;
        Vector3 pickPos = pickaxe.transform.position;
        //Debug.Log(pos.x);
        //Debug.Log(pos.y);
        if (pos.x >= pickPos.x && pos.x <= pickPos.x + 300 && pos.y >= pickPos.y && pos.y <= pickPos.y + 300)
        {
            if(Input.GetMouseButtonDown(0))
            {
                movePick = true;
            }

            if(Input.GetMouseButtonUp(0))
            {
                movePick = false;
            }
        }
        
        
    }


    private void movePickAxe()
    {
        if(movePick)
        {
            pickaxe.transform.position = Input.mousePosition;
        }
    }


    private int hitCount = 0;
    private bool through;
    public Sprite p1200;
    public Sprite p900;
    public Sprite p600;
    public Sprite p300;


    private List<Sprite> spriteList = new List<Sprite>();
    private List<double> fracList = new List<double>();
    private bool destroyedStone = false;
    public AudioSource rockSound;


    private void HitStone()
    {
        Vector3 stonePos = stone.transform.position;
        Vector3 pickPos = pickaxe.transform.position;

        List<Vector2> pointPos = new List<Vector2>();
        pointPos.Add(pickPos);
        pointPos.Add(new Vector2(pickPos.x, pickPos.y + 300));
        pointPos.Add(new Vector2(pickPos.x + 300, pickPos.y));
        pointPos.Add(new Vector2(pickPos.x + 300, pickPos.y + 300));

        bool b = false;

        for(int i = 0; i < pointPos.Count; i++)
        {
            if (pointPos[i].x >= stonePos.x && pointPos[i].x <= stonePos.x + 300 && pointPos[i].y >= stonePos.y && pointPos[i].y <= stonePos.y + 300)
            {
                b = true;
            }

        }

        if (b)
        {
            through = true;
            b = false;
        } else if(through == true)
        {
            rockSound.Play();
            through = false;
            hitCount += 1;

            if(hitCount == 10)
            {
                destroyedStone = true;
                stone.gameObject.active = false;
                pickaxe.gameObject.active = false;
                CancelInvoke("showText");
                infoText.text = "";
                i = 0;
                count += 1;
                InvokeRepeating("showText", 0f, 0.1f);
                displayGems();
                return;
            }


            if (hitCount % 2 == 0)
            {
                stone.image.sprite = spriteList[hitCount/2 - 1];
                var stoneTransform = stone.transform as RectTransform;
                double percent = fracList[hitCount/2 - 1];
                double width = 500 * percent;
                stoneTransform.sizeDelta = new Vector2((float)width, 500);
            }
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

    public Text gt1;
    public Text gt2;
    public Text gt3;
    public Text gt4;

    


    // Update is called once per frame
    void Update () {
        if (destroyedStone == false && mine.gameObject.active == false)
        {
            checkDrag();
            movePickAxe();
            HitStone();
        } 

        if(timer == 6)
        {
            CancelInvoke("showText");
            CancelInvoke("time");
            timer = 7;
            infoText.text = "";
            i = 0;
            count += 1;
            InvokeRepeating("showText", 0f, 0.1f);
        }

        if(timer == 7 && Input.GetMouseButton(0))
        {
            PlayerPrefs.SetInt("order", 1);
            int ra = Random.Range(0, 5);
            if (ra == 0)
            {
                if (Random.Range(0, 101) > PlayerPrefs.GetInt("durability"))
                {
                    PlayerPrefs.SetInt("scenario", 1);
                    SceneManager.LoadScene(6);
                }
                else
                {

                    SceneManager.LoadScene(1);
                }
            } else if(ra == 1 || ra == 2)
            {
                if(Random.Range(0, 7) == 1)
                {
                    PlayerPrefs.SetInt("scenario", 2);
                    SceneManager.LoadScene(6);
                    
                }
                else
                {

                    SceneManager.LoadScene(1);
                }
            } else
            {
                SceneManager.LoadScene(1);
            }
            
        }
	}


    private int timer = 0;

    private void time()
    {
        timer += 1;
    }

    private void displayGems()
    {
        InvokeRepeating("time", 0f, 1f);
        List<int> a = new List<int>();
        for (int i = 1; i < 9; i++)
        {
            a.Add(i);
        }

        string gems = "";
        for (int i = 0; i < 4; i++)
        {
            float index = Random.RandomRange(0, a.Count);
            gems += a[Mathf.FloorToInt(index)];
            a.RemoveAt(Mathf.FloorToInt(index));
        }

        PlayerPrefs.SetString("gems", gems);
        Debug.Log(gems);

        gem1.gameObject.active = true;
        gem2.gameObject.active = true;
        gem3.gameObject.active = true;
        gem4.gameObject.active = true;



        gem1.image.sprite = gemList[int.Parse(gems.Substring(0, 1))-1];
        gem2.image.sprite = gemList[int.Parse(gems.Substring(1, 1))-1];
        gem3.image.sprite = gemList[int.Parse(gems.Substring(2, 1))-1];
        gem4.image.sprite = gemList[int.Parse(gems.Substring(3, 1))-1];


        int upper = (PlayerPrefs.GetInt("pickaxe") * 6) + 30;

        PlayerPrefs.SetInt("q1", Random.RandomRange(8, upper));
        PlayerPrefs.SetInt("q2", Random.RandomRange(8, upper));
        PlayerPrefs.SetInt("q3", Random.RandomRange(8, upper));
        PlayerPrefs.SetInt("q4", Random.RandomRange(8, upper));

        PlayerPrefs.SetInt("w1", Random.RandomRange(1, 10));
        PlayerPrefs.SetInt("w2", Random.RandomRange(1, 10));
        PlayerPrefs.SetInt("w3", Random.RandomRange(1, 10));
        PlayerPrefs.SetInt("w4", Random.RandomRange(1, 10));

        PlayerPrefs.SetInt("v1", Random.RandomRange(1, 10));
        PlayerPrefs.SetInt("v2", Random.RandomRange(1, 10));
        PlayerPrefs.SetInt("v3", Random.RandomRange(1, 10));
        PlayerPrefs.SetInt("v4", Random.RandomRange(1, 10));

        List<string> gemName = new List<string>();

        gemName.Add("Diamond");
        gemName.Add("Gold");
        gemName.Add("Emerald");
        gemName.Add("Sapphire");
        gemName.Add("Amethyst");
        gemName.Add("Pearl");
        gemName.Add("Ruby");
        gemName.Add("Opal");

        gt1.text = gemName[int.Parse(gems.Substring(0, 1)) - 1] + "\n" + "Weight: " + PlayerPrefs.GetInt("w1") + "\n" + "Quantity: " + PlayerPrefs.GetInt("q1") + "\n" + "Value: $" + PlayerPrefs.GetInt("v1");
        gt2.text = gemName[int.Parse(gems.Substring(1, 1)) - 1] + "\n" + "Weight: " + PlayerPrefs.GetInt("w2") + "\n" + "Quantity: " + PlayerPrefs.GetInt("q2") + "\n" + "Value: $" + PlayerPrefs.GetInt("v2");
        gt3.text = gemName[int.Parse(gems.Substring(2, 1)) - 1] + "\n" + "Weight: " + PlayerPrefs.GetInt("w3") + "\n" + "Quantity: " + PlayerPrefs.GetInt("q3") + "\n" + "Value: $" + PlayerPrefs.GetInt("v3");
        gt4.text = gemName[int.Parse(gems.Substring(3, 1)) - 1] + "\n" + "Weight: " + PlayerPrefs.GetInt("w4") + "\n" + "Quantity: " + PlayerPrefs.GetInt("q4") + "\n" + "Value: $" + PlayerPrefs.GetInt("v4");

        

    }
}
