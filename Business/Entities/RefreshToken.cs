﻿namespace Business.Entities;

public class RefreshToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = string.Empty;
   
    public User User { get; set; } = null!;
}   