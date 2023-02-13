import { ChevronRightIcon } from '@chakra-ui/icons';
import { Box, Button, Flex, IconButton, Input } from '@chakra-ui/react'
import useSelector from 'hooks/useSelector';
import { observer } from 'mobx-react-lite';
import React, { useEffect, useMemo, useRef, useState } from 'react'
import { useParams } from 'react-router-dom';
import DateSections from './DateSections';
import formatMessages from './formatMessages';

const chatBoxStyles = {
  '&::-webkit-scrollbar': {
    width: '2px',
  },
  '&::-webkit-scrollbar-track': {
    width: '2px',
  },
  '&::-webkit-scrollbar-thumb': {
    background: 'theme.700',
    borderRadius: '2px',
  },
};

const Chat = () => {
  const [textInput, setTextInput] = useState<string>('');
  const [sending, setSending] = useState<boolean>(false);
  const {chatId} = useParams();
  const chatBox = useRef<HTMLDivElement>(null);

  const rootStore = useSelector(store => store);
  const [chatStore] = useState(() => rootStore.createChatStore(chatId!))

  const formatedMessages = useMemo(
    () => formatMessages(chatStore.chat?.messages || []), 
    [chatStore.chat?.messages.length]
  );

  useEffect(() => {
    scrollBottomChat();
  }, [chatStore.chat?.messages.length])
    
  useEffect(() => {
    return () => {
      chatStore.chatHub?.disconnectChat();
    };
  }, []);

  const handleSendMessage = async () => {
    if (textInput.trim().length === 0) return;
    setSending(true);
    await chatStore.sendMessage(textInput);
    setSending(false);
    setTextInput('');
  };

  const scrollBottomChat = () => {
    const y = chatBox.current?.scrollHeight;
    chatBox.current?.scrollTo({top: y, behavior: 'smooth'});
  };

  return (
    <Box>
      <Box maxH="90vh" overflowY="scroll" padding="0 20px" __css={chatBoxStyles} ref={chatBox}>
        {formatedMessages && <DateSections data={formatedMessages} />}
      </Box>
      <Flex gap={3} alignItems="center" mt="10px">
        <Input value={textInput} onChange={e => setTextInput(e.target.value)}></Input>
        <IconButton size="md" icon={<ChevronRightIcon />} aria-label={'Send'} rounded="50%" onClick={handleSendMessage} />
      </Flex>
    </Box>
  );
};

export default observer(Chat);