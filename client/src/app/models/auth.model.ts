export type User = {
  id: number;
  firstName: string;
  lastName: string;
  username: string;
};

export type Register = {
  firstName: string;
  lastName: string;
  username: string;
  password: string;
};

export type Login = {
  username: string;
  password: string;
};

export type AuthResponse = {
  message: string;
  data: string | User;
  success: boolean;
};
