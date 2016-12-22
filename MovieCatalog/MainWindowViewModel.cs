using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MovieCatalog
{
    public class MainWindowViewModel : INotifyPropertyChanged 
    {
        private ObservableCollection <MovieName> movies = new ObservableCollection<MovieName>();
        private ObservableCollection <MovieName> filteredMovies = new ObservableCollection<MovieName>();
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        private MovieName selectedMovie;
        private string searchValue;

        public MainWindowViewModel()
        {
            SearchCommand = new DelegateCommand(Search);
            DeleteCommand = new DelegateCommand(Delete);
            AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand(Edit);
            ExitCommand = new DelegateCommand(Exit);
            ExportCommand = new DelegateCommand(Export);
            ImportCommand = new DelegateCommand(Import);
          //  movies = MovieCatalog.MovieName.getMovie();
        }

       

        public void Exit()
        {

            var result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        public void Edit()
        {
            if (selectedMovie == null)
            {
                MessageBox.Show("Nothing is selected", "Error!", MessageBoxButton.OK);
            }
            else
            {
                var selectedMovie = this.selectedMovie;
                var editDialog = new EditMovie(selectedMovie);


                if (editDialog.ShowDialog() == true)
                {
                }
            }
        }

        public void Add()
        {
            AddMovie addDialog = new AddMovie();

            if (addDialog.ShowDialog() == true)
            {
                movies.Add(addDialog.AddMovieName);
            }
        }

        public void Delete()
        {
            if
          (selectedMovie == null)
            {
                MessageBox.Show("Nothing is selected", "Error", MessageBoxButton.OK);
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    movies.Remove((MovieName)selectedMovie);
                }
            }
        }

        public void Search()
        {
            OnPropertyChanged("Movies");
        }
        public string SearchValue
        {
            get
            {
                return searchValue;
            }
            set
            {
                searchValue = value;
                OnPropertyChanged("SearchValue");
                OnPropertyChanged("Movies");
            }
        }

        public void Import()
        {
            Microsoft.Win32.OpenFileDialog Import = new Microsoft.Win32.OpenFileDialog();
            Import.Filter = "XML Files (*.xml)|*.xml|JSON Files (*.json)|*.json";

            if (Import.ShowDialog() == true)
            {
                string impext = Path.GetExtension(Import.FileName);

                if (impext.Equals(".xml"))
                {
                    XmlSerializer x = new XmlSerializer(typeof(ObservableCollection<MovieName>));

                    using (StreamReader reader = new StreamReader(Import.FileName))
                    {
                        Movies = (ObservableCollection<MovieName>)x.Deserialize(reader);

                    }
                }
                else if (impext.Equals(".json"))
                {
                    string jsonimp = File.ReadAllText(Import.FileName);
                    ObservableCollection<MovieName> data = JsonConvert.DeserializeObject<ObservableCollection<MovieName>>(jsonimp);
                    Movies = data;
                }
            }

        }

        public void Export()
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

                // izvlacenje extenzije (ubaci u if else)

                string ext = Path.GetExtension(filename);

                // Serializer
                if (ext.Equals(".xml"))
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<MovieName>));

                    using (FileStream writer = new FileStream(filename, FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(writer, movies);
                    }
                }
                else if (ext == ".json")
                {
                    string json = JsonConvert.SerializeObject(movies, Formatting.Indented);

                    File.WriteAllText(filename, json);
                }
            }
        }

        public ObservableCollection<MovieName> Movies
        {
            get
            {
                if (string.IsNullOrEmpty(SearchValue))
                {
                    return movies;
                }
                else
                {
                    string text = SearchValue;
                    filteredMovies = new ObservableCollection<MovieName>();
                    foreach (var movie in movies)
                    {
                        if (movie.Name.StartsWith(text) || movie.GenrePick.ToString().StartsWith(text))
                        {
                            filteredMovies.Add(movie);
                        }
                    }
                    return filteredMovies;
                }
            }

            set
            {
                movies = value;
                OnPropertyChanged("Movies");
            }
        }

        public MovieName SelectedMovie
        {
            get
            {
                return selectedMovie;
            }

            set
            {
                selectedMovie = value;
                OnPropertyChanged("SelectedMovie");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
