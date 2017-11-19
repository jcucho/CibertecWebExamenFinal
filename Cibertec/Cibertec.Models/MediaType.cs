using Dapper.Contrib.Extensions;

namespace Cibertec.Models
{
    public class MediaType
    {
        [ExplicitKey]
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

    }
}
