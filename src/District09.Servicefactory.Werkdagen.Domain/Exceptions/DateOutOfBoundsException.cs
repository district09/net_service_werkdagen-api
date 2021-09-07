using System;
using System.Runtime.Serialization;

namespace District09.Servicefactory.Werkdagen.Domain.Exceptions
{
    public class DateOutOfBoundsException : Exception
    {
        private readonly DateTime _input;

        public DateOutOfBoundsException(DateTime input) : base($"input {input:O} is out of bounds")
        {
            _input = input;
        }

        protected DateOutOfBoundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            info.AddValue("InputDate", _input);
        }
    }
}