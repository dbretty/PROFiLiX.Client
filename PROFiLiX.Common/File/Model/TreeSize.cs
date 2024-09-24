// <copyright file="TreeSize.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.File.Model
{
    /// <summary>
    /// Files and Folders Tree SizeClass.
    /// </summary>
    public class TreeSize
    {
        /// <summary>
        /// Gets or sets the FolderName value.
        /// </summary>
        public string? FolderName { get; set; }

        /// <summary>
        /// Gets or sets the FolderName value.
        /// </summary>
        public string? Size { get; set; }

        /// <summary>
        /// Gets or sets the FolderName value.
        /// </summary>
        public long? RawSize { get; set; }
    }
}
