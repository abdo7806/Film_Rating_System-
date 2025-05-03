namespace Film_Rating_System.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db; // تهيئة DbContext باستخدام الاعتماديات
        }
        
        public int Add(User item)
        {
            if (_db.Database.CanConnect())
            {
                _db.Users.Add(item);
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
                var student = _db.Users.Find(id); // استخدم Find بدلاً من Where

                if (student != null)
                {
                    _db.Users.Remove(student);
                    _db.SaveChanges();
                }
                return student.Id;

            }

            return -1;
        }

        public int Edit(int id, User item)
        {
            if (_db.Database.CanConnect())
            {
                _db.Users.Update(item);
                _db.SaveChanges();
                return item.Id;
            }
            else
                return -1;


        }

        public List<User> GetAll()
        {
            return _db.Users.ToList(); // لا حاجة للتحقق من الاتصال هنا
        }

        public User GetById(int id)
        {
            return _db.Users.Find(id); // استخدم Find بدلاً من Where
        }
    }
}
