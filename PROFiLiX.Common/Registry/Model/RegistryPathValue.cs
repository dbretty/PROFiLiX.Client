// <copyright file="RegistryPathValue.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Registry.Model
{
    /// <summary>
    /// Class to hold the Path Type Values.
    /// </summary>
    public class RegistryPathValue
    {
        /// <summary>
        /// Gets or sets the Path to the registry value.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public object? Value { get; set; }
    }
}