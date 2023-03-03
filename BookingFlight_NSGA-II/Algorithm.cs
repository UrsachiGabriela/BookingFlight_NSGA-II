/**************************************************************************
 *                                                                        *
 *  File:        Algorithm.cs                                             *
 *  Copyright:   (c) 2023, Bogdan-Costel Marian,                          *  
 *                         Valentin Mihai                                 *  
 *                         Gabriela Ursachi                               *
 *  E-mail:      @student.tuiasi.ro                                       *
 *  Description: This file contains the implementation NSGA-II            *
 *                algorithm and its helper functions                      *
 *                                                                        *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingFlight_NSGA_II
{
    /// <summary>
    /// This class acts as an implementation of a multi-objective optimization algorithm.
    /// </summary>
    public class Algorithm
    {
        private const int NoOfObjectives = 3;
        private const int NoOfGenes = 3;

        private int _populationSize = AlgParameters.GetInstance().PopulationSize;
        private int _maxNumOfGenerations = AlgParameters.GetInstance().MaxNumOfGenerations;
        private double _crossoverRate = AlgParameters.GetInstance().CrossoverRate;
        private double _mutationRate = AlgParameters.GetInstance().MutationRate;

        public int MaxNoOfDays { get; set; }
        public int MinNoOfDays { get; set; }
        public int MinKm { get; set; }
        public int MaxKm { get; set; }
        public int MinHour { get; set; }
        public int MaxHour { get; set; }

        private Random _rand;

        /// <summary>
        /// All args constructor
        /// </summary>
        /// <param name="rand">Pseudo-random number generator</param>
        /// <param name="minNoOfDays">Lower limit for generating time to the flight</param>
        /// <param name="maxNoOfDays">Upper limit for generating time to the flight</param>
        /// <param name="minKm">Lower limit for generating distance from source to destination of the flight</param>
        /// <param name="maxKm">Upper limit for generating distance from source to destination of the flight</param>
        /// <param name="minHour">Lower limit for generating hour of the flight</param>
        /// <param name="maxHour">Upper limit for generating hour of the flight</param>
        public Algorithm(Random rand, int minNoOfDays, int maxNoOfDays, int minKm, int maxKm, int minHour, int maxHour)
        {
            _rand = rand;
            MinNoOfDays = minNoOfDays;
            MaxNoOfDays = maxNoOfDays;
            MinKm = minKm;
            MaxKm = maxKm;
            MinHour = minHour;
            MaxHour = maxHour;
        }

        /// <summary>
        /// It shows the flow of the evloutionary alorithm
        /// </summary>
        /// <returns>First front of population</returns>
        public List<Chromosome> NSGA_II()
        {
            List<Front> fronts;

            // initializare populatie + calcul functie fitness pentru fiecare cromozom
            Chromosome[] population = GeneratePopulation();

            // sortare dupa fronturi Pareto
            FastNondominatedSort(ref population);

            // generare copii(parinti selectati dupa rank)
            Chromosome[] children = GenerateChildren(population);

            // while !stopCondition
            for (int gen = 0; gen < _maxNumOfGenerations; gen++)
            {
                // concatenare populatie veche cu populatia nou-generata
                Chromosome[] union = population.Concat(children).ToArray();

                fronts = FastNondominatedSort(ref union);

                Chromosome[] newPopulation = new Chromosome[0];
                Front frontL = new Front();

                // se parcurg fronturile, in ordine pentru a se stabili care vor trece in urmatoarea generatie
                foreach (Front frontI in fronts)
                {
                    Front currentFront = frontI;
                    CrowdingDistanceAssignment(ref currentFront);

                    // daca numarul de cromozomi din frontul curent depaseste numarul necesar pentru
                    // a ajunge la o populatie de dimensiune n, se salveaza frontul curent pentru a fi
                    // sortat pe baza distantei de aglomerare
                    if (newPopulation.Length + currentFront.Chromosomes.Count > _populationSize)
                    {
                        frontL = currentFront;
                        break;
                    }
                    else // in caz contrar, se adauga cromozomii frontului curent la noua populatie
                    {
                        newPopulation = newPopulation.Concat(currentFront.Chromosomes).ToArray();
                    }
                }

                // numarul de cromozomi necesari in noua populatie pentru a atinge dimensiunea n
                int dif = _populationSize - newPopulation.Length;
                if (dif > 0)
                {
                    frontL = SortByCrowdingDistance(frontL);
                    for (int i = 0; i < dif; i++)
                    {
                        newPopulation = newPopulation.Append(frontL.Chromosomes[i]).ToArray();
                    }
                }

                population = newPopulation;
                children = GenerateChildren(newPopulation);
            }


            Front firstFront = FastNondominatedSort(ref children)[0];


            return firstFront.Chromosomes;
        }


        /// <summary>
        /// It generates a new population and initialize its chromosomes based on given constraints
        /// </summary>
        /// <returns></returns>
        private Chromosome[] GeneratePopulation()
        {
            // Se genereaza o populatie de dimensiune PopulationSize
            Chromosome[] population = new Chromosome[_populationSize];

            // Se initializeaza fiecare cromozom din populatie si se calculeaza functia de fitness
            for (int i = 0; i < _populationSize; i++)
            {
                population[i] = new Chromosome(MinNoOfDays,MaxNoOfDays,MinKm, MaxKm,MinHour,MaxHour);
                population[i].ComputeFitness();
            }

            return population;

        }

        /// <summary>
        /// It creates a child chromosome from 2 parents and
        /// applies crossover and mutation operators with a given probability
        /// </summary>
        /// <param name="mother">First parent of newly generated child</param>
        /// <param name="father">Second parent of newly generated child</param>
        /// <returns>Generated child chromosome</returns>
        private Chromosome CrossoverAndMutation(Chromosome mother, Chromosome father)
        {
            double a = _rand.NextDouble();
            double rCrossover;
            double rMutation;

            // Initial, copilul preia genele mamei
            Chromosome child = new Chromosome(mother);


            // Genele copilului sunt modificate daca are loc incrucisarea (cu probabilitatea crossoverRate)
            rCrossover = _rand.NextDouble();
            if (rCrossover < _crossoverRate)
            {
                for (int i = 0; i < NoOfGenes; i++)
                {
                    child.NoOfDays = (int)Math.Round(a * mother.NoOfDays + (1 - a) * father.NoOfDays);
                    child.Kilometers = (int)Math.Round(a * mother.Kilometers + (1 - a) * father.Kilometers);
                    child.HourOfTheDay = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,
                        (int)Math.Round(a * mother.HourOfTheDay.Hour + (1 - a) * father.HourOfTheDay.Hour), 
                        (int)Math.Round(a * mother.HourOfTheDay.Minute + (1 - a) * father.HourOfTheDay.Minute),0);
                }
            }

            // Fiecare gena a copilului este reinitializata cu o anumita probabilitate
            rMutation = _rand.NextDouble();
            if (rMutation < _mutationRate)
            {
                child.NoOfDays = _rand.Next(MinNoOfDays, MaxNoOfDays);
            }

            rMutation = _rand.NextDouble();
            if (rMutation < _mutationRate)
            {
                child.Kilometers = _rand.Next(MinKm,MaxKm);
            }

            rMutation = _rand.NextDouble();
            if (rMutation < _mutationRate)
            {
                child.ComputeHour(MinHour, MaxHour);
            }

            child.ComputeFitness();
            return child;
        }

        /// <summary>
        /// It verifies if one solution c1 is better than another one (c2)
        /// </summary>
        /// <param name="c1">The first compared solution</param>
        /// <param name="c2">The second compared solution</param>
        /// <returns>
        ///     True if the first solution is better than the second one.
        ///     False if the first solution is not better than the second one.
        /// </returns>
        private bool Dominates(Chromosome c1, Chromosome c2)
        {
            bool indicator = false;

            for(int i = 0; i < c1.Objectives.Length; i++)
            {
                double objC1 = c1.Objectives[i];
                double objC2 = c2.Objectives[i];

                // S1 este strict superioara lui S2 in raport cu cel putin un obiectiv
                if (objC1 < objC2)
                {
                    indicator = true;
                }

                // S1 nu este inferioara lui S2 in raport cu toate obiectivele
                // daca e inferioara in raport cu cel putin un obiectiv => S1 nu domina S2
                if (objC2 < objC1)
                {
                    return false;
                }
            }

            return indicator;
        }

        /// <summary>
        /// It assign each chromosome of the given population to the corresponding front
        /// based on its non-domination rank
        /// </summary>
        /// <param name="population">All chromosomes of population</param>
        /// <returns>All generated fronts</returns>
        private List<Front> FastNondominatedSort(ref Chromosome[] population)
        {
            int currentIndex = 0;
            List<Front> fronts = new List<Front>();

            // deep copy of population
            List<Chromosome> copyOfPopulation = new List<Chromosome>();
            for (int i = 0; i < population.Length; i++)
            {
                copyOfPopulation.Add(new Chromosome(population[i]));
            }

            // populatia primita ca parametru este reinitializata pentru a contine elementele sortate la final
            population = new Chromosome[population.Length];


            while (copyOfPopulation.Count > 0)
            {
                Front currentFront = new Front();
                Boolean isDominant = true;

                // se compara fitness-ul unui cromozom cu fitness-ul tuturor celorlalti cromozomi
                // si se adauga intr-un front toti cromozomii care nu sunt dominati
                // dupa ce se finalizeaza crearea unui astfel de front, se reia procesul pana cand 
                // nu mai raman cromozomi neasignati vreunui front
                for (int i = 0; i < copyOfPopulation.Count; i++)
                {
                    isDominant = true;

                    for (int j = 0; j < copyOfPopulation.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (Dominates(copyOfPopulation[j], copyOfPopulation[i]))
                        {
                            isDominant = false;
                            break;
                        }
                    }

                    if (isDominant)
                    {
                        currentFront.AddChromosome(copyOfPopulation[i]);
                        population[currentIndex] = copyOfPopulation[i];
                        currentIndex++;
                    }
                }

                fronts.Add(currentFront);

                for (int k = 0; k < currentFront.Chromosomes.Count; k++)
                {
                    copyOfPopulation.Remove(currentFront.Chromosomes[k]);
                }
            }

            return fronts;
        }

        /// <summary>
        /// It uses selection by competition to give one parent
        /// </summary>
        /// <param name="population">All chromosomes from the population</param>
        /// <returns>The best chromosome from the two ones that are fetched from population.</returns>
        private Chromosome SelectParents(Chromosome[] population)
        {
            int index1 = _rand.Next(0, population.Length);
            int index2 = _rand.Next(0, population.Length);

            // pt ca populatia e sortata "crescator", conform fronturilor Pareto
            if (index1 < index2)
            {
                return population[index1];
            }
            return population[index2];
        }

        /// <summary>
        /// It generates a new population of children based on an old population.
        /// </summary>
        /// <param name="population">Old population</param>
        /// <returns>Newly generated population.</returns>
        private Chromosome[] GenerateChildren(Chromosome[] population)
        {
            // se genereaza PopulationSize copii
            Chromosome[] children = new Chromosome[population.Length];

            for (int i = 0; i < population.Length; i++)
            {
                Chromosome mother = SelectParents(population);
                Chromosome father = SelectParents(population);

                Chromosome child = CrossoverAndMutation(mother, father);
                children[i] = child;
            }

            return children;
        }

        /// <summary>
        /// It assigns a property for each chromosome, in order to make better decisions 
        /// in comparing chromosomes from the same front.
        /// </summary>
        /// <param name="front">All chromosomes for which the crowding distance will be assigned.</param>
        private void CrowdingDistanceAssignment(ref Front front)
        {
            int size = front.Chromosomes.Count;
            for (int i = 0; i < size; i++)
            {
                front.Chromosomes[i].CrowdingDistance = 0;
            }

            // pentru fiecare obiectiv
            for (int obj = 0; obj < NoOfObjectives; obj++)
            {
                // sortez cromozomii in ordinea crescatoare a functiei de fitness pentru obiectivul curent
                front.Chromosomes = SortChromosomesByObjective(front.Chromosomes, obj);

                // cromozomii de la limite vor avea distanta infinita
                front.Chromosomes[0].CrowdingDistance = Double.PositiveInfinity;
                front.Chromosomes[size - 1].CrowdingDistance = Double.PositiveInfinity;

                Double maxFitness = front.Chromosomes[size - 1].Objectives[obj];
                Double minFitness = front.Chromosomes[0].Objectives[obj];

                Double norm = maxFitness - minFitness;

                // if norm=0 => toti cromozomii au aceeasi functie de fitnes pe obiectivul curent
                // deci nu mai are rost bucla for pentru a face diferenta dintre functiile de fitness
                if (norm != 0)
                {
                    for (int i = 1; i <= size - 2; i++)
                    {
                        front.Chromosomes[i].CrowdingDistance = front.Chromosomes[i].CrowdingDistance +
                            (front.Chromosomes[i + 1].Objectives[obj] - front.Chromosomes[i - 1].Objectives[obj]) / norm;
                    }
                }

            }

        }

        /// <summary>
        /// Sorts the chromosomes on the same front in the descending order of the crowding distance
        /// </summary>
        /// <param name="front">All chromosomes in the sorted front.</param>
        /// <returns>All chromosomes sorted.</returns>
        public static Front SortByCrowdingDistance(Front front)
        {
            Front result = new Front();
            result.Chromosomes = front.Chromosomes.OrderByDescending(c => c.CrowdingDistance).ToList();

            return result;
        }

        public static List<Chromosome> SortChromosomesByObjective(List<Chromosome> chromosomes, int obj)
        {
            int size = chromosomes.Count;

            for (int i = 1; i < size; i++)
            {
                Chromosome c = new Chromosome(chromosomes[i]);
                c.ComputeFitness();

                int j = i - 1;
                while (j >= 0 && chromosomes[j].Objectives[obj] > c.Objectives[obj])
                {
                    chromosomes[j + 1] = new Chromosome(chromosomes[j]);
                    chromosomes[j + 1].ComputeFitness();

                    j = j - 1;
                }

                chromosomes[j + 1] = new Chromosome(c);
                chromosomes[j + 1].ComputeFitness();
            }

            return chromosomes;
        }


    }
}
