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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs args)
        {
            mainMenuScreen.Visibility = Visibility.Hidden;
            newGameScreen.Visibility = Visibility.Visible;
        }

        private void continueButton_Click(object sender, RoutedEventArgs args)
        {
            mainMenuScreen.Visibility = Visibility.Hidden;
            continueScreen.Visibility = Visibility.Visible;
        }
        private void shutdownButton_Click(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }
    }

   

}