namespace DotNetAlegrarioAPI.Models
{
        public class Emotion
    {
        public int UserHour { get; set; }
        public DateTime CellDay { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; } = "" ;
        public int Score { get; set; }
    }
}