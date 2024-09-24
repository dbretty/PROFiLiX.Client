// <copyright file="LoggerTests.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Tests.Logging
{
    using NUnit.Framework;
    using PROFiLiX.Common.Logging;

    /// <summary>
    /// Class to do Logger unit tests.
    /// </summary>
    [TestFixture]
    public class LoggerTests
    {
        /// <summary>
        /// Test method to ensure WriteLogAsync completes successfully.
        /// </summary>
        [Test]
        public void LogAsync_WithValidMessage_ShouldSucceed()
        {
            // Arrange
            ILogger logger = new Logger();

            // Act + Assert
            Assert.DoesNotThrowAsync(async () => await logger.LogAsync("unit test message"));
        }

        /// <summary>
        /// Test method to ensure WriteLogAsync errors correctly.
        /// </summary>
        /// <param name="logMessage">The log message to pass in.</param>
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void LogAsync_WithInvalidMessage_ShouldThrowArgumentNullException(string logMessage)
        {
            // Arrange
            ILogger logger = new Logger();

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await logger.LogAsync(logMessage));
        }
    }
}
