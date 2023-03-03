/**************************************************************************
 *                                                                        *
 *  File:        Objective.cs                                             *
 *  Copyright:   (c) 2023, Gabriela Ursachi                               *
 *  E-mail:      gabriela.ursachi@student.tuiasi.ro                       *
 *  Description: This file contains the definition for objectives         *
 *                  and also for degrees of inconvenience                 *
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

namespace BookingFlight_NSGA_II
{
    public enum Objective
    {
        Price,
        Duration,
        Inconvenience
    }

    public enum InconvenienceDegree
    {
        Maximum = 10,
        Medium = 5,
        Minimum = 1
    }
}
