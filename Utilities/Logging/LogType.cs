//-----------------------------------------------------------------------
// <copyright file="LogType.cs" company="Zealag">
//    Copyright © Zealag 2016. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zealag.Utilities.Logging
{
    /// <summary>
    /// Specifies constants that define Log types that are available from this namespace.
    /// </summary>
    public enum LogType : byte
    {
        /// <summary>
        /// The Console log.
        /// </summary>
        ConsoleLog = 1,

        /// <summary>
        /// The File log.
        /// </summary>
        FileLog
    }
}
