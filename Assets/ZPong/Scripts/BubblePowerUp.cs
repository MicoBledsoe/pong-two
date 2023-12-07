using UnityEngine;
using ZPong;

public class BubblePowerUp : MonoBehaviour
{
    public float duration = 5f; // Duration of the power-up effect

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                ball.ActivatePowerUp(duration);
            }
        }
    }
}

