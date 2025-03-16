namespace NOROFF_ASPNET.Models
{
    public class Actor
    {
        //Constructor
        public Actor(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        //One Actor has Many MovieActors (Many-to-Many)
        public virtual ICollection<MovieActor>? MovieActors { get; set; }
    }
}