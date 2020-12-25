﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using BLApi;

//netanel mm

namespace Wpf
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Window
    {
        private double place = 0;
        DispatcherTimer gameTimer = new DispatcherTimer();

        public FirstPage()
        {
            InitializeComponent();
            busFunc();

        }

        private void busFunc()
        {
            place = movingBus.Margin.Left;
            FirstPage1.Focus();
            gameTimer.Tick += gameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(0.5);
            gameTimer.Start();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            if (movingBus.Margin.Left >= -600)
                movingBus.Margin = new Thickness(movingBus.Margin.Left - 8, movingBus.Margin.Top, movingBus.Margin.Right, movingBus.Margin.Bottom);
            else
                movingBus.Margin = new Thickness(place, movingBus.Margin.Top, movingBus.Margin.Right, movingBus.Margin.Bottom);
        }

        private void user_Click(object sender, RoutedEventArgs e)
        {
            new SignIn("USER").Show();
            this.Close();
        }

        private void manager_Click(object sender, RoutedEventArgs e)
        {
            new SignIn("MANAGER").Show();
            this.Close();
        }
    }
}
