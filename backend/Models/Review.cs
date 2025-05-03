namespace Film_Rating_System.Models
{
    public class Review
    {
        // معرف التقييم (المفتاح الأساسي)
        public int Id { get; set; }

        // معرف الفيلم الذي يتم تقييمه (مفتاح خارجي)
        public int MovieId { get; set; }

        // معرف المستخدم الذي قام بالتقييم (مفتاح خارجي)
        public int UserId { get; set; }

        // تقييم الفيلم (من 1 إلى 5)
        public int Rating { get; set; }

        // تعليق المستخدم على الفيلم
        public string Comment { get; set; }

        // تاريخ إنشاء التقييم
        public DateTime CreatedAt { get; set; }

        // علاقة مع الفيلم
        public Movie Movie { get; set; }

        // علاقة مع المستخدم
        public User User { get; set; }
    }
}
