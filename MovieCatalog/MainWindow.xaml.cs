using System;
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
            model.AllMovie = MovieName.getMovie();
            dataGrid.ItemsSource = model.AllMovie;
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        AddorEditViewModel model = new AddorEditViewModel();
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddorEditViewModel addDialog = new AddorEditViewModel();
            model.CurrentMovie = new MovieName();
            if (addDialog.ShowDialog() == true)
            {
                model.AllMovie.Add(model.CurrentMovie);
          //      dataGrid.Items.Refresh();
                // model.AllMovie.Add(model.CurrentMovie);
            }
           
          //  AddMovie addDialog = new AddMovie();
          //  if (addDialog.ShowDialog() == true)
          //  {
          //      moviesList.Add(addDialog.Movie);
          //      dataGrid.Items.Refresh();
          //  }
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
            Microsoft.Win32.OpenFileDialog Import = new Microsoft.Win32.OpenFileDialog();

            Import.DefaultExt = ".xml";
            Import.Filter = "XML Files (*.xml)|*.xml|JSON Files (*.json)|*.json";

            Nullable<bool> result = Import.ShowDialog();
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

        private void button_Export_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog SaveFile = new Microsoft.Win32.SaveFileDialog();
            SaveFile.FileName = "Movies";
            SaveFile.DefaultExt = ".xml"; // Default file extension
            SaveFile.Filter = "XML Files (.xml)|*.xml|JSON Files (*.json)|*.json"; // Filter by extension

            // Process save file dialog box results
            if (SaveFile.ShowDialog() == true)
            {
                // Save
                string filename = SaveFile.FileName;
            }
        }
    }
}
