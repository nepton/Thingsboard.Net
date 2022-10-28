﻿using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbCustomerClient;

public class GetCustomerTitleTests
{
    [Fact]
    public async Task TestGetCustomerTitle()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var actual = await client.GetCustomerTitleAsync(TbTestData.GetTestCustomerId());

        Assert.NotNull(actual);
    }

    [Fact]
    public async Task TestWhenCustomerNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var customerId = Guid.Empty;
        var actual     = await client.GetCustomerTitleAsync(customerId);

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.GetCustomerTitleAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.GetCustomerTitleAsync(Guid.NewGuid());
            });
    }
}
