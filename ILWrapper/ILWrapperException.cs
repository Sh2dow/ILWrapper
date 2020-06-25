using System;
using ILWrapper.Enums;



namespace ILWrapper
{
	public class ILWrapperException : Exception
	{
        /// <summary>
        /// Initializes new instance of <see cref="ILWrapperException"/> 
        /// with default message.
        /// </summary>
        public ILWrapperException()
            : base("Exception has occured when trying to manage an Image.") { }

        /// <summary>
        /// Initializes new instance of <see cref="ILWrapperException"/> 
        /// with custom message passed.
        /// </summary>
        /// <param name="message">Custom message.</param>
        public ILWrapperException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes new instance of <see cref="ILWrapperException"/> 
        /// with default message specifying maximum length allowed.
        /// </summary>
        /// <param name="maxlength">Maximum length allowed.</param>
        public ILWrapperException(ErrorType error)
            : base($"Error of type {error} has occured when trying to manage an Image") { }
    }
}
