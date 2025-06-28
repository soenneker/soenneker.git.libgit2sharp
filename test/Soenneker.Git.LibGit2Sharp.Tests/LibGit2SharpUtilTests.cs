using Soenneker.Git.LibGit2Sharp.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Git.LibGit2Sharp.Tests;

[Collection("Collection")]
public sealed class LibGit2SharpUtilTests : FixturedUnitTest
{
    private readonly ILibGit2SharpUtil _util;

    public LibGit2SharpUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ILibGit2SharpUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
