import services from "services/singleton";
import AuthStore from "./auth.store";
import AlertStore from "./alert.store";
import ChatStore from "./chat.store";

class RootStore {
  public authStore: AuthStore;
  public alertStore: AlertStore;

  constructor() {
    this.authStore = new AuthStore(this, services.authService);
    this.alertStore = new AlertStore(this);
  };

  public createChatStore = (chatId: string) => {
    return new ChatStore(chatId, this, services.chatService);
  };
};

export default RootStore;