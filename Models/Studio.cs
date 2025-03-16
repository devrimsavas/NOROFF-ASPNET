using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace NOROFF_ASPNET.Models
{

    public class Studio 

    {
        public int Id {get;set;}
        public string Name {get;set;}

        public Studio(int Id,string Name) 
        {
            this.Id=Id;
            this.Name=Name;
        }

        //one to many 

        public ICollection<Movie> Movies {get;set;}
    }
}