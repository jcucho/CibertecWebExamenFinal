using Dapper.Contrib.Extensions;

namespace Cibertec.Models
{
    public class Album
    {
        [ExplicitKey]
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
    }
}
