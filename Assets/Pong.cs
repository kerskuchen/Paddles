using UnityEngine;

public class Pong : MonoBehaviour
{
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
    private const float BASE_PONG_X_MAX_SPEED = 50.0f;

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
        // If we hit both a wall and a paddle, we only count the hit for the paddle
        if (this.hitWallLeft && this.hitPaddleLeft)
            this.hitWallLeft = false;
        if (this.hitWallRight && this.hitPaddleRight)
            this.hitWallRight = false;

        if (this.hitPaddleLeft)
        {
            this.matchState.HitPaddleLeft();
            this.paddleLeft.GetComponent<Glower>().StartGlow();
            this.AdjustVelocityAfterPaddleHit(paddleLeft.transform.position);
        }
        if (this.hitPaddleRight)
        {
            this.matchState.HitPaddleRight();
            this.paddleRight.GetComponent<Glower>().StartGlow();
            this.AdjustVelocityAfterPaddleHit(paddleRight.transform.position);
        }
        if (this.hitWallLeft || this.hitWallRight)
        {
            // Reset x speed to base speed when hitting wall
            this.pongCollider.velocity =
                new Vector2(BASE_PONG_X_SPEED * Mathf.Sign(this.pongCollider.velocity.x),
                            this.pongCollider.velocity.y);

            if (this.hitWallLeft)
            {
                this.matchState.HitWallLeft();
                this.wallLeft.GetComponent<Glower>().StartGlow();
            }
            if (this.hitWallRight)
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


        // If we were faded out, we move the ball back to the origin
        if (this.GetComponent<Glower>().IsInvisible())
        {
            ResetPosition();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponent<Glower>().StartGlow();
        // sound.PlayOneShot(sound.clip);

        if (collision.gameObject == this.wallLeft)
            this.hitWallLeft = true;
        else if (collision.gameObject == this.paddleLeft)
            this.hitPaddleLeft = true;
        else if (collision.gameObject == this.wallRight)
            this.hitWallRight = true;
        else if (collision.gameObject == this.paddleRight)
            this.hitPaddleRight = true;
        else
            // We hit a top/bottom wall
            collision.gameObject.GetComponent<Glower>().StartGlow();
    }

    // Adjust reflection direction depending on wheter we hit the center or the edge of the paddle
    void AdjustVelocityAfterPaddleHit(Vector2 paddlePos)
    {
        float yVel = this.transform.position.y - paddlePos.y;
        float xVel = ClampAbsolute(1.1f * this.pongCollider.velocity.x, BASE_PONG_X_MAX_SPEED);
        Vector2 newVel = new Vector2(xVel, 5 * yVel);
        this.pongCollider.velocity = newVel;
    }

    float ClampAbsolute(float value, float absoluteMax)
    {
        float absVal = Mathf.Clamp(Mathf.Abs(value), 0, absoluteMax);
        return Mathf.Sign(value) * absVal;
    }
}
