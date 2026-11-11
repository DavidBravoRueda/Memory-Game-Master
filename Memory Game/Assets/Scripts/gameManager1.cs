using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class gameManager1 : MonoBehaviour {

	public Sprite[] cardFace;
	public Sprite cardBack;
	public GameObject[] cards;
	public GameObject gameTime;

	private bool _init  = false;
	private int _matches = 4;

	// Update is called once per frame
	void Update () {
		if (!_init)
			initializeCards ();

		if (Input.GetMouseButtonUp (0))
			checkCards ();

	}

	void initializeCards() {
		for (int id = 0; id < 2; id++) {
			for (int i = 1; i < 5; i++) {

				bool test = false;
				int choice = 0;
				while (!test) {
					choice = Random.Range (0, cards.Length);
					test = !(cards [choice].GetComponent<cardScript1> ().initialized);
				}
				cards [choice].GetComponent<cardScript1> ().cardValue = i;
				cards [choice].GetComponent<cardScript1> ().initialized = true;
			}
		}

		foreach (GameObject c in cards)
			c.GetComponent<cardScript1> ().setupGraphics ();

		if (!_init)
			_init = true;
	}

	public Sprite getCardBack() {
		return cardBack;
	}

	public Sprite getCardFace(int i) {
		return cardFace[i - 1];
	}

	void checkCards() {
		List<int> c = new List<int> ();

		for (int i = 0; i < cards.Length; i++) {
			if (cards [i].GetComponent<cardScript1> ().state == 1)
				c.Add (i);
		}

		if (c.Count == 2)
			cardComparison (c);
	}

	void cardComparison(List<int> c){
		cardScript.DO_NOT = true;

		int x = 0;

		if (cards [c [0]].GetComponent<cardScript1> ().cardValue == cards [c [1]].GetComponent<cardScript1> ().cardValue) {
			x = 2;
			_matches--;
			if (_matches == 0)
				gameTime.GetComponent<timeScript> ().endGame ();
		}


		for (int i = 0; i < c.Count; i++) {
			cards [c [i]].GetComponent<cardScript1> ().state = x;
			cards [c [i]].GetComponent<cardScript1> ().falseCheck ();
		}
	
	}

	public void reGame(){
		SceneManager.LoadScene ("gameScene1");
	}

	public void resscore(){
		SceneManager.LoadScene ("scoreScene1");
	}

	public void reMenu(){
		ClearScores();
		SceneManager.LoadScene ("menuScene");
	}


    // Función para borrar las puntuaciones
    void ClearScores() {
        string savedKeys = PlayerPrefs.GetString("scoreKeys", "");

        if (!string.IsNullOrEmpty(savedKeys)) {
            foreach (string key in savedKeys.Split(',')) {
                if (!string.IsNullOrEmpty(key)) {
                    PlayerPrefs.DeleteKey(key);
                    Debug.Log($"Borrando clave: {key}");
                }
            }
        }

        PlayerPrefs.DeleteKey("scoreKeys");
        PlayerPrefs.Save();
        Debug.Log("Puntuaciones borradas.");
    }
}
