namespace API.ViewModels.Input
{
    public class GetQuestionsInput
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public string filter { get; set; }
    }
}
