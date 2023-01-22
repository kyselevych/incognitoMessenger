import type { User } from "behavior/auth/types";

export type AccessToken = {
  key: string,
  expiryTime: number
};

export type LoginPayload = {
  login: string,
  password: string
};

export type AuthResponse = {
  user: User,
  accessToken: AccessToken
};

export type RegisterPayload = {
  login: string,
  password: string,
  nickname: string,
};