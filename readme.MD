# Self learning Basketball Game

## Basketball Game made with Unity

A simple 2D basketball game where the only challenge for the regular player is to score a point (yes, one point, it's not real basketball). It is pretty challenging, time to bring out your inner Michael Jordan.

## Self learning with a genetic algorithm

If you got tired of being bad at the game, no worries, there is a solution for the computer to play with itself ( ͡° ͜ʖ ͡°). Using a parameterized genetic algorithm by the launch force and direction of the basketball, the algorithm will find the perfect pairing to score a point and to be better than you.

![](basket.gif)

# Some interesting results

Having played around with four types of fitnesses (so far) all of them being more restrictive than the other, we've got some intersting results of the evolution of the fitness varying based on the impact of different evolutionary operators. Here you can see some of the fitnesses graphed for the same number of generations but with different mutation rates:

Collision fitness
![](collisionFitness.png)

Collision fitness with lower mutation
![](collisionFitnessLowerMutation.png) |

# Technical details

If you want to try this project on your PC you will need to have [Unity 2020.3.21f1](https://unity3d.com/get-unity/download?thank-you=update&download_nid=65278&os=Win) installed on your PC and any version of Visual Studio.
