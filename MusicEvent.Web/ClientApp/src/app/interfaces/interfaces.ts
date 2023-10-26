export interface ApiResponse<T> {
    data: T[];
    success: boolean;
    errors?: any;
  }
  
  export interface ApiResponseSingle<T> {
    data: T;
    success: boolean;
    errors?: any;
  }
  
  export interface ILogin {
    authenticated: boolean;
    created: Date;
    expiration: Date;
    accessToken: string;
    message: string;
  }