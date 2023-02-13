import { Avatar, Box, Flex, Text } from '@chakra-ui/react';
import { Message } from 'behavior/chat/types';
import dayjs from 'dayjs';

type Props = {
  model: Message,
  reverse?: boolean
};

const MessageElement = ({model, reverse = false}: Props) => {
  return (
    <Box>
      <Flex gap={5} flexDirection={reverse ? 'row-reverse' : 'row'}>
        <Avatar name={model.user.nickname} size='md'></Avatar>
        <Box w="100%">
          <Text fontSize="12px" textAlign={reverse ? 'end' : 'start'} mt='7px'>{model.user.nickname}</Text>
          <Flex alignItems='center' w="100%" flexDirection={reverse ? 'row-reverse' : 'row'}>
            <Box
              mt="5px"
              padding={3} 
              borderRadius={reverse ? '15px 0 15px 15px' : '0 15px 15px 15px'}
              bg={reverse ? 'accent.300' : 'theme.700'}
              color={reverse ? 'accent.200' : 'theme.600'}
              fontSize="14px"
              position="relative"
            > 
              {model.text}
            </Box>
            <Text fontSize="10px" margin={reverse ? '0 15px 0 0' : '0 0 0 15px'} color='theme.600'>{dayjs(model.dateTime).format('HH:mm')}</Text>
          </Flex>
        </Box>
      </Flex>
    </Box>
  );
};

export default MessageElement;