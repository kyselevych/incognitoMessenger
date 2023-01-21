import { StoreContext, store } from "hooks/useSelector";
import { type ReactNode } from "react";

type Props = {
  children: ReactNode
};

const StoreProvider = ({ children }: Props) => {
  return (
    <StoreContext.Provider value={store}>
      {children}
    </StoreContext.Provider>
  );
};

export {StoreContext, StoreProvider};
