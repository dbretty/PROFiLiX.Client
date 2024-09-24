// <copyright file="IDeviceInfo.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Device
{
    /// <summary>
    /// Interface for the DeviceInfo Class.
    /// </summary>
    public interface IDeviceInfo
    {
        /// <summary>
        /// Gets or Sets The Operating System.
        /// </summary>
        string? OperatingSystem { get; set; }

        /// <summary>
        /// Gets or Sets The Operating System Version.
        /// </summary>
        string? OperatingSystemVersion { get; set; }

        /// <summary>
        /// Gets or Sets The Number Of CPUs.
        /// </summary>
        int NumberOfCPUs { get; set; }

        /// <summary>
        /// Gets or Sets The Memory in MB.
        /// </summary>
        int MemoryInMB { get; set; }

        /// <summary>
        /// Gets or Sets The Memory in GB.
        /// </summary>
        int MemoryInGB { get; set; }

        /// <summary>
        /// Gets or Sets The User Domain.
        /// </summary>
        string? UserDomain { get; set; }
    }
}