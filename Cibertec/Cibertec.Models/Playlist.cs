using Dapper.Contrib.Extensions;

namespace Cibertec.Models
{
    public class Playlist
    {
        [ExplicitKey]
        public int PlaylistId { get; set; }
        public string Name { get; set; }
    }
}
