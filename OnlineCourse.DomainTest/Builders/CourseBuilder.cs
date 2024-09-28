using OnlineCourse.DomainTest.Courses;

namespace OnlineCourse.DomainTest.Builders
{
    public class CourseBuilder
    {
        private string _name = "Basic Informatics";
        private double _workload = 80;
        private TargetAudience _audience = TargetAudience.Student;
        private double _price = 950;
        private string _description = "The basic for using a computer";

        public static CourseBuilder New()
        {
            return new CourseBuilder();
        }
        public CourseBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CourseBuilder WithWorkload (double workload)
        {
            _workload = workload;
            return this;
        }
        public CourseBuilder WithAudience(TargetAudience audience)
        {
            _audience = audience;
            return this;
        }
        public CourseBuilder WithPrice(double price)
        {
            _price = price;
            return this;
        }
        public CourseBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
        public Course Build()
        {
            return new Course(_name, _workload, _audience, _price, _description);
        }
    }
}
