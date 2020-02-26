namespace Business.Model
{
    public class Option
    {
        public int Id { get; set; }
        public string Choice { get; set; }
        public int Votes { get; set; }

        public int PollId { get; set; }
        public Poll Poll { get; set; }
    }
}
