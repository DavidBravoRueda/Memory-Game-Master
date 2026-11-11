using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuScript1 : MonoBehaviour {

	public void triggerMenu(int trigger) {
		switch (trigger) {
		case(0) :
			SceneManager.LoadScene("gameScene1");
			break;
		case(1) :
			SceneManager.LoadScene("gameScene");
			break;
		case(2) :
			SceneManager.LoadScene("menuScene");
			break;
		}
	}
}
