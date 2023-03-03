/**************************************************************************
 *                                                                        *
 *  File:        Chromosome.cs                                            *
 *  Copyright:   (c) 2023, Gabriela Ursachi                               *
 *  E-mail:      gabriela.ursachi@student.tuiasi.ro                       *
 *  Description: This file contains the structure of a population's       *
 *                 individ and methods related to it's initialization     *
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

namespace BookingFlight_NSGA_II
{
    /// <summary>
    /// The structure of an individ from population
    /// </summary>
    public class Chromosome
    {
        public int Kilometers { get; set; }
        public int NoOfDays { get; set; }
        public DateTime HourOfTheDay { get; set; }
        public double CrowdingDistance { get; set; }
        public double[] Objectives { get; set; } // valoarea functiei de adaptare a individului

        private static Random _rand = new Random();


        public Chromosome() { }

        public Chromosome(int minNoOfDays,int maxNoOfDays,int minKm,int maxKm, int minHour, int maxHour)
        {           
            NoOfDays = _rand.Next(minNoOfDays, maxNoOfDays);
            Kilometers = _rand.Next(minKm,maxKm);
            ComputeHour(minHour, maxHour);

            Objectives = new double[3];
        }

        /// <summary>
        /// Copy builder
        /// </summary>
        /// <param name="c">Copied object</param>
        public Chromosome(Chromosome c) // constructor de copiere
        {
            Kilometers = c.Kilometers;
            NoOfDays = c.NoOfDays;
            HourOfTheDay = c.HourOfTheDay;

            Objectives = new double[3];
            Objectives[(int)Objective.Price] = c.Objectives[(int)Objective.Price];
            Objectives[(int)Objective.Duration] = c.Objectives[(int)Objective.Duration];
            Objectives[(int)Objective.Inconvenience] = c.Objectives[(int)Objective.Inconvenience];
        }

        /// <summary>
        /// Establishes the values of the objectives functions according to a certain formula.
        /// </summary>
        public void ComputeFitness()
        {           
            Objectives[(int)Objective.Duration] = NoOfDays;
            Objectives[(int)Objective.Inconvenience] = ComputeInconvenienceDegree(HourOfTheDay);
            Objectives[(int)Objective.Price] = 10 * (1 + Kilometers) / (NoOfDays + Objectives[(int)Objective.Inconvenience]);
        }

        /// <summary>
        /// Given a value for time, it assigns a degree of inconvenience for a chromosome
        /// </summary>
        /// <param name="timeOfTheDay">Time value</param>
        /// <returns>Inconvenience degree</returns>
        public int ComputeInconvenienceDegree(DateTime timeOfTheDay)
        {
            if( ((0 <= timeOfTheDay.Hour) && (timeOfTheDay.Hour <= 5)) || (23 == timeOfTheDay.Hour) )
            {
                return (int)InconvenienceDegree.Maximum;
            }
            else if (((6<= timeOfTheDay.Hour) && (timeOfTheDay.Hour <= 8)) || ((20 <= timeOfTheDay.Hour) && (timeOfTheDay.Hour <= 22)))
            {
                return (int)InconvenienceDegree.Medium;
            }
            else 
            {
                return (int)InconvenienceDegree.Minimum;
            }           
        }

        /// <summary>
        /// It randomly generates an hour between minHour and MaxHour
        /// </summary>
        public void ComputeHour(int minHour, int maxHour)
        {
            //pick the hour
            int h = _rand.Next(minHour, maxHour);

            //pick the minute
            int m = 0;
            if (h < maxHour)
                m = _rand.Next(0, 60);

            //compose the DateTime
            this.HourOfTheDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, h, m, 0);
        }
    }
}
