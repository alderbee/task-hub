namespace TaskPulse.Domain.Exceptions;

using System;

public class GeneralException : Exception
{
    public GeneralException() 
    { }
    
    public GeneralException(string message) 
        : base(message)
    { }
    
    public GeneralException(string message, Exception inner) 
        : base(message, inner)
    { }
}