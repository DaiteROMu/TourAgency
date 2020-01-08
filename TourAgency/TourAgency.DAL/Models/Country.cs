using System.Collections.Generic;

namespace TourAgency.DAL.Models
{
    public class Country
    {
        public int Id  {get; set; }
        public string Name{ get; set; }

        public List<City> Cities { get; set; }
    }
}