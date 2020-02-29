namespace API.ViewModels.Output
{
    public class GenericOutput
    {
        public string Status { get; set; }

        public GenericOutput(string status)
        {
            Status = status;
        }
    }
}
