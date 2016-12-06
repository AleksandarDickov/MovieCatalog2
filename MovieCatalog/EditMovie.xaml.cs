using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for EditMovie.xaml
    /// </summary>
    public partial class EditMovie : Window
    {
        public MovieName Movie { get; set; }
        public EditMovie()
        {
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

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }



        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Ok_Click(object sender, RoutedEventArgs e)
        {
             Fullname.Text = Movie.Name;
      //     Genre.Text = Movie.Genre;
      //     Director.Text= Movie.Director ;

            
         
            this.DialogResult = true;
            this.Close();

            //  if (dataGrid.SelectedItem != null)
            //      (lbUsers.SelectedItem as User).Name = "Random Name";
            //
        }
    }
}
