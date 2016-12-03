using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog
{
    public class MovieName : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(
            [CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }



        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                RaisePropertyChanged();
            }
        }

        private string _director;
        public string Director
        {
            get { return _director; }
            set
            {
                _director = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _releaseDate;
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged()
        {
          
        }

        public static List<MovieName> getMovie()
        {
            var movie = new List<MovieName>();
            movie.Add(new MovieName() { Name = "Avatar", Genre = "Fantasy", Director = "James Cameron", ReleaseDate = new DateTime(2009, 12, 18) });
            movie.Add(new MovieName() { Name = "Dark Knight", Genre = "Action", Director = "Christopher Nolan", ReleaseDate = new DateTime(2008, 7 , 9) });
            movie.Add(new MovieName() { Name = "Gilmors girls", Genre = "Sci-Fi", Director = "Ivan Peric", ReleaseDate = new DateTime(2018 , 1, 1) });
            movie.Add(new MovieName() { Name = "Scrubs", Genre = "Family", Director = "Aleksandar Dickov", ReleaseDate = new DateTime(2066,5,30) });
            return movie;
        }




    }
}
