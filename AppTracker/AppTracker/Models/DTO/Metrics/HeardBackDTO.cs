namespace AppTracker.Models.DTO.Metrics
{
    public class HeardBackDTO
    {
        public string format;
        public decimal value;

        public HeardBackDTO(string format, decimal value)
        {
            this.format = format;
            this.value = value;
        }
    }
}
