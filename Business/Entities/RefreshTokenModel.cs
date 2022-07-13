﻿namespace Business.Entities;

public class RefreshTokenModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
    
    public DateTime ExpiryTime { get; set; }
}   