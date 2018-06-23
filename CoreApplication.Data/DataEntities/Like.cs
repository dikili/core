namespace CoreApplication.Data.DataEntities
{
    public class Like
    {
        public int LikerId { get; set; }

        public int LikeeId { get; set; }

        public LoginUser Liker { get; set; }

        public LoginUser Likee { get; set; }
    }
}