using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.DAL.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        [ForeignKey("Model")]
        public int? ModelId { get; set; }
    }
}
