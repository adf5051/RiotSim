using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public struct Observation
{
    public int health;
    public int strength;
    public bool carryingBarrier;
    public bool fight;
}


public class PoliceBayes
{

    double sqrt2PI = Math.Sqrt(2.0 * Math.PI);

    List<Observation> obsTab = new List<Observation>();

    int[] healthSum = new int[2];
    int[] healthSumSq = new int[2];
    double[] healthMean = new double[2];
    double[] healthStdDev = new double[2];

    int[] stSum = new int[2];
    int[] stSumSq = new int[2];
    double[] stMean = new double[2];
    double[] stStdDev = new double[2];

    int[,] hasBarrier = new int[2, 2];
    double[,] barrierprp = new double[2, 2];

    int[] fightct = new int[2];
    double[] fightprp = new double[2];

    public PoliceBayes()
    {
        ResetStats();
    }

    void ResetStats()
    {
        for (int i = 0; i < healthSum.Length; i++)
        {
            healthSum[i] = 0;
            healthSumSq[i] = 0;
        }

        for (int i = 0; i < stSum.Length; i++)
        {
            stSum[i] = 0;
            stSumSq[i] = 0;
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                hasBarrier[i, j] = 0;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            fightct[i] = 0;
        }
    }

    // Read the observations from text file fName.txt and add them to the list
    public void ReadObsTab(string fName)
    {
        try
        {
            using (StreamReader rdr = new StreamReader(Application.dataPath + "/" + fName))
            {
                string lineBuf = null;
                while ((lineBuf = rdr.ReadLine()) != null)
                {
                    string[] lineAra = lineBuf.Split(' ');

                    // Map strings to correct data types for conditions & action
                    // and Add the observation to List obsTab
                    AddObs(int.Parse(lineAra[0]), int.Parse(lineAra[1]), (lineAra[2] == "True" ? true : false), (lineAra[3] == "True" ? true : false));
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("Problem reading and/or parsing observations in " + Application.dataPath + "/" + fName);
            Debug.Log(e.Message);
            //Environment.Exit(-1);
        }
    }

    // Add an observation to the list.
    // Used when reading the file and when adding new observations on the fly
    public void AddObs(int health, int strength, bool barrier, bool fight)
    {
        // Build an Observation struct
        Observation obs;
        obs.health = health;
        obs.strength = strength;
        obs.carryingBarrier = barrier;
        obs.fight = fight;

        // Add it to the List
        obsTab.Add(obs);
    }

    // Dump obsTab to text file fName so it can be read next time
    public void Tab2File(string fName)
    {
        try
        {
            using (StreamWriter wtr = new StreamWriter(Application.dataPath + "/" + fName))
            {
                foreach (Observation obs in obsTab)
                {
                    wtr.Write("{0}", obs.health);
                    wtr.Write(" {0}", obs.strength);
                    wtr.Write(" {0}", obs.carryingBarrier);
                    wtr.WriteLine(" {0}", obs.fight);
                }
            }
        }
        catch
        {
            Debug.Log("Problem writing out the Observations to " + fName);
            Debug.Log("File not changed.");
        }
    }

    public void BuildStats()
    {
        ResetStats();

        foreach(Observation obs in obsTab)
        {
            int fightOff = obs.fight ? 0 : 1;

            healthSum[fightOff] += obs.health;
            healthSumSq[fightOff] += obs.health * obs.health;

            stSum[fightOff] += obs.strength;
            stSumSq[fightOff] += obs.strength * obs.strength;

            hasBarrier[obs.carryingBarrier ? 0 : 1, fightOff]++;

            fightct[fightOff]++;
        }

        CalcProps(hasBarrier, fightct, barrierprp);

        healthMean[0] = Mean(healthSum[0], fightct[0]);
        healthMean[1] = Mean(healthSum[1], fightct[1]);
        healthStdDev[0] = StdDev(healthSumSq[0], healthSum[0], fightct[0]);
        healthStdDev[1] = StdDev(healthSumSq[1], healthSum[1], fightct[1]);

        stMean[0] = Mean(stSum[0], fightct[0]);
        stMean[1] = Mean(stSum[1], fightct[1]);
        stStdDev[0] = StdDev(stSumSq[0], stSum[0], fightct[0]);
        stStdDev[1] = StdDev(stSumSq[1], stSum[1], fightct[1]);

        fightprp[0] = (double)fightct[0] / obsTab.Count;
        fightprp[1] = (double)fightct[1] / obsTab.Count;
    }

    // Calculates the proportions for a discrete table of counts
    // Handles the 0-frequency problem by assigning an artificially
    // low value that is still greater than 0.
    void CalcProps(int[,] counts, int[] n, double[,] props)
    {
        for (int i = 0; i < counts.GetLength(0); i++)
            for (int j = 0; j < counts.GetLength(1); j++)
                // Detects and corrects a 0 count by assigning a proportion
                // that is 1/10 the size of a proportion for a count of 1
                if (counts[i, j] == 0)
                    props[i, j] = 0.1d / fightct[j]; // Can't have 0
                else
                    props[i, j] = (double)counts[i, j] / n[j];
    }

    double Mean(int sum, int n)
    {
        return (double)sum / n;
    }

    double StdDev(int sumSq, int sum, int n)
    {
        return Math.Sqrt((sumSq - (sum * sum) / (double)n) / (n - 1));
    }

    // Calculates probability of x in a normal distribution of
    // mean and stdDev.  This corrects a mistake in the pseudo-code,
    // which used a power function instead of an exponential.
    double GauProb(double mean, double stdDev, int x)
    {
        double xMinusMean = x - mean;
        return (1.0d / (stdDev * sqrt2PI)) *
        Math.Exp(-1.0d * xMinusMean * xMinusMean / (2.0d * stdDev * stdDev));
    }

    // Bayes likelihood for three condition values and one action value
    // For each possible action value, call this with a specific set of three
    // condition values, and pick the action that returns the highest
    // likelihood as the most likely action to take, given the conditions.
    double CalcBayes(int health, int strength, bool barrier, bool fight)
    {
        int fightOff = fight ? 0 : 1;
        double propHealth = GauProb(healthMean[fightOff], healthStdDev[fightOff], health);
        double propSt = GauProb(stMean[fightOff], stStdDev[fightOff], strength);

        double like = propHealth * propSt *
                      barrierprp[barrier ? 0 : 1, fightOff] *
                      fightprp[fightOff];
        return like;
    }

    // Decide whether to fight or not.
    // Returns true if decision is to fight, false o/w
    // Can turn on/off diagnostic output to Console by playing with "*/"
    public bool Decide(int health, int strength, bool barrier)
    {
        double playYes = CalcBayes(health, strength, barrier, true);
        double playNo = CalcBayes(health, strength, barrier, false);

        /* To turn off output, remove this end comment -> 
        double yesNno = playYes + playNo;
        Console.WriteLine("playYes: {0}", playYes); // Use scientifice notation
        Console.WriteLine("playNo:  {0}", playNo);      // for very small numbers
        Console.WriteLine("playYes Normalized: {0,6:F4}", playYes / yesNno);
        Console.WriteLine("playNo  Normalized: {0,6:F4}", playNo / yesNno);
        /* */

        return playYes > playNo;
    }
}
