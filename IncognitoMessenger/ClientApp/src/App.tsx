import './styles/index.scss';
import axios from 'axios';
import { RouterProvider } from 'react-router-dom';
import { router } from './routes';
import { ChakraProvider } from '@chakra-ui/react';
import theme from './styles/theme';
import { StoreProvider } from 'components/StoreProvider';
import AlertProvider from 'components/AlertProvider';

const App = () => {
  axios.defaults.baseURL = process.env.REACT_APP_API_URL;

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
