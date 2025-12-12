namespace Kash.Shared.Domain.Abstractions.Enums;


public enum ErrorType
{
    Failure = 0,      // 500
    Validation = 1,   // 400
    NotFound = 2,     // 404
    Conflict = 3,     // 409
    Unauthorized = 4, // 401
    Forbidden = 5,    // 403
    TooManyRequests = 6 // 429
}
