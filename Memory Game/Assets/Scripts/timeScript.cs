using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class timeScript : MonoBehaviour
{
    public Text counterText;
    public bool timeCounter = true;
    private float seconds;

    void Start()
    {
        counterText = GetComponent<Text>();
    }

    void Update()
    {
        if (timeCounter)
        {
            seconds = Time.timeSinceLevelLoad;
            counterText.text = "Tiempo: " + ((int)seconds).ToString("00") + " seg";
        }
    }

    public void endGame()
    {
        timeCounter = false;
        counterText.color = Color.yellow;

        // Generar clave única con timestamp
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Guardar clave en lista de scores
        string savedKeys = PlayerPrefs.GetString("scoreKeys", "");

        List<string> keys = new List<string>(savedKeys.Split(',')); // Convertir a lista
        keys.Add(timestamp); // Agregar nueva clave
        PlayerPrefs.SetString("scoreKeys", string.Join(",", keys)); // Guardar lista actualizada

        // Guardar puntuación con la clave generada
        PlayerPrefs.SetString(timestamp, ((int)seconds).ToString());
        PlayerPrefs.Save();
    }
}
