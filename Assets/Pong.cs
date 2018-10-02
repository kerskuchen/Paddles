using UnityEngine;

public class Pong : MonoBehaviour
{
    public GameObject wallTop;
    public GameObject wallBottom;
    public GameObject wallLeft;
    public GameObject wallRight;

    public GameObject paddleLeft;
    public GameObject paddleRight;

    public GameObject globalState;

    private Rigidbody2D pongCollider;

    private bool hitWallLeft = false;
    private bool hitPaddleLeft = false;

    private bool hitWallRight = false;
    private bool hitPaddleRight = false;

    private MatchState matchState;
    private AudioSource sound;

    private const float BASE_PONG_X_SPEED = 10.0f;

    void Start()
    {
        this.sound = this.GetComponent<AudioSource>();
        this.matchState = this.globalState.GetComponent<MatchState>();
        ResetPosition();
        StartMoving();
    }

    public void ResetPosition()
    {
        this.transform.position = new Vector2(0, 0);
        this.pongCollider = GetComponent<Rigidbody2D>();
        this.pongCollider.velocity = new Vector2(0, 0);
    }

    public void StartMoving()
    {
        this.pongCollider = GetComponent<Rigidbody2D>();
        float yVel = Random.Range(0.1f, 0.5f);
        this.pongCollider.velocity = BASE_PONG_X_SPEED * new Vector2(1, yVel);
    }

    void FixedUpdate()
    {
        // Send message to global state if necessary
        if (this.hitWallLeft)
        {
            if (this.hitPaddleLeft)
            {
                this.matchState.HitPaddleLeft();
                this.paddleLeft.GetComponent<Glower>().StartGlow();
                this.AdjustVelocityAfterPaddleHit(paddleLeft.transform.position);
            }
            else
            {
                this.matchState.HitWallLeft();
                this.wallLeft.GetComponent<Glower>().StartGlow();
            }
        }
        if (this.hitWallRight)
        {
            if (this.hitPaddleRight)
            {
                this.matchState.HitPaddleRight();
                this.paddleRight.GetComponent<Glower>().StartGlow();
                this.AdjustVelocityAfterPaddleHit(paddleRight.transform.position);
            }
            else
            {
                this.matchState.HitWallRight();
                this.wallRight.GetComponent<Glower>().StartGlow();
            }
        }

        // Reset flags
        hitWallLeft = false;
        hitPaddleLeft = false;

        hitWallRight = false;
        hitPaddleRight = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Handle collision response
        Vector2 normal = Vector2.zero;
        if (collider.gameObject == this.wallTop)
        {
            normal = Vector2.down;
            collider.GetComponent<Glower>().StartGlow();
        }
        else if (collider.gameObject == this.wallBottom)
        {
            normal = Vector2.up;
            collider.GetComponent<Glower>().StartGlow();
        }
        else if (collider.gameObject == this.wallLeft)
            normal = Vector2.right;
        else if (collider.gameObject == this.wallRight)
            normal = Vector2.left;

        if (normal != Vector2.zero)
        {
            // Change direction
            this.pongCollider.velocity = Vector2.Reflect(this.pongCollider.velocity, normal);

            this.GetComponent<Glower>().StartGlow();
            //sound.pitch = Random.Range(0.99f, 1.01f);
            //sound.PlayOneShot(sound.clip);
        }

        // Handle scoring flags
        if (collider.gameObject == this.wallLeft)
            this.hitWallLeft = true;
        else if (collider.gameObject == this.paddleLeft)
            this.hitPaddleLeft = true;
        else if (collider.gameObject == this.wallRight)
            this.hitWallRight = true;
        else if (collider.gameObject == this.paddleRight)
            this.hitPaddleRight = true;
    }

    // Adjust direction depending on wheter we hit the center or the edge of the paddle
    void AdjustVelocityAfterPaddleHit(Vector2 paddlePos)
    {
        float yVel = this.transform.position.y - paddlePos.y;
        Vector2 newVel = new Vector2(this.pongCollider.velocity.x, 5 * yVel);
        this.pongCollider.velocity = newVel;
    }
}
