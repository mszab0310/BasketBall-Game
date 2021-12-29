using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population
{
    private int populationSize;
    private List<Individual> population;
    private List<Individual> children;

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


    public void Recombinate()
    {
        int daddy;
        int mommy = Random.Range(0,populationSize);
        do
        {
            daddy = Random.Range(0, populationSize);
        } while ( mommy != daddy);
        // [ (x,y), f ] = > [ ( 50% , 50% ), 50% ]
        float kidX;
        float kidY;
        float kidForce;
        float chance = Random.Range(0f, 1f);

        kidX = population[mommy].getDirection().x;
        if (chance >= 0.5f)
            kidX = population[daddy].getDirection().x;
        
        chance = Random.Range(0f, 1f);

        kidY = population[daddy].getDirection().y;
        if (chance >= 0.5f)
            kidY = population[mommy].getDirection().y;
        
        chance = Random.Range(0f,1f);

        kidForce = population[mommy].getForce();
        if(chance >= 0.5f)
            kidForce = population[daddy].getForce();
        
    }

    public int GetPopulationSize()
    {
        return this.populationSize;
    }

 }


   

