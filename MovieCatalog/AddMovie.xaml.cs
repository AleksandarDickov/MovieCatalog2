using Prism.Commands;
using System;
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

namespace MovieCatalog
{
    /// <summary>
    /// Interaction logic for AddMovie.xaml
    /// </summary>
    public partial class AddMovie : Window
    {
        public MovieName AddMovieName { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddMovie()
        {
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);

            AddMovieName = new MovieName();

            InitializeComponent();

        }

        //Ok funkcija
        public void Ok()
        {

            this.DialogResult = true;
            this.Close();
        }

        //Cancel funkcija
        public void Cancel()
        {
            this.Close();
        }

     //  public List<Genre> Genres
     //  {
     //      get
     //      {
     //          return Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList<Genre>();
     //      }
     //  }





        //  public MovieName Movie { get; set; }
        //  public AddMovie()
        //  {
        //      InitializeComponent();
        //   
        //  }
        private void DatePicker_SelectedDateChanged(object sender,
        SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;
        
            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }
          }
        //
        //  private void button_Cancel_Click(object sender, RoutedEventArgs e)
        //  {
        //      this.Close();
        //  }
        //
        //  private void button_Ok_Click(object sender, RoutedEventArgs e)
        //  {
        //      Movie = new MovieName();
        //      Movie.Name = Fullname.Text;
        //      Movie.Director = Director.Text;
        //      Movie.GenrePick = (Genre)Genre.SelectedItem;
        //      Movie.ReleaseDate = Date.SelectedDate.Value;
        //
        //      this.DialogResult = true;
        //      this.Close(); 
        //      
        //  }
    }
}
