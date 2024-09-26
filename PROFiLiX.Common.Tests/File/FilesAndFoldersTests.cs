// <copyright file="FilesAndFoldersTests.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Tests.File
{
    using System.IO;
    using Moq;
    using NUnit.Framework;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.File.Model;
    using PROFiLiX.Common.Logging;

    /// <summary>
    /// Class to do Files and Folders unit tests.
    /// </summary>
    [TestFixture]
    public class FilesAndFoldersTests
    {
        /// <summary>
        /// Test method to ensure TreeSize with valid data succeeds.
        /// </summary>
        [Test]
        public void TreeSize_WithValidPropertyValues_ShouldSucceed()
        {
            // Arrange + Act
            var ts = new TreeSize()
            {
                FolderName = "folder",
                Size = "size",
                RawSize = 1_000_000L,
            };

            // Assert
            Assert.That(ts.FolderName, Is.EqualTo("folder"));
            Assert.That(ts.Size, Is.EqualTo("size"));
            Assert.That(ts.RawSize, Is.EqualTo(1_000_000L));
        }

        /// <summary>
        /// Checks that DeleteFileAsync with with Valid data succeeds.
        /// </summary>
        [Test]
        public void DeleteFileAsync_WithValidData_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string fileName = "C:\\windows\\temp\\temp.txt";
            int maxRetries = 5;
            int millisecondsDelay = 5;
            File.WriteAllText(fileName, "Hello World");

            // Act
            var response = mockFilesAndFolders.DeleteFileAsync(fileName, maxRetries, millisecondsDelay).Result;

            // Assert
            Assert.That(response, Is.True);
        }

        /// <summary>
        /// Checks that DeleteFileAsync with with Valid data succeeds.
        /// </summary>
        [Test]
        public void DeleteFolderAsync_WithValidData_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string folderName = "C:\\windows\\temp\\eucprofilebuddy";
            int maxRetries = 5;
            int millisecondsDelay = 5;
            Directory.CreateDirectory(folderName);
            Thread.Sleep(2000);

            // Act
            var response = mockFilesAndFolders.DeleteFolderAsync(folderName, maxRetries, millisecondsDelay).Result;

            // Assert
            Assert.That(response, Is.True);
        }

        /// <summary>
        /// Checks that DeleteFileAsync with with Valid data succeeds.
        /// </summary>
        [Test]
        public void DirectorySizeAsync_WithValidData_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string folderName = "C:\\windows\\temp";
            Directory.CreateDirectory(folderName);
            Thread.Sleep(2000);
            DirectoryInfo di = new DirectoryInfo(folderName);

            // Act
            var response = mockFilesAndFolders.DirectorySizeAsync(di).Result;

            // Assert
            Assert.That(response, Is.GreaterThan(0));
        }

        /// <summary>
        /// Checks that FormatFileSize Valid data succeeds.
        /// </summary>
        [Test]
        public void FormatFileSize_WithSmallData_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var response = mockFilesAndFolders.FormatFileSize(1000);

            Assert.That(response, Is.EqualTo("1000 B"));
        }

        /// <summary>
        /// Checks that FormatFileSize Valid data succeeds.
        /// </summary>
        [Test]
        public void FormatFileSize_WithLargeData_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var response = mockFilesAndFolders.FormatFileSize(100000000);

            Assert.That(response, Is.EqualTo("95.37 MB"));
        }

        /// <summary>
        /// Checks that BuildTreeSizeFolders with Valid data succeeds.
        /// </summary>
        [Test]
        public void BuildTreeSizeFolders_WithValidFolderName_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default";
            var response = mockFilesAndFolders.BuildTreeSizeFolders(rootFolder);

            // Assert
            Assert.That(response, Is.TypeOf<List<TreeSize>>());
        }

        /// <summary>
        /// Checks that BuildTreeSizeFolders with Valid data succeeds.
        /// </summary>
        [Test]
        public void BuildTreeSizeFolders_WithValidFolderNameAndUnsorted_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default";
            var response = mockFilesAndFolders.BuildTreeSizeFolders(rootFolder, false);

            // Assert
            Assert.That(response, Is.TypeOf<List<TreeSize>>());
        }

        /// <summary>
        /// Checks that BuildTreeSizeFolders with Valid data succeeds.
        /// </summary>
        [Test]
        public void BuildTreeSizeFoldersAsync_WithValidFolderName_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default";
            var response = mockFilesAndFolders.BuildTreeSizeFoldersAsync(rootFolder).Result;

            // Assert
            Assert.That(response, Is.TypeOf<List<TreeSize>>());
        }

        /// <summary>
        /// Checks that BuildTreeSizeFiles with Valid data succeeds.
        /// </summary>
        [Test]
        public void BuildTreeSizeFiles_WithValidFolderName_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default\\AppData\\Local\\Microsoft\\Windows\\Shell";
            var response = mockFilesAndFolders.BuildTreeSizeFiles(rootFolder);

            // Assert
            Assert.That(response, Is.TypeOf<List<TreeSize>>());
        }

        /// <summary>
        /// Checks that BuildTreeSizeFolders with Valid data succeeds.
        /// </summary>
        [Test]
        public void BuildTreeSizeFiles_WithValidFolderNameAndUnsorted_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default\\AppData\\Local\\Microsoft\\Windows\\Shell";
            var response = mockFilesAndFolders.BuildTreeSizeFiles(rootFolder, false);

            // Assert
            Assert.That(response, Is.TypeOf<List<TreeSize>>());
        }

        /// <summary>
        /// Checks that CheckDirectory with Valid data succeeds.
        /// </summary>
        [Test]
        public void CheckDirectory_WithValidFolder_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default";
            var response = mockFilesAndFolders.CheckDirectory(rootFolder);

            // Assert
            Assert.That(response, Is.True);
        }

        /// <summary>
        /// Checks that BuildTreeSizeFilesAsync with Valid data succeeds.
        /// </summary>
        [Test]
        public void BuildTreeSizeFilesAsync_WithValidFolderName_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default\\AppData\\Local\\Microsoft\\Windows\\Shell";
            var response = mockFilesAndFolders.BuildTreeSizeFilesAsync(rootFolder).Result;

            // Assert
            Assert.That(response, Is.TypeOf<List<TreeSize>>());
        }

        /// <summary>
        /// Checks that CheckDirectory with Valid data succeeds.
        /// </summary>
        [Test]
        public void CheckDirectory_WithInvalidFolder_ShouldReturnFalse()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act
            var rootFolder = "C:\\Users\\Default\\NTUser.DAT";
            var response = mockFilesAndFolders.CheckDirectory(rootFolder);

            // Assert
            Assert.That(response, Is.False);
        }

        /// <summary>
        /// Checks that CreateDirectory with Valid data succeeds.
        /// </summary>
        [Test]
        public void CreateDirectory_WithValidFolderName_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            Random rand = new Random();
            int stringlen = rand.Next(4, 10);
            int randValue;
            string str = string.Empty;
            char letter;
            for (int i = 0; i < stringlen; i++)
            {
                randValue = rand.Next(0, 26);
                letter = Convert.ToChar(randValue + 65);
                str = str + letter;
            }

            var directoryName = Path.Join("C:\\windows\\temp", str);

            // Act + Assert
            Assert.DoesNotThrow(() => mockFilesAndFolders.CreateDirectory(directoryName));
        }

        /// <summary>
        /// Checks that CheckDirectory with invalid data errors.
        /// </summary>
        /// <param name="rootFolder">The foldername to build from.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void CreateDirectory_WithInvalidFolderName_ShouldThrowArgumentNullException(string rootFolder)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockFilesAndFolders.CreateDirectory(rootFolder));
        }

        /// <summary>
        /// Checks that CheckDirectory with invalid data errors.
        /// </summary>
        [Test]
        public void DirectorySizeAsync_WithInvalidFolderName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            DirectoryInfo? directoryInfo = null;

            // Act + Assert
            Assert.Throws<AggregateException>(() =>
            {
                long result = mockFilesAndFolders.DirectorySizeAsync(directoryInfo).Result;
            });
        }

        /// <summary>
        /// Checks that CheckDirectory with invalid data errors.
        /// </summary>
        /// <param name="rootFolder">The foldername to build from.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void CheckDirectory_WithInvalidFolderName_ShouldThrowArgumentNullException(string rootFolder)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockFilesAndFolders.CheckDirectory(rootFolder));
        }

        /// <summary>
        /// Checks that BuildTreeSizeFiles with invalid data errors.
        /// </summary>
        /// <param name="rootFolder">The foldername to build from.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void BuildTreeSizeFilesAsync_WithInvalidFolderName_ShouldThrowArgumentNullException(string rootFolder)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => mockFilesAndFolders.BuildTreeSizeFilesAsync(rootFolder));
        }

        /// <summary>
        /// Checks that BuildTreeSizeFiles with invalid data errors.
        /// </summary>
        /// <param name="rootFolder">The foldername to build from.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void BuildTreeSizeFiles_WithInvalidFolderName_ShouldThrowArgumentNullException(string rootFolder)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockFilesAndFolders.BuildTreeSizeFiles(rootFolder));
        }

        /// <summary>
        /// Checks that BuildTreeSizeFoldersAsync with invalid data errors.
        /// </summary>
        /// <param name="rootFolder">The foldername to build from.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void BuildTreeSizeFoldersAsync_WithInvalidFolderName_ShouldThrowArgumentNullException(string rootFolder)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => mockFilesAndFolders.BuildTreeSizeFoldersAsync(rootFolder));
        }

        /// <summary>
        /// Checks that BuildTreeSizeFolders with invalid data errors.
        /// </summary>
        /// <param name="rootFolder">The foldername to build from.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void BuildTreeSizeFolders_WithInvalidFolderName_ShouldThrowArgumentNullException(string rootFolder)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockFilesAndFolders.BuildTreeSizeFolders(rootFolder));
        }

        /// <summary>
        /// Checks that DeleteFileAsync with with Invalid data thrown.
        /// </summary>
        [Test]
        public void DeleteFolderAsync_WithInvalidData_ShouldReturnFalse()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string folderName = "C:\\dave";
            int maxRetries = 5;
            int millisecondsDelay = 5;

            // Act
            var response = mockFilesAndFolders.DeleteFolderAsync(folderName, maxRetries, millisecondsDelay).Result;

            // Assert
            Assert.That(response, Is.False);
        }

        /// <summary>
        /// Checks that DeleteFileAsync with invalie maxRetries errors.
        /// </summary>
        [Test]
        public void DeleteFolderAsync_WithInvalidMaxRetries_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string folderName = "folder";
            int maxRetries = 0;

            // Act + Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockFilesAndFolders.DeleteFolderAsync(folderName, maxRetries));
        }

        /// <summary>
        /// Checks that DeleteFileAsync with invalid maxRetries errors.
        /// </summary>
        [Test]
        public void DeleteFolderAsync_WithInvalidMillisecondsDelay_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string folderName = "folder";
            int maxRetries = 1;
            int millisecondsDelay = 0;

            // Act + Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockFilesAndFolders.DeleteFolderAsync(folderName, maxRetries, millisecondsDelay));
        }

        /// <summary>
        /// Checks that DeleteFolderAsync with invalid data errors.
        /// </summary>
        /// <param name="folderName">The foldername to delete.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void DeleteFolderAsync_WithInvalidFolderName_ShouldThrowArgumentNullException(string folderName)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => mockFilesAndFolders.DeleteFolderAsync(folderName));
        }

        /// <summary>
        /// Checks that DeleteFileAsync with invalid data errors.
        /// </summary>
        /// <param name="fileName">The filename to delete.</param>
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void DeleteFileAsync_WithInvalidFileName_ShouldThrowArgumentNullException(string fileName)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => mockFilesAndFolders.DeleteFileAsync(fileName));
        }

        /// <summary>
        /// Checks that DeleteFileAsync with invalie maxRetries errors.
        /// </summary>
        [Test]
        public void DeleteFileAsync_WithInvalidMaxRetries_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string fileName = "testFileName";
            int maxRetries = 0;

            // Act + Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockFilesAndFolders.DeleteFileAsync(fileName, maxRetries));
        }

        /// <summary>
        /// Checks that DeleteFileAsync with invalid maxRetries errors.
        /// </summary>
        [Test]
        public void DeleteFileAsync_WithInvalidMillisecondsDelay_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string fileName = "testFileName";
            int maxRetries = 1;
            int millisecondsDelay = 0;

            // Act + Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockFilesAndFolders.DeleteFileAsync(fileName, maxRetries, millisecondsDelay));
        }

        /// <summary>
        /// Checks that DeleteFileAsync with with Invalid data thrown.
        /// </summary>
        [Test]
        public void DeleteFileAsync_WithInvalidData_ShouldReturnFalse()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockFilesAndFolders = new FilesAndFolders(mockILogger.Object);
            string fileName = "C:\\dave\\temp1.txt";
            int maxRetries = 5;
            int millisecondsDelay = 5;

            // Act
            var response = mockFilesAndFolders.DeleteFileAsync(fileName, maxRetries, millisecondsDelay).Result;

            // Assert
            Assert.That(response, Is.False);
        }
    }
}
