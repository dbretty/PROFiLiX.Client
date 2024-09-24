// <copyright file="AppConfigTests.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Tests.AppConfig
{
    using Moq;
    using NUnit.Framework;
    using PROFiLiX.Common.Configuration;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Profile;
    using PROFiLiX.Common.Registry;
    using PROFiLiX.Common.User;

    /// <summary>
    /// Class to do AppConfig unit tests.
    /// </summary>
    [TestFixture]
    public class AppConfigTests
    {
        /// <summary>
        /// Test method to ensure AppConfig with valid data succeeds.
        /// </summary>
        [Test]
        public void AppConfig_WithValidPropertyValues_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new Mock<IWindowsRegistry>();
            var mockFilesAndFolders = new Mock<IFilesAndFolders>();
            var mockUserProfile = new Mock<IUserProfile>();
            var mockUserDetail = new Mock<IUserDetail>();

            // Act
            var ac = new AppConfig()
            {
                Logger = mockILogger.Object,
                Registry = mockRegistry.Object,
                FilesAndFolders = mockFilesAndFolders.Object,
                UserProfile = mockUserProfile.Object,
                UserDetail = mockUserDetail.Object,
            };

            // Assert
            Assert.That(ac.AppRegistryKey, Is.EqualTo("Software\\PROFiLiX"));
            Assert.That(ac.LogLevel, Is.EqualTo("Info"));
            Assert.That(ac.LoggingServerUri, Is.EqualTo("localhost"));
            Assert.That(ac.LoggingServerPort, Is.EqualTo("5120"));
            Assert.That(ac.LoggingServerProtocol, Is.EqualTo("http"));
        }
    }
}
