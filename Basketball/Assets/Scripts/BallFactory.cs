using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public Rigidbody2D ball;
    public List<Rigidbody2D> balls;
    public List<Ball> ballscripts;
    void Start()
    {
        
    }

    public void CreateBalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            balls.Add(Instantiate(ball, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity) as Rigidbody2D);
            ballscripts.Add(balls[i].GetComponent<Ball>());
              
        }
    }

    public void Wait(List<Individual> population, int populationSize)
    {
        StartCoroutine(WaitFiveSeconds(population,populationSize));
    }

    public void ResetSimulation()
    {
        balls.Clear();
        ballscripts.Clear();
    }


    public void LaunchPopulation(List<Individual> population, int populationSize)
    {
        for(int i = 0; i < populationSize; i++)
        {
            ballscripts[i].LaunchBall(population[i].getDirection(), population[i].getForce(), i);
        }
    }

    public void LaunchBall(Individual ball)
    {
        ballscripts[0].LaunchBall(ball.getDirection(), ball.getForce(),0);
    }

    public void setTimeScale(float timeScale)
    {
        foreach(Ball ball in ballscripts)
        {
            ball.setTimeScale(timeScale);
        }
    }
    private IEnumerator WaitFiveSeconds(List<Individual> population, int populationSize)
    {
        yield return new WaitForSeconds(7.5f);
        for(int i = 0;i < populationSize; i++)
        {
            population[i].setFitness(ballscripts[i].GetFitness());
            Debug.Log(population[i].getFitness() + " <> " + i);
            
        }
    }
}
