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
    }

    public void StartSimulation()
    {
        ScoreScript.scoreValue = 0;
        ballFactoryScript.CreateBalls(population.GetPopulationSize());
        ballFactoryScript.LaunchPopulation(population.GetPopulation(), population.GetPopulationSize());
        ballFactoryScript.Wait(population.GetPopulation(),population.GetPopulationSize());
    }
}
