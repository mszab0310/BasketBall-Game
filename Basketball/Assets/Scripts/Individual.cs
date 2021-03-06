using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual 
{
    private Vector2 direction;
    private float force;
    private float fitness;
    private int age;

    public Individual(Vector2 direction, float force)
    {
        this.direction = direction;
        this.force = force;
        this.fitness = 0f;
        this.age = 0;
    }

    public float getForce()
    {
        return this.force;
    }

    public Vector2 getDirection()
    {
        return this.direction;
    }

    public int getAge()
    {
        return this.age;
    }

    public void setAge(int age)
    {
        this.age = age;
    }

    public void setForce(float force)
    {
        this.force = force;
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void setFitness(float fitness)
    {
        this.fitness = fitness;
    }
    
    public float getFitness()
    {
        return this.fitness;
    }
    public static int CompareIndividual(Individual a, Individual b)
    {
        if (a.getFitness() > b.getFitness())
            return 1;
        else
        if (a.getFitness() < b.getFitness())
            return -1;
        else
            return 0;
    }
    public bool Equals(Individual individual)
    {
        if(this.direction == individual.direction && this.force == individual.force)
        {
            return true;
        }
        return false;
    }
}
