/**************************************************************************
 *                                                                        *
 *  File:        AlgParameters.cs                                         *
 *  Copyright:   (c) 2023, Bogdan-Costel Marian                           *
 *  E-mail:      bogdan-costel.marian@student.tuiasi.ro                   *
 *  Description: This file contains helper method for initializing        *
 *                 parameters by reading them from a resource file        *
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
    /// This class retains all algorithm parameters
    /// </summary>
    public class AlgParameters
    {
        public int PopulationSize { get; set; }
        public int MaxNumOfGenerations { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }


        private static AlgParameters _instance;

        /// <summary>
        /// It reads from file all algorithm parameters.
        /// </summary>
        private AlgParameters()
        {
            String[] lines = System.IO.File.ReadAllLines(@"AlgParameters.txt");

            foreach (String line in lines)
            {
                String[] elements = line.Split('=');
                String parameterName = elements[0];
                Double parameterValue = Double.Parse(elements[1]);

                switch (parameterName)
                {
                    case "PopulationSize":
                        PopulationSize = (int)parameterValue;
                        break;
                    case "MaxNumOfGenerations":
                        MaxNumOfGenerations = (int)parameterValue;
                        break;
                    case "CrossoverRate":
                        CrossoverRate = parameterValue;
                        break;
                    case "MutationRate":
                        MutationRate = parameterValue;
                        break;
                    default:
                        break;
                }
            }
        }

        public static AlgParameters GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AlgParameters();
            }

            return _instance;
        }
    }
}
