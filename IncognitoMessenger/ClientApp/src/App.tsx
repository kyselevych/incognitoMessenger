import './styles/index.scss';
import { RouterProvider } from 'react-router-dom';
import { router } from './routes';
import { ChakraProvider } from '@chakra-ui/react';
import theme from './styles/theme';

const App = () => {
  return (
    <ChakraProvider theme={theme}>
      <RouterProvider router={router} />
    </ChakraProvider>
  );
}

export default App;
