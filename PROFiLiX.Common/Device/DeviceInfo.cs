// <copyright file="DeviceInfo.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Device
{
    using Microsoft.Win32;
    using PROFiLiX.Common.Registry;

    /// <summary>
    /// Device Info Class.
    /// </summary>
    public class DeviceInfo : IDeviceInfo
    {
        /// <summary>
        /// Private Windows Registry interface.
        /// </summary>
        private readonly IWindowsRegistry windowsRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInfo"/> class.
        /// </summary>
        /// <param name="registry">The registry interface.</param>
        public DeviceInfo(IWindowsRegistry registry)
        {
            this.windowsRegistry = registry;
            this.OperatingSystem = (string?)registry.GetRegistryValue("ProductName", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", RegistryHive.LocalMachine);
            this.OperatingSystemVersion = (string?)registry.GetRegistryValue("DisplayVersion", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", RegistryHive.LocalMachine);
            this.NumberOfCPUs = GetProcessorCount();
            var gcMemoryInfo = GC.GetGCMemoryInfo();
            this.MemoryInMB = (int)(gcMemoryInfo.TotalAvailableMemoryBytes / 1024) / 1024;
            this.MemoryInGB = this.MemoryInMB / 1000;
            this.UserDomain = (string?)registry.GetRegistryValue("USERDOMAIN", "Volatile Environment", RegistryHive.CurrentUser);
        }

        /// <inheritdoc/>
        public string? OperatingSystem { get; set; }

        /// <inheritdoc/>
        public string? OperatingSystemVersion { get; set; }

        /// <inheritdoc/>
        public int NumberOfCPUs { get; set; }

        /// <inheritdoc/>
        public int MemoryInMB { get; set; }

        /// <inheritdoc/>
        public int MemoryInGB { get; set; }

        /// <inheritdoc/>
        public string? UserDomain { get; set; }

        /// <summary>
        /// Private Gets the processor count.
        /// </summary>
        private static int GetProcessorCount()
        {
            return Environment.ProcessorCount;
        }
    }
}
