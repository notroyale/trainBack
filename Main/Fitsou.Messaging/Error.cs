﻿namespace Messaging;

public record Error(string Code, string Message)
{
    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    internal static Error None => new(string.Empty, string.Empty);
}