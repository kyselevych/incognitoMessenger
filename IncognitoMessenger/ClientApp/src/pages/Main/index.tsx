import { Grid, GridItem } from '@chakra-ui/react';
import axios from 'axios';
import { ChatCard } from 'behavior/chat/types';
import ChatList from 'components/ChatsList/ChatList';
import Loader from 'components/Loader';
import React, { useEffect, useState } from 'react'
import { Outlet } from 'react-router-dom';
import { ApiResponse } from 'services/common/types';

const Main = () => {
  const [chats, setChats] = useState<ChatCard[] | null>(null);

  useEffect(() => {
    axios.get<ApiResponse<ChatCard[]>>(process.env.REACT_APP_API_URL + 'chat/list')
      .then((response) => setChats(response.data.data));
  }, [])
  

  return (
    <Grid 
      maxH='100vh'
      templateColumns='5% 20% 55% 20%'
      templateRows='1fr'
    >
      <GridItem></GridItem>
      <GridItem>
        {chats 
          ? <ChatList model={chats} />
          : <Loader />
        }
      </GridItem>
      <GridItem>
        <Outlet />
      </GridItem>
      <GridItem></GridItem>
    </Grid>
  )
}

export default Main;