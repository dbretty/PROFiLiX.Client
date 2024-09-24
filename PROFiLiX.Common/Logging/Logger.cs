// <copyright file="Logger.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Logging
{
    using System.IO;
    using PROFiLiX.Common.File;
    using PROFiLiX.Common.Logging.Model;

    /// <summary>
    /// Class to do logging.
    /// </summary>
    public class Logger : ILogger
    {
        private const string FileName = "PROFiLiX.Log.txt";
        private const string LogPath = "PROFiLiX";
        private string fullLogFile = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
            IFilesAndFolders filesAndFolders = new FilesAndFolders(this);

            string directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                LogPath);

            this.fullLogFile = Path.Combine(
                directory,
                string.Format($"{DateTime.Now:yyyyMMdd}_{FileName}"));

            filesAndFolders.CreateDirectory(directory);
        }

        /// <inheritdoc/>
        public async Task LogAsync(string logMessage, LogLevel logLevel = LogLevel.INFO, LogType logType = LogType.FILE)
        {
            if (string.IsNullOrWhiteSpace(logMessage))
            {
                throw new ArgumentNullException();
            }

            try
            {
                using (StreamWriter streamWriter = new (this.fullLogFile, true))
                {
                    await streamWriter.WriteLineAsync($"{DateTime.Now:dd/MM/yyyy HH:mm:ss}[{logLevel}] {logMessage}");
                    streamWriter.Close();
                }
            }
            catch
            {
                var errorLog = "Unable to write error";
                errorLog = string.Empty;
            }
        }
    }
}
