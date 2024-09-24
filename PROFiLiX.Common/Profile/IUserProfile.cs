// <copyright file="IUserProfile.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Profile
{
    using PROFiLiX.Common.Profile.Model;

    /// <summary>
    /// Public Interface for the UserProfile Class.
    /// </summary>
    public interface IUserProfile
    {
        /// <summary>
        /// Gets or Sets The LocalRootKey.
        /// </summary>
        public string[] LocalRootKey { get; set; }

        /// <summary>
        /// Gets or Sets The TempFolders.
        /// </summary>
        public string[] TempFolders { get; set; }

        /// <summary>
        /// Gets or Sets The ShellFolders.
        /// </summary>
        public string[] ShellFolders { get; set; }

        /// <summary>
        /// Gets or Sets The Citrix Root Key.
        /// </summary>
        public string[] CitrixRootKey { get; set; }

        /// <summary>
        /// Gets or Sets The FSLogixRootKey.
        /// </summary>
        public string[] FSLogixRootKey { get; set; }

        /// <summary>
        /// Gets or Sets The LiquidwareRootKey.
        /// </summary>
        public string[] LiquidwareRootKey { get; set; }

        /// <summary>
        /// Gets or Sets The ProfileActions.
        /// </summary>
        public ProfileAction[] ProfileActions { get; set; }

        /// <summary>
        /// Gets or Sets The CustomScriptsLocation.
        /// </summary>
        public string CustomScriptsLocation { get; set; }

        /// <summary>
        /// Gets or Sets The CustomScript Executable.
        /// </summary>
        public string CustomScriptExecutable { get; set; }

        /// <summary>
        /// Execute a profile action.
        /// </summary>
        /// <param name="actionDefinition">The Action Description.</param>
        /// <param name="profileDirectory">The Profile Directory.</param>
        /// <param name="userProfile">The User Profile.</param>
        public void ExecuteAction(ProfileActionDefinition actionDefinition, string profileDirectory, IUserProfile userProfile);
    }
}