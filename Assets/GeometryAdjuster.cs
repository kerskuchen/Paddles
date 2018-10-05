using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryAdjuster : MonoBehaviour
{
    // We want to have corners at the points where adjacent walls meet so we have to adjust the
    // vertices a little
    void Start()
    {
        // Left wall
        GameObject leftWall = GameObject.Find("Left");
        BoxCollider2D leftWallCollider = leftWall.GetComponent<BoxCollider2D>();
        Mesh leftWallMesh = leftWall.GetComponent<MeshFilter>().mesh;

        leftWall.transform.localScale = Vector3.one;
        leftWallCollider.size = new Vector2(0.5f, 12.0f);
        leftWallMesh.vertices = new Vector3[] {
            new Vector3(-0.25f, -6.5f, 0.0f),
            new Vector3(0.25f, 6.0f, 0.0f),
            new Vector3(0.25f, -6.0f, 0.0f),
            new Vector3(-0.25f, 6.5f, 0.0f),
        };

        // Right wall
        GameObject rightWall = GameObject.Find("Right");
        BoxCollider2D rightWallCollider = rightWall.GetComponent<BoxCollider2D>();
        Mesh rightWallMesh = rightWall.GetComponent<MeshFilter>().mesh;

        rightWall.transform.localScale = Vector3.one;
        rightWallCollider.size = new Vector2(0.5f, 12.0f);
        rightWallMesh.vertices = new Vector3[] {
            new Vector3(-0.25f, -6.0f, 0.0f),
            new Vector3(0.25f, 6.5f, 0.0f),
            new Vector3(0.25f, -6.5f, 0.0f),
            new Vector3(-0.25f, 6.0f, 0.0f),
        };

        // Top wall
        GameObject topWall = GameObject.Find("Top");
        BoxCollider2D topWallCollider = topWall.GetComponent<BoxCollider2D>();
        Mesh topWallMesh = topWall.GetComponent<MeshFilter>().mesh;

        topWall.transform.localScale = Vector3.one;
        topWallCollider.size = new Vector2(21.0f, 0.5f);
        topWallMesh.vertices = new Vector3[] {
            new Vector3(-10.0f, -0.25f, 0.0f),
            new Vector3(10.5f, 0.25f, 0.0f),
            new Vector3(10.0f, -0.25f, 0.0f),
            new Vector3(-10.5f, 0.25f, 0.0f),
        };

        // Bottom wall
        GameObject bottomWall = GameObject.Find("Bottom");
        BoxCollider2D bottomWallCollider = bottomWall.GetComponent<BoxCollider2D>();
        Mesh bottomWallMesh = bottomWall.GetComponent<MeshFilter>().mesh;

        bottomWall.transform.localScale = Vector3.one;
        bottomWallCollider.size = new Vector2(21.0f, 0.5f);
        bottomWallMesh.vertices = new Vector3[] {
            new Vector3(-10.5f, -0.25f, 0.0f),
            new Vector3(10.0f, 0.25f, 0.0f),
            new Vector3(10.5f, -0.25f, 0.0f),
            new Vector3(-10.0f, 0.25f, 0.0f),
        };


        // Left paddle 
        GameObject leftPaddle = GameObject.Find("PaddleLeft");
        BoxCollider2D leftPaddleCollider = leftPaddle.GetComponent<BoxCollider2D>();
        Mesh leftPaddleMesh = leftPaddle.GetComponent<MeshFilter>().mesh;

        leftPaddle.transform.localScale = Vector3.one;
        leftPaddleCollider.size = new Vector2(0.5f, 4.0f);
        leftPaddleMesh.vertices = new Vector3[] {
            new Vector3(-0.25f, -1.25f, 0.0f),
            new Vector3(0.25f, 1.75f, 0.0f),
            new Vector3(0.25f, -1.75f, 0.0f),
            new Vector3(-0.25f, 1.25f, 0.0f),
        };

        // Right paddle 
        GameObject rightPaddle = GameObject.Find("PaddleRight");
        BoxCollider2D rightPaddleCollider = rightPaddle.GetComponent<BoxCollider2D>();
        Mesh rightPaddleMesh = rightPaddle.GetComponent<MeshFilter>().mesh;

        rightPaddle.transform.localScale = Vector3.one;
        rightPaddleCollider.size = new Vector2(0.5f, 4.0f);
        rightPaddleMesh.vertices = new Vector3[] {
            new Vector3(-0.25f, -1.75f, 0.0f),
            new Vector3(0.25f, 1.25f, 0.0f),
            new Vector3(0.25f, -1.25f, 0.0f),
            new Vector3(-0.25f, 1.75f, 0.0f),
        };
    }
}
