namespace ProjectDroit.Core.Exceptions.Infrastructure;

public class RateLimiterException : Exception
{
    public override string Message => "Rate limiter exceeded.";
}