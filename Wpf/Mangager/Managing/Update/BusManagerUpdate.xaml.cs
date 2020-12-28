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
using Wpf.Mangager.Presentation;
using System.Windows.Threading;

namespace Wpf.Mangager.Managing
{
    /// <summary>
    /// Interaction logic for BusManager.xaml
    /// </summary>
    public partial class BusManager : Window
    {
        private double place = 0;
        DispatcherTimer gameTimer = new DispatcherTimer();
        public BO.Bus managingBus;

        float kM;
        float fuel;
        int statusInput;

        BLApi.IBL bl;


        public BusManager(BO.Bus bus)
        {
            InitializeComponent();
            managingBus = new BO.Bus();
            managingBus = bus;
            busFunc();
            bl = BLApi.Factory.GetBL("1");
        }

        private void busFunc()
        {
            place = movingBus.Margin.Left;
            FirstPage.Focus();
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



        private void home_Click(object sender, RoutedEventArgs e)
        {
            new FirstPage().Show();
            this.Close();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new PresentationBusses().Show();
            this.Close();
        }




        private void MyTextBox_TextChanged_0(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(MyTextBox0.Document.ContentStart, MyTextBox0.Document.ContentEnd);
            if (textRange.Text.Length >= 3 && textRange.Text[textRange.Text.Length - 3] == '\n')
            {
                try
                {
                    Travel_Update_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }


        private void MyTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(MyTextBox1.Document.ContentStart, MyTextBox1.Document.ContentEnd);
            if (textRange.Text.Length >= 3 && textRange.Text[textRange.Text.Length - 3] == '\n')
            {
                try
                {
                    Fuel_Update_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void MyTextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(MyTextBox2.Document.ContentStart, MyTextBox2.Document.ContentEnd);
            if (textRange.Text.Length >= 3 && textRange.Text[textRange.Text.Length - 3] == '\n')
            {
                try
                {
                    Status_Update_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void Travel_Update_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(MyTextBox0.Document.ContentStart, MyTextBox0.Document.ContentEnd);
            float result = 0;
            if (float.TryParse(textRange.Text, out result))
            {
                kM = result;
                try 
                {
                    bl.UpdateBusKM(kM, managingBus.LicenseNumber);
                    MessageBox.Show("input updated" + result);
                    MyTextBox0.Document.Blocks.Clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MyTextBox0.Document.Blocks.Clear();
                }
            }
            else
            {
                MessageBox.Show("wrong input!!!!");
                MyTextBox1.Document.Blocks.Clear();
            }
        }
        private void Fuel_Update_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(MyTextBox1.Document.ContentStart, MyTextBox1.Document.ContentEnd);
            float result = 0;
            if (float.TryParse(textRange.Text, out result))
            {
                fuel = result;
                try
                {
                    bl.UpdateBusFuel(fuel, managingBus.LicenseNumber);
                    MessageBox.Show("input submited" + result);
                    MyTextBox1.Document.Blocks.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MyTextBox1.Document.Blocks.Clear();
                }

            }
            else
            {
                MessageBox.Show("wrong input!!!!");
                MyTextBox1.Document.Blocks.Clear();
            }
        }
        private void Status_Update_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(MyTextBox2.Document.ContentStart, MyTextBox2.Document.ContentEnd);
            int result = 0;
            if (int.TryParse(textRange.Text, out result))
            {
                statusInput = result;
                try
                {
                    bl.UpdateBusFuel(statusInput, managingBus.LicenseNumber);
                    MessageBox.Show("input submited" + result);
                    MyTextBox2.Document.Blocks.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MyTextBox2.Document.Blocks.Clear();
                }


            }
            else
            {
                MessageBox.Show("wrong input!!!!");
                MyTextBox2.Document.Blocks.Clear();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
