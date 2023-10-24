using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace ClassLibraryDomain.Exceptions
{
    public class UserAccessException : Exception
    {
        public UserAccessException(string alias, string message) : base($"Error login user {alias}. {message} ")
        {

        }

        [ExcludeFromCodeCoverage]
        protected UserAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
