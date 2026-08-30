[![](https://img.shields.io/nuget/v/soenneker.git.libgit2sharp.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.git.libgit2sharp/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.git.libgit2sharp/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.git.libgit2sharp/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.git.libgit2sharp.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.git.libgit2sharp/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.git.libgit2sharp/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.git.libgit2sharp/actions/workflows/codeql.yml)

# Soenneker.Git.LibGit2Sharp

A DI-ready wrapper for cloning, inspecting, fetching, pulling, committing, and pushing Git repositories, including bulk operations beneath a directory.

## Install

```bash
dotnet add package Soenneker.Git.LibGit2Sharp
```

## Registration

```csharp
using Soenneker.Git.LibGit2Sharp.Registrars;
using Microsoft.Extensions.DependencyInjection;

services.AddLibGit2SharpUtilAsSingleton();
```

Scoped registration is also available through `AddLibGit2SharpUtilAsScoped()`.

Authentication defaults are read from configuration:

```json
{
  "Git": {
    "Token": "<GitHub token>",
    "Name": "Commit author",
    "Email": "author@example.com"
  }
}
```

`Token` is used by clone, fetch, and pull. `Name` and `Email` are used when pull or commit does not receive explicit author values. Push receives its token as a method argument.

## Examples

```csharp
ILibGit2SharpUtil git = serviceProvider.GetRequiredService<ILibGit2SharpUtil>();

string checkout = await git.CloneToTempDirectory(
    "https://github.com/example/project.git",
    cancellationToken);

if (git.IsRepositoryDirty(checkout))
{
    git.Commit(checkout, "Update generated files");
    await git.Push(checkout, token, cancellationToken);
}
```

Bulk methods discover repository roots recursively and then apply the requested operation sequentially. Once a repository is found, nested directories beneath that root are not treated as separate repositories.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `Clone`, `CloneToTempDirectory` | Clone into a chosen or newly created temporary directory. | Requires `Git:Token`; the caller owns temporary-directory cleanup. |
| `Fetch`, `Pull` | Update remote references or integrate upstream changes. | Pull fails on conflicts and does not create a merge commit automatically. |
| `Commit`, `Push`, `CommitAndPush` | Stage changes, create a commit, and/or push local `main`. | No commit is created for a clean repository. |
| `IsRepository`, `IsRepositoryDirty` | Inspect a directory. | Does not mutate the repository. |
| `GetAllGitRepositoriesRecursively`, `GetAllDirtyRepositories` | Discover repository roots beneath a directory. | Returns materialized paths. |
| `*AllGitRepositories`, `CommitAllRepositories`, `PushAllRepositories` | Apply an operation to every discovered repository. | Runs sequentially and stops when an operation fails. |
| `SwitchToRemoteBranch` | Checkout local `main`. | Does not force or discard conflicting working-tree changes. |
| `RunCommand` | Run arbitrary Git arguments in a working directory. | Inputs must be trusted; this is intentionally a raw command escape hatch. |

## Practical notes

- LibGit2Sharp operations are synchronous; cancellation applies to directory discovery, process execution, retry waits, and async orchestration rather than interrupting an in-progress native Git operation.
- Methods mutate real repositories and remotes. Use a token with only the permissions required by the target repository.
