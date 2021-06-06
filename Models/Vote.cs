namespace Planning_Poker.Models
{
    public class Vote
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int card_id { get; set; }
        public int story_id { get; set; }
    }
}