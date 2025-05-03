namespace Film_Rating_System.Models.Repositories
{
    
    public class MovieRepository : IRepository<Movie>
    {
        private readonly AppDbContext _db;

        public MovieRepository(AppDbContext db)
        {
            _db = db; // تهيئة DbContext باستخدام الاعتماديات
        }

        public int Add(Movie item)
        {
            if (_db.Database.CanConnect())
            {
                _db.Movies.Add(item);
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
                var student = _db.Movies.Find(id); // استخدم Find بدلاً من Where

                if (student != null)
                {
                    _db.Movies.Remove(student);
                    _db.SaveChanges();
                }
                return student.Id;

            }

            return -1;
        }

        public int Edit(int id, Movie item)
        {
            if (_db.Database.CanConnect())
            {
                _db.Movies.Update(item);
                _db.SaveChanges();
                return item.Id;
            }
            else
                return -1;


        }

        public List<Movie> GetAll()
        {
            return _db.Movies.ToList(); // لا حاجة للتحقق من الاتصال هنا
        }

        public Movie GetById(int id)
        {
            return _db.Movies.Find(id); // استخدم Find بدلاً من Where
        }
    }
}
