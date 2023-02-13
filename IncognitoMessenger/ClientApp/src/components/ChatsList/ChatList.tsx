import { Box, Stack } from '@chakra-ui/react'
import { ChatCard } from 'behavior/chat/types';
import ChatCardElement from './ChatCardElement';

type Props = {
  model: ChatCard[]
};

const ChatList = ({model}: Props) => {
  return (
    <Box maxH='100%' overflow-y='scroll' bg='theme.800' maxW='100%' p='0 8px'>
      <Stack >
        {model.map(chat => <ChatCardElement model={chat} />)}
      </Stack>
    </Box>
  );
};

export default ChatList;