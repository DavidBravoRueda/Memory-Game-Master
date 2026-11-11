using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScoresManager : MonoBehaviour
{
    public TMP_Text scoresText;

    void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        string savedKeys = PlayerPrefs.GetString("scoreKeys", "");
        Debug.Log($"scoreKeys almacenadas: {savedKeys}");

        if (string.IsNullOrEmpty(savedKeys))
        {
            scoresText.text = "No hay tiempos registrados.";
            return;
        }

        List<string> scores = new List<string>();

        foreach (string key in savedKeys.Split(','))
        {
            if (!string.IsNullOrEmpty(key))
            {
                string score = PlayerPrefs.GetString(key, "0");
                Debug.Log($"Clave: {key}, Puntuación: {score}");
                scores.Add($"{key}: {score} seg");
            }
        }

        scoresText.text = string.Join("\n", scores);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void ClearScores()
{
    // Eliminar todas las claves almacenadas
    string savedKeys = PlayerPrefs.GetString("scoreKeys", "");

    if (!string.IsNullOrEmpty(savedKeys))
    {
        foreach (string key in savedKeys.Split(','))
        {
            if (!string.IsNullOrEmpty(key))
            {
                PlayerPrefs.DeleteKey(key);
                Debug.Log($"Borrando clave: {key}");
            }
        }
    }

    // Eliminar también la clave "scoreKeys"
    PlayerPrefs.DeleteKey("scoreKeys");
    PlayerPrefs.Save();

    // Actualizar la UI de inmediato
    LoadScores();
}
}
