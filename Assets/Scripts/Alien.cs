using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    public float speed = 10;

    public int points;

    public Rigidbody2D rigidBody;

    public Sprite startingImage;

    public Sprite altImage;

    private SpriteRenderer spriteRenderer;

    public float secBeforeSpriteChange = 0.5f;

    public GameObject alienBullet;

    public float minFireRateTime = 1.0f;

    public float maxFireRateTime = 3.0f;

    public float baseFireWaitTime = 3.0f;

    public Sprite explodedShipImage;

    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = new Vector2(1, 0) * speed;

        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ChangeAlienSprite());

        baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);

        speed += PlayerPrefs.GetInt("Level") * 2;

        Turn(1);
    }

    void Turn(int direction)
    {
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = speed * direction;
        rigidBody.velocity = newVelocity;
    }
    
    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "LeftWall")
        {
            Turn(1);
            MoveDown();
        }

        if(col.gameObject.name == "RightWall")
        {
            Turn(-1);
            MoveDown();
        }

        if(col.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
            Destroy(gameObject);
        }
    }

    public IEnumerator ChangeAlienSprite()
    {
        while(alive)
        {
            if(spriteRenderer.sprite == startingImage)
            {
                spriteRenderer.sprite = altImage;
            } else{
                spriteRenderer.sprite = startingImage;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz2);
            }

            yield return new WaitForSeconds(secBeforeSpriteChange);
        }
    }

    public void Kill()
    {
        alive = false;
    }

    void FixedUpdate()
    {
        if(Time.timeSinceLevelLoad > baseFireWaitTime)
        {
            baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);

            Instantiate(alienBullet, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            Destroy(gameObject);

            Destroy(col.gameObject, 0.5f);
        }
    }

}
