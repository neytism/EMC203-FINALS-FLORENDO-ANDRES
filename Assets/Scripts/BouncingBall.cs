using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BouncingBall : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = new Vector2(1, 1);

    public float screenMinX;
    public float screenMaxX;
    public float screenMinY;
    public float screenMaxY;

    public PaddleMovement player1;
    public PaddleMovement player2;

    [SerializeField] private bool _canMove = false;

    private void Start()
    {
        StartCoroutine(BallCountDown());
    }

    void FixedUpdate()
    {
        // Move the ball
        if (!_canMove) return;
        
        Vector2 move = direction.normalized * (speed * Time.fixedDeltaTime);
        transform.Translate(move);

        // Limit the ball's speed
        if (move.magnitude > 1)
        {
            move = move.normalized;
        }

        // Check for bounce
        CheckBounce(screenMaxY, screenMinY, Vector2.up);
        CheckBounce(screenMaxX, screenMinX, Vector2.right);

        // Check for collision with players using AABB
        CheckCollisionWithPlayer(player1.transform, player1.Velocity());
        CheckCollisionWithPlayer(player2.transform, player2.Velocity());
    }

    void CheckBounce(float max, float min, Vector2 normal)
    {
        if ((normal == Vector2.up && (transform.position.y > max || transform.position.y < min)) ||
            (normal == Vector2.right && (transform.position.x > max || transform.position.x < min)))
        {
            float dot = Vector2.Dot(direction, normal);
            direction = direction - 2 * dot * normal;
            
            direction.y = Mathf.Clamp(direction.y, -0.9f, 0.9f);

            // Correct position if out of bounds
            if (normal == Vector2.up)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y > max ? max : min);
            }
            else
            {
                transform.position = new Vector2(transform.position.x > max ? max : min, transform.position.y);
            }
        }
    }

    void CheckCollisionWithPlayer(Transform player, Vector2 playerVelocity)
    {
        Vector2 playerMin = (Vector3)player.position - player.localScale / 2;
        Vector2 playerMax = (Vector3)player.position + player.localScale / 2;
        Vector2 ballMin = (Vector2)transform.position - Vector2.one / 2; 
        Vector2 ballMax = (Vector2)transform.position + Vector2.one / 2;

        if (playerMin.x < ballMax.x && playerMax.x > ballMin.x && playerMin.y < ballMax.y && playerMax.y > ballMin.y)
        {
            Vector2 normal;
            float dx = Mathf.Min(playerMax.x - ballMin.x, ballMax.x - playerMin.x);
            float dy = Mathf.Min(playerMax.y - ballMin.y, ballMax.y - playerMin.y);
            if (dx > dy)
            {
                normal = transform.position.y > player.position.y ? Vector2.up : Vector2.down;
            }
            else
            {
                normal = transform.position.x > player.position.x ? Vector2.right : Vector2.left;
            }

            // Reflect the direction and add player's velocity
            float dot = Vector2.Dot(direction, normal);
            direction = direction - 2 * dot * normal + (playerVelocity.normalized * 0.5f);
            
            direction.y = Mathf.Clamp(direction.y, -0.9f, 0.9f); //limit  he angle and speed

            // Correct position if inside the player
            if (normal == Vector2.up || normal == Vector2.down)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y > player.position.y ? player.position.y + player.localScale.y / 2 + 0.5f : player.position.y - player.localScale.y / 2 - 0.5f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x > player.position.x ? player.position.x + player.localScale.x / 2 + 0.5f : player.position.x - player.localScale.x / 2 - 0.5f, transform.position.y);
            }
        }
    }

    private void RandomizeDirection()
    {
        direction = new Vector2(Random.Range(-0.9f, 0.9f), Random.Range(-0.5f, 0.5f));
    }

    IEnumerator BallCountDown()
    {
        yield return new WaitForSeconds(3);
        RandomizeDirection();
        _canMove = true;
    }

    public void ResetBall()
    {
        _canMove = false;
        transform.position = Vector3.zero;
        StartCoroutine(BallCountDown());
    }
    


}
