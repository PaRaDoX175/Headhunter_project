export interface IErrors {
  details: string;
  errors: string[];
  message: string;
  statusCode: number;
}

export interface IResponse {
  error: IErrors;
  headers: any[];
  message: string;
  name: string;
  ok: boolean;
  status: number;
  statusText: string;
  url: string;
}
