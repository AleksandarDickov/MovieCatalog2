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
    public enum Genre
    {
        Fantasy,
        Action,
        Comedy,
        Family,
        Drama,
    }

    public class MovieName : INotifyPropertyChanged
    {
        public MovieName()
        {

        }

        public MovieName(MovieName newMovie)
        {
            CopyProperties(newMovie);
        }

        public void CopyProperties(MovieName movie)
        { 
            this.Name = movie.Name;
            this.Director = movie.Director;
            this.GenrePick = movie.GenrePick;
            this.ReleaseDate = movie.ReleaseDate;
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

        private Genre _genrePick;
        public Genre GenrePick
        {
            get { return _genrePick; }
            set
            {
                _genrePick = value;
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

        private DateTime _releaseDate = DateTime.Now;
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                RaisePropertyChanged();
            }
        }
       

     //  public static ObservableCollection<MovieName> getMovie()
     //  {
     //      var movie = new ObservableCollection<MovieName>();
     //    //  movie.Add(new MovieName() { Name = "Avatar", GenrePick = Genre.Fantasy , Director = "James Cameron", ReleaseDate = new DateTime(2009, 12, 18) });
     //    //  movie.Add(new MovieName() { Name = "Dark Knight", GenrePick = Genre.Action, Director = "Christopher Nolan", ReleaseDate = new DateTime(2008, 7 , 9) });
     //    //  movie.Add(new MovieName() { Name = "Gilmors girls", GenrePick = Genre.Drama, Director = "Ivan Peric", ReleaseDate = new DateTime(2018 , 1, 1) });
     //    //  movie.Add(new MovieName() { Name = "Scrubs", GenrePick = Genre.Family , Director = "Aleksandar Dickov", ReleaseDate = new DateTime(2066,5,30) });
     //      return movie;
     //  }
     //
    }


}
