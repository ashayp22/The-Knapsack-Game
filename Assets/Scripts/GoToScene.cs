using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {
    public AudioSource type;

	public void goToScene(int i)
    {
        int canGo = PlayerPrefs.GetInt("order");
        type.Play();

        if (i == 0 && canGo == 0)
        {
            SceneManager.LoadScene(2);
        } else if(i == 1 && canGo == 1)
        {
            SceneManager.LoadScene(3);
        } else if(i == 2 && canGo == 2)
        {
            SceneManager.LoadScene(4);

        }
    }
}
