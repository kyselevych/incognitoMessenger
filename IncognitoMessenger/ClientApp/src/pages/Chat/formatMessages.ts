import { Message } from "behavior/chat/types";
import dayjs from "dayjs";

type DateMessage = Record<string, Message[]>;

const formatMessages = (messages: Message[]) => {
  const dateMessages: DateMessage = {};

  messages.forEach(m => {
    const date = dayjs(m.dateTime).format('DD.MM.YYYY');
    if (!dateMessages[date]) dateMessages[date] = [];
    dateMessages[date].push(m);
  });

  return dateMessages;
};

export default formatMessages;