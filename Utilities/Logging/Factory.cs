//-----------------------------------------------------------------------
// <copyright file="Factory.cs" company="Zealag">
//     Copyright © Zealag 2016. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zealag.Utilities.Logging
{
    using System;

    /// <summary>
    /// The static factory class to create Log objects when provided with the enum value from <see cref="LogType"/>.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// The static method to create <see cref="Log"/> objects for logging purpose.
        /// </summary>
        /// <returns>Should return an instance of <see cref="Log"/>.</returns>
        /// <exception cref=""
        public static Log GetLog(LogType logType)
        {
            string logClassName = "";
            object returnValue = null;
            Type logClassType = null;
            
            logClassName = Enum.GetName(typeof(LogType), logType);            
            logClassType = Type.GetType(typeof(Factory).Namespace + "." + logClassName);
            returnValue = Activator.CreateInstance(logClassType, true);

            return (Log)returnValue;
        }
    }
}
