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
    private Material pongMaterial;

    private bool hitWallLeft = false;
    private bool hitPaddleLeft = false;

    private bool hitWallRight = false;
    private bool hitPaddleRight = false;

    void Start()
    {
        this.pongCollider = GetComponent<Rigidbody2D>();
        this.pongMaterial = GetComponent<Renderer>().material;
        this.pongCollider.velocity = new Vector2(-10, 10);
    }

    void FixedUpdate()
    {
        // Send message to global state if necessary
        if (this.hitWallLeft)
        {
            if (this.hitPaddleLeft)
            {
                this.globalState.SendMessage("HitPaddleLeft");
                this.paddleLeft.gameObject.SendMessage("StartGlow");
            }
            else
            {
                this.globalState.SendMessage("HitWallLeft");
                this.wallLeft.gameObject.SendMessage("StartGlow");
            }
        }
        if (this.hitWallRight)
        {
            if (this.hitPaddleRight)
            {
                this.globalState.SendMessage("HitPaddleRight");
                this.paddleRight.gameObject.SendMessage("StartGlow");
            }
            else
            {
                this.globalState.SendMessage("HitWallRight");
                this.wallRight.gameObject.SendMessage("StartGlow");
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
            collider.gameObject.SendMessage("StartGlow");
        }
        else if (collider.gameObject == this.wallBottom)
        {
            normal = Vector2.up;
            collider.gameObject.SendMessage("StartGlow");
        }
        else if (collider.gameObject == this.wallLeft)
            normal = Vector2.right;
        else if (collider.gameObject == this.wallRight)
            normal = Vector2.left;

        if (normal != Vector2.zero)
        {
            // Change direction
            this.pongCollider.velocity = Vector2.Reflect(this.pongCollider.velocity, normal);
            this.gameObject.SendMessage("StartGlow");
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
}
