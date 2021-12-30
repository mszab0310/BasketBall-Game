using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Population
{
    private int populationSize;
    private List<Individual> population;
    private List<Individual> children;
    private List<Tuple<int, int>> parents;

    public Population(int populationSize)
    {
        this.populationSize = populationSize;
        this.population = new List<Individual>(populationSize);
        this.children = new List<Individual>(populationSize / 2);
        this.parents = new List<Tuple<int, int>>(populationSize / 2);
    }

    public void generatePopulation()
    {
        for (int i = 0; i < this.populationSize; i++)
        {
            population.Add(new Individual(GetRandomVector2(), UnityEngine.Random.Range(-500f, 500f)));
        }
    }

    private Vector2 GetRandomVector2()
    {
        return new Vector2(UnityEngine.Random.Range(-15f, 15f), Random.Range(-15f, 15f));
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
        float recombFactor = 0.5f;
        int daddy;
        int mommy;
        for (int i = 0; i < populationSize / 2; i++)
        {
            mommy = Random.Range(0, populationSize);
            do
            {
                daddy = Random.Range(0, populationSize);
            } while (mommy != daddy);
            // [ (x,y), f ] = > [ ( 50% , 50% ), 50% ]
            float kidX;
            float kidY;
            float kidForce;
            float chance = Random.Range(0f, 1f); //the chance to get from one of the parents

            kidX = population[mommy].getDirection().x; //initialize with one and if the chance falls trough, swap it
            if (chance >= recombFactor)
                kidX = population[daddy].getDirection().x;

            chance = Random.Range(0f, 1f);

            kidY = population[daddy].getDirection().y;
            if (chance >= recombFactor)
                kidY = population[mommy].getDirection().y;

            chance = Random.Range(0f, 1f);

            kidForce = population[mommy].getForce();
            if (chance >= recombFactor)
                kidForce = population[daddy].getForce();
            children.Add(new Individual(new Vector2(kidX, kidY), kidForce));
            parents.Add(Tuple.Create(daddy, mommy));
        }
        
    }

    public void MutateChildren()
    {
        //Children will have a 25% chance of mutation
        //if the will mutate, the will have a 25% chance on each genome to mutate
        float mutationChance = 0.25f;
        float genomeChance = 0.25f;
        float mutate = Random.Range(0f, 1f);
        foreach (Individual child in children)
        {
            if (mutate <= mutationChance) //checks if the children will mutate or not
            {
                float genomeMutate = Random.Range(0f, 1f);
                if (genomeMutate <= genomeChance) //checks if the first genome will mutate or not
                {
                    child.setDirection(MutateDirection(child.getDirection()));
                }
                genomeMutate = Random.Range(0f, 1f);
                if (genomeMutate <= genomeChance) //checks if the second genom will mutate or not
                {
                    float randForce = Random.Range(-50f, 50f);
                    child.setForce(child.getForce() + randForce);
                }

            }
            mutate = Random.Range(0f, 1f);
        }
    }

    private Vector2 MutateDirection(Vector2 direction)
    {
        float dirCh = Random.Range(0f, 1f);

        float randX = Random.Range(-2f, 2f);
        float randY = Random.Range(-2f, 2f);

        if(dirCh >= 0.5f)
        {
            direction.x += randX;
        }

        dirCh = Random.Range(0f, 1f);

        if (dirCh <= 0.5f)
        {
            direction.y += randY;
        }
       
        return direction;
    }

    public List<Individual> GetChildren()
    {
        return this.children;
    }

    public int GetPopulationSize()
    {
        return this.populationSize;
    }

    

 }


   

