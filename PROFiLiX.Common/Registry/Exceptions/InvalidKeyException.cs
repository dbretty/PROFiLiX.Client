// <copyright file="InvalidKeyException.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Registry.Exceptions
{
    /// <summary>
    /// Class for InvalidKeyException.
    /// </summary>
    public class InvalidKeyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidKeyException"/> class.
        /// </summary>
        public InvalidKeyException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidKeyException"/> class.
        /// </summary>
        /// <param name="message">The message to send to the exception.</param>
        public InvalidKeyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidKeyException"/> class.
        /// </summary>
        /// <param name="message">The message to send to the exception.</param>
        /// <param name="innerException">The innerException to send to the exception.</param>
        public InvalidKeyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
