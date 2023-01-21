import { Alert, AlertIcon, Stack } from '@chakra-ui/react';
import useSelector from 'hooks/useSelector';
import { observer } from 'mobx-react-lite';

const AlertProvider = () => {
  const alerts = useSelector(store => store.alertStore.alerts);

  return (
    <>
      <Stack spacing={3} position="fixed" bottom="20px" left="20px" width="400px">
        {alerts.map(alert => (
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