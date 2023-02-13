import type RootStore from "store";
import { makeAutoObservable } from "mobx";
import { ChatInfo, Message } from "behavior/chat/types";
import ChatService from "services/chat";
import ChatHub from "hubs/chat";

class ChatStore {
  public chat: ChatInfo | null = null;
  public chatHub: ChatHub | null = null;

  constructor(readonly chatId: string, readonly rootStore: RootStore, readonly chatService: ChatService) {
    this.rootStore = rootStore;
    this.chatService = chatService;
    this.chatId = chatId;

    this.initializeStore();
    makeAutoObservable(this);
  };
  
  private initializeChutHub = () => {
    this.chatHub = new ChatHub();
    this.chatHub.buildConnection(this.chatId);
  };

  private initializeStore = () => {
    this.initializeChutHub();
    this.chatHub?.connection?.start()
      .then(() => this.chatHub?.joinChat())
      .then(() => {
        this.chatHub?.connection?.on("ReceiveMessage", (message) => {
          
          this.addMessage(message)
          console.log(this.chat);
        });
      })
      .then(() => this.chatService.getChatInfo(this.chatId))
      .then(response => this.chat = response.data.data);
  };

  private addMessage = (message: Message) => {
    this.chat?.messages.push(message);
  };

  public sendMessage = async (text: string) => {
    await this.chatService.sendMessage({chatId: this.chatId, text});
  };

  deleteMessage = (messageId: number) => {
    if (this.chat?.messages) {
      this.chat.messages = this.chat.messages.filter(m => m.id !== messageId);
    }
  };
};

export default ChatStore;