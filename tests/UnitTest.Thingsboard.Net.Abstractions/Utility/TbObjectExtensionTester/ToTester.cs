using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Abstractions.Utility.TbObjectExtensionTester;

public class ToTester
{
    [Fact]
    public void ToTest()
    {
        Assert.Equal("1", 1.To<string>());
        Assert.Equal("1", 1.To<string?>());

        Assert.Equal(1, "1".To<int>());
        Assert.Equal(1, "1".To<int?>());
        Assert.Equal(0, "abc".To<int>());
        Assert.Null("abc".To<int?>());
    }
}
