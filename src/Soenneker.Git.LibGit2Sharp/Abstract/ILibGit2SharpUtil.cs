using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Git.LibGit2Sharp.Abstract;

/// <summary>
/// A utility interface for managing Git repositories using LibGit2Sharp and custom operations.
/// </summary>
public interface ILibGit2SharpUtil
{
    /// <summary>
    /// Clones a Git repository to the specified directory.
    /// </summary>
    /// <param name="uri">Receives the normalized absolute URI when parsing succeeds.</param>
    /// <param name="directory">Directory to read from or write to.</param>
    void Clone(string uri, string directory);

    /// <summary>
    /// Clones a Git repository into a temporary directory.
    /// </summary>
    /// <param name="uri">Receives the normalized absolute URI when parsing succeeds.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The path of the temporary directory the repository was cloned into.</returns>
    ValueTask<string> CloneToTempDirectory(string uri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pulls the latest changes for all Git repositories recursively found in the specified directory.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the pull all git repositories operation is complete.</returns>
    ValueTask PullAllGitRepositories(string directory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all remote changes for all Git repositories recursively found in the specified directory.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the fetch all git repositories operation is complete.</returns>
    ValueTask FetchAllGitRepositories(string directory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Switches all repositories in the specified directory to the tracked remote branch (main).
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the switch all git repositories to remote branch operation is complete.</returns>
    ValueTask SwitchAllGitRepositoriesToRemoteBranch(string directory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits all repositories with the given message.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="commitMessage">Commit Message for the commit all repositories operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the commit all repositories operation is complete.</returns>
    ValueTask CommitAllRepositories(string directory, string commitMessage, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pushes all repositories in the directory using the given credentials.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="token">Arbitrary utility token to append.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the push all repositories operation is complete.</returns>
    ValueTask PushAllRepositories(string directory, string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Switches the specified repository to the main remote branch (origin/main).
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    void SwitchToRemoteBranch(string directory);

    /// <summary>
    /// Determines whether the given directory contains a Git repository with uncommitted changes.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <returns>true if the given directory contains a Git repository with uncommitted changes; otherwise, false.</returns>
    bool IsRepositoryDirty(string directory);

    /// <summary>
    /// Determines whether the specified directory is a valid Git repository.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <returns>true if the specified directory is a valid Git repository; otherwise, false.</returns>
    bool IsRepository(string directory);

    /// <summary>
    /// Executes a raw Git command in the given directory.
    /// </summary>
    /// <param name="command">Command for the run command operation.</param>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the run command operation is complete.</returns>
    ValueTask RunCommand(string command, string directory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pulls changes from the remote repository into the specified local repository.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="name">Name of the Lib Git2 Sharp value to target.</param>
    /// <param name="email">Email address to validate or query.</param>
    void Pull(string directory, string? name = null, string? email = null);

    /// <summary>
    /// Commits changes in the given repository using the specified message and author info.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="message">Message content to send.</param>
    /// <param name="name">Name of the Lib Git2 Sharp value to target.</param>
    /// <param name="email">Email address to validate or query.</param>
    void Commit(string directory, string message, string? name = null, string? email = null);

    /// <summary>
    /// Pushes commits from the given repository to its remote using provided credentials.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="token">Arbitrary utility token to append.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the push operation is complete.</returns>
    ValueTask Push(string directory, string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stages a file if it is not already present in the index.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="relativeFilePath">Path of the relative file to use.</param>
    void AddIfNotExists(string directory, string relativeFilePath);

    /// <summary>
    /// Fetches changes from the remote repository without merging them.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    void Fetch(string directory);

    /// <summary>
    /// Recursively retrieves all directories containing valid Git repositories within the given path.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the collection returned by get All Git Repositories Recursively.</returns>
    ValueTask<List<string>> GetAllGitRepositoriesRecursively(string directory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all dirty repositories (with uncommitted changes) under the specified path.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the collection returned by get All Dirty Repositories.</returns>
    ValueTask<List<string>> GetAllDirtyRepositories(string directory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits and pushes a repository using the given credentials and message.
    /// </summary>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="name">Name of the Lib Git2 Sharp value to target.</param>
    /// <param name="email">Email address to validate or query.</param>
    /// <param name="token">Arbitrary utility token to append.</param>
    /// <param name="message">Message content to send.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the commit and push operation is complete.</returns>
    ValueTask CommitAndPush(string directory, string name, string email, string token, string message, CancellationToken cancellationToken = default);
}
