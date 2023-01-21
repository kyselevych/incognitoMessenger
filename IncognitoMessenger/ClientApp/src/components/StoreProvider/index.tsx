import { StoreContext, store } from "hooks/useStore";
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
