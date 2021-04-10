using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class Ball : MonoBehaviour
{
    public float launchSpeed = 5f;
    public float spawnPad = 15f;
    public float speedBump, maxSpeed;
    
    private Rigidbody2D _rb;
    private Vector2 prevVel;
    private bool playable;
    private int cols;

    private GameManager _manager;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _manager = FindObjectOfType<GameManager>();
        
        float rad = Mathf.Deg2Rad * (Random.Range(spawnPad, 90 - spawnPad) + 90*Random.Range(0,4));
        _rb.velocity = launchSpeed * new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    private void FixedUpdate()
    {
        prevVel = _rb.velocity;
        if (Mathf.Abs(_rb.position.x) < _manager.paddleDist) playable = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        cols++;
        CheckScored();
        Vector2 normal = other.GetContact(0).normal;
        Vector2 outVel = prevVel - 2 * Vector2.Dot(normal, prevVel) * normal;
        _rb.velocity = outVel.normalized * Mathf.Min(prevVel.magnitude + speedBump, maxSpeed);
    }

    private void CheckScored()
    {
        if (Mathf.Abs(_rb.position.x) > _manager.paddleDist && playable)
        {
            playable = false;
            if (_rb.position.x > _manager.paddleDist)
            {
                _manager.AddPoint(PaddleType.Left, 1);
            }
            else
            {
                _manager.AddPoint(PaddleType.Right, 1);
            }
        }
    }
}
