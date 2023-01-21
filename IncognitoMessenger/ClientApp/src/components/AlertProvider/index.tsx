import { Alert, AlertIcon, Stack } from '@chakra-ui/react';
import useStore from 'hooks/useStore'
import { observer } from 'mobx-react-lite';

const AlertProvider = () => {
  const { alertStore } = useStore();

  return (
    <>
      <Stack spacing={3} position="fixed" bottom="20px" left="20px" width="400px">
        {alertStore.alerts.map(alert => (
          <Alert status={alert.status} key={alert.id}>
            <AlertIcon />
            {alert.message}
          </Alert>
        ))}
      </Stack>
    </>
  );
};

export default observer(AlertProvider);