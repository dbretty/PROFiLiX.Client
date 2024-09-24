// <copyright file="IFilesAndFolders.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.File
{
    using PROFiLiX.Common.File.Model;

    /// <summary>
    /// Interface for the FilesAndFolders Class.
    /// </summary>
    public interface IFilesAndFolders
    {
        /// <summary>
        /// Checks for a Directory or File.
        /// </summary>
        /// <param name="path">The Path to the file or folder.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        public bool CheckDirectory(string path);

        /// <summary>
        /// Gets a directory size based on a path.
        /// </summary>
        /// <param name="directory">The DirectoryInfo object to size.</param>
        /// <returns>A <see cref="long"/>.</returns>
        public long DirectorySize(DirectoryInfo directory);

        /// <summary>
        /// Gets a directory size based on a path (Async).
        /// </summary>
        /// <param name="directory">The DirectoryInfo object to size.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public Task<long> DirectorySizeAsync(DirectoryInfo directory);

        /// <summary>
        /// Formats the folder size from byte to a readable number.
        /// </summary>
        /// <param name="bytes">The Folder Size in Bytes.</param>
        /// <returns>A <see cref="string"/>.</returns>
        public string FormatFileSize(long bytes);

        /// <summary>
        /// Builds a directory tree size list.
        /// </summary>
        /// <param name="rootFolder">The root folder to build the tree from.</param>
        /// <param name="sorted">Boolean value to sort results.</param>
        /// <returns>A <see cref="string"/>.</returns>
        public List<TreeSize> BuildTreeSizeFolders(string rootFolder, bool sorted = true);

        /// <summary>
        /// Builds a directory tree size list (Async).
        /// </summary>
        /// <param name="rootFolder">The root folder to build the tree from.</param>
        /// <param name="sorted">Boolean value to sort results.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public Task<List<TreeSize>> BuildTreeSizeFoldersAsync(string rootFolder, bool sorted = true);

        /// <summary>
        /// Builds a file tree size list.
        /// </summary>
        /// <param name="rootFolder">The root folder to build the tree from.</param>
        /// <param name="sorted">Boolean value to sort results.</param>
        /// <returns>A <see cref="string"/>.</returns>
        public List<TreeSize> BuildTreeSizeFiles(string rootFolder, bool sorted = true);

        /// <summary>
        /// Builds a file tree size list.
        /// </summary>
        /// <param name="rootFolder">The root folder to build the tree from.</param>
        /// <param name="sorted">Boolean value to sort results.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public Task<List<TreeSize>> BuildTreeSizeFilesAsync(string rootFolder, bool sorted = true);

        /// <summary>
        /// Deletes a folder.
        /// </summary>
        /// <param name="folderName">The folder to delete.</param>
        /// <param name="maxRetries">The max retires.</param>
        /// <param name="millisecondsDelay">The ms delay before a retry.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public Task<bool> DeleteFolderAsync(string folderName, int maxRetries = 10, int millisecondsDelay = 30);

        /// <summary>
        /// Deletes a folder.
        /// </summary>
        /// <param name="fileName">The root file to delete.</param>
        /// <param name="maxRetries">The max retires.</param>
        /// <param name="millisecondsDelay">The ms delay before a retry.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public Task<bool> DeleteFileAsync(string fileName, int maxRetries = 10, int millisecondsDelay = 30);

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="folder">The root folder to create.</param>
        public void CreateDirectory(string folder);
    }
}