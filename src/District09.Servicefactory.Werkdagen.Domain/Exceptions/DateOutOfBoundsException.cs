using System;
using System.Runtime.Serialization;

namespace District09.Servicefactory.Werkdagen.Domain.Exceptions
{
    public class DateOutOfBoundsException : Exception
    {
        private readonly DateTime _input;

        public DateOutOfBoundsException(DateTime input) : base($"requested date is exceeding boundary of {input}")
        {
            _input = input;
        }

        protected DateOutOfBoundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            info.AddValue("InputDate", _input);
        }
    }
}