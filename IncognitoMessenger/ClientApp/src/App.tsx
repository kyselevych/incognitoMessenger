import './styles/index.scss';
import axios from 'axios';
import { RouterProvider } from 'react-router-dom';
import { router } from './routes';
import { ChakraProvider } from '@chakra-ui/react';
import theme from './styles/theme';

import AlertProvider from 'hoc/AlertProvider';
import { StoreProvider } from 'hoc/StoreProvider';
import { useEffect } from 'react';
import useSelector from 'hooks/useSelector';

const App = () => {
  axios.defaults.baseURL = process.env.REACT_APP_API_URL;
  const authStore = useSelector(store => store.authStore);
  
  useEffect(() => {
    authStore.refresh();
  }, [authStore])

  return (
    <ChakraProvider theme={theme}>
      <StoreProvider>
        <RouterProvider router={router} />
        <AlertProvider />
      </StoreProvider>
    </ChakraProvider>
  );
}

export default App;
