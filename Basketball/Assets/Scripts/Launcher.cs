using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        string path = Directory.GetCurrentDirectory();
        path += "/Assets/Scripts/data/fitnesses.txt";
        File.Create(path).Close();
        StartCoroutine(Simulate());
    }

    public void PlayTopTen()
    {
        StartCoroutine(SimulateTopTen());
    }

    private IEnumerator Simulate()
    {
        for (int i = 0; i < 125; i++)
        {
            ScoreScript.scoreValue = 0;
            WriteAllToFile();
            ballFactoryScript.LaunchPopulation(population.GetPopulation(), population.GetPopulationSize(),false);
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
            ChildScore.scoreValue = 0;
            ballFactoryScript.LaunchPopulation(population.GetChildren(), population.GetChildren().Count,true);
            ballFactoryScript.Wait(population.GetChildren(), population.GetChildren().Count);

            yield return new WaitForSeconds(7.6f);

            population.Selection();
            Debug.Log("Generation " + i);
        }

        string path = Directory.GetCurrentDirectory();
        path += "/Assets/Scripts/top10.txt";
        WriteToFile(path);
       // bestIndividuals.Clear();
    }

    private IEnumerator SimulateTopTen()
    {
        
        Debug.Log("--------TOP---10-----");
        Debug.Log(Time.timeScale  + " - " + bestIndividuals.Count);
        ballFactoryScript.setTimeScale(1f);
        ScoreScript.scoreValue = 0;
        ChildScore.scoreValue = 0;
        for(int i = 0; i < bestIndividuals.Count; i++)
        {
           if(i != 0 && !bestIndividuals[i].Equals(bestIndividuals[i - 1]))
            {
                ballFactoryScript.LaunchBall(bestIndividuals[i]);
                yield return new WaitForSeconds(7.6f);
           }
        }        
        Debug.Log("-------------");
    }

    private void AddBestIndividualFromGeneration()
    {
        populationCopy = new List<Individual>(population.GetPopulation());
        populationCopy.Sort(Individual.CompareIndividual);
        //Adds the best element from one generation
        int i = 1;
        /*while(goodIndividuals.Contains(populationCopy[populationCopy.Count - i]) && i < populationCopy.Count)
        {
            i++;
        }*/
        goodIndividuals.Add(populationCopy[populationCopy.Count-i]);

    }
    private void BestFromTen()
    {
        goodIndividuals.Sort(Individual.CompareIndividual);
        bestIndividuals.Add(goodIndividuals[goodIndividuals.Count-1]);
        goodIndividuals.Clear();
      
    }

    private void WriteAllToFile()
    {
        string path = Directory.GetCurrentDirectory();
        path += "/Assets/Scripts/data/fitnesses.txt";
        using StreamWriter file = new StreamWriter(path, append: true);
        string fitnesses = "";
        foreach(Individual individual in population.GetPopulation())
        {
            fitnesses += individual.getFitness();
            fitnesses += " ";
        }
        file.WriteLine(fitnesses);
        file.Close();
    }

    private void WriteToFile(string path)
    {
        using StreamWriter file = new StreamWriter(path, append: false);
        for (int j = 0; j < bestIndividuals.Count - 1; j++)
        {
            file.WriteLine( bestIndividuals[j].getDirection() 
                + " " + bestIndividuals[j].getForce());
        }
        file.Write( bestIndividuals[bestIndividuals.Count - 1].getDirection() 
            + " " + bestIndividuals[bestIndividuals.Count - 1].getForce() );
        file.Close();
    }

}
