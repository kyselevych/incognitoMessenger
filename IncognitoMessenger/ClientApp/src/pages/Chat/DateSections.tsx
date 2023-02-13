import { Box, Center, Stack } from "@chakra-ui/react";
import useSelector from "hooks/useSelector";
import MessageElement from "./MessageElement";
import { DateMessage } from "./types";

type Props = {
  data: DateMessage
};

const DateSections = ({data}: Props) => {
  const user = useSelector(store => store.authStore.user);

  return (
    <>
      {Object.keys(data).map(date => (
        <Box key={date}>
          <Center fontSize='14px' color='theme.600'>{date}</Center>
          <Stack spacing='15px'>
            {data[date] && data[date].map(message => <MessageElement model={message} reverse={user?.id === message.user.id} key={message.id}/>)}
          </Stack>
        </Box>
      ))}
    </>
  );
};

export default DateSections;