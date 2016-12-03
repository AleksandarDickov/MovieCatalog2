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

namespace MovieCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MovieName> movies;
        public MainWindow()
        {
            InitializeComponent();
            movies = MovieName.getMovie();
            dataGrid.ItemsSource = movies;
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddMovie add = new AddMovie();
            if (add.ShowDialog() == true)
            {

                movies.Add(add.Movie);

            }
        }
    }
}
