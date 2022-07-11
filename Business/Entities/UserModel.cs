﻿namespace Business.Entities;

public class UserModel
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Pseudonym { get; set; } = string.Empty;
}   