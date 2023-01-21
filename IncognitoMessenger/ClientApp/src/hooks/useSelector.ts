import RootStore from "store";
import { createContext } from "react";
import { useStoreData } from "./useStoreData";

export const store = new RootStore();
export const StoreContext = createContext<RootStore>(store);

const useSelector = <Selection>(dataSelector: (store: RootStore) => Selection) => 
  useStoreData(StoreContext, contextData => contextData!, dataSelector);

export default useSelector;
