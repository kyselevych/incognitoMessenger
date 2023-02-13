import { Avatar, Box, Flex, Text } from '@chakra-ui/react';
import { ChatCard } from 'behavior/chat/types';
import dayjs from 'dayjs';
import { m } from 'framer-motion';

import React from 'react'
import { useNavigate } from 'react-router-dom';
import { Route } from 'routes';

type Props = {
  model: ChatCard
};

const ChatCardElement = ({model}: Props) => {
  const navigate = useNavigate();

  return (
    <Box 
      p={2}
      borderRadius={5}
      style={{transition: '.2s'}}
      _hover={{background: 'accent.300'}} 
      cursor="pointer" 
      onClick={() => navigate(Route.Chat + model.chat.id)}
    >
      <Flex gap={3}>
        <Avatar size="sm" name={model.chat.title}/>
        <Box w="100%">
          <Flex wrap="nowrap" justifyContent="space-between">
            <Text fontSize="12px" isTruncated>{model.chat.title}</Text>
            <Text fontSize="10px" ml="20px" color="accent.200">{dayjs(model.lastMessage?.dateTime).format('DD.MM.YY HH:mm')}</Text>
          </Flex>
          {model.lastMessage && 
            <Text fontSize="10px" isTruncated color="accent.200">
              <span style={{fontWeight: 500}}>{model.lastMessage.user?.nickname}</span>: {model.lastMessage.text}
            </Text>
          }
          </Box>
      </Flex>
    </Box>
  );
};

export default ChatCardElement;