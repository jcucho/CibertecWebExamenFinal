using Dapper.Contrib.Extensions;

namespace Cibertec.Models
{
    public class Artist
    {
        [ExplicitKey]
        public int ArtistId { get; set; }
        public string Name { get; set; }
    }
}