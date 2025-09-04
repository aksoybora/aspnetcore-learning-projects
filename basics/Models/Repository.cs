namespace basics.Models
{
    public class Repository
    {
        private static readonly List<Course> _courses = new();
        static Repository()
        {
            _courses = new List<Course>()
            {
                new Course() { Id = 1, Title = "ASP.NET Kursu", Description = "WEB Programlama", Image = "1.jpg"},
                new Course() { Id = 2, Title = "PHP Kursu", Description = "WEB Programlama", Image = "2.jpg"},
                new Course() { Id = 3, Title = "Django Kursu", Description = "WEB Programlama", Image = "3.jpg"},
                new Course() { Id = 4, Title = "Angular Kursu", Description = "WEB Programlama", Image = "2.jpg"},
            };
        }

        public static List<Course> Courses
        {
            get
            {
                return _courses;
            }
        }

        public static Course? GetById(int? id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }
    }
}