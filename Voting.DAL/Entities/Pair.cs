
namespace Voting.DAL.Entities
{
    public class Pair
    {
        public int Id { get; set; }
        public int? FirstModelId { get; set; }
        public int? SecondModelId { get; set; }
        public Model FirstModel { get; set; }
        public Model SecondModel { get; set; }
        public int WinnerId { get; set; }
        public bool IsVoted { get; set; }
    }
}
