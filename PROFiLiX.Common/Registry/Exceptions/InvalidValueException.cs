// <copyright file="InvalidValueException.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Registry.Exceptions
{
    /// <summary>
    /// Class for InvalidValueException.
    /// </summary>
    public class InvalidValueException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidValueException"/> class.
        /// </summary>
        public InvalidValueException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidValueException"/> class.
        /// </summary>
        /// <param name="message">The message to send to the exception.</param>
        public InvalidValueException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidValueException"/> class.
        /// </summary>
        /// <param name="message">The message to send to the exception.</param>
        /// <param name="innerException">The innerException to send to the exception.</param>
        public InvalidValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
