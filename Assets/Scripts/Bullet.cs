using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{

    public float speed = 30;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImage;

    public int player;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.up * speed;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(col.tag == "Alien")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);

            IncreaseTextUIScore(col.gameObject.GetComponent<Alien>().points);

            col.gameObject.GetComponent<Alien>().points = 0;
            col.gameObject.GetComponent<Alien>().Kill();

            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;

            Destroy(gameObject);

            Destroy(col.gameObject, 0.5f);
        }

        if(col.tag == "Shield")
        {
            Destroy(gameObject);

            Destroy(col.gameObject);
        }

        if(col.tag == "1Player")
        {
            Destroy(gameObject);

            PlayerPrefs.SetInt("Players", 1);

            PlayerPrefs.SetInt("Level", 1);

            PlayerPrefs.SetInt("Lives1", 3);
            PlayerPrefs.SetInt("Lives2", 0);

            PlayerPrefs.SetInt("Score1", 0);
            PlayerPrefs.SetInt("Score2", 0);

            SceneManager.LoadScene("LevelDisplay");
        }

        if(col.tag == "2Players")
        {
            Destroy(gameObject);

            PlayerPrefs.SetInt("Players", 2);

            PlayerPrefs.SetInt("Level", 1);

            PlayerPrefs.SetInt("Lives1", 3);
            PlayerPrefs.SetInt("Lives2", 3);

            PlayerPrefs.SetInt("Score1", 0);
            PlayerPrefs.SetInt("Score2", 0);

            SceneManager.LoadScene("LevelDisplay");
        }
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

    void IncreaseTextUIScore(int points)
    {
        if(player == 1)
        {
            var textUIComp = GameObject.Find("Score1").GetComponent<Text>();

            int score = int.Parse(textUIComp.text);

            score += points;

            textUIComp.text = score.ToString();

            PlayerPrefs.SetInt("Score1", score);
        } 
        else if (player == 2)
        {
            var textUIComp = GameObject.Find("Score2").GetComponent<Text>();

            int score = int.Parse(textUIComp.text);

            score += points;

            textUIComp.text = score.ToString();

            PlayerPrefs.SetInt("Score2", score);
        }

    }
}
