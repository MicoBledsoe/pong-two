using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//My directives above ^

namespace ZPong
{
    [RequireComponent(typeof(Collider2D))]
    public class Paddle : MonoBehaviour
    {
        public bool isLeftPaddle = true;

        private float halfPlayerHeight;
        public float screenTop { get; private set; }
        public float screenBottom { get; private set; }
        
        private RectTransform rectTransform;
        private Vector2 OGSize;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            OGSize = rectTransform.sizeDelta; // Save the OG size

            if (PlayerPrefs.HasKey("PaddleSize"))
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, PlayerPrefs.GetFloat("PaddleSize"));
                this.GetComponent<BoxCollider2D>().size = rectTransform.sizeDelta;
            }

            halfPlayerHeight = rectTransform.sizeDelta.y / 2f;

            var height = UIScaler.Instance.GetUIHeight();
            screenTop = height / 2;
            screenBottom = -height / 2;
        }

        public void Move(float movement)
        {
            //Set temporary variable
            Vector2 newPosition = rectTransform.anchoredPosition;

            //Manipulate the temporary variable
            newPosition.y += movement;
            newPosition.y = Mathf.Clamp(newPosition.y, screenBottom + halfPlayerHeight, screenTop - halfPlayerHeight);

            //Apply temporary variable back to original component
            rectTransform.anchoredPosition = newPosition;
        }

        public float GetHalfHeight()
        {
            return halfPlayerHeight;
        }

        public Vector2 AnchorPos()
        {
            return rectTransform.anchoredPosition;
        }

        // public void Reflect(Ball ball)
        // {
        //     float y = BallHitPaddleWhere(ball.GetPosition(), rectTransform.anchoredPosition, rectTransform.sizeDelta.y / 2f);
        //     //Debug.Log("X: " + bounceDirection + " Y: " + y);
        //     ball.Reflect(new Vector2(bounceDirection, y));
        // }

        // activating the Cloud size boost for upper advantage
        public void ActivateCloudSizeBoost(float Multi, float Dura)
        {
            StopCoroutine("SizeBoostRoutine"); // Stopss any existing Cloud Boost
            StartCoroutine(CloudSizeBoostRoutine(Multi, Dura)); //Start it upp
            Debug.Log("Activating Cloud Size Boost");
        }

        // to handle the Cloud boost / scaling it in the size for advantage so seahorse can be smacked easier
        private IEnumerator CloudSizeBoostRoutine(float Multi, float Dura)
        {
            Debug.Log("Starting Cloud Size Boost Routine");
            Vector2 boostedSize = OGSize * Multi;
            rectTransform.sizeDelta = boostedSize;
            yield return new WaitForSeconds(Dura);
            rectTransform.sizeDelta = OGSize;
        }
    }
}
