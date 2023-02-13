import axios from "axios";
import { ChatInfo } from "behavior/chat/types";
import { ApiResponse } from "services/common/types";


class ChatService {
  public getChatInfo = async (chatId: string) => {
    return await axios.get<ApiResponse<ChatInfo>>(process.env.REACT_APP_API_URL + 'chat/info/' + chatId);
  };

  public sendMessage = async (message: {chatId: string, text: string}) => {
    return await axios.post(process.env.REACT_APP_API_URL + 'chat/sendmessage', message);
  };
}

export default ChatService;