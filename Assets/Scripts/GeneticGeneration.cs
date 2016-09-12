using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GeneticGeneration {

    // HHH SpSpSp StStSt

    public static List<IAIGuy> BreedGeneration(List<IAIGuy> population)
    {
        population.Sort((x, y) => y.fitness.CompareTo(x.fitness));

        List<IAIGuy> nextGen = new List<IAIGuy>();
        int children = population.Count;

        BitArray p1;
        BitArray p2;

        while(nextGen.Count < children)
        {
            IAIGuy c1;
            IAIGuy c2;

            // incase there are an odd number of guys, just keep the last guy;
            if(population.Count == 1)
            {
                c1 = population[0];
                nextGen.Add(c1);
                population.Remove(c1);
                continue;
            }

            c1 = population[0];
            c2 = population[1];
            p1 = c1.Genes;
            p2 = c2.Genes;

            // crossover - turn this random
            c1.Genes[0] = p1[0];
            c1.Genes[1] = p1[1];
            c1.Genes[2] = p1[2];
            c1.Genes[3] = p2[3];
            c1.Genes[4] = p2[4];
            c1.Genes[5] = p2[5];
            c1.Genes[6] = p1[6];
            c1.Genes[7] = p1[7];
            c1.Genes[8] = p1[8];

            c2.Genes[0] = p2[0];
            c2.Genes[1] = p2[1];
            c2.Genes[2] = p2[2];
            c2.Genes[3] = p1[3];
            c2.Genes[4] = p1[4];
            c2.Genes[5] = p1[5];
            c2.Genes[6] = p2[6];
            c2.Genes[7] = p2[7];
            c2.Genes[8] = p2[8];

            // 2 percent chance of mutation
            if (Random.Range(0f,1f) < 0.02)
            {
                int chrom = Random.Range(0, 7);

                c1.Genes[chrom] = !c1.Genes[chrom];
            }

            nextGen.Add(c1);
            nextGen.Add(c2);
            population.Remove(c1);
            population.Remove(c2);
        }

        return nextGen;
    }
}
