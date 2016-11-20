using System;
using System.Runtime.Serialization;

[Serializable]
internal class MissingParticleException : Exception
{
    public MissingParticleException()
    {
    }

    public MissingParticleException(string message) : base(message)
    {
    }

    public MissingParticleException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected MissingParticleException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}