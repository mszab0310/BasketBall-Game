using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public Rigidbody2D ball;
    public List<Rigidbody2D> balls;
    public List<Ball> ballscripts;
    private int _inputSize = 0;
    private Vector2[] _directions = new Vector2[100];
    private float[] _launchForces = new float[100];
    void Start()
    {
       /* ScoreScript.scoreValue = 0;
        CreateBalls(10);
        SetLaunch();
        LaunchBalls(10);
        StartCoroutine(WaitFiveSeconds());*/
        
    }

    public void CreateBalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            balls.Add(Instantiate(ball, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity) as Rigidbody2D);
            ballscripts.Add(balls[i].GetComponent<Ball>());
              
        }
    }

    public void Wait()
    {
        StartCoroutine(WaitFiveSeconds());
    }

    private void LaunchBalls(int count)
    {
        int i = 0;
        foreach (Rigidbody2D ball in balls)
        {
            ballscripts[i].LaunchBall(_directions[i], _launchForces[i], i);
            i++;
        }

    }

    public void LaunchPopulation(List<Individual> population, int populationSize)
    {
        for(int i = 0; i < populationSize; i++)
        {
            ballscripts[i].LaunchBall(population[i].getDirection(), population[i].getForce(), i);
        }
    }

    private void SetLaunch()
    {
        string path = Directory.GetCurrentDirectory();
        path += "/Assets/Scripts/input.txt";
        ReadTextFile(path);
    }

    private void ReadTextFile(string filePath)
    {
        StreamReader streamReader = new StreamReader(filePath);
        while (!streamReader.EndOfStream)
        {
            string input = streamReader.ReadLine();
            string[] words = input.Split(' ');
            _directions[_inputSize].x = (float)System.Convert.ToDouble(words[0]);
            _directions[_inputSize].y = (float)System.Convert.ToDouble(words[1]);
            _launchForces[_inputSize] = (float)System.Convert.ToDouble(words[2]);
           
            _inputSize++;
        }
        streamReader.Close();
    }

    private IEnumerator WaitFiveSeconds()
    {
        yield return new WaitForSeconds(7.5f);
        foreach (Rigidbody2D ball in balls)
        {
            Debug.Log("Ball " + ball.GetComponent<Ball>().GetIndex() + ": " + ball.GetComponent<Ball>().GetMinDistanceFromHoop() +
                " Fitness: " + ball.GetComponent<Ball>().GetFitness());
        }
    }
}
