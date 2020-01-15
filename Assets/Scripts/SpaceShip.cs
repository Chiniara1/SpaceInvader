using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{

    public float speed = 30;

    public bool alive = true;

    public GameObject theBullet;

    public Sprite healthyShipImage;

    public Sprite explodedShipImage;

    public int player;

    public KeyCode left;
    public KeyCode right;
    public KeyCode shoot;

    void Start()
    {
        if(gameObject.tag == "Player 2" && PlayerPrefs.GetInt("Players") == 1)
        {
            Destroy(gameObject);
        }

        if(player == 1 && PlayerPrefs.GetInt("Lives1") == 0)
        {
            Destroy(gameObject);
        }
        if(player == 2 && PlayerPrefs.GetInt("Lives2") == 0)
        {
            Destroy(gameObject);
        }
    }



    public void Die(){

        gameObject.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

        int lives = 3;

        if(player == 1)
        {
            var textUIComp = GameObject.Find("Lives1").GetComponent<Text>();
            lives = int.Parse(textUIComp.text);
            lives -= 1;
            PlayerPrefs.SetInt("Lives1", lives);
            textUIComp.text = lives.ToString();
        }
        else if (player == 2)
        {
            var textUIComp = GameObject.Find("Lives2").GetComponent<Text>();
            lives = int.Parse(textUIComp.text);
            lives -= 1;
            PlayerPrefs.SetInt("Lives2", lives);
            textUIComp.text = lives.ToString();
        }

        StartCoroutine(respawn(lives));

    }

    private IEnumerator respawn(float lives)
    {
            yield return new WaitForSeconds(3);
            
            if(PlayerPrefs.GetInt("Lives1") == 0 && PlayerPrefs.GetInt("Lives2") == 0)
            {
                SceneManager.LoadScene("GameOver");
            } 
            else if(lives == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                alive = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = healthyShipImage;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(shoot) && alive)
        {
            GameObject bullet = Instantiate(theBullet, transform.position, Quaternion.identity);
            
            bullet.gameObject.GetComponent<Bullet>().player = player;
        
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.bulletFire);
        }

        if(Input.GetKey(left))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        } 
        else if(Input.GetKey(right))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        } 
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public void Kill()
    {
        alive = false;
    }
}
