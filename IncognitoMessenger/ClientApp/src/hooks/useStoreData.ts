import { useObserver } from "mobx-react-lite";
import React, { useContext } from "react";

export const useStoreData = <Selection, ContextData, Store>(
  context: React.Context<ContextData>,
  storeSelector: (contextData: ContextData) => Store,
  dataSelector: (store: Store) => Selection
) => {
  const value = useContext(context);
  if (!value) throw new Error();
  const store = storeSelector(value);
  return useObserver(() => dataSelector(store));
};