// <copyright file="UserDetail.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>
namespace PROFiLiX.Common.User
{
    using Microsoft.Win32;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Logging.Model;
    using PROFiLiX.Common.Registry;
    using PROFiLiX.Common.User.Model;

    /// <summary>
    /// Class for User Detail.
    /// </summary>
    public class UserDetail : IUserDetail
    {
        /// <summary>
        /// Volatile environment registry location.
        /// </summary>
        private const string VolatileEnvironmentValue = "Volatile Environment";

        /// <summary>
        /// Volatile environment Username location.
        /// </summary>
        private const string UserNameValue = "USERNAME";

        /// <summary>
        /// Volatile environment Domain location.
        /// </summary>
        private const string UserDomainValue = "USERDOMAIN";

        /// <summary>
        /// Volatile environment User Profile location.
        /// </summary>
        private const string UserProfileDirectoryValue = "USERPROFILE";

        /// <summary>
        /// Volatile environment Local AppData location.
        /// </summary>
        private const string UserLocalAppDataValue = "LOCALAPPDATA";

        /// <summary>
        /// Volatile environment Roaming AppData location.
        /// </summary>
        private const string UserRoamingAppDataValue = "APPDATA";

        /// <summary>
        /// FSLogix Enabled Value.
        /// </summary>
        private const int FSLogixValue = 1;

        /// <summary>
        /// FSLogix Key Value.
        /// </summary>
        private const string FSLogixKey = "Enabled";

        /// <summary>
        /// FSLogix Registry Location.
        /// </summary>
        private const string FSLogixRoot = "Software\\FSLogix\\Profiles";

        /// <summary>
        /// CPM Enabled Value.
        /// </summary>
        private const int CPMValue = 1;

        /// <summary>
        /// CPM Key Value.
        /// </summary>
        private const string CPMKey = "ServiceActive";

        /// <summary>
        /// CPM Registry Location.
        /// </summary>
        private const string CPMRoot = "Software\\Policies\\Citrix\\UserProfileManager";

        /// <summary>
        /// CPM Enabled Value.
        /// </summary>
        private const int LiquidwareValue = 1;

        /// <summary>
        /// CPM Key Value.
        /// </summary>
        private const string LiquidwareKey = "pdenabled";

        /// <summary>
        /// CPM Registry Location.
        /// </summary>
        private const string LiquidwareRoot = "Software\\Liquidware Labs\\ProfileUnity";

        /// <summary>
        /// Private ILogger interface.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Private IWindowsRegistry interface.
        /// </summary>
        private readonly IWindowsRegistry registry;

        /// <summary>
        /// Private IFilesAndFolders interface.
        /// </summary>
        private readonly IFilesAndFolders filesAndFolders;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetail"/> class.
        /// </summary>
        /// <param name="logger">The Logging interface.</param>
        /// <param name="registry">The Registry interface.</param>
        /// <param name="filesAndFolders">The Files and Folders interface.</param>
        public UserDetail(ILogger logger, IWindowsRegistry registry, IFilesAndFolders filesAndFolders)
        {
            this.logger = logger;
            this.registry = registry;
            this.filesAndFolders = filesAndFolders;
            this.GetUserData();
        }

        /// <summary>
        /// Gets or sets the ProfileTypes.
        /// </summary>
        public ProfileType[] ProfileType { get; set; } =
            {
                new ProfileType
                {
                    ProfileTypeLabel = "Local",
                    ProfileTypeDefinition = ProfileTypeDefinition.Local,
                },
                new ProfileType
                {
                    ProfileTypeLabel = "Citrix",
                    ProfileTypeDefinition = ProfileTypeDefinition.Citrix,
                },
                new ProfileType
                {
                    ProfileTypeLabel = "FSLogix",
                    ProfileTypeDefinition = ProfileTypeDefinition.FSLogix,
                },
                new ProfileType
                {
                    ProfileTypeLabel = "Liquidware",
                    ProfileTypeDefinition = ProfileTypeDefinition.Liquidware,
                },
            };

        /// <inheritdoc/>
        public string? UserName { get; set; }

        /// <inheritdoc/>
        public string? Domain { get; set; }

        /// <inheritdoc/>
        public string? ProfileDirectory { get; set; }

        /// <inheritdoc/>
        public string? AppDataLocal { get; set; }

        /// <inheritdoc/>
        public string? AppDataRoaming { get; set; }

        /// <inheritdoc/>
        public string? ProfileSize { get; set; }

        /// <inheritdoc/>
        public long? ProfileSizeRaw { get; set; }

        /// <inheritdoc/>
        public string? UserProfileType { get; set; }

        /// <inheritdoc/>
        public ProfileTypeDefinition ProfileDefinition { get; set; }

