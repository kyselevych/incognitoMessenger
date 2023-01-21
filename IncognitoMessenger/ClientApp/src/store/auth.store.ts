import type RootStore from "store";
import type { User } from "../behavior/auth/types";
import type AuthService from "services/auth";
import type { LoginPayload, RegisterPayload } from "services/auth/types";
import { makeAutoObservable, runInAction } from "mobx";
import { ApiError } from "services/common/types";

export enum AuthStatus {
  Authorized = 'authorized',
  Unauthorized = 'unauthorized',
  Pending = 'pending'
};

class AuthStore {
  public status = AuthStatus.Pending;
  public user: User | null = null;
  
  constructor(readonly rootStore: RootStore, readonly authService: AuthService) {
    this.rootStore = rootStore;
    this.authService = authService;
    makeAutoObservable(this, { rootStore: false });
  };

  login = (payload: LoginPayload) => {
    return this.authService.login(payload)
      .then(response => {
        runInAction(() => this.setAuthorized(response.data.user));
        this.rootStore.alertStore.setAlert('You have authorized successfully');
      })
      .catch((error: ApiError) => {
        this.rootStore.alertStore.setAlert(error.message, 'error');
      });
  };

  register = (payload: RegisterPayload) => {
    return this.authService.register(payload)
      .then(response => {
        runInAction(() => this.setAuthorized(response.data.user));
      })
      .catch((error: ApiError) => {
        this.rootStore.alertStore.setAlert(error.message, 'error');
      });
  };

  refresh = () => {
    return this.authService.refresh()
      .then(response => {
        runInAction(() => {
          this.setAuthorized(response.data.user);
        });
      })
      .catch((error: ApiError) => {
        this.rootStore.alertStore.setAlert(error.message, 'error');
      });
  };

  logout = () => {
    this.authService.logout();
    this.setUnauthorized();
  };

  setAuthorized = (user: User) => {
    this.user = user;
    this.status = AuthStatus.Authorized;
  };

  setUnauthorized = () => {
    this.user = null;
    this.status = AuthStatus.Unauthorized;
  };

  setPending = () => {
    this.status = AuthStatus.Pending;
  };
};

export default AuthStore;