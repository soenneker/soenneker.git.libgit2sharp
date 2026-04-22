using Soenneker.Git.LibGit2Sharp.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Git.LibGit2Sharp.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class LibGit2SharpUtilTests : HostedUnitTest
{
    private readonly ILibGit2SharpUtil _util;

    public LibGit2SharpUtilTests(Host host) : base(host)
    {
        _util = Resolve<ILibGit2SharpUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
