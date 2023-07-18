namespace DotNetAlegrarioAPI.Dtos
{
    
        public class EmotionStatToReceiveDto
    {
        private int _emotionValue;
        public int EmotionValue {
            get => _emotionValue;
            set => _emotionValue = IsValidEmotionValue(value) ? value : 0; 
        }
        public int Amount { get; set; }
        public decimal Average { get; set; }

        public string Emotion
    {
        get
        {
            switch (_emotionValue)
            {
                case 1:
                    return "Happy";
                case 2:
                    return "Neutral";
                case 3:
                    return "Angry";
                case 4:
                    return "Sad";
                default:
                    return "AllEmotions";
            }
        }
        set
        {
            switch (value.ToLower())
            {
                case "happy":
                    _emotionValue = 1;
                    break;
                case "neutral":
                    _emotionValue = 2;
                    break;
                case "angry":
                    _emotionValue = 3;
                    break;
                case "sad":
                    _emotionValue = 4;
                    break;
                default:
                    _emotionValue = 0;
                    break;
            }
        }
    }

    private bool IsValidEmotionValue(int value)
    {
        return value >= 1 && value <= 4;
    }
    }
}