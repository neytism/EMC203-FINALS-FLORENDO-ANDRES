using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BouncingBall : MonoBehaviour
{
    public static event Action GameLoaded;
    public static event Action<bool> IsBallCDUpdateEvent;
    public float speed = 5f;
    
    public float angleLimitY = 0.1f;
    public float angleLimitX = 0.1f;
    
    private Vector2 direction = new Vector2(1, 1);

    public float screenMinX;
    public float screenMaxX;
    public float screenMinY;
    public float screenMaxY;

    public PaddleMovement player1;
    public PaddleMovement player2;

    [SerializeField] private bool _canMove = false;


    private void OnEnable()
    {
        GameManager.GameReadyEvent += StartBounce;
        UIManager.RestartGameEvent += ResetBall;
        GameManager.SetBallSpeedEvent += SetBallSpeed;
    }
    
    private void OnDisable()
    {
        GameManager.GameReadyEvent -= StartBounce;
        UIManager.RestartGameEvent -= ResetBall;
        GameManager.SetBallSpeedEvent -= SetBallSpeed;
    }
    private void Start()
    {
        GameLoaded?.Invoke();
    }

    private void StartBounce()
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
        if(player1 != null) CheckCollisionWithPlayer(player1.transform, player1.Velocity());
        if(player2 != null) CheckCollisionWithPlayer(player2.transform, player2.Velocity());
    }

    void CheckBounce(float max, float min, Vector2 normal)
    {
        if ((normal == Vector2.up && (transform.position.y > max || transform.position.y < min)) ||
            (normal == Vector2.right && (transform.position.x > max || transform.position.x < min)))
        {
            float dot = Vector2.Dot(direction, normal);
            direction = direction - 2 * dot * normal;
            
            direction.y = Mathf.Clamp(direction.y, -0.7f, 0.7f);

            if (Mathf.Abs(direction.x) < angleLimitX)
            {
                direction.x = Mathf.Sign(direction.x) * angleLimitX;
            }

            // Correct position if out of bounds
            if (normal == Vector2.up)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y > max ? max : min);
            }
            else
            {
                transform.position = new Vector2(transform.position.x > max ? max : min, transform.position.y);
            }
            
            if (!SoundManager.Instance.SoundSfx.isPlaying)
            {
                SoundManager.Instance.PlayOnce(SoundManager.Sounds.BallHit);
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

            if (Mathf.Abs(direction.x) < angleLimitX)
            {
                direction.x = Mathf.Sign(direction.x) * angleLimitX;
            }

            // Correct position if inside the player
            if (normal == Vector2.up || normal == Vector2.down)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y > player.position.y ? player.position.y + player.localScale.y / 2 + 0.5f : player.position.y - player.localScale.y / 2 - 0.5f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x > player.position.x ? player.position.x + player.localScale.x / 2 + 0.5f : player.position.x - player.localScale.x / 2 - 0.5f, transform.position.y);
            }

            if (!SoundManager.Instance.SoundSfx.isPlaying)
            {
                SoundManager.Instance.PlayOnce(SoundManager.Sounds.BallHit);
            }
            
        }
    }

    private void RandomizeDirection()
    {
        direction = new Vector2(Random.Range(-0.9f, 0.9f), Random.Range(-0.4f, 0.4f));
        
        direction.y = Mathf.Clamp(direction.y, -0.9f, 0.9f); 

        if (Mathf.Abs(direction.x) < angleLimitX)
        {
            direction.x = Mathf.Sign(direction.x) * angleLimitX;
        }
    }
    

    IEnumerator BallCountDown()
    {
        yield return new WaitForSeconds(3);
        RandomizeDirection();
        IsBallCDUpdateEvent?.Invoke(false);
        _canMove = true;
    }
    
    public void ResetBall()
    {
        _canMove = false;
        transform.position = Vector3.zero;
        IsBallCDUpdateEvent?.Invoke(true);
        StartCoroutine(BallCountDown());
    }

    private void SetBallSpeed(int x)
    {
        
        switch (x)
        {
            case 1 :
                speed = 25f;
                break;
            case 2 :
                speed = 35;
                break;
            case 3 :
                speed = 45;
                break;
            default :
                speed = 25f;
                break;
        }

        ToggleTrail();
    }


    private void ToggleTrail()
    {
        if (GameManager.Instance.isEffectOn)
        {
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }
    
    

}
