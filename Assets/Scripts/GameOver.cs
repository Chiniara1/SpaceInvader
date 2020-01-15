using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countdown());
    }


    IEnumerator countdown()
    {
        var textUIComp = GameObject.Find("Counter").GetComponent<Text>();

        int time = int.Parse(textUIComp.text);

        while(time > 0)
        {
            yield return new WaitForSeconds(1);
            time = time - 1;
            if(time == 0)
            {
                Application.Quit();
            }
            textUIComp.text = time.ToString();
        }

        
    }

    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

}
