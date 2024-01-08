﻿using Shared;

namespace Domain.Users;

public class EmailAddress
{
    public string Value { get; }
    public bool IsEmailValidated { get; }

    private EmailAddress(string value, bool isEmailValidated)
    {
        Value = value;
        IsEmailValidated = isEmailValidated;
    }

    public static Result<EmailAddress> BuildEmail(string value)
    {
        var validationResult = ValidateEmail(value);

        return validationResult.IsSuccess
            ? Result<EmailAddress>.Success(new EmailAddress(value.ToLower(), isEmailValidated: true))
            : Result<EmailAddress>.Failure(null, validationResult.Error);
    }

    private static Result ValidateEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure(EmailAddressErrors.InvalidFormat);
        }

        string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
        {
            return Result.Failure(EmailAddressErrors.InvalidFormat);
        }

        return Result.Success();
    }

}