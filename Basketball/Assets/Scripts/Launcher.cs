using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ballFactory;
    private Population population;
    private List<Individual> populationCopy;
    private List<Individual> bestIndividuals;
    private List<Individual> goodIndividuals;
    private BallFactory ballFactoryScript;

    void Awake()
    {
        population = new Population(100);
        population.generatePopulation();
        ballFactoryScript = ballFactory.GetComponent<BallFactory>();
        ballFactoryScript.CreateBalls(population.GetPopulationSize());
        goodIndividuals = new List<Individual>();
        bestIndividuals = new List<Individual>();
    }

    public void StartSimulation()
    {
        StartCoroutine(Simulate());
    }

    private IEnumerator Simulate()
    {
        for (int i = 0; i < 100; i++)
        {
            ScoreScript.scoreValue = 0;
            ballFactoryScript.LaunchPopulation(population.GetPopulation(), population.GetPopulationSize());
            ballFactoryScript.Wait(population.GetPopulation(), population.GetPopulationSize());
            

            yield return new WaitForSeconds(7.6f);

            AddBestIndividualFromGeneration();

            if (i % 10 == 9)
            {
                BestFromTen();
            }

            population.Recombinate();
            Debug.Log("Childrn");
            population.MutateChildren();

            ballFactoryScript.LaunchPopulation(population.GetChildren(), population.GetChildren().Count);
            ballFactoryScript.Wait(population.GetChildren(), population.GetChildren().Count);

            yield return new WaitForSeconds(7.6f);

            population.Selection();
            Debug.Log("Generation " + i);
        }
        for (int j = 0; j < goodIndividuals.Count; j++)
        {
            Debug.Log("Good individuals " + j + ": " + goodIndividuals[j].getFitness());
        }
        for (int j = 0; j < bestIndividuals.Count; j++)
        {
            Debug.Log("Best individuals " + j + ": " + bestIndividuals[j].getFitness());
        }
    }

    private void AddBestIndividualFromGeneration()
    {
        populationCopy = new List<Individual>(population.GetPopulation());
        populationCopy.Sort(Individual.CompareIndividual);
        //Adds the best element from one generation
        goodIndividuals.Add(populationCopy[populationCopy.Count-1]);
       
    }
    private void BestFromTen()
    {
        goodIndividuals.Sort(Individual.CompareIndividual);
        bestIndividuals.Add(goodIndividuals[goodIndividuals.Count-1]);
      
    }
}
