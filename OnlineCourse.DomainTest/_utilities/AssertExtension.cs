namespace OnlineCourse.DomainTest._utilities
{
    public static class AssertExtension
    {
        public static void WithMessage(this ArgumentException exception, string message)
        {
            if (exception.Message == message)
                Assert.True(true);
            else
                Assert.False(true, $"Expected the message '{message}'");
        }
    }
}
