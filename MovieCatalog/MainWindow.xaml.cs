﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<MovieName> moviesList = new ObservableCollection<MovieName>();

        public MainWindow()
        {
            InitializeComponent();
            moviesList = MovieName.getMovie();
            dataGrid.ItemsSource = moviesList;
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddMovie addDialog = new AddMovie();
            if (addDialog.ShowDialog() == true)
            {
                moviesList.Add(addDialog.Movie);
                dataGrid.Items.Refresh();
            }
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("There is nothing to select");

            }
            else
            {
                var selectedMovie = (MovieName)dataGrid.SelectedItem;
                var editDialog = new EditMovie(selectedMovie);
                

                if (editDialog.ShowDialog() == true)
                {

                }
            }
        }

        private void button_Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("There is nothing to select");

            }
            else
            {
                if ((MessageBox.Show("Are you sure you want to delete?", "Please confirm.", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    moviesList.Remove((MovieName)dataGrid.SelectedItem);
                }
            }
        }
        
    
    }
}
