using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float paddleDist = 5f;

    private Paddle _leftPaddle;
    private Paddle _rightPaddle;
    // Start is called before the first frame update
    void Start()
    {
        Paddle[] paddles = FindObjectsOfType<Paddle>();
        foreach(Paddle paddle in paddles)
        {
            if (paddle.type == PaddleType.Left)
            {
                _leftPaddle = paddle;
            }

            if (paddle.type == PaddleType.Right)
            {
                _rightPaddle = paddle;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(PaddleType type, int num)
    {
        if (type == PaddleType.Left)
        {
            _leftPaddle.score += num;
        }

        if (type == PaddleType.Right)
        {
            _rightPaddle.score += num;
        }
    }
}
