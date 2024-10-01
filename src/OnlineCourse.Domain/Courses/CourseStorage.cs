namespace OnlineCourse.DomainTest.Courses
{
    public class CourseStorage
    {
        private readonly ICourseRepository _courseRepository;

        public CourseStorage(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Store(DtoCourse dtoCourse)
        {
            var savedCourse = _courseRepository.ChooseByName(dtoCourse.Name);

            if (savedCourse != null)
                throw new ArgumentException("Name already exists in our database");

            if(!Enum.TryParse<TargetAudience>(dtoCourse.TargetAudience, out var targetAudience))
                throw new ArgumentException("Invalid Target Audience");
            
            var course =
                new Course(dtoCourse.Name, dtoCourse.Workload, targetAudience, dtoCourse.Value, dtoCourse.Description);

            _courseRepository.Add(course);
        }
    }
    public interface ICourseRepository
    {
        void Add(Course course);
        Course ChooseByName(string course);
    }
    public class DtoCourse
    {
        public string Name { get; set; }
        public double Workload { get; set; }
        public string TargetAudience { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
    }
}
