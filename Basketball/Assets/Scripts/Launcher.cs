using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ballFactory;
    private Population population;
    private BallFactory ballFactoryScript;

    void Awake()
    {
        population = new Population(100);
        population.generatePopulation();
        ballFactoryScript = ballFactory.GetComponent<BallFactory>();
        ballFactoryScript.CreateBalls(population.GetPopulationSize());
    }

    public void StartSimulation()
    {
        StartCoroutine(Simulate());
    }

    private IEnumerator Simulate()
    {
        for (int i = 0; i < 1000; i++)
        {
            ScoreScript.scoreValue = 0;
            ballFactoryScript.LaunchPopulation(population.GetPopulation(), population.GetPopulationSize());
            ballFactoryScript.Wait(population.GetPopulation(), population.GetPopulationSize());

            yield return new WaitForSeconds(7.6f);

            population.Recombinate();
            Debug.Log("Childrn");
            population.MutateChildren();

            ballFactoryScript.LaunchPopulation(population.GetChildren(), population.GetChildren().Count);
            ballFactoryScript.Wait(population.GetChildren(), population.GetChildren().Count);

            yield return new WaitForSeconds(7.6f);

            population.Selection();
            Debug.Log("Generation " + i);
        }
    }
}
