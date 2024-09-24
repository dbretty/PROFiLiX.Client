// <copyright file="ProfileAction.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Profile.Model
{
    /// <summary>
    /// Class to hold the Profile Actions.
    /// </summary>
    public class ProfileAction
    {
        /// <summary>
        /// Gets or Sets the Profile Action Label.
        /// </summary>
        public string? ActionLabel { get; set; }

        /// <summary>
        /// Gets or Sets the ProfileActionDefinition.
        /// </summary>
        public ProfileActionDefinition ActionDefinition { get; set; }

        /// <summary>
        /// Gets or Sets the Profile Type Label.
        /// </summary>
        /// <returns>A <see cref="string"/> with the profile size.</returns>
        public override string? ToString()
        {
            return this.ActionLabel;
        }
    }
}
