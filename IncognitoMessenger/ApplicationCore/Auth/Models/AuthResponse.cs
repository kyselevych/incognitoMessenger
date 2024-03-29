﻿using IncognitoMessenger.ApplicationCore.Auth.Models;
using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.Models.Auth;

public class AuthResponse
{
    public AccessToken AccessToken { get; set; } = new AccessToken();

    public UserResponse User { get; set; } = new UserResponse();
}