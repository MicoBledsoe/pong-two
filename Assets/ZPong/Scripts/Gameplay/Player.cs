using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//^My directives
namespace ZPong
{
    [RequireComponent(typeof(Paddle))]
    public class Player : MonoBehaviour
    {
        public KeyCode upKey;
        public KeyCode downKey;
        public float speed = 5f;

        private Paddle thisPaddle;
        private float targetPositionY; // Target position for the CLoud paddle

        private void Start()
        {
            thisPaddle = GetComponent<Paddle>();
            targetPositionY = transform.position.y; // Initialize with the current position

            // Retrieve player-specific input keys from PlayerPrefs.
            string upKeyPref = "Player" + (thisPaddle.isLeftPaddle ? "One" : "Two") + "UpInput";
            string downKeyPref = "Player" + (thisPaddle.isLeftPaddle ? "One" : "Two") + "DownInput";

            if (PlayerPrefs.HasKey(upKeyPref) && PlayerPrefs.HasKey(downKeyPref))
            {
                upKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(upKeyPref));
                downKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(downKeyPref));
            }

            if (PlayerPrefs.HasKey("PaddleSpeed"))
            {
                speed = PlayerPrefs.GetFloat("PaddleSpeed");
            }
        }

        private void Update()
        {
            // Determine the direction of movement based on input
            float movementDirection = 0f;
            if (Input.GetKey(upKey))
            {
                movementDirection = 1f;
            }
            else if (Input.GetKey(downKey))
            {
                movementDirection = -1f;
            }

            // Calculate the target position
            targetPositionY += movementDirection * speed * Time.deltaTime;

            // Clamp the target position within da screen bounds
            //targetPositionY = Mathf.Clamp(targetPositionY, thisPaddle.screenBottom + thisPaddle.GetHalfHeight(), thisPaddle.screenTop + thisPaddle.GetHalfHeight());

            // Applying smooth interpolation aka tweening to the Clouds paddle's movement which is user
            float interpolationFactor = Time.deltaTime * 10; // Adjustable so i control the speed of the interpo
            float newPositionY = Mathf.Lerp(transform.position.y, targetPositionY, interpolationFactor);
            transform.position = new Vector2(transform.position.x, newPositionY);

            // for interp bug aka it was the clamp target preventing Cloud user from moving oh my !
            Debug.Log("Screen Bottom: " + thisPaddle.screenBottom + ", Screen Top: " + thisPaddle.screenTop);
            Debug.Log("Target Position Y: " + targetPositionY + ", New Position Y: " + newPositionY);
        }
    }
}
