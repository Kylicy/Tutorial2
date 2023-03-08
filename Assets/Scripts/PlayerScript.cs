using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Public float speed adds speed multiplier variable to game object
    private Rigidbody2D rd2d;
    public float speed;

    // Start is called before the first frame update
    // rd2d finds and saves Rigidbody2D component
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // These lines attach project's horizontal and vertical controls
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
}
