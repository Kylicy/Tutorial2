using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Public float speed adds speed multiplier variable to game object
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text livesText;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    private int scoreValue = 0; // initial score value
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private int lives; // Lives integer

    // Start is called before the first frame update
    // rd2d finds and saves Rigidbody2D component
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString(); //automatically updates score to 0 on very first frame
        winTextObject.SetActive(false);
        lives = 3;
        
        SetLivesText(); // Sets initial lives
        loseTextObject.SetActive(false);

        musicSource.clip = musicClipOne;
        musicSource.Play();
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    // Update is called once per frame
    // FixedUpdate is for physics related things
    // These lines attach project's horizontal and vertical controls
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1; // Updates score value by 1
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
         if(scoreValue >= 4)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            Destroy(this);
        }
        else if(collision.collider.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            lives = lives - 1;

            SetLivesText();
        }
        if (lives == 0)
        {
            Destroy(this);

            loseTextObject.SetActive(true);
        }
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground") // Checks to see if player is colliding with ground
        {
            if(Input.GetKey(KeyCode.W)) // Checks for key being pressed, in this case "W"
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); // Sudden impluse, then gravity takes effect, force of 3 to verticle, simulates jump
            }
        }
    }
}
