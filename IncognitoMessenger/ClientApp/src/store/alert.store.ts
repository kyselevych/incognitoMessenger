import type RootStore from "store";
import { makeAutoObservable } from "mobx";
import { v4 as uuid } from 'uuid';

type AlertStatus = 'error' | 'warning' | 'success' | 'info';

type Alert = {
  id: string,
  message: string,
  status: AlertStatus
};

class AlertStore {
  public alerts: Alert[] = [];

  constructor(readonly rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  };

  setAlert = (message: string, status: AlertStatus = 'success') => {
    const id = uuid();
    this.alerts.push({ id, message, status});

    setTimeout(() => {
      this.alerts = this.alerts.filter(err => err.id !== id);
    }, 3000);
  };
};

export default AlertStore;