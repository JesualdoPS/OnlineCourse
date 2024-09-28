namespace OnlineCourse.DomainTest.Courses
{
    public class Course
    {
        public Course(string name, double workload, TargetAudience audience, double price, string description)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid Name");

            if (workload < 1)
                throw new ArgumentException("Workload cannot be less than 1 hour");

            if (price < 1)
                throw new ArgumentException("Price cannot be less than $1");

            Name = name;
            Workload = workload;
            Audience = audience;
            Price = price;
            Description = description;
        }

        public string Name { get; private set; }
        public double Workload { get; private set; }
        public TargetAudience Audience { get; private set; }
        public double Price { get; private set; }
        public string Description { get; private set; }
    }
}