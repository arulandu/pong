using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PaddleType
{
    Left,
    Right
}

public class Paddle : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;
    public int score = 0;

    public PaddleType type;
    public float decForce, accForce;

    private int moveInput, rotInput;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool up = (type == PaddleType.Left && Input.GetKey(KeyCode.W)) ||
                  (type == PaddleType.Right && Input.GetKey(KeyCode.UpArrow));

        bool down = (type == PaddleType.Left && Input.GetKey(KeyCode.S)) ||
                    (type == PaddleType.Right && Input.GetKey(KeyCode.DownArrow));

        moveInput = (up ? 1 : 0) + (down ? -1 : 0);
        scoreTxt.text = score + "";
    }

    void FixedUpdate()
    {
        if (_rb.velocity.x != 0)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        bool decel = (_rb.velocity.y > 0 && moveInput == -1) || (_rb.velocity.y < 0 && moveInput == 1);
        _rb.AddForce(moveInput * (decel ? decForce : accForce) * Vector2.up, ForceMode2D.Impulse);
    }
}