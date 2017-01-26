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
        private MovieContext context;

        public MainWindowViewModel()
        {
            SearchCommand = new DelegateCommand<string>(Search);
            DeleteCommand = new DelegateCommand(Delete);
            AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand(Edit);
            ExitCommand = new DelegateCommand(Exit);
            ExportCommand = new DelegateCommand(Export);
            ImportCommand = new DelegateCommand(Import);
        }

        public MainWindowViewModel(MovieContext context)
        {
            this.context = context;
        }

        public void Exit()
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        public void Edit()
        {
            if (selectedMovie == null)
            {
                MessageBox.Show("Nothing is selected", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var selectedMovie = this.selectedMovie;
                var editDialog = new AddorEdit(selectedMovie);

                if (editDialog.ShowDialog() == true)
                {

                }
            }
        }

        public void Add()
        {
            AddorEdit addDialog = new AddorEdit(null);

            if (addDialog.ShowDialog() == true)
            {
                movies.Add(addDialog.Movie);
            }
        }

        public void Delete()
        {
            if
          (selectedMovie == null)
            {
                MessageBox.Show("Nothing is selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    movies.Remove((MovieName)selectedMovie);
                }
            }
        }

        public void Search(string value)
        {
            if (value != null)
            {
                SearchValue = value;
            }
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
            try
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
            catch
            {
                MessageBox.Show("This fail is invalid");
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
                        if (movie.Name.StartsWith(text, System.StringComparison.InvariantCultureIgnoreCase) 
                            || movie.GenrePick.ToString().StartsWith(text, System.StringComparison.InvariantCultureIgnoreCase))
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
