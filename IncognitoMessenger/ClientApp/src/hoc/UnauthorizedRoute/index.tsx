import Loader from 'components/Loader';
import useSelector from 'hooks/useSelector';
import { Navigate } from 'react-router-dom';
import { Route } from 'routes';
import { AuthStatus } from 'store/auth.store';

type Props = {
  children: JSX.Element
};

const UnauthorizedRoute = ({ children }: Props): JSX.Element => {
  const status = useSelector(store => store.authStore.status); 

  switch(status) {
    case AuthStatus.Unauthorized: 
      return <Navigate to={Route.Index} />

    case AuthStatus.Authorized: 
      return children;

    case AuthStatus.Pending:
    default:
      return <Loader />;
  }
};

export default UnauthorizedRoute;