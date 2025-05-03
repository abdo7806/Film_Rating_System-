namespace Film_Rating_System.Models.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly AppDbContext _db;

        public ReviewRepository(AppDbContext db)
        {
            _db = db; // تهيئة DbContext باستخدام الاعتماديات
        }

        public int Add(Review item)
        {
            if (_db.Database.CanConnect())
            {
                _db.Reviews.Add(item);
                _db.SaveChanges();
                return item.Id;
            }
            else
                return -1;

        }

        public int Delete(int id)
        {
            if (_db.Database.CanConnect())
            {
                var student = _db.Reviews.Find(id); // استخدم Find بدلاً من Where

                if (student != null)
                {
                    _db.Reviews.Remove(student);
                    _db.SaveChanges();
                }
                return student.Id;

            }

            return -1;
        }

        public int Edit(int id, Review item)
        {
            if (_db.Database.CanConnect())
            {
                _db.Reviews.Update(item);
                _db.SaveChanges();
                return item.Id;
            }
            else
                return -1;


        }

        public List<Review> GetAll()
        {
            return _db.Reviews.ToList(); // لا حاجة للتحقق من الاتصال هنا
        }

        public Review GetById(int id)
        {
            return _db.Reviews.Find(id); // استخدم Find بدلاً من Where
        }
    }
}
