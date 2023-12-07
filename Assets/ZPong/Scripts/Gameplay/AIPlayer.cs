using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//My directives
namespace ZPong
{
    public enum AILevel
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }

    public class AIPlayer : MonoBehaviour
    {
        public float speed = 5f;
        public AILevel difficulty = AILevel.Easy;

        private float halfPlayerHeight;
        private Ball ball;

        private Paddle thisPaddle;
        private bool letsPlay;

        private float speedIncrement = 5f; // Incremental speed increase for better in game player for the Cloud AI Player by +5
        private float speedIncreaseInterval = 10f; // Interval in seconds for speed increase over the amount of time to make it get more challenging
        private float timer; // Timer for tracking the interval

        private void Start()
        {
            if (PlayerPrefs.HasKey("PaddleSpeed"))
            {
                speed = PlayerPrefs.GetFloat("PaddleSpeed");
            }
            if (PlayerPrefs.HasKey("AILevel"))
            {
                difficulty = (AILevel) PlayerPrefs.GetInt("AILevel");
            }

            StartCoroutine(StartDelay());
        }

        private void Update()
        {
            // Update the timer
            timer += Time.deltaTime;
            if (timer >= speedIncreaseInterval)
            {
                speed += speedIncrement; // Increase AI Cloud by Increments
                Debug.Log("AI Cloud Speed Increased to: " + speed); // Log the speed increase so i can see the stamps for duration to suit to game
                timer = 0; // Reset the timer
            }
        }


        private void FixedUpdate()
        {
            if (letsPlay)
            {
                float speedFactor = 1f;
                if(difficulty == AILevel.Easy)
                {
                    speedFactor = 1f; 
                }
                if (difficulty == AILevel.Medium)
                {
                    speedFactor = 1.2f;
                }
                else if (difficulty == AILevel.Hard)
                {
                    speedFactor = 1.5f;
                }

                thisPaddle.Move(Math.Sign(ball.transform.position.y - transform.position.y) * speed * speedFactor * Time.fixedDeltaTime);
            }
        }

        IEnumerator StartDelay()
        {
            //Disable AI from playing
            letsPlay = false;

            var playerParent = transform.parent;
            halfPlayerHeight = transform.localScale.y / 2f;
            thisPaddle = playerParent.GetComponent<Paddle>();

            // Disabling the Player script if present
            Player playerScript = playerParent.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.enabled = false;
            }

            //Delay start
            yield return new WaitForSeconds(3f);

            //Enable AI to react
            ball = GameManager.Instance.activeBall;
            letsPlay = true;
        }
    }
}
