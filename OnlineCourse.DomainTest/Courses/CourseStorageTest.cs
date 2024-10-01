using Bogus;
using Moq;
using OnlineCourse.DomainTest._utilities;
using OnlineCourse.DomainTest.Builders;

namespace OnlineCourse.DomainTest.Courses
{
    public class CourseStorageTest
    {
        private readonly DtoCourse _dtoCourse;
        private readonly CourseStorage _courseStorage;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;

        public CourseStorageTest()
        {
            var fake = new Faker();

            _dtoCourse = new DtoCourse
            {
                Name = fake.Random.Words(),
                Workload = fake.Random.Double(10, 100),
                TargetAudience = "Student",
                Value = fake.Random.Double(100, 1000),
                Description = fake.Lorem.Paragraph()
            };

            _courseRepositoryMock = new Mock<ICourseRepository>();

            _courseStorage = new CourseStorage(_courseRepositoryMock.Object);
        }
        [Fact]
        public void ShouldAddCourse()
        {
            _courseStorage.Store(_dtoCourse);

            _courseRepositoryMock.Verify(r => r.Add(
                It.Is<Course>(
                    c => c.Name == _dtoCourse.Name &&
                    c.Description == _dtoCourse.Description)));
        }

        [Fact]
        public void ShouldNotAddInvalidTargetAudience()
        {
            var invalidTargetAudience = "Medic";
            _dtoCourse.TargetAudience = invalidTargetAudience;

            Assert.Throws<ArgumentException>(() => _courseStorage.Store(_dtoCourse))
                .WithMessage("Invalid Target Audience");
        }

        [Fact]
        public void ShouldNotAddCourseWithSameName()
        {
            var savedCourse = CourseBuilder.New().WithName(_dtoCourse.Name).Build();

            _courseRepositoryMock.Setup(r => r.ChooseByName(_dtoCourse.Name)).Returns(savedCourse);

            Assert.Throws<ArgumentException>(() => _courseStorage.Store(_dtoCourse))
                .WithMessage("Name already exists in our database");
        }
    }
}
