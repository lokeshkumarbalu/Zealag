//-----------------------------------------------------------------------
// <copyright file="FileLog.cs" company="Zealag">
//     Copyright © Zealag 2016. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zealag.Utilities.Logging.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    using Zealag.Utilities.Logging.Model;
    using Zealag.Utilities.Logging.Constant;

    /// <summary>
    /// The <see cref="FileLog"/> class is a derived class of <see cref="Log"/> class, used to log messages to the initialized log file.
    /// If the log file is not specified will create a default log file at the executable location.
    /// </summary>
    public class FileLog : Log, IDisposable
    {
        /// <summary>
        /// Specifies if the maximum file limit is enabled for a log file.
        /// </summary>
        private bool isSizeLimitEnabled;

        /// <summary>
        /// Specifies the maximum file limit in bytes.
        /// </summary>
        private int logFileSizeLimit;

        /// <summary>
        /// Specifies the path to the log file.
        /// </summary>
        private string logPath;

        /// <summary>
        /// Specifies the name of the log file.
        /// </summary>
        private string logFileName;

        /// <summary>
        /// Specifies the extension of the log file.
        /// </summary>
        private string logFileExtension;

        /// <summary>
        /// Specifies the timestamp format for the file name.
        /// </summary>
        private string logFileTimestampFormat;

        /// <summary>
        /// Specifies the fully qualified log file path.
        /// </summary>
        private string logFileFullPath;

        /// <summary>
        /// Stream writer to write logs to the file stream.
        /// </summary>
        private StreamWriter logStreamWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLog"/> class.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when OS denies access because of IO error or security error.</exception>
        public FileLog() : base()
        {
            this.IsTimestampEnabled = true;
            this.isSizeLimitEnabled = true;
            this.logFileSizeLimit = 1048576;
            this.logPath = ".\\";
            this.logFileName = "Log";
            this.logFileExtension = ".log";
            this.logFileTimestampFormat = "o";
        }

        /// <summary>
        /// Gets or sets a value indicating whether the log file has size limit.
        /// </summary>
        public bool IsSizeLimitEnabled
        {
            get
            {
                return this.isSizeLimitEnabled;
            }

            set
            {
                this.isSizeLimitEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the log file max size.
        /// </summary>
        public int MaxSize
        {
            get
            {
                return this.logFileSizeLimit;
            }

            set
            {
                this.logFileSizeLimit = value;
            }
        }

        /// <summary>
        /// Gets or sets log path.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if one of the arguments passed is not valid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if null reference is passed to method.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when OS denies access because of IO error or security error.</exception>
        public string LogPath
        {
            get
            {
                return this.logPath;
            }

            set
            {
                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentNullException("The directory path cannot be null.");
                }
                else if (!Directory.Exists(value))
                {
                    throw new ArgumentException("The specified directory does not exist.");
                }
                else
                {
                    this.logPath = value;
                    if (!object.ReferenceEquals(null, this.logStreamWriter))
                    {
                        this.logStreamWriter.Dispose();
                        this.logStreamWriter = null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets log file name.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if one of the arguments passed is not valid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if null reference is passed to method.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when OS denies access because of IO error or security error.</exception>
        public string LogFileName
        {
            get
            {
                return this.logFileName;
            }

            set
            {
                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentNullException("The file name cannot be null'");
                }
                else if (!Regex.IsMatch(value, "^[a-zA-Z0-9]*$"))
                {
                    throw new ArgumentException("The specified file name contains special characters or spaces");
                }
                else 
                {
                    this.logFileName = value;
                    if (!object.ReferenceEquals(null, this.logStreamWriter))
                    {
                        this.logStreamWriter.Dispose();
                        this.logStreamWriter = null;
                    }
                }    
            }
        }

        /// <summary>
        /// Writes the log message to console as Info.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <exception cref="ArgumentException">Thrown if one of the argument passed in not valid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="System.IO.IOException">Thrown if an IO error occurs.</exception>
        public override void Write(string message)
        {
            this.Write(message, MessageType.Info);
        }

        /// <summary>
        /// Writes the log message using specified messageType to console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="messageType">The messageType to be used while logging the message.</param>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="ArgumentException">Thrown if one of the argument passed in not valid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="System.IO.IOException">Thrown if an IO error occurs.</exception>
        public override void Write(string message, MessageType messageType)
        {
            if (object.ReferenceEquals(null, this.logStreamWriter))
            {
                this.InitializeLogFileStreamWriter();
            }

            this.logStreamWriter.WriteLine(this.FormatMessage(message, messageType));
            this.logStreamWriter.Flush();
        }

        /// <summary>
        /// Writes the log message using specified messageType to console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="messageType">The messageType to be used while logging the message.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <exception cref="ArgumentException">Thrown if one of the argument passed is not valid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="System.IO.IOException">Thrown if an IO error occurs.</exception>
        public override void Write(string message, MessageType messageType, Exception exception)
        {
            this.Write(message, messageType);
            this.logStreamWriter.WriteLine(exception.ToString());
            this.logStreamWriter.Flush();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)this.logStreamWriter).Dispose();
        }

        /// <summary>
        /// Initializes the log file stream writer after building the fully qualified log path.
        /// </summary>
        private void InitializeLogFileStreamWriter()
        {
            this.logFileFullPath = Path.Combine(this.logPath, this.logFileName) +
                DateTime.Now.ToString(this.logFileTimestampFormat)
                    .Replace(":", string.Empty)
                    .Replace("-", string.Empty)
                    .Replace(".", string.Empty)
                    .Insert(0, "_") +
                this.logFileExtension;

            this.logStreamWriter = new StreamWriter(new FileStream(this.logFileFullPath, FileMode.Append, FileAccess.Write));
        }
    }
}
