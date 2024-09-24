// <copyright file="ProfileTests.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Tests.Profile
{
    using Moq;
    using NUnit.Framework;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Profile;
    using PROFiLiX.Common.Profile.Model;

    /// <summary>
    /// Class to do Profile unit tests.
    /// </summary>
    [TestFixture]
    public class ProfileTests
    {
        /// <summary>
        /// Test method to ensure ExecuteAction with valid data succeeds.
        /// </summary>
        [Test]
        public void ExecuteAction_WithClearTempFiles_ShouldSucceed()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockFilesAndFolders = new Mock<IFilesAndFolders>();
            var mockUserProfile = new UserProfile(mockLogger.Object, mockFilesAndFolders.Object);

            Assert.DoesNotThrow(() => mockUserProfile.ExecuteAction(ProfileActionDefinition.ClearTempFiles, "test", mockUserProfile));
        }

        /// <summary>
        /// Test method to ensure ExecuteAction with valid data succeeds.
        /// </summary>
        [Test]
        public void ExecuteAction_WithRunCustomScripts_ShouldSucceed()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockFilesAndFolders = new Mock<IFilesAndFolders>();
            var mockUserProfile = new UserProfile(mockLogger.Object, mockFilesAndFolders.Object);

            Assert.DoesNotThrow(() => mockUserProfile.ExecuteAction(ProfileActionDefinition.RunCustomScripts, "c:\\users", mockUserProfile));
        }

        /// <summary>
        /// Test method to ensure ExecuteAction with valid data succeeds.
        /// </summary>
        [Test]
        public void ExecuteAction_WithResetEdge_ShouldSucceed()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockFilesAndFolders = new Mock<IFilesAndFolders>();
            var mockUserProfile = new UserProfile(mockLogger.Object, mockFilesAndFolders.Object);

            Assert.DoesNotThrow(() => mockUserProfile.ExecuteAction(ProfileActionDefinition.ResetEdge, "test", mockUserProfile));
        }

        /// <summary>
        /// Checks that ProfileAction returns string value.
        /// </summary>
        [Test]
        public void ProfileAction_WithValidAction_ShouldReturnString()
        {
            // Arrange
            var profileAction = new ProfileAction[]
            {
                new ProfileAction
                {
                    ActionLabel = "Clean Temporary Data",
                    ActionDefinition = ProfileActionDefinition.ClearTempFiles,
                },
            };

            // Act
            var response = profileAction[0].ToString();

            // Assert
            Assert.That(response, Is.EqualTo("Clean Temporary Data"));
        }

        /// <summary>
        /// Checks that ProfileAction returns valud data.
        /// </summary>
        [Test]
        public void ProfileAction_WithValidData_ShouldReturnString()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockFilesAndFolders = new Mock<IFilesAndFolders>();
            var mockUserProfile = new UserProfile(mockLogger.Object, mockFilesAndFolders.Object);

            // Act
            ProfileAction profileAction = mockUserProfile.ProfileActions[0];
            var response = profileAction;

            // Assert
            Assert.That(response, Is.EqualTo(profileAction));
        }
    }
}
