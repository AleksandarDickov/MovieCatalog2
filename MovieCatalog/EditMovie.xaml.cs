using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for EditMovie.xaml
    /// </summary>
    public partial class EditMovie : Window
    {
        private MovieName _movie;

        public MovieName Movie
        {
            get
            {
                return _movie;
            }
            set
            {
                _movie = value;
                RaisePropertyChanged();
            }
        }

        public EditMovie(MovieName movie)
        {
            DataContext = this;
            Movie = movie;
            InitializeComponent();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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

        public event PropertyChangedEventHandler PropertyChanged;


        private void RaisePropertyChanged(
            [CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Ok_Click(object sender, RoutedEventArgs e)
        {
            Movie.GenrePick = (Genre)Genre.SelectedItem;

            this.DialogResult = true;
            this.Close();
        }
    }
}
