using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            int lives1 = PlayerPrefs.GetInt("Lives1");
            GameObject.Find("Lives1").GetComponent<Text>().text = lives1.ToString();
            
            int lives2 = PlayerPrefs.GetInt("Lives2");
            GameObject.Find("Lives2").GetComponent<Text>().text = lives2.ToString();

            int score1 = PlayerPrefs.GetInt("Score1");
            GameObject.Find("Score1").GetComponent<Text>().text = score1.ToString();

            int score2 = PlayerPrefs.GetInt("Score2");
            GameObject.Find("Score2").GetComponent<Text>().text = score2.ToString();
    }

}
