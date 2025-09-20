﻿namespace MiniOrderSystem.Application.Common.Exceptions
{
    public class BusinessValidationException(List<ValidationError> errors) : Exception
    {
        public readonly List<ValidationError> Errors = errors;
    }
}
