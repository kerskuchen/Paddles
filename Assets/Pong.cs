using UnityEngine;

public class Pong : MonoBehaviour
{
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;

    private Rigidbody2D pongCollider;

    void Start()
    {
        this.pongCollider = GetComponent<Rigidbody2D>();
        this.pongCollider.velocity = new Vector2(-10, 10);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector2 normal = Vector2.zero;
        if (collider.gameObject == this.topWall)
            normal = Vector2.down;
        else if (collider.gameObject == this.bottomWall)
            normal = Vector2.up;
        else if (collider.gameObject == this.leftWall)
            normal = Vector2.right;
        else if (collider.gameObject == this.rightWall)
            normal = Vector2.left;
        else
            throw new UnityException("Unknown Collider");

        if (normal != Vector2.zero)
        {
            this.pongCollider.velocity = Vector2.Reflect(this.pongCollider.velocity, normal);
        }
    }
}
