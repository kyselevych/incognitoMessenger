import { User } from "behavior/auth/types";

export type Chat = {
  id: number,
  title: string,
  userId: number
};

export type Message = {
  id: number,
  text: string,
  dateTime: string,
  chatId: number
  user: User
};

export type ChatCard = {
  chat: Chat,
  lastMessage?: Message
};

export type ChatInfo = Chat & { messages: Message[], users: User[] };
  
  