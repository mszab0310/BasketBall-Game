using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual 
{
    private Vector2 direction;
    private float force;
    private float fitness;

    public Individual(Vector2 direction, float force)
    {
        this.direction = direction;
        this.force = force;
        this.fitness = 0f;
    }

    public float getForce()
    {
        return this.force;
    }

    public Vector2 getDirection()
    {
        return this.direction;
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

}
