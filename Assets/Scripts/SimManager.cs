using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimManager : MonoBehaviour {

    //List<IAIGuy> PolicePop = new List<IAIGuy>();
    //List<IAIGuy> RiotPop = new List<IAIGuy>();

	// Use this for initialization
	void Start () {
        //TestGeneticAlgorithm();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void TestGeneticAlgorithm()
    {
        IAIGuy one = new Police();
        IAIGuy two = new Police();
        IAIGuy three = new Police();
        IAIGuy four = new Police();
        IAIGuy five = new Police();

        bool[] genes = { true, true, false, false, true, false, false, true, true};
        one.Genes = new BitArray(genes);
        one.fitness = 40;

        genes[0] = false;
        genes[1] = false;
        two.Genes = new BitArray(genes);
        two.fitness = 100;

        genes[2] = true;
        genes[3] = true;
        three.Genes = new BitArray(genes);
        three.fitness = 80;

        genes[4] = false;
        genes[5] = true;
        four.Genes = new BitArray(genes);
        four.fitness = 30;

        five.Genes = new BitArray(genes);
        five.fitness = 50;

        List<IAIGuy> guys = new List<IAIGuy>();
        guys.Add(one);
        guys.Add(two);
        guys.Add(three);
        guys.Add(four);
        guys.Add(five);

        guys = GeneticGeneration.BreedGeneration(guys);

        string output = "";
        foreach(IAIGuy guy in guys)
        {
            foreach(bool b in guy.Genes)
            {
                output += b;
            }
            output += "\n";
        }

        Debug.Log(output);
    }
}
