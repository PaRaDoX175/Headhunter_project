export interface IResume {
  name: string;
  aboutMe: string;
  email: string;
  phoneNumber: string;
  profession: string;
  skills: string;
}

export interface IUser {
  displayName: string;
  firstName: string;
  lastName: string;
  password: string;
  phoneNumber: string;
  email: string;
}

export interface IRegister {
  displayName: string;
  firstName: string;
  lastName: string;
  resume: IResume;
  password: string;
  phoneNumber: string;
  email: string;
  basketId: string;
}

export interface IResponse {
  displayName: string;
  email: string;
  token: string;
  basketId: string;
}
