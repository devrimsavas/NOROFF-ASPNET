namespace NOROFF_ASPNET.Models
{
    public class MovieActor
    {
        //Constructor
        public MovieActor(int movieId, int actorId)
        {
            this.MovieId = movieId;
            this.ActorId = actorId;
        }

        //navigation to Movie 
        public Movie? Movie { get; set; }

        //Foreign Key 
        public int MovieId { get; set; }
        //Navigation to Actor
        public Actor? Actor { get; set; }
        //Foreign Key
        public int ActorId { get; set; }
    }
}