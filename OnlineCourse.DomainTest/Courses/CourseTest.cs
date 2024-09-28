using Bogus;
using ExpectedObjects;
using OnlineCourse.DomainTest._utilities;
using OnlineCourse.DomainTest.Builders;
using Xunit.Abstractions;

namespace OnlineCourse.DomainTest.Courses
{
    public class CourseTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _name;
        private readonly double _workload;
        private readonly TargetAudience _audience;
        private readonly double _price;
        private readonly string _description;

        public CourseTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Constructor running");
            var faker = new Faker();

            _name = faker.Random.Word();
            _workload = faker.Random.Double(10, 100);
            _audience = TargetAudience.Student;
            _price = faker.Random.Double(100, 1000);
            _description = faker.Lorem.Paragraph();
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose is running");
        }

        [Fact]
        public void ShouldCreateCourse()
        {
            //Assert
            var expectedCourse = new
            {
                Name = _name,
                Workload = _workload,
                Audience = _audience,
                Price = _price,
                Description = _description
            };

            //Act
            var course = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.Audience, expectedCourse.Price, expectedCourse.Description);

            //Assert
            expectedCourse.ToExpectedObject().ShouldMatch(course);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotHaveInvalidName(string invalidName)
        {
            Assert.Throws<ArgumentException>(() =>
            CourseBuilder.New().WithName(invalidName).Build())
                .WithMessage("Invalid Name");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotHaveWorkloadLessThanOne(double invalidWorkload)
        {
            Assert.Throws<ArgumentException>(() =>
            CourseBuilder.New().WithWorkload(invalidWorkload).Build())
                .WithMessage("Workload cannot be less than 1 hour");
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotHavePriceLessThanOne(double invalidPrice)
        {
            Assert.Throws<ArgumentException>(() =>
                CourseBuilder.New().WithPrice(invalidPrice).Build())
                .WithMessage("Price cannot be less than $1");
        }
    }
}