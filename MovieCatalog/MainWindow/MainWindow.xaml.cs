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
using System.Data.Entity;

namespace MovieCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<MovieName> moviesList = new ObservableCollection<MovieName>();
        public MainWindowViewModel viewModel;
        public MovieContext Context;


        public MainWindow()
        {
            InitializeComponent();
       //     DataContext = new MainWindowViewModel();

            Context = new MovieContext();
            var MovieContext = new MainWindowViewModel(Context);
            
                foreach (MovieName film in Context.Movies)
                {
                    MovieContext.Movies.Add(film);
                }
            DataContext = MovieContext;
            
    
            

        }

        

    }
}
