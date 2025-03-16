
using Microsoft.AspNetCore.SignalR;

namespace NOROFF_ASPNET.Models

{
//#nullable disable
    public class Movie
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
                public double? TicketPrice { get; set; }

        //GENRE 
        // foreign Key 
        public int GenreId {get;set;}
        //navitagion
        public Genre? Genre{get;set;}

        //STUDIO
        //foreign Key 
        public int StudioId {get;set;}
        //navigation
        public Studio? Studio {get;set;}

        //many movies have many actory 
        //lazy loading 
        public virtual ICollection<MovieActor>? MovieActors {get;set;}
    }

    


}
