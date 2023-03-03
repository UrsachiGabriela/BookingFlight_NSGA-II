/**************************************************************************
 *                                                                        *
 *  File:        Front.cs                                                 *
 *  Copyright:   (c) 2023,  Gabriela Ursachi                              *
 *  E-mail:      gabriela.ursachi@student.tuiasi.ro                       *
 *  Description: This file contains the structure of one Front            *
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

using System.Collections.Generic;


namespace BookingFlight_NSGA_II
{
    /// <summary>
    /// This class is used for grouping togheter all chromosomes with the same non-domination rank
    /// </summary>
    public class Front
    {
        public List<Chromosome> Chromosomes { get; set; }
        
        public Front()
        {
            Chromosomes = new List<Chromosome>();
        }

        public void AddChromosome(Chromosome chromosome)
        {
            Chromosomes.Add(chromosome);
        }
    }
}
