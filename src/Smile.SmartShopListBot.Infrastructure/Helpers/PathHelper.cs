// SPDX-License-Identifier: MIT

namespace Smile.SmartShopListBot.Infrastructure.Helpers;

/// <summary>
/// The path helper class.
/// </summary>
internal static class PathHelper
{
    /// <summary>
    /// Determines whether the specified path string is a valid absolute or relative file system path.
    /// </summary>
    /// <param name="path">The input path to validate.</param>
    /// <returns>
    ///   <c>true</c> if <paramref name="path"/> is a valid absolute or valid relative path; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidPath(string? path)
    {
        return IsValidAbsolutePath(path) || IsValidRelativePath(path);
    }

    #region Private Methods

    /// <summary>
    /// Determines whether the specified path string is a valid absolute file system path.
    /// </summary>
    /// <param name="path">The input path to validate.</param>
    /// <returns>
    ///   <c>true</c> if <paramref name="path"/> is non-null, rooted, contains no invalid characters,
    ///   and can be successfully normalized by <see cref="Path.GetFullPath(string)"/>; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsValidAbsolutePath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path)) return false;

        // Reject invalid characters
        if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0) return false;

        // Allow trailing directory separators, but ensure path not empty after trimming
        string trimmed = path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        if (string.IsNullOrEmpty(trimmed)) return false;

        // Must be absolute
        if (!Path.IsPathRooted(trimmed)) return false;

        try
        {
            // Normalize and ensure no exceptions
            Path.GetFullPath(trimmed);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether the specified path string is a valid relative path.
    /// </summary>
    /// <param name="relativePath">The relative path to validate.</param>
    /// <returns>
    ///   <c>true</c> if <paramref name="relativePath"/> is non-null, not rooted, contains no invalid characters,
    ///   and does not include empty segments; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsValidRelativePath(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath)) return false;

        // Reject absolute paths
        if (Path.IsPathRooted(relativePath)) return false;

        // Reject invalid characters
        if (relativePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0) return false;

        // Allow trailing directory separators, but ensure path not empty after trimming
        string trimmed = relativePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        if (string.IsNullOrEmpty(trimmed)) return false;

        // Split on directory separators and reject empty segments
        string[] parts = trimmed.Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar],
            StringSplitOptions.None);

        return !parts.Any(string.IsNullOrEmpty);
    }

    #endregion
}