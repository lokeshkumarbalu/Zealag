//-----------------------------------------------------------------------
// <copyright file="MessageType.cs" company="Zealag">
//    Copyright © Zealag 2016. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zealag.Utilities.Logging
{
    /// <summary>
    /// Specifies constants that define message types that can be logged by the loggers.
    /// </summary>
    public enum MessageType : byte
    {
        /// <summary>
        /// The informative message.
        /// </summary>
        Info = 1,

        /// <summary>
        /// The trace message.
        /// </summary>
        Trace,

        /// <summary>
        /// The debug message.
        /// </summary>
        Debug,

        /// <summary>
        /// The error message.
        /// </summary>
        Error,

        /// <summary>
        /// The fatal error message.
        /// </summary>
        Fatal,

        /// <summary>
        /// The warning message.
        /// </summary>
        Warn,
    }
}
