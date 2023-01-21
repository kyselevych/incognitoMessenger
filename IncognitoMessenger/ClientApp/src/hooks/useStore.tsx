import RootStore from "store";
import { createContext, useContext } from "react";

export const store = new RootStore();
export const StoreContext = createContext<RootStore>(store);

export const useStore = () => {
  return useContext(StoreContext)
};

export default useStore;