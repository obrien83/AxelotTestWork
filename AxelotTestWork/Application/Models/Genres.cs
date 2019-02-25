using System.Collections.Generic;

namespace AxelotTestWork.Application.Models
{
    public class Genres
    {
        public Genres()
        {
            Tracks = new HashSet<Tracks>();
        }

        public long GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tracks> Tracks { get; set; }
    }
}
