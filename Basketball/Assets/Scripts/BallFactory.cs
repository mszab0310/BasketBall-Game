using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public Rigidbody2D ball;
    public List<Rigidbody2D> balls;
    public Ball ballscript;
    void Start()
    {
        CreateBalls(10);
        LaunchBalls(10);
    }

    private void CreateBalls(int count)
    {
        for(int i = 0; i < count; i++)
        {
            balls.Add(Instantiate(ball, new Vector2(ball.transform.position.x + i, ball.transform.position.y), Quaternion.identity));
            balls[i].isKinematic = true;
            
        }
    }

    private void LaunchBalls(int count)
    {
        foreach(Rigidbody2D ball in balls){
            ball.GetComponent<Ball>().LaunchBall(new Vector2(1, 1), 410);
        }
    }

    
}
