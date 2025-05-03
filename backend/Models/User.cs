using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Film_Rating_System.Models
{
    public class User
    {
        // معرف المستخدم (المفتاح الأساسي)
        public int Id { get; set; }

        // اسم المستخدم
        public string Username { get; set; }

        // كلمة المرور (محفوظة بشكل آمن)
        public string PasswordHash { get; set; }

        // بريد المستخدم الإلكتروني
        public string Email { get; set; }

        // تاريخ إنشاء الحساب
        public DateTime CreatedAt { get; set; }

        // خاصية جديدة
        public string Role { get; set; } = "User";

        // قائمة التقييمات المقدمة من قبل المستخدم
        public ICollection<Review> Reviews { get; set; }
    }
}
