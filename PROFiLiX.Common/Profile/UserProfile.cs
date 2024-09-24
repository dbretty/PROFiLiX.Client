// <copyright file="UserProfile.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Profile
{
    using System.Diagnostics;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Profile.Model;

    /// <summary>
    /// Class to get user profile information.
    /// </summary>
    public class UserProfile : IUserProfile
    {
        /// <summary>
        /// Private ILogger interface.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Private ILogger interface.
        /// </summary>
        private readonly IFilesAndFolders filesAndFolders;

        /// <summary>
        /// Custom Scripts Location.
        /// </summary>
        private readonly string customScriptsLocation = "AppData\\Local\\PROFiLiX";

        /// <summary>
        /// Local Profile Management Keys.
        /// </summary>
        private readonly string customScriptExecutable = "powershell.exe";

        /// <summary>
        /// Temporary Files Location.
        /// </summary>
        private readonly string[] tempFolders =
        {
            "AppData\\Local\\Temp",
            "AppData\\Roaming\\Temp",
            "AppData\\Local\\Microsoft\\Windows\\GameExplorer",
            "AppData\\Local\\Microsoft\\Windows\\WER",
            "AppData\\Local\\Microsoft\\Windows\\CrashReports",
            "AppData\\Local\\Microsoft\\MSOIdentityCRL\\Tracing",
            "AppData\\Local\\CrashDumps",
            "AppData\\Local\\Package Cache",
            "AppData\\Local\\D3DSCache",
            "AppData\\Local\\Microsoft\\Windows\\WebCache.old",
        };

        /// <summary>
        /// Shell Folders Location.
        /// </summary>
        private readonly string[] shellFolders =
        {
            "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\User Shell Folders",
        };

        /// <summary>
        /// Citrix Profile Management Keys.
        /// </summary>
        private readonly string[] citrixRootKey =
        {
            "Software\\Policies\\Citrix\\UserProfileManager",
        };

        /// <summary>
        /// FSLogix Management Keys.
        /// </summary>
        private readonly string[] fslogixRootKey =
        {
            "Software\\FSLogix\\Profiles",
        };

        /// <summary>
        /// Liquidware Management Keys.
        /// </summary>
        private readonly string[] liquidwareRootKey =
        {
            "Software\\Liquidware Labs\\ProfileUnity",
            "Software\\WOW6432NODE\\Liquidware Labs\\ProfileUnity",
        };

        /// <summary>
        /// Local Profile Management Keys.
        /// </summary>
        private readonly string[] localRootKey =
        {
            "Volatile Environment",
        };

        /// <summary>
        /// Microsoft Edge Exe File.
        /// </summary>
        private readonly string msEdgeFile = "msedge";

        /// <summary>
        /// Google Chrome Exe File.
        /// </summary>
        private readonly string googleChromeFile = "chrome";

        /// <summary>
        /// Mozilla Firefox Exe File.
        /// </summary>
        private readonly string mozillaFirefoxFile = "firefox";

        /// <summary>
        /// Teams Classic Exe File.
        /// </summary>
        private readonly string teamsClassicFile = "msteams";

        /// <summary>
        /// Teams Exe File.
        /// </summary>
        private readonly string teamsFile = "msteams";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        /// <param name="filesAndFolders">The Files and Folders interface.</param>
        public UserProfile(ILogger logger, IFilesAndFolders filesAndFolders)
        {
            this.logger = logger;
            this.filesAndFolders = filesAndFolders;
            this.ShellFolders = this.shellFolders;
            this.CitrixRootKey = this.citrixRootKey;
            this.FSLogixRootKey = this.fslogixRootKey;
            this.LiquidwareRootKey = this.liquidwareRootKey;
            this.LocalRootKey = this.localRootKey;
            this.CustomScriptsLocation = this.customScriptsLocation;
            this.CustomScriptExecutable = this.customScriptExecutable;
            this.TempFolders = this.tempFolders;
        }

        /// <summary>
        /// Gets or sets the Actions.
        /// </summary>
        public ProfileAction[] ProfileActions { get; set; } =
            {
                new ProfileAction
                {
                    ActionLabel = "Clean Temporary Data",
                    ActionDefinition = ProfileActionDefinition.ClearTempFiles,
                },
                new ProfileAction
                {
                    ActionLabel = "Run Custom Scripts",
                    ActionDefinition = ProfileActionDefinition.RunCustomScripts,
                },
                new ProfileAction
                {
                    ActionLabel = "Reset Microsoft Edge",
                    ActionDefinition = ProfileActionDefinition.ResetEdge,
                },
                new ProfileAction
                {
                    ActionLabel = "Reset Google Chrome",
                    ActionDefinition = ProfileActionDefinition.ResetChrome,
                },
                new ProfileAction
                {
                    ActionLabel = "Reset Mozilla Firefox",
                    ActionDefinition = ProfileActionDefinition.ResetFirefox,
                },
                new ProfileAction
                {
                    ActionLabel = "Reset Teams Classic",
                    ActionDefinition = ProfileActionDefinition.ResetTeamsv1,
                },
                new ProfileAction
                {
                    ActionLabel = "Reset Teams",
                    ActionDefinition = ProfileActionDefinition.ResetTeamsv2,
                },
            };

        /// <inheritdoc/>
        public string[] ShellFolders { get; set; }

        /// <inheritdoc/>
        public string[] CitrixRootKey { get; set; }

        /// <inheritdoc/>
        public string[] FSLogixRootKey { get; set; }

        /// <inheritdoc/>
        public string[] LiquidwareRootKey { get; set; }

        /// <inheritdoc/>
        public string[] LocalRootKey { get; set; }

        /// <inheritdoc/>
        public string CustomScriptsLocation { get; set; }

        /// <inheritdoc/>
        public string CustomScriptExecutable { get; set; }

        /// <inheritdoc/>
        public string[] TempFolders { get; set; }

        /// <inheritdoc/>
        public async void ExecuteAction(ProfileActionDefinition actionDefinition, string profileDirectory, IUserProfile userProfile)
        {
            ArgumentException.ThrowIfNullOrEmpty(profileDirectory, nameof(profileDirectory));
            ArgumentNullException.ThrowIfNull(actionDefinition, nameof(actionDefinition));
            ArgumentNullException.ThrowIfNull(userProfile, nameof(userProfile));

            switch (actionDefinition)
            {
                case ProfileActionDefinition.ClearTempFiles:
                    await this.logger.LogAsync($"Executing action: Clear Temp Files");
                    foreach (var subFolder in userProfile.TempFolders)
                    {
                        await this.filesAndFolders.DeleteFolderAsync(Path.Join(profileDirectory, subFolder));
                    }

                    await this.logger.LogAsync($"Completed action: Clear Temp Files");
                    break;
                case ProfileActionDefinition.RunCustomScripts:
                    await this.logger.LogAsync($"Executing action: Run Custom Scripts");
                    this.ExecuteCustomScriptAsync(profileDirectory);
                    await this.logger.LogAsync($"Completed action: Run Custom Scripts");
                    break;
                case ProfileActionDefinition.ResetEdge:
                    await this.logger.LogAsync($"Executing action: Reset Microsoft Edge");
                    this.ResetMicrosoftEdge();
                    await this.logger.LogAsync($"Completed action: Reset Microsoft Edge");
                    break;
                case ProfileActionDefinition.ResetChrome:
                    await this.logger.LogAsync($"Executing action: Reset Google Chrome");
                    this.ResetGoogleChrome();
                    await this.logger.LogAsync($"Completed action: Reset Google Chromee");
                    break;
                case ProfileActionDefinition.ResetFirefox:
                    await this.logger.LogAsync($"Executing action: Reset Mozilla Firefox");
                    this.ResetMozillaFirefox();
                    await this.logger.LogAsync($"Completed action: Reset Mozilla Firefox");
                    break;
                case ProfileActionDefinition.ResetTeamsv1:
                    await this.logger.LogAsync($"Executing action: Reset Teams Classic");
                    this.ResetTeamsClassic();
                    await this.logger.LogAsync($"Completed action: Reset Teams Classic");
                    break;
                case ProfileActionDefinition.ResetTeamsv2:
                    await this.logger.LogAsync($"Executing action: Reset Teams");
                    this.ResetTeams();
                    await this.logger.LogAsync($"Completed action: Reset Teams");
                    break;
            }
        }

        /// <summary>
        /// Executes Custom Scripts.
        /// </summary>
        /// <param name="customScriptsLocation">The Custom Scripts Directory.</param>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private void ExecuteCustomScript(string customScriptsLocation)
        {
            ArgumentException.ThrowIfNullOrEmpty(customScriptsLocation, nameof(customScriptsLocation));

            try
            {
                var scriptFiles = Path.Join(customScriptsLocation, this.customScriptsLocation);

                var files = Directory.EnumerateFiles(customScriptsLocation, "*.ps1", SearchOption.AllDirectories);
                if (files is not null)
                {
                    foreach (var file in files)
                    {
                        var startInfo = new ProcessStartInfo()
                        {
                            FileName = this.customScriptExecutable,
                            Arguments = $"-NoProfile -ExecutionPolicy ByPass -File \"{file}\"",
                            UseShellExecute = false,
                        };
                        Process.Start(startInfo);
                    }
                }
            }
            catch (Exception)
            {
                this.logger.LogAsync($"Error executing custom scripts");
            }
        }

        /// <summary>
        /// Executes Custom Scripts (Async).
        /// </summary>
        /// <param name="customScriptsLocation">The Custom Scripts Directory.</param>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private async void ExecuteCustomScriptAsync(string customScriptsLocation)
        {
            ArgumentNullException.ThrowIfNull(customScriptsLocation, nameof(customScriptsLocation));

            await Task.Run(() => this.ExecuteCustomScript(customScriptsLocation));
        }

        /// <summary>
        /// Resets the Microsoft Edge Settings.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private async void ResetMicrosoftEdge()
        {
            var msEdgeData = "Microsoft\\Edge";

            string directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                msEdgeData);

            Process[] processes = Process.GetProcessesByName(this.msEdgeFile);
            foreach (var process in processes)
            {
                process.Kill();
            }

            await this.filesAndFolders.DeleteFolderAsync(directory);
        }

        /// <summary>
        /// Resets the Microsoft Edge Settings.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private async void ResetGoogleChrome()
        {
            var googleChromeData = "Google\\Chrome\\User Data";

            string directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                googleChromeData);

            Process[] processes = Process.GetProcessesByName(this.googleChromeFile);
            foreach (var process in processes)
            {
                process.Kill();
            }

            await this.filesAndFolders.DeleteFolderAsync(directory);
        }

        /// <summary>
        /// Resets the Mozilla Firefox Settings.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private async void ResetMozillaFirefox()
        {
            var mozillaFirefoxData = "Mozilla\\Firefox\\profiles.ini";

            string mozillaIniFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                mozillaFirefoxData);

            Process[] processes = Process.GetProcessesByName(this.mozillaFirefoxFile);
            foreach (var process in processes)
            {
                process.Kill();
            }

            await this.filesAndFolders.DeleteFileAsync(mozillaIniFile);
        }

        /// <summary>
        /// Resets Microsoft Teams Classic.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private async void ResetTeamsClassic()
        {
            var teamsClassic = "Microsoft\\Teams";

            string directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                teamsClassic);

            Process[] processes = Process.GetProcessesByName(this.teamsClassicFile);
            foreach (var process in processes)
            {
                process.Kill();
            }

            await this.filesAndFolders.DeleteFolderAsync(directory);
        }

        /// <summary>
        /// Resets Microsoft Teams.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private async void ResetTeams()
        {
            var teams = "Packages\\MSTeams_8wekyb3d8bbwe\\LocalCache\\Microsoft\\MSTeams";

            string directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                teams);

            Process[] processes = Process.GetProcessesByName(this.teamsFile);
            foreach (var process in processes)
            {
                process.Kill();
            }

            await this.filesAndFolders.DeleteFolderAsync(directory);
        }
    }
}
