using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual 
{
    private Vector2 direction;
    private float force;

    public Individual(Vector2 direction, float force)
    {
        this.direction = direction;
        this.force = force;
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

}
