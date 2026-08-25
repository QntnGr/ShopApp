using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class ArchiTest
{
    [Fact]
    public void Domain_Should_Not_Depend_On_Other_Projects()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("ShopApp.Domain")
            .Should()
            .NotHaveDependencyOn("ShopApp.Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