        /// <inheritdoc/>
        public string UpdateProfileSize(string profileDirectory)
        {
            if (string.IsNullOrWhiteSpace(profileDirectory))
            {
                throw new ArgumentNullException();
            }

            var profileSizeRaw = this.filesAndFolders.DirectorySizeAsync(new DirectoryInfo(profileDirectory));
            long profileSizeLong = profileSizeRaw.GetAwaiter().GetResult();
            var profileSize = this.filesAndFolders.FormatFileSize(profileSizeLong);

            this.ProfileSize = profileSize;
            this.ProfileSizeRaw = profileSizeLong;

            return profileSize;
        }

        /// <inheritdoc/>
        public async Task<string> UpdateProfileSizeAsync(string profileDirectory)
        {
            if (string.IsNullOrWhiteSpace(profileDirectory))
            {
                throw new ArgumentNullException();
            }

            return await Task.Run(() => this.UpdateProfileSize(profileDirectory));
        }

        /// <summary>
        /// Gets the user data (async).
        /// </summary>
        private async void GetUserData()
        {
            await this.logger.LogAsync("Getting User Details");

            var userNameTask = await this.registry.GetRegistryValueAsync(UserNameValue, VolatileEnvironmentValue, RegistryHive.CurrentUser);
            this.UserName = (string?)userNameTask;
            await this.logger.LogAsync($"Username: {this.UserName}");

            var domainTask = await this.registry.GetRegistryValueAsync(UserDomainValue, VolatileEnvironmentValue, RegistryHive.CurrentUser);
            this.Domain = (string?)domainTask;
            await this.logger.LogAsync($"Domain: {this.Domain}");

            var profileDirectoryTask = await this.registry.GetRegistryValueAsync(UserProfileDirectoryValue, VolatileEnvironmentValue, RegistryHive.CurrentUser);
            this.ProfileDirectory = (string?)profileDirectoryTask;
            await this.logger.LogAsync($"Profile Directory: {this.ProfileDirectory}");

            var localAppDataTask = await this.registry.GetRegistryValueAsync(UserLocalAppDataValue, VolatileEnvironmentValue, RegistryHive.CurrentUser);
            this.AppDataLocal = (string?)localAppDataTask;
            await this.logger.LogAsync($"Appdata Local: {this.AppDataLocal}");

            var roamingAppDataTask = await this.registry.GetRegistryValueAsync(UserRoamingAppDataValue, VolatileEnvironmentValue, RegistryHive.CurrentUser);
            this.AppDataRoaming = (string?)roamingAppDataTask;
            await this.logger.LogAsync($"Appdata Roaming: {this.AppDataRoaming}");

            this.GetProfileType();
            await this.logger.LogAsync($"Profile Type: {this.ProfileType}");

            this.ProfileSize = "Calculating";
        }

        /// <summary>
        /// Gets the User Profile Type.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private void GetProfileType()
        {
            var profileTypeFound = false;

            try
            {
                if (this.registry.GetRegistryKey(FSLogixRoot, RegistryHive.LocalMachine))
                {
                    var fslValue = this.registry.GetRegistryValue(FSLogixKey, FSLogixRoot, RegistryHive.LocalMachine);
                    if (fslValue is not null)
                    {
                        switch (fslValue)
                        {
                            case FSLogixValue:
                                this.UserProfileType = ProfileTypeDefinition.FSLogix.ToString();
                                this.ProfileDefinition = ProfileTypeDefinition.FSLogix;
                                profileTypeFound = true;
                                break;
                            }
                        }
                    }
                }
                catch
                {
                // No Registry Key Found
                }

            try
            {
                if (this.registry.GetRegistryKey(CPMRoot, RegistryHive.LocalMachine))
                {
                    var cpmValue = this.registry.GetRegistryValue(CPMKey, CPMRoot, RegistryHive.LocalMachine);
                    if (cpmValue is not null)
                    {
                        switch (cpmValue)
                        {
                            case CPMValue:
                                this.UserProfileType = ProfileTypeDefinition.Citrix.ToString();
                                this.ProfileDefinition = ProfileTypeDefinition.Citrix;
                                profileTypeFound = true;
                                break;
                        }
                    }
                }
            }
            catch
            {
                // No Registry Key Found
            }

            try
            {
                if (this.registry.GetRegistryKey(CPMRoot, RegistryHive.LocalMachine))
                {
                    var liquidwareValue = this.registry.GetRegistryValue(LiquidwareKey, LiquidwareRoot, RegistryHive.LocalMachine);
                    if (liquidwareValue is not null)
                    {
                        switch (liquidwareValue)
                        {
                            case LiquidwareValue:
                                this.UserProfileType = ProfileTypeDefinition.Liquidware.ToString();
                                this.ProfileDefinition = ProfileTypeDefinition.Liquidware;
                                profileTypeFound = true;
                                break;
                        }
                    }
                }
            }
            catch
            {
                // No Registry Key Found
            }

            if (!profileTypeFound)
            {
                this.UserProfileType = ProfileTypeDefinition.Local.ToString();
                this.ProfileDefinition = ProfileTypeDefinition.Local;
            }
        }
    }
}
