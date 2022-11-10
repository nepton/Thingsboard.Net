using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Abstractions.Utility.TbObjectExtensionTester;

public class ToTester
{
    private readonly object? _nullObject = null;

    [Fact]
    public void ToTest()
    {
        Assert.Equal("1", 1.To<string>());
        Assert.Equal("1", 1.To<string?>());

        Assert.Equal(1, "1".To<int>());
        Assert.Equal(1, "1".To<int?>());
        Assert.Equal(0, "abc".To<int>());
        Assert.Null("abc".To<int?>());
        Assert.Null(_nullObject.To<int?>());
        Assert.Equal(0, _nullObject.To<int>());

        Assert.Equal(TestEnum.A, "A".To<TestEnum>());
        Assert.Equal(TestEnum.A, "X".To<TestEnum>());
        Assert.Equal(TestEnum.A, "".To<TestEnum>());
        Assert.Equal(TestEnum.B, "1".To<TestEnum>());
        Assert.Equal(TestEnum.A, "10".To<TestEnum>());

        Assert.Equal(TestEnum.C, ((int) (TestEnum.C)).To<TestEnum>());
        Assert.Equal(TestEnum.A, 10.To<TestEnum>());

        Assert.Equal(TestEnum.A, _nullObject.To<TestEnum>());
    }

    [Fact]
    public void ToDefaultTest()
    {
        Assert.Equal("1", 1.To("2"));
        Assert.Equal("1", 1.To<string>("2"));

        Assert.Equal(1, "1".To(2));
        Assert.Equal(1, "1".To<int>(2));
        Assert.Equal(2, "abc".To(2));
        Assert.Equal(2, "abc".To<int>(2));
        Assert.Equal(1, _nullObject.To(1));

        Assert.Null("abc".To<int?>(null));

        Assert.Equal(TestEnum.A, "A".To(TestEnum.B));
        Assert.Equal(TestEnum.B, "X".To(TestEnum.B));
        Assert.Equal(TestEnum.B, "".To(TestEnum.B));


        Assert.Equal(TestEnum.C, ((int) (TestEnum.C)).To(TestEnum.B));
        Assert.Equal(TestEnum.B, 10.To(TestEnum.B));

        Assert.Equal(TestEnum.B, _nullObject.To(TestEnum.B));
    }

    public enum TestEnum
    {
        A, B, C
    }
}
