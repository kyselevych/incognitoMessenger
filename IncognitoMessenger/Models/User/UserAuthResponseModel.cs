﻿namespace IncognitoMessenger.Models.User;

public class UserAuthResponseModel
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Pseudonym { get; set; } = string.Empty;
}