// <copyright file="AppConfig.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Configuration
{
    using Microsoft.Win32;
    using PROFiLiX.Common.ApiClient;
    using PROFiLiX.Common.Device;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Profile;
    using PROFiLiX.Common.Registry;
    using PROFiLiX.Common.User;

    /// <summary>
    /// App Configuration Class.
    /// </summary>
    public class AppConfig : IAppConfig
    {
        /// <summary>
        /// Private ILogger interface.
        /// </summary>
        private const string ApplicationRegistryKey = "Software\\PROFiLiX";

        /// <summary>
        /// Private LogLevel Info.
        /// </summary>
        private const string LoggingLevelInfo = "Info";

        /// <summary>
        /// Private LogLevel Info.
        /// </summary>
        private const string LoggingLevelDebug = "Debug";

        /// <summary>
        /// Private Clear Temp at Start.
        /// </summary>
        private const string ClearTemp = "No";

        /// <summary>
        /// Private Log To Server.
        /// </summary>
        private const string LogServer = "No";

        /// <summary>
        /// Private Logging Server Uri.
        /// </summary>
        private const string LogServerUri = "localhost";

        /// <summary>
        /// Private Logging Server Port.
        /// </summary>
        private const string LogServerPort = "5120";

        /// <summary>
        /// Private Logging Server protocol.
        /// </summary>
        private const string LogServerProtocol = "http";

        /// <summary>
        /// Private Server Online.
        /// </summary>
        private const bool Online = false;

        /// <summary>
        /// Hub Connection.
        /// </summary>
        private const bool HubConnectionActive = false;

        /// <summary>
        /// Hub Connection.
        /// </summary>
        private string hubConnectionId = string.Empty;

        /// <summary>
        /// Private User Profile GUID.
        /// </summary>
        private int userId = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfig"/> class.
        /// </summary>
        public AppConfig()
        {
            this.Logger = new Logger();
            this.Registry = new WindowsRegistry(this.Logger);
            this.FilesAndFolders = new FilesAndFolders(this.Logger);
            this.UserProfile = new PROFiLiX.Common.Profile.UserProfile(this.Logger, this.FilesAndFolders);
            this.UserDetail = new UserDetail(this.Logger, this.Registry, this.FilesAndFolders);
            this.Device = new DeviceInfo(this.Registry);
            this.TaskClient = new ProfilixTaskClient();
            this.UserProfileClient = new UserProfileClient();
            this.AppRegistryKey = ApplicationRegistryKey;
            this.LogLevel = LoggingLevelInfo;
            this.ClearTempAtStart = ClearTemp;
            this.LogToServer = LogServer;
            this.LoggingServerUri = LogServerUri;
            this.LoggingServerPort = LogServerPort;
            this.LoggingServerProtocol = LogServerProtocol;
            this.UserId = this.userId;
            this.HubConnection = HubConnectionActive;
            this.HubConnectionId = this.hubConnectionId;
            this.ServerOnline = Online;
            this.EUCProfileBuddyStartup();
        }

        /// <inheritdoc/>
        public ILogger Logger { get; set; }

        /// <inheritdoc/>
        public IWindowsRegistry Registry { get; set; }

        /// <inheritdoc/>
        public IFilesAndFolders FilesAndFolders { get; set; }

        /// <inheritdoc/>
        public IUserProfile UserProfile { get; set; }

        /// <inheritdoc/>
        public IUserDetail UserDetail { get; set; }

        /// <inheritdoc/>
        public IDeviceInfo Device { get; set; }

        /// <inheritdoc/>
        public ProfilixTaskClient TaskClient { get; set; }

        /// <inheritdoc/>
        public UserProfileClient UserProfileClient { get; set; }

        /// <inheritdoc/>
        public string AppRegistryKey { get; set; }

        /// <inheritdoc/>
        public string LogLevel { get; set; }

        /// <inheritdoc/>
        public string ClearTempAtStart { get; set; }

        /// <inheritdoc/>
        public string LogToServer { get; set; }

        /// <inheritdoc/>
        public string LoggingServerUri { get; set; }

        /// <inheritdoc/>
        public string LoggingServerPort { get; set; }

        /// <inheritdoc/>
        public string LoggingServerProtocol { get; set; }

        /// <inheritdoc/>
        public int UserId { get; set; }

        /// <inheritdoc/>
        public bool HubConnection { get; set; }

        /// <inheritdoc/>
        public string HubConnectionId { get; set; }

        /// <inheritdoc/>
        public bool ServerOnline { get; set; }

        /// <summary>
        /// Startup module to cater for start actions.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private void EUCProfileBuddyStartup()
        {
            this.Logger.LogAsync($"Checking for PROFiLiX registry key: {this.AppRegistryKey}");
            var keyExists = this.Registry.GetRegistryKey(this.AppRegistryKey, RegistryHive.CurrentUser);
            if (keyExists)
            {
                this.Logger.LogAsync($"Key exists, reading in application settings");
                var tempFolders = this.Registry.GetRegistryValue("TempDataLocations", this.AppRegistryKey, RegistryHive.CurrentUser);
                if (tempFolders is not null)
                {
                    this.Logger.LogAsync($"Temp folders: {tempFolders}");
                    this.UserProfile.TempFolders = (string[])tempFolders;
                }
                else
                {
                    this.Registry.SetRegistryValue("TempDataLocations", this.AppRegistryKey, this.UserProfile.TempFolders, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating TempDataLocations: {this.UserProfile.TempFolders}");
                }

                var logLevel = this.Registry.GetRegistryValue("LogLevel", this.AppRegistryKey, RegistryHive.CurrentUser);
                if (logLevel is not null)
                {
                    switch ((string)logLevel)
                    {
                        case LoggingLevelInfo:
                            this.Logger.LogAsync($"Log level: {LoggingLevelInfo}");
                            this.LogLevel = LoggingLevelInfo;
                            break;
                        case LoggingLevelDebug:
                            this.Logger.LogAsync($"Log level: {LoggingLevelDebug}");
                            this.LogLevel = LoggingLevelDebug;
                            break;
                        default:
                            this.Logger.LogAsync($"Log level: {LoggingLevelInfo}");
                            this.LogLevel = LoggingLevelInfo;
                            break;
                    }
                }
                else
                {
                    this.Registry.SetRegistryValue("LogLevel", this.AppRegistryKey, this.LogLevel, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LogLevel: {this.LogLevel}");
                }

                var clearTemp = this.Registry.GetRegistryValue("ClearTempAtStart", this.AppRegistryKey, RegistryHive.CurrentUser);
                if (clearTemp is not null)
                {
                    this.Logger.LogAsync($"Clear Temp At Start: {clearTemp}");
                    this.ClearTempAtStart = (string)clearTemp;
                }
                else
                {
                    this.Registry.SetRegistryValue("ClearTempAtStart", this.AppRegistryKey, this.ClearTempAtStart, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating ClearTempAtStart: {this.ClearTempAtStart}");
                }

                var logToServerRegistry = this.Registry.GetRegistryValue("LogToServer", this.AppRegistryKey, RegistryHive.CurrentUser);
                if (logToServerRegistry is not null)
                {
                    this.Logger.LogAsync($"Log To Server: {logToServerRegistry}");
                    this.LogToServer = (string)logToServerRegistry;
                }
                else
                {
                    this.Registry.SetRegistryValue("LogToServer", this.AppRegistryKey, this.LogToServer, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LogToServer: {this.LogToServer}");
                }

                var logServerUriRegistry = this.Registry.GetRegistryValue("LoggingServer", this.AppRegistryKey, RegistryHive.CurrentUser);
                var logServerPortRegistry = this.Registry.GetRegistryValue("LoggingServerPort", this.AppRegistryKey, RegistryHive.CurrentUser);
                var logServerProtocolRegistry = this.Registry.GetRegistryValue("LoggingServerProtocol", this.AppRegistryKey, RegistryHive.CurrentUser);
                if ((logServerUriRegistry is not null) && (logServerPortRegistry is not null) && (logServerProtocolRegistry is not null))
                {
                    this.Logger.LogAsync($"Logging Server Uri: {logServerUriRegistry}");
                    this.LoggingServerUri = (string)logServerUriRegistry;
                    this.LoggingServerPort = (string)logServerPortRegistry;
                    this.LoggingServerProtocol = (string)logServerProtocolRegistry;
                    var uri = this.LoggingServerProtocol + "://" + this.LoggingServerUri + ":" + this.LoggingServerPort;
                    this.TaskClient.BaseUrl = uri;
                    this.UserProfileClient.BaseUrl = uri;
                }
                else
                {
                    this.Registry.SetRegistryValue("LoggingServer", this.AppRegistryKey, this.LoggingServerUri, RegistryHive.CurrentUser);
                    this.Registry.SetRegistryValue("LoggingServerPort", this.AppRegistryKey, this.LoggingServerPort, RegistryHive.CurrentUser);
                    this.Registry.SetRegistryValue("LoggingServerProtocol", this.AppRegistryKey, this.LoggingServerProtocol, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LoggingServer: {this.LoggingServerUri}");
                    var uri = this.LoggingServerProtocol + "://" + this.LoggingServerUri + ":" + this.LoggingServerPort;
                    this.TaskClient.BaseUrl = uri;
                    this.UserProfileClient.BaseUrl = uri;
                }

                var userProfileIdRegistry = this.Registry.GetRegistryValue("UserProfileId", this.AppRegistryKey, RegistryHive.CurrentUser);
                if (userProfileIdRegistry is not null)
                {
                    var intUserId = (int)userProfileIdRegistry;
                    this.Logger.LogAsync($"UserProfileId: {userProfileIdRegistry}");
                    this.UserId = intUserId;
                }
                else
                {
                    this.Registry.SetRegistryValue("UserProfileId", this.AppRegistryKey, this.UserId, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating UserProfileId: {this.UserId}");
                }
            }
            else
            {
                this.Logger.LogAsync($"Key does not exist, creating: {this.AppRegistryKey}");
                if (this.Registry.CreateRegistryKey(this.AppRegistryKey, RegistryHive.CurrentUser))
                {
                    this.Registry.SetRegistryValue("TempDataLocations", this.AppRegistryKey, this.UserProfile.TempFolders, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating TempDataLocations: {this.UserProfile.TempFolders}");

                    this.Registry.SetRegistryValue("LogLevel", this.AppRegistryKey, this.LogLevel, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LogLevel: {this.LogLevel}");

                    this.Registry.SetRegistryValue("ClearTempAtStart", this.AppRegistryKey, this.ClearTempAtStart, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating ClearTempAtStart: {this.ClearTempAtStart}");

                    this.Registry.SetRegistryValue("LogToServer", this.AppRegistryKey, this.LogToServer, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LogToServer: {this.LogToServer}");

                    this.Registry.SetRegistryValue("LoggingServer", this.AppRegistryKey, this.LoggingServerUri, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LoggingServer: {this.LoggingServerUri}");

                    this.Registry.SetRegistryValue("LoggingServerPort", this.AppRegistryKey, this.LoggingServerPort, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LoggingServerPort: {this.LoggingServerPort}");

                    this.Registry.SetRegistryValue("LoggingServerProtocol", this.AppRegistryKey, this.LoggingServerProtocol, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating LoggingServerProtocol: {this.LoggingServerProtocol}");

                    this.Registry.SetRegistryValue("UserProfileId", this.AppRegistryKey, this.UserId, RegistryHive.CurrentUser);
                    this.Logger.LogAsync($"Creating UserProfileId: {this.UserId}");
                }
                else
                {
                    this.Logger.LogAsync($"Error creating: {this.AppRegistryKey}", Logging.Model.LogLevel.ERROR);
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
