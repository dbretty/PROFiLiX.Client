// <copyright file="IAppConfig.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Configuration
{
    using PROFiLiX.Common.ApiClient;
    using PROFiLiX.Common.Device;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Profile;
    using PROFiLiX.Common.Registry;
    using PROFiLiX.Common.User;

    /// <summary>
    /// Interface for the AppConfig Class.
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// Gets The FilesAndFolders.
        /// </summary>
        IFilesAndFolders FilesAndFolders { get; }

        /// <summary>
        /// Gets The Logger.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Gets The Registry.
        /// </summary>
        IWindowsRegistry Registry { get;  }

        /// <summary>
        /// Gets The UserDetail.
        /// </summary>
        IUserDetail UserDetail { get;  }

        /// <summary>
        /// Gets The UserProfile.
        /// </summary>
        IUserProfile UserProfile { get; }

        /// <summary>
        /// Gets The Device Details.
        /// </summary>
        IDeviceInfo Device { get; }

        /// <summary>
        /// Gets The Task Information Client.
        /// </summary>
        ProfilixTaskClient TaskClient { get; }

        /// <summary>
        /// Gets The Task Information Client.
        /// </summary>
        UserProfileClient UserProfileClient { get; }

        /// <summary>
        /// Gets The Application Registry Key.
        /// </summary>
        string AppRegistryKey { get; }

        /// <summary>
        /// Gets or Sets The Application Registry Key.
        /// </summary>
        string LogLevel { get; set; }

        /// <summary>
        /// Gets or Sets Clear Temp At Start.
        /// </summary>
        string ClearTempAtStart { get; set; }

        /// <summary>
        /// Gets or Sets Log To Server.
        /// </summary>
        string LogToServer { get; set; }

        /// <summary>
        /// Gets or Sets Logging Server Uri.
        /// </summary>
        string LoggingServerUri { get; set; }

        /// <summary>
        /// Gets or Sets Logging Server Port.
        /// </summary>
        string LoggingServerPort { get; set; }

        /// <summary>
        /// Gets or Sets Logging Server Protocol.
        /// </summary>
        string LoggingServerProtocol { get; set; }

        /// <summary>
        /// Gets or Sets User Profile Guid.
        /// </summary>
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Hub Connection is active.
        /// </summary>
        bool HubConnection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the Hub Connection Id.
        /// </summary>
        string HubConnectionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Server Online Status.
        /// </summary>
        bool ServerOnline { get; set; }
    }
}