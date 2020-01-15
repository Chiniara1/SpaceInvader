using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            int currentLevel = PlayerPrefs.GetInt("Level");
            currentLevel++;
            PlayerPrefs.SetInt("Level", currentLevel);
            SceneManager.LoadScene("LevelDisplay");
        }
    }
}
