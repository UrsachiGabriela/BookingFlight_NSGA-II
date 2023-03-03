/**************************************************************************
 *                                                                        *
 *  File:        Objective.cs                                             *
 *  Copyright:   (c) 2023, Valentin Mihai                                 *
 *  E-mail:      valentin.mihai@student.tuiasi.ro                         *
 *  Description: This file contains helper method for initializing        *
 *          existing flight routes by reading them from a resource file   *
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


namespace BookingFlight_NSGA_II
{

    public class ValidDestinations
    {
        // <Source, List{<Destination, maxKm> , <Destination2, maxKm>, etc}>
        public Dictionary<String, List<KeyValuePair<String, List<Double>>>> Flights { get; set; }
        private static ValidDestinations _instance;

        /// <summary>
        /// It reads from file all source and destination cities with distance between them
        /// </summary>
        private ValidDestinations()
        {
            Flights = new Dictionary<string, List<KeyValuePair<string, List<Double>>>>();
            String[] lines = System.IO.File.ReadAllLines(@"ValidDestinations.txt");

            foreach(String line in lines)
            {
                List<KeyValuePair<String, List<Double>>> destinations = new List<KeyValuePair<String, List<Double>>>();

                String[] elements = line.Split(':');
                
                String sourceCity = elements[0];
                String[] dest = elements[1].Split(';');

                foreach(String d in dest)
                {
                    String destCity = d.Split(',')[0];
                    Double minKm = Double.Parse(d.Split(',')[1]);
                    Double maxKm = Double.Parse(d.Split(',')[2]);

                    destinations.Add(new KeyValuePair<string, List<Double>>(destCity, new List<Double> { minKm, maxKm})); ;
                }

                Flights.Add(sourceCity, destinations);
            }
        }

        
        public static ValidDestinations GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ValidDestinations();
            }

            return _instance;
        }
    }
}
