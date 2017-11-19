using Dapper.Contrib.Extensions;

namespace Cibertec.Models
{
    public class PlaylistTrack
    {
        [ExplicitKey]
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

    }
}
