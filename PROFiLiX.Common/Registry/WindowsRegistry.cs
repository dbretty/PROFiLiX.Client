// <copyright file="WindowsRegistry.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>
namespace PROFiLiX.Common.Registry
{
    using System.Security;
    using Microsoft.Win32;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Logging.Model;
    using PROFiLiX.Common.Registry.Exceptions;
    using PROFiLiX.Common.Registry.Model;

    /// <summary>
    /// Class for Windows Registry.
    /// </summary>
    public class WindowsRegistry : IWindowsRegistry
    {
        /// <summary>
        /// Private ILogger interface.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRegistry"/> class.
        /// </summary>
        /// <param name="logger">The logger to pass in.</param>
        public WindowsRegistry(ILogger logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public object? GetRegistryValue(string valueName, string valueKey, RegistryHive registryHive)
        {
            if (string.IsNullOrWhiteSpace(valueName) || string.IsNullOrWhiteSpace(valueKey))
            {
                throw new ArgumentNullException();
            }

            using (RegistryKey? localKey = GetRegistryHive(registryHive))
            {
                if (localKey is null)
                {
                    this.logger.LogAsync($"Invalid registry hive used. LocalMachine, CurrentUser are supported", LogLevel.ERROR);
                    throw new InvalidRootKeyException();
                }
                else
                {
                    RegistryKey? localFullKey = localKey.OpenSubKey(valueKey);
                    if (localFullKey is null)
                    {
                        this.logger.LogAsync($"Registry path not found: {valueKey}", LogLevel.ERROR);
                        throw new InvalidKeyException();
                    }
                    else
                    {
                        object? localValue = localFullKey.GetValue(valueName);
                        if (localValue is null)
                        {
                            this.logger.LogAsync($"Registry value not found: {valueName}", LogLevel.ERROR);
                            return null;
                        }
                        else
                        {
                            this.logger.LogAsync($"Registry value read: {valueKey} - {valueName} - {localValue}");
                            return localValue;
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task<object?> GetRegistryValueAsync(string valueName, string valueKey, RegistryHive registryHive)
        {
            if (string.IsNullOrWhiteSpace(valueName) || string.IsNullOrWhiteSpace(valueKey))
            {
                throw new ArgumentNullException();
            }

            return await Task.Run(() => this.GetRegistryValue(valueName, valueKey, registryHive));
        }

        /// <inheritdoc/>
        public bool GetRegistryKey(string valueKey, RegistryHive registryHive)
        {
            if (string.IsNullOrWhiteSpace(valueKey))
            {
                throw new ArgumentNullException();
            }

            using (RegistryKey? localKey = GetRegistryHive(registryHive))
            {
                if (localKey is null)
                {
                    this.logger.LogAsync($"Invalid registry hive used. LocalMachine, CurrentUser are supported", LogLevel.ERROR);
                    throw new InvalidRootKeyException();
                }
                else
                {
                    RegistryKey? localFullKey = localKey.OpenSubKey(valueKey, false);
                    if (localFullKey is not null)
                    {
                        this.logger.LogAsync($"Registry key found: {localFullKey}");
                        return true;
                    }
                    else
                    {
                        this.logger.LogAsync($"Registry key not found: {localFullKey}", LogLevel.WARNING);
                        return false;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public List<RegistryPathValue> GetRegistryPathValue(string[] rootPath, RegistryHive registryHive)
        {
            if (rootPath is null || rootPath.Length == 0)
            {
                throw new ArgumentNullException();
            }

            using (RegistryKey? localKey = GetRegistryHive(registryHive))
            {
                if (localKey is null)
                {
                    this.logger.LogAsync($"Invalid registry hive used. LocalMachine, CurrentUser are supported", LogLevel.ERROR);
                    throw new InvalidRootKeyException();
                }
                else
                {
                    var regPathValue = new List<RegistryPathValue>();
                    foreach (var key in rootPath)
                    {
                        RegistryKey? localFullKey = localKey.OpenSubKey(key, false);
                        if (localFullKey is not null)
                        {
                            this.logger.LogAsync($"Registry key found: {localFullKey}, building Key/Value pairs");
                            foreach (string value in localFullKey.GetValueNames())
                            {
                                RegistryPathValue localValue = new RegistryPathValue();
                                localValue.Path = localFullKey.Name;
                                localValue.Key = value;
                                object? localValueDetail = localFullKey.GetValue(value);
                                localValue.Value = localValueDetail;
                                regPathValue.Add(localValue);
                            }
                        }
                        else
                        {
                            this.logger.LogAsync($"Registry key not found: {localFullKey}", LogLevel.WARNING);
                        }
                    }

                    return regPathValue;
                }
            }
        }

        /// <inheritdoc/>
        public async Task<List<RegistryPathValue>> GetRegistryPathValueAsync(string[] rootPath, RegistryHive registryHive)
        {
            if (rootPath is null || rootPath.Length == 0)
            {
                throw new ArgumentNullException();
            }

            return await Task.Run(() => this.GetRegistryPathValue(rootPath, registryHive));
        }

        /// <inheritdoc/>
        public bool CreateRegistryKey(string valueKey, RegistryHive registryHive)
        {
            if (string.IsNullOrWhiteSpace(valueKey))
            {
                throw new ArgumentNullException();
            }

            using (RegistryKey? localKey = GetRegistryHive(registryHive))
            {
                if (localKey is null)
                {
                    this.logger.LogAsync($"Invalid registry hive used. LocalMachine, CurrentUser are supported", LogLevel.ERROR);
                    throw new InvalidRootKeyException();
                }
                else
                {
                    RegistryKey? localFullKey = localKey.OpenSubKey(valueKey, true);
                    if (localFullKey is null)
                    {
                        localKey.CreateSubKey(valueKey, true);
                        this.logger.LogAsync($"Registry key: {localKey} created");
                        return true;
                    }
                    else
                    {
                        this.logger.LogAsync($"Registry key already exists: {localKey}", LogLevel.ERROR);
                        throw new InvalidKeyException("Registry Key already exists");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public bool SetRegistryValue(string valueName, string valueKey, object valueData, RegistryHive registryHive)
        {
            if (string.IsNullOrWhiteSpace(valueKey) || string.IsNullOrWhiteSpace(valueName))
            {
                throw new ArgumentNullException();
            }

            using (RegistryKey? localKey = GetRegistryHive(registryHive))
            {
                if (localKey is null)
                {
                    this.logger.LogAsync($"Invalid registry hive used. LocalMachine, CurrentUser are supported", LogLevel.ERROR);
                    throw new InvalidRootKeyException();
                }
                else
                {
                    RegistryKey? localFullKey = localKey.OpenSubKey(valueKey, true);
                    if (localFullKey is null)
                    {
                        return false;
                    }
                    else
                    {
                        localFullKey.SetValue(valueName, valueData);
                        this.logger.LogAsync($"Registry value created: {valueName} - {valueData}");
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the root key for a registry action.
        /// </summary>
        /// <param name="registryHive">The base root key to action (HKLM, HKCU, HKCR). <see cref="RegistryHive"/>.</param>
        /// <returns>A <see cref="RegistryKey"/> or a null value.</returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static RegistryKey? GetRegistryHive(RegistryHive registryHive = RegistryHive.LocalMachine)
        {
            RegistryKey? localKey = null;

            try
            {
                localKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Registry64);
                return localKey;
            }
            catch (Exception)
            {
                return localKey;
            }
        }
    }
}