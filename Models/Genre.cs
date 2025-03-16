namespace NOROFF_ASPNET.Models
{


    public class Genre
    {

        public int Id {get;set;}
        public string Name{get;set;}

        public Genre(int Id,string Name)
        {
            this.Id=Id;
            this.Name=Name;
        }

        //one to many 
        //one genre has many movies
        public ICollection<Movie>? Movies {get;set;}
    }
}