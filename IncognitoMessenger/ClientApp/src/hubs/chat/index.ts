import { HttpTransportType, HubConnection, HubConnectionBuilder } from "@microsoft/signalr";


class ChatHub {
  private _connection: HubConnection | null = null;
  private _chatId: string | null = null;

  public get connection() {
    return this._connection;
  }

  public buildConnection = (chatId: string) => {
    this._chatId = chatId;
    this._connection = new HubConnectionBuilder()
      .withUrl(process.env.REACT_APP_URL + 'hubs/chat', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets}
      )
      .withAutomaticReconnect()
      .build();

    return this._connection;
  };

  public joinChat = async () => {
    if (this._connection) {
      return await this._connection.invoke("Join", this._chatId);
    }
    else {
      throw new Error('Connection is not initialized');
    }
  };

  public disconnectChat = async () => {
    if (this._connection) {
      return await this._connection.invoke("Disconnect", this._chatId);
    }
    else {
      throw new Error('Connection is not uninitialized');
    }
  };
}

export default ChatHub;