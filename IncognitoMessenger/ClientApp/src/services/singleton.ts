import AuthService from "./auth";
import ChatService from "./chat";

const services = {
  authService: new AuthService(),
  chatService: new ChatService()
};

export default services;