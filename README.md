[![](https://img.shields.io/nuget/v/soenneker.git.libgit2sharp.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.git.libgit2sharp/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.git.libgit2sharp/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.git.libgit2sharp/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.git.libgit2sharp.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.git.libgit2sharp/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.git.libgit2sharp/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.git.libgit2sharp/actions/workflows/codeql.yml)

# Soenneker.Git.LibGit2Sharp

A utility interface for managing Git repositories using LibGit2Sharp and custom operations.

## Install

```bash
dotnet add package Soenneker.Git.LibGit2Sharp
```

## Quick start

```csharp
using Soenneker.Git.LibGit2Sharp.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddLibGit2SharpUtilAsSingleton();
```

Adds `ILibGit2SharpUtil` as a singleton service.

## What you get

- `ILibGit2SharpUtil` — A utility interface for managing Git repositories using LibGit2Sharp and custom operations.
- `LibGit2SharpUtilRegistrar` — A wrapper around LibGit2Sharp.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ILibGit2SharpUtil.Clone(uri, directory)` | Clones a Git repository to the specified directory. | Returns no value; the requested change is complete when the method returns. |
| `ILibGit2SharpUtil.CloneToTempDirectory(uri, cancellationToken)` | Clones a Git repository into a temporary directory. | The path of the temporary directory the repository was cloned into. |
| `ILibGit2SharpUtil.PullAllGitRepositories(directory, cancellationToken)` | Pulls the latest changes for all Git repositories recursively found in the specified directory. | A task that completes when the pull all git repositories operation is complete. |
| `ILibGit2SharpUtil.FetchAllGitRepositories(directory, cancellationToken)` | Fetches all remote changes for all Git repositories recursively found in the specified directory. | A task that completes when the fetch all git repositories operation is complete. |
| `ILibGit2SharpUtil.SwitchAllGitRepositoriesToRemoteBranch(directory, cancellationToken)` | Switches all repositories in the specified directory to the tracked remote branch (main). | A task that completes when the switch all git repositories to remote branch operation is complete. |
| `ILibGit2SharpUtil.CommitAllRepositories(directory, commitMessage, cancellationToken)` | Commits all repositories with the given message. | A task that completes when the commit all repositories operation is complete. |
| `ILibGit2SharpUtil.PushAllRepositories(directory, token, cancellationToken)` | Pushes all repositories in the directory using the given credentials. | A task that completes when the push all repositories operation is complete. |
| `ILibGit2SharpUtil.SwitchToRemoteBranch(directory)` | Switches the specified repository to the main remote branch (origin/main). | Returns no value; the requested change is complete when the method returns. |
| `ILibGit2SharpUtil.IsRepositoryDirty(directory)` | Determines whether the given directory contains a Git repository with uncommitted changes. | true if the given directory contains a Git repository with uncommitted changes; otherwise, false. |
| `ILibGit2SharpUtil.IsRepository(directory)` | Determines whether the specified directory is a valid Git repository. | true if the specified directory is a valid Git repository; otherwise, false. |
| `ILibGit2SharpUtil.RunCommand(command, directory, cancellationToken)` | Executes a raw Git command in the given directory. | A task that completes when the run command operation is complete. |
| `ILibGit2SharpUtil.Pull(directory, name, email)` | Pulls changes from the remote repository into the specified local repository. | Returns no value; the requested change is complete when the method returns. |
| `ILibGit2SharpUtil.Commit(directory, message, name, email)` | Commits changes in the given repository using the specified message and author info. | Returns no value; the requested change is complete when the method returns. |
| `ILibGit2SharpUtil.Push(directory, token, cancellationToken)` | Pushes commits from the given repository to its remote using provided credentials. | A task that completes when the push operation is complete. |
| `ILibGit2SharpUtil.AddIfNotExists(directory, relativeFilePath)` | Stages a file if it is not already present in the index. | Returns no value; the requested change is complete when the method returns. |
| `ILibGit2SharpUtil.Fetch(directory)` | Fetches changes from the remote repository without merging them. | Returns no value; the requested change is complete when the method returns. |
| `ILibGit2SharpUtil.GetAllGitRepositoriesRecursively(directory, cancellationToken)` | Recursively retrieves all directories containing valid Git repositories within the given path. | The matching records as a materialized collection. |
| `ILibGit2SharpUtil.GetAllDirtyRepositories(directory, cancellationToken)` | Retrieves all dirty repositories (with uncommitted changes) under the specified path. | The matching records as a materialized collection. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
