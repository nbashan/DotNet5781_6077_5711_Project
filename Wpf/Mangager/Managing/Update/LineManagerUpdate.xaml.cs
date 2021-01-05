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
using Wpf.Mangager.Managing.Add;
using Wpf.Mangager.Information;

namespace Wpf.Mangager.Managing
{
    /// <summary>
    /// Interaction logic for LineManager.xaml
    /// </summary>
    public partial class LineManager : Window
    {
        BLApi.IBL bl;

        long number;
        string area;
        int numberInLine = 0;
        List<BO.Stop> stopListInput = new List<BO.Stop>();

        BO.Line managingLine;
        BO.Stop tempStop;

        public LineManager(BO.Line line)
        {
            InitializeComponent();
            managingLine = new BO.Line();
            managingLine = line;
            bl = BLApi.Factory.GetBL("1");
            stopsList.ItemsSource = bl.GetAllStops().ToList();
        }








        //private void MyTextBox0_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return)
        //    {
        //        try
        //        {
        //            Number_Click(sender, e);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);

        //        }
        //    }
        //}
        private void MyTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    Area_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }


        /// <summary>
        /// Defines actions to be performed when the user enters input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Line_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tempStop = (BO.Stop)stopsList.SelectedItem;
            for (int i = 1; i <= managingLine.Stops.Count() + 1; i++)
            {
                number_in_lines.Items.Add(i);
            }
        }

        /// <summary>
        /// Defines actions to be performed when the user enters input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberInLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numberInLine = (int)number_in_lines.SelectedItem;

            submit.Visibility = Visibility.Visible;
        }





        private void Area_Click(object sender, RoutedEventArgs e)
        {
            string textRange = MyTextBox1.Text;
            area = textRange;
            try
            {
                bl.UpdateLineArea(area, managingLine.Id);
                MessageBox.Show("input submited" + area + "  click on X to exit");

                MyTextBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyTextBox1.Clear();
            }
        }
        //private void StopList_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (stopListInput.Count == 0)
        //        {
        //            MessageBox.Show("please add at least one stop");
        //            return;
        //        }
        //        bl.AddStopInLine(managingLine.Id, tempStop.StopCode, numberInLine);

        //        MessageBox.Show("input updated successfully!"+  "  click on X to exit");

        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        private void lineList_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }



        private void AddStopToLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bl.AddStopInLine(managingLine.Id, tempStop.StopCode, numberInLine);

                MessageBox.Show("input updated successfully!" + "  click on X to exit");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            foreach (Window w in Application.Current.Windows)
            {
                if (w.Name == "PresentationLines1")
                {
                    w.Close();
                }
            }
            new PresentationLines().Show();

            this.Close();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {

        }



    }
}
