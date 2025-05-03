using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Film_Rating_System.Models
{
    public class Movie
    {
        // معرف الفيلم (المفتاح الأساسي)
        public int Id { get; set; }

        // عنوان الفيلم
        public string Title { get; set; }

        // وصف الفيلم
        public string Description { get; set; }

        // تاريخ إصدار الفيلم
        public DateTime ReleaseDate { get; set; }

        // نوع الفيلم (مثل دراما، أكشن، كوميدي)
        public string Genre { get; set; }


        // رابط الصورة الخاصة بالفيلم
        public string? ImageUrl { get; set; }

        // قائمة التقييمات المرتبطة بالفيلم
        public ICollection<Review> Reviews { get; set; }
    }
}
