namespace DotNetAlegrarioAPI.Dtos
{
        public class EmotionToAddDto
    {
        public int HourId { get; set; }
        public DateTime CellDay { get; set; }
        public int EmotionValue { get; set; }
        public string Comment { get; set; } = "" ;
        public int Score { get; set; }
    }
}