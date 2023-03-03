/**************************************************************************
 *                                                                        *
 *  File:        Form1.cs                                                 *
 *  Copyright:   (c) 2023,  Valentin Mihai                                *
 *  E-mail:      valentin.mihai@student.tuiasi.ro                         *
 *  Description: This file contains the implementation of                 *
 *                  callback functions for UI buttons                     *
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
using System.Windows.Forms;


namespace BookingFlight_NSGA_II
{
    public partial class Form1 : Form
    {

        private int _maxNoOfDays;
        private int _minNoOfDays;
        private int _minKm;
        private int _maxKm;
        private int _minHour = 0;
        private int _maxHour = 12;

        private Random _rand = new Random();
        private Algorithm _algorithm;
        private ValidDestinations _validDestinations = ValidDestinations.GetInstance();

        public Form1()
        {
            InitializeComponent();
            foreach (var entry in _validDestinations.Flights)
            {
                comboBoxSourceCity.Items.Add(entry.Key);
            }
        }

   
        private void buttonGetResult_Click(object sender, EventArgs e)
        {
            textBoxResults.Text = "";

            if (comboBoxSourceCity.SelectedItem ==null || comboBoxDestCity.SelectedItem==null)
            {
                MessageBox.Show("Please select the source and destination city.");
                return;
            }


            _algorithm = new Algorithm(_rand, _minNoOfDays, _maxNoOfDays, _minKm, _maxKm, _minHour, _maxHour);
            HashSet<Chromosome> result = new HashSet<Chromosome>(_algorithm.NSGA_II());


            String text="";
            int index = 1;
            string newLine = Environment.NewLine.ToString();

            foreach (Chromosome chromosome in result)
            {
                
                String ticket = String.Format("Ticket {0}: {1}" +
                    "\t Date:{2} {3}" +
                    "\t Hour: {4} {5}" +
                    "\t Price: {6} {7}{8}", 
                    index.ToString(),
                    newLine,
                    DateTime.Now.AddDays(1+chromosome.NoOfDays).ToShortDateString() ,
                    newLine,
                    chromosome.HourOfTheDay.ToShortTimeString(),                    
                    newLine,
                    chromosome.Objectives[(int)Objective.Price].ToString("f2"),
                    newLine,
                    newLine);

                
                
                text += ticket;
                index++;
            }

            textBoxResults.Text = text;
        }

        private void comboBoxSourceCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            String sourceCity = comboBoxSourceCity.SelectedItem.ToString();
            List<KeyValuePair<String, List<Double>>> destinations = _validDestinations.Flights[sourceCity];

            comboBoxDestCity.Items.Clear();
            foreach(KeyValuePair<String,List<Double>> dest in destinations)
            {
                comboBoxDestCity.Items.Add(dest.Key);
            }

        }

        private void comboBoxDestCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            String destCity = comboBoxDestCity.SelectedItem.ToString();

            String sourceCity = comboBoxSourceCity.SelectedItem.ToString();
            List<KeyValuePair<String, List<Double>>> destinations = _validDestinations.Flights[sourceCity];


            _minKm = (int)destinations.Find(d => d.Key == destCity).Value[0];
            _maxKm = (int)destinations.Find(d => d.Key==destCity).Value[1];
           
        }

        private void flightCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void flightCalendar_MouseCaptureChanged(object sender, EventArgs e)
        {
            DateTime startDate = flightCalendar.SelectionStart.Date;
            DateTime endDate = flightCalendar.SelectionEnd.Date;

            DateTime currentDate = DateTime.Now;

            if(startDate<currentDate || endDate < currentDate)
            {
                MessageBox.Show("Please select a valid period for flight");
                return;
            }

            _minNoOfDays = startDate.Subtract(currentDate).Days;
            _maxNoOfDays = endDate.Subtract(currentDate).Days;
        }

 
        private void radioButtonNight_CheckedChanged(object sender, EventArgs e)
        {

            _minHour = 12;
            _maxHour = 24;
        }

        private void radioButtonDay_CheckedChanged(object sender, EventArgs e)
        {
            _minHour = 0;
            _maxHour = 12;
        }
    }
}
