//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Zealag">
//     Copyright © Zealag 2016. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zealag.Utilities.Logging.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Zealag.Utilities.Logging.Model;
    using Zealag.Utilities.Logging.Constant;

    /// <summary>
    /// The base class for other logger classes, defines abstract methods to be implemented.
    /// </summary>
    public abstract class Log
    {
        /// <summary>
        /// Specifies if the log message should contain timestamp.
        /// </summary>
        private bool isTimestampEnabled = false;

        /// <summary>
        /// A string builder of type <see cref="System.Text.StringBuilder"/> class to help format log message.
        /// </summary>
        private StringBuilder messageBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        protected Log()
        {
            this.messageBuilder = new StringBuilder();
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable timestamps in log messages.
        /// </summary>
        public bool IsTimestampEnabled 
        {
            get
            {
                return this.isTimestampEnabled;
            }

            set
            {
                this.isTimestampEnabled = value;
            }
        }

        /// <summary>
        /// A abstract method to be implemented by derived classes. 
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public abstract void Write(string message);

        /// <summary>
        /// A abstract method to be implemented by derived classes. 
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="messageType">The type of message to be logged.</param>
        public abstract void Write(string message, MessageType messageType);

        /// <summary>
        /// A abstract method to be implemented by derived classes. 
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="messageType">The type of message to be logged.</param>
        /// <param name="exception">The exception to be logged.</param>
        public abstract void Write(string message, MessageType messageType, Exception exception);

        /// <summary>
        /// The Default method to convert 'message' into a formatted string.
        /// </summary>
        /// <param name="message">The message to be formatted.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <returns>A formatted string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if message is very long.</exception>
        protected string FormatMessage(string message, MessageType messageType)
        {
            this.messageBuilder.Clear();

            if (this.IsTimestampEnabled)
            {
                this.messageBuilder.Append("[" + DateTime.Now.ToString() + "]");
                this.messageBuilder.Append(" ");
            }

            // The argument in the string is for the type of message - Enumeration MessageType.
            this.messageBuilder.Append("[" + messageType.ToString().ToUpper() + "] - " + message);
            return this.messageBuilder.ToString();
        }
    }
}
