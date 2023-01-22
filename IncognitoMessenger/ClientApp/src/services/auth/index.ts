import type { ApiResponse } from 'services/common/types';
import type { LoginPayload, AuthResponse, RegisterPayload } from './types';
import axios from 'axios';

class AuthService {
  login = async (payload: LoginPayload) => {
    return await this.executeTryCatch(async () => {
      const { data } = await axios.post<ApiResponse<AuthResponse>>('auth/login', payload);
      axios.defaults.headers.common['Authorization'] = data.data.accessToken.key;
      return data;
    });
  };

  register = async (payload: RegisterPayload) => {
    return await this.executeTryCatch(async () => {
      const { data } = await axios.post<ApiResponse<AuthResponse>>('auth/register', payload);
      axios.defaults.headers.common['Authorization'] = data.data.accessToken.key;
      return data;
    });
  };

  logout = async () => {
    await this.executeTryCatch(async () => {
      await axios.post('auth/revoke');
      delete axios.defaults.headers.common['Authorization'];
    });
  };

  refresh = async () => {
    return await this.executeTryCatch(async () => {
      const { data } = await axios.post<ApiResponse<AuthResponse>>('auth/refresh');
      axios.defaults.headers.common['Authorization'] = data.data.accessToken.key;
      return data;
    });
  };

  private executeTryCatch = async<T> (func: () => T): Promise<T> => {
    try {
      return await func();
    }
    catch (error) {
      if (axios.isAxiosError<ApiResponse<AuthResponse>>(error)) {
        throw error.response?.data.error;
      }

      throw error;
    }
  };
};

export default AuthService;