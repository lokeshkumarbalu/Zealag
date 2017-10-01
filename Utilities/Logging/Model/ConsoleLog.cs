//-----------------------------------------------------------------------
// <copyright file="ConsoleLog.cs" company="Zealag">
//     Copyright © Zealag 2016. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zealag.Utilities.Logging
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ConsoleLog"/> class is a derived class of <see cref="Log"/> class.
    /// Used to log messages to standard console.
    /// </summary>
    public class ConsoleLog : Log
    {
        /// <summary>
        /// Specifies if the log message must be color coded.
        /// </summary>
        private bool isColorEnabled = false;

        /// <summary>
        /// A dictionary to hold log message color codes.
        /// </summary>
        private Dictionary<MessageType, ConsoleColor> textColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLog" /> class.
        /// Also sets the attributes IsColorEnabled and IsTimestampEnabled to true.
        /// </summary>
        internal ConsoleLog() : base()
        {
            this.isColorEnabled = true;
            this.IsTimestampEnabled = true;
            this.textColor = new Dictionary<MessageType, ConsoleColor>();

            int iterator = 9; // Start from color Blue.
            Array values = Enum.GetValues(typeof(MessageType));
            foreach (MessageType value in values)
            {
                this.textColor.Add(value, (ConsoleColor)((iterator % 15) + 1));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable colored console messages.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="System.IO.IOException">Thrown if an IO error occurs.</exception>
        public bool IsColorEnabled
        {
            get
            {
                return this.isColorEnabled;
            }

            set
            {
                Console.ResetColor();
                this.isColorEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets dictionary for message colors to be used.
        /// </summary>
        /// <exception cref = "ArgumentNullException">Thrown if a null reference is passed to method.</exception>
        /// <exception cref = "ArgumentException">Thrown if one of the argumets passed is not valid.</exception>
        public Dictionary<MessageType, ConsoleColor> TextColor
        {
            get
            {
                Dictionary<MessageType, ConsoleColor> result = new Dictionary<MessageType, ConsoleColor>();

                // Copy each key value pair and return it.
                foreach (KeyValuePair<MessageType, ConsoleColor> entry in this.textColor)
                {
                    result.Add(entry.Key, entry.Value);
                }

                return result;
            }

            set
            {
                // Update the ones present and keep the defaults.
                foreach (KeyValuePair<MessageType, ConsoleColor> entry in value)
                {
                    this.textColor.Remove(entry.Key);
                    this.textColor.Add(entry.Key, entry.Value);
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
        /// <exception cref="ArgumentException">Thrown if one of the argument passed in not valid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="System.IO.IOException">Thrown if an IO error occurs.</exception>
        public override void Write(string message, MessageType messageType)
        {
            if (message == null)
            {
                throw new ArgumentNullException("The message parameter cannot be null.");
            }

            ConsoleColor value;
            if (this.TextColor.TryGetValue(messageType, out value) && this.IsColorEnabled)
            {
                Console.ForegroundColor = value;
            }

            // Print line.
            Console.WriteLine(this.FormatMessage(message, messageType));
            
            // Reset default color.
            if (this.IsColorEnabled)
            {
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Writes the log message using specified messageType to console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="messageType">The messageType to be used while logging the message.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <exception cref="ArgumentException">Thrown if one of the argument passed in not valid.</exception>
        /// <exception cref="ArgumentNullException">Thrown if passed argument is null.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown if a security error is detected.</exception>
        /// <exception cref="System.IO.IOException">Thrown if an IO error occurs.</exception>
        public override void Write(string message, MessageType messageType, Exception exception)
        {
            this.Write(message, messageType);
            Console.WriteLine("Exception: " + exception.Message);
        }
    }
}       
