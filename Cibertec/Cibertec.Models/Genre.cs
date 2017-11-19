using Dapper.Contrib.Extensions;

namespace Cibertec.Models
{
    public class Genre
    {
        [ExplicitKey]
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
