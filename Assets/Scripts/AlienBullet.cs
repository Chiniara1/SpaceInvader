using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    public float speed = 30;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.down * speed;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Player 2")
        {

            if(col.gameObject.GetComponent<SpaceShip>().alive)
            {
                Destroy(gameObject);

                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);

                col.gameObject.GetComponent<SpaceShip>().Kill();

                col.gameObject.GetComponent<SpaceShip>().Die();
            }

        }

        if(col.tag == "Shield")
        {
            Destroy(gameObject);

            Destroy(col.gameObject);
        }
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

}
