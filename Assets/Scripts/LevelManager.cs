using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int level = PlayerPrefs.GetInt("Level");
        GameObject.Find("LevelNumber").GetComponent<Text>().text = level.ToString();

        StartCoroutine(loadGame());
    }

    private IEnumerator loadGame()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainScene");
    }

}
