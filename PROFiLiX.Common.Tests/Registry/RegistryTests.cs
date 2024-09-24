// <copyright file="RegistryTests.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Tests.Registry
{
    using Microsoft.Win32;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;
    using PROFiLiX.Common.Logging;
    using PROFiLiX.Common.Registry;
    using PROFiLiX.Common.Registry.Exceptions;
    using PROFiLiX.Common.Registry.Model;

    /// <summary>
    /// Class to do Registry unit tests.
    /// </summary>
    [TestFixture]
    public class RegistryTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryTests"/> class.
        /// </summary>
        public RegistryTests()
        {
            this.RegistryKey = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
            this.RegistryValue = "CurrentBuild";
        }

        /// <summary>
        /// Gets or Sets the RegistryKey.
        /// </summary>
        public string RegistryKey { get; set; }

        /// <summary>
        /// Gets or Sets the RegistryValue.
        /// </summary>
        public string RegistryValue { get; set; }

        /// <summary>
        /// Test method to ensure GetRegistryValue with valid data succeeds.
        /// </summary>
        [Test]
        public void GetRegistryValue_WithValidValue_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act
            var response = mockRegistry.GetRegistryValue(this.RegistryValue, this.RegistryKey, RegistryHive.LocalMachine);

            // Assert
            Assert.That(response, Is.TypeOf<string>());
        }

        /// <summary>
        /// Test method to ensure GetRegistryValueAsync with valid data succeeds.
        /// </summary>
        [Test]
        public void GetRegistryValueAsync_WithValidValue_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act
            var response = mockRegistry.GetRegistryValueAsync(this.RegistryValue, this.RegistryKey, RegistryHive.LocalMachine).Result;

            // Assert
            Assert.That(response, Is.TypeOf<string>());
        }

        /// <summary>
        /// Test method to ensure GetRegistryKey with valid data succeeds.
        /// </summary>
        [Test]
        public void GetRegistryKey_WithValidValue_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act
            var response = mockRegistry.GetRegistryKey(this.RegistryKey, RegistryHive.LocalMachine);

            // Assert
            Assert.That(response, Is.TypeOf<bool>());
        }

        /// <summary>
        /// Test method to ensure RegistryPathValue builds correctly.
        /// </summary>
        [Test]
        public void RegistryPathValue_WithValidPropertyValues_ShouldSucceed()
        {
            // Arrange + Act
            var rpv = new RegistryPathValue()
            {
                Path = "path",
                Key = "key",
                Value = "value",
            };

            // Assert
            Assert.That(rpv.Path, Is.EqualTo("path"));
            Assert.That(rpv.Key, Is.EqualTo("key"));
            Assert.That(rpv.Value, Is.EqualTo("value"));
        }

        /// <summary>
        /// Test method to ensure GetRegistryPathValue with a valid key succeeds.
        /// </summary>
        [Test]
        public void GetRegistryPathValue_WithValidKey_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);
            var key = new string[] { this.RegistryKey };
            var hive = RegistryHive.LocalMachine;

            // Act
            var response = mockRegistry.GetRegistryPathValue(key, hive);

            // Assert
            Assert.That(response.Count, Is.GreaterThan(0));
        }

        /// <summary>
        /// Test method to ensure GetRegistryPathValueAsync with a valid key succeeds.
        /// </summary>
        [Test]
        public void GetRegistryPathValueAsync_WithValidKey_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);
            var key = new string[] { this.RegistryKey };
            var hive = RegistryHive.LocalMachine;

            // Act
            var response = mockRegistry.GetRegistryPathValueAsync(key, hive).Result;

            // Assert
            Assert.That(response.Count, Is.GreaterThan(0));
        }

        /// <summary>
        /// Test method to ensure CreateRegistryKey with a valid key succeeds.
        /// </summary>
        [Test]
        public void CreateRegistryKey_WithValidKey_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);
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

            var hive = RegistryHive.CurrentUser;

            // Act
            var response = mockRegistry.CreateRegistryKey(str, hive);

            // Assert
            Assert.That(response, Is.True);
        }

        /// <summary>
        /// Test method to ensure SetRegistryValue with a valid data succeeds.
        /// </summary>
        [Test]
        public void SetRegistryValue_WithValidData_ShouldSucceed()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);
            var valueData = 1;
            var valueKey = this.RegistryKey;
            var valueName = "EUCProfileBuddy";
            var registryHive = RegistryHive.CurrentUser;

            // Act
            var response = mockRegistry.SetRegistryValue(valueName, valueKey, valueData, registryHive);

            // Assert
            Assert.That(response, Is.True);
        }

        /// <summary>
        /// Test method to ensure SetRegistryValue with an invalid key returns false.
        /// </summary>
        /// <param name="valueName">The registry value name.</param>
        /// <param name="valueKey">The registry key.</param>
        /// <param name="valueData">The registry value.</param>
        /// <param name="registryHive">The registry hive.</param>
        [Test]
        [TestCase("value", "key", "data", RegistryHive.CurrentUser)]
        public void SetRegistryValue_WithInvalidKey_ShouldReturnFalse(string valueName, string valueKey, object valueData, RegistryHive registryHive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act
            var response = mockRegistry.SetRegistryValue(valueName, valueKey, valueData, registryHive);

            // Assert
            Assert.That(response, Is.False);
        }

        /// <summary>
        /// Test method to ensure SetRegistryValue with an invalid hive throws an error.
        /// </summary>
        /// <param name="valueName">The registry value name.</param>
        /// <param name="valueKey">The registry key.</param>
        /// <param name="valueData">The registry value.</param>
        /// <param name="registryHive">The registry hive.</param>
        [Test]
        [TestCase("value", "key", "data", 7)]
        public void SetRegistryValue_WithInvalidRootKey_ShouldThrowInvalidRootKeyException(string valueName, string valueKey, object valueData, RegistryHive registryHive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<InvalidRootKeyException>(() => mockRegistry.SetRegistryValue(valueName, valueKey, valueData, registryHive));
        }

        /// <summary>
        /// Test method to ensure SetRegistryValue with null data throws correctly.
        /// </summary>
        /// <param name="valueName">The registry value name.</param>
        /// <param name="valueKey">The registry key.</param>
        /// <param name="valueData">The registry value.</param>
        /// <param name="registryHive">The registry hive.</param>
        [Test]
        [TestCase(null, "key", "data", RegistryHive.CurrentUser)]
        [TestCase("value", null, "data", RegistryHive.CurrentUser)]
        public void SetRegistryValue_WithNullData_ShouldThrowArgumentNullException(string valueName, string valueKey, object valueData, RegistryHive registryHive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockRegistry.SetRegistryValue(valueName, valueKey, valueData, registryHive));
        }

        /// <summary>
        /// Test method to ensure CreateRegistryKey with an existing key throws correctly.
        /// </summary>
        [Test]
        public void CreateRegistryKey_WithExistingKey_ShouldThrowInvalidKeyException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);
            var key = "Software";
            var hive = RegistryHive.CurrentUser;

            // Act + Assert
            Assert.Throws<InvalidKeyException>(() => mockRegistry.CreateRegistryKey(key, hive));
        }

        /// <summary>
        /// Test method to ensure CreateRegistryKey with an invalid hive throws correctly.
        /// </summary>
        /// <param name="rootPath">The registry rootPath.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("rootPath", 7)]
        public void CreateRegistryKey_WithInvalidRootKey_ShouldThrowInvalidRootKeyException(string rootPath, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<InvalidRootKeyException>(() => mockRegistry.CreateRegistryKey(rootPath, hive));
        }

        /// <summary>
        /// Test method to ensure CreateRegistryKey fails with null key.
        /// </summary>
        /// <param name="valueKey">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase(null, RegistryHive.LocalMachine)]
        public void CreateRegistryKey_WithNullKey_ShouldThrowArgumentNullException(string valueKey, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockRegistry.CreateRegistryKey(valueKey, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryPathValue fails with null key.
        /// </summary>
        /// <param name="rootPath">The registry rootPath.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase(null, RegistryHive.LocalMachine)]
        public void GetRegistryPathValue_WithNullRootPath_ShouldThrowArgumentNullException(string[] rootPath, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockRegistry.GetRegistryPathValue(rootPath, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryPathValueAsync fails with null key.
        /// </summary>
        /// <param name="rootPath">The registry rootPath.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase(null, RegistryHive.LocalMachine)]
        public void GetRegistryPathValueAsync_WithNullRootPath_ShouldThrowArgumentNullException(string[] rootPath, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => mockRegistry.GetRegistryPathValueAsync(rootPath, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryPathValue with invalid hive throws.
        /// </summary>
        /// <param name="rootPath">The registry rootPath.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase(new[] { "rootPath" }, 7)]
        public void GetRegistryPathValue_WithInvalidRootKey_ShouldThrowInvalidRootKeyException(string[] rootPath, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<InvalidRootKeyException>(() => mockRegistry.GetRegistryPathValue(rootPath, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryPathValue with an invalid key returns an empty array.
        /// </summary>
        [Test]
        public void GetRegistryPathValue_WithInvalidKey_ShouldReturnAnEmptyArray()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);
            var key = new string[] { "dummyKey" };
            var hive = RegistryHive.LocalMachine;

            // Act
            var response = mockRegistry.GetRegistryPathValue(key, hive);

            // Assert
            Assert.That(response.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValue throws with null Key values.
        /// </summary>
        /// <param name="value">The registry value.</param>
        /// <param name="key">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("value", "", RegistryHive.LocalMachine)]
        [TestCase("value", " ", RegistryHive.LocalMachine)]
        [TestCase("value", null, RegistryHive.LocalMachine)]
        public void GetRegistryValue_WithNullKey_ShouldThrowArgumentNullException(string value, string key, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockRegistry.GetRegistryValue(value, key, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValueAsync throws with null Key values.
        /// </summary>
        /// <param name="value">The registry value.</param>
        /// <param name="key">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("value", "", RegistryHive.LocalMachine)]
        [TestCase("value", " ", RegistryHive.LocalMachine)]
        [TestCase("value", null, RegistryHive.LocalMachine)]
        public void GetRegistryValueAsync_WithNullKey_ShouldThrowArgumentNullException(string value, string key, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await mockRegistry.GetRegistryValueAsync(value, key, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryKey throws with null Key values.
        /// </summary>
        /// <param name="key">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("", RegistryHive.LocalMachine)]
        [TestCase(" ", RegistryHive.LocalMachine)]
        [TestCase(null, RegistryHive.LocalMachine)]
        public void GetRegistryKey_WithNullKey_ShouldThrowArgumentNullException(string key, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockRegistry.GetRegistryKey(key, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryKey throws with invalid root key values.
        /// </summary>
        /// <param name="key">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("key", 7)]
        public void GetRegistryKey_WithInvalidRootKey_ShouldThrowInvalidRootKeyException(string key, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<InvalidRootKeyException>(() => mockRegistry.GetRegistryKey(key, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryKey throws with an invalid key path.
        /// </summary>
        [Test]
        public void GetRegistryKey_WithInvalidKeyPath_ShouldReturnFalse()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            var response = mockRegistry.GetRegistryKey("dummy key", RegistryHive.LocalMachine);

            // Assert
            ClassicAssert.IsFalse(response);
        }

        /// <summary>
        /// Test method to ensure GetRegistryValue throws with null values.
        /// </summary>
        /// <param name="value">The registry value.</param>
        /// <param name="key">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("", "key", RegistryHive.LocalMachine)]
        [TestCase(" ", "key", RegistryHive.LocalMachine)]
        [TestCase(null, "key", RegistryHive.LocalMachine)]
        public void GetRegistryValue_WithNullValue_ShouldThrowArgumentNullException(string value, string key, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => mockRegistry.GetRegistryValue(value, key, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValueAsync throws with null values.
        /// </summary>
        /// <param name="value">The registry value.</param>
        /// <param name="key">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("", "key", RegistryHive.LocalMachine)]
        [TestCase(" ", "key", RegistryHive.LocalMachine)]
        [TestCase(null, "key", RegistryHive.LocalMachine)]
        public void GetRegistryValueAsync_WithNullValue_ShouldThrowArgumentNullException(string value, string key, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await mockRegistry.GetRegistryValueAsync(value, key, hive));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValue throws with an invalid key path.
        /// </summary>
        [Test]
        public void GetRegistryValue_WithInvalidKeyPath_ShouldThrowInvalidKeyException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<InvalidKeyException>(() => mockRegistry.GetRegistryValue(this.RegistryValue, "dummy key", RegistryHive.LocalMachine));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValueAsync throws with an invalid key path.
        /// </summary>
        [Test]
        public void GetRegistryValueAsync_WithInvalidKeyPath_ShouldThrowInvalidKeyException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.ThrowsAsync<InvalidKeyException>(async () => await mockRegistry.GetRegistryValueAsync(this.RegistryValue, "dummy key", RegistryHive.LocalMachine));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValue throws with an invalid value.
        /// </summary>
        [Test]
        public void GetRegistryValue_WithInvalidValue_ShouldThrowInvalidValueException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.DoesNotThrow(() => mockRegistry.GetRegistryValue("dummy value", this.RegistryKey, RegistryHive.LocalMachine));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValueAsync throws with an invalid value.
        /// </summary>
        [Test]
        public void GetRegistryValueAsync_WithInvalidValue_ShouldThrowInvalidValueException()
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.DoesNotThrowAsync(async () => await mockRegistry.GetRegistryValueAsync("dummy value", this.RegistryKey, RegistryHive.LocalMachine));
        }

        /// <summary>
        /// Test method to ensure GetRegistryValue throws with an invalid root key.
        /// </summary>
        /// <param name="value">The registry value.</param>
        /// <param name="key">The registry key.</param>
        /// <param name="hive">The registry hive.</param>
        [Test]
        [TestCase("value", "key", 7)]
        public void GetRegistryValue_WithInvalidRootKey_ShouldThrowInvalidRootKeyException(string value, string key, RegistryHive hive)
        {
            // Arrange
            var mockILogger = new Mock<ILogger>();
            var mockRegistry = new WindowsRegistry(mockILogger.Object);

            // Act + Assert
            Assert.Throws<InvalidRootKeyException>(() => mockRegistry.GetRegistryValue(value, key, hive));
        }

        /// <summary>
        /// Test method to ensure InvalidRootKeyException throws with a message correctly.
        /// </summary>
        [Test]
        public void InvalidRootKeyException_WithMessage_ShouldThrowException()
        {
            // Arrange + Act
            var ex = new InvalidRootKeyException("test message");

            // Assert
            if (ex.Message is not null)
            {
                Assert.That(ex.Message, Is.EqualTo("test message"));
            }
        }

        /// <summary>
        /// Test method to ensure InvalidRootKeyException Throws correctly.
        /// </summary>
        [Test]
        public void InvalidRootKeyException_WithMessageAndInnerException_ShouldThrowException()
        {
            // Arrange + Act
            var innerException = new Exception("inner");
            var ex = new InvalidRootKeyException("test message", innerException);

            // Assert
            if (ex.Message is not null && ex.InnerException is not null)
            {
                Assert.That(ex.Message, Is.EqualTo("test message"));
                Assert.That(ex.InnerException.Message, Is.EqualTo("inner"));
            }
        }

        /// <summary>
        /// Test method to ensure InvalidKeyException with message throws correctly.
        /// </summary>
        [Test]
        public void InvalidKeyException_WithMessage_ShouldThrowException()
        {
            // Arrange + Act
            var ex = new InvalidKeyException("test message");

            // Assert
            if (ex.Message is not null)
            {
                Assert.That(ex.Message, Is.EqualTo("test message"));
            }
        }

        /// <summary>
        /// Test method to ensure InvalidKeyException Throws correctly.
        /// </summary>
        [Test]
        public void InvalidKeyException_WithMessageAndInnerException_ShouldThrowException()
        {
            // Arrange + Act
            var innerException = new Exception("inner");
            var ex = new InvalidKeyException("test message", innerException);

            // Assert
            if (ex.Message is not null && ex.InnerException is not null)
            {
                Assert.That(ex.Message, Is.EqualTo("test message"));
                Assert.That(ex.InnerException.Message, Is.EqualTo("inner"));
            }
        }

        /// <summary>
        /// Test method to ensure InvalidValueException with message throws correctly.
        /// </summary>
        [Test]
        public void InvalidValueException_WithMessage_ShouldThrowException()
        {
            // Arrange + Act
            var ex = new InvalidValueException("test message");

            // Assert
            if (ex.Message is not null)
            {
                Assert.That(ex.Message, Is.EqualTo("test message"));
            }
        }

        /// <summary>
        /// Test method to ensure InvalidValueException Throws correctly.
        /// </summary>
        [Test]
        public void InvalidValueException_WithMessageAndInnerException_ShouldThrowException()
        {
            // Arrange + Act
            var innerException = new Exception("inner");
            var ex = new InvalidValueException("test message", innerException);

            // Assert
            if (ex.Message is not null && ex.InnerException is not null)
            {
                Assert.That(ex.Message, Is.EqualTo("test message"));
                Assert.That(ex.InnerException.Message, Is.EqualTo("inner"));
            }
        }
    }
}
