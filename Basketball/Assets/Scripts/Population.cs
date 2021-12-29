using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population
{
    private int populationSize;
    private List<Individual> population;

    public Population(int populationSize)
    {
        this.populationSize = populationSize;
        this.population = new List<Individual>(populationSize);
    }

    public void generatePopulation()
    {
        for (int i = 0; i < this.populationSize; i++)
        {
            population.Add(new Individual(GetRandomVector2(), Random.Range(-500f, 500f)));
        }
    }

    private Vector2 GetRandomVector2()
    {
        return new Vector2(Random.Range(-15f, 15f), Random.Range(-15f, 15f));
    }

    public List<Individual> GetPopulation()
    {
        return this.population;
    }

    public void SortPopulation()
    {
        this.population.Sort(CompareIndividual);
    }

    public int CompareIndividual(Individual a, Individual b)
    {
        if (a.getFitness() > b.getFitness())
            return 1;
        else
        if (a.getFitness() < b.getFitness())
            return -1; 
        else
            return 0;
    }

    public int GetPopulationSize()
    {
        return this.populationSize;
    }

 }


   

