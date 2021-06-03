using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToHeal : MonoBehaviour {

	public void goToHealth()
    {
        SceneManager.LoadScene(5);
    }
}
