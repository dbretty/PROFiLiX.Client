// <copyright file="ProfileType.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.User.Model
{
    /// <summary>
    /// Class to hold the Profile Type.
    /// </summary>
    public class ProfileType
    {
        /// <summary>
        /// Gets or Sets the Profile Type Label.
        /// </summary>
        public string? ProfileTypeLabel { get; set; }

        /// <summary>
        /// Gets or Sets the PolicyTypeDefinition.
        /// </summary>
        public ProfileTypeDefinition ProfileTypeDefinition { get; set; }

        /// <summary>
        /// Gets or Sets the Profile Type Label.
        /// </summary>
        /// <returns>A <see cref="string"/> with the profile size.</returns>
        public override string? ToString()
        {
            return this.ProfileTypeLabel;
        }
    }
}
