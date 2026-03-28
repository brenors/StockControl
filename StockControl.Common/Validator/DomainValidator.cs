using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.RegularExpressions;

namespace StockControl.Common.Validator
{
    public class DomainValidator : ApplicationException
    {
        public List<string> Notifications { get; private set; } = new List<string>();
        public HttpStatusCode Status { get; private set; }

        public DomainValidator() { }
        public DomainValidator(string message) : base(message) { }
        public DomainValidator(string message, Exception innerException) : base(message, innerException) { }

        public void AddNotification(string message) => Notifications.Add(message);
        public void AddNotifications(List<string> messages) => Notifications.AddRange(messages);
        public static DomainValidator Contract(HttpStatusCode status = HttpStatusCode.BadRequest) => new() { Status = status };
        public static DomainValidator Assert([DoesNotReturnIf(false)] bool assert, string message, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            var validator = Contract(status)
                .Assert(assert, message)
                .Validate();

            return validator;
        }
    }

    public static class DomainValidatorBuilder
    {
        public static DomainValidator Assert(this DomainValidator validator, [DoesNotReturnIf(false)] bool assert, string message)
        {
            if (!assert)
                validator.AddNotification(message);
            return validator;
        }

        public static DomainValidator Validate(this DomainValidator validator)
        {
            if (validator.Notifications.Any())
                throw validator;
            return validator;
        }

        public static DomainValidator AssumeNotNull<T>(this DomainValidator validator, T? value, string message)
        {
            if (value == null)
                validator.AddNotification(message);
            return validator;
        }

        public static DomainValidator AssumeNull<T>(this DomainValidator validator, T? value, string message)
        {
            if (value != null)
                validator.AddNotification(message);
            return validator;
        }
    }
}


public static class ValidatorExtension
{
    //generic
    public static bool IsValid(this string value, string pattern) => Regex.IsMatch(value, pattern);

    //string
    public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? value) => !value.IsNullOrWhiteSpace();
    public static bool IsNotNull([NotNullWhen(true)] this string? value) => !string.IsNullOrEmpty(value);
    public static bool IsShorterThan(this string value, int length) => value.Length < length;
    public static bool IsLongerThan(this string value, int length) => value.Length > length;
    public static bool IsEqualTo(this string value, int length) => value.Length.Equals(length);
    public static bool IsEqualTo(this string value, string comparison) => value.Equals(comparison);
    public static bool IsAlphabetic(this string value) => Regex.IsMatch(value, @"^[a-zA-Z]+$");
    public static bool IsNumeric(this string value) => Regex.IsMatch(value, @"^\d+$");
    public static bool IsValidEmail(this string value) => Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    //int
    public static bool IsGreaterThan(this int value, int comparison) => value > comparison;
    public static bool IsLessThan(this int value, int comparison) => value < comparison;
    public static bool IsSignificant(this int value) => value > 0;
    public static bool IsSignificant(this int? value) => value.HasValue && value.Value.IsSignificant();
    public static bool IsEqualTo(this int value, int comparison) => value.Equals(comparison);
    public static bool IsInRange(this int value, int min, int max) => value >= min && value <= max;
    public static bool IsPositive(this int value) => value > 0;
    public static bool IsNegative(this int value) => value < 0;

    //decimal
    public static bool IsGreaterThan(this decimal value, decimal comparison) => value > comparison;
    public static bool IsLessThan(this decimal value, decimal comparison) => value < comparison;
    public static bool IsSignificant(this decimal value) => value > 0;
    public static bool IsSignificant(this decimal? value) => value.HasValue && value.Value.IsSignificant();
    public static bool IsEqualTo(this decimal value, decimal comparison) => value.Equals(comparison);
    public static bool IsInRange(this decimal value, decimal min, decimal max) => value >= min && value <= max;
    public static bool IsPositive(this decimal value) => value > 0;
    public static bool IsNegative(this decimal value) => value < 0;

    //date
    public static bool IsGreaterThan(this DateTime value, DateTime comparison) => value > comparison;
    public static bool IsLessThan(this DateTime value, DateTime comparison) => value < comparison;
    public static bool IsSignificant(this DateTime value) => value != default;
    public static bool IsSignificant(this DateTime? value) => value.HasValue && value.Value.IsSignificant();
    public static bool IsInPast(this DateTime value) => value < DateTime.Now;
    public static bool IsInFuture(this DateTime value) => value > DateTime.Now;

    //guid
    public static bool IsNullOrEmpty(this Guid value) => value == Guid.Empty;
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Guid value) => !value.IsNullOrEmpty();
    public static bool IsNullOrEmpty([NotNullWhen(false)] this Guid? value) => !value.HasValue;
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Guid? value) => value.HasValue && !value.Value.IsNullOrEmpty();
}
