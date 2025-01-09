import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResponse, IResume } from '../interfaces/appUser';
import { ILoginDto } from '../interfaces/login';
import { IEmailResp } from '../interfaces/emailResp';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  isTokenExpired() {
    const token = localStorage.getItem('TOKEN');
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<boolean>(
      'http://localhost:5045/api/account/istokenexpired',
      {
        headers,
      }
    );
  }

  getResume() {
    const token = localStorage.getItem('TOKEN');

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<IResume>(
      'http://localhost:5045/api/account/getresume',
      { headers }
    );
  }

  getLoginInfo(token: string | null) {
    if (token === null) {
      return null;
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<IResponse>(
      'http://localhost:5045/api/account/autologin',
      {
        headers,
      }
    );
  }

  updateResume(token: string | null, resume: IResume) {
    if (token === null) {
      return null;
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.put<IResume>(
      'http://localhost:5045/api/account/updateresume',
      resume,
      { headers }
    );
  }

  login(loginDto: ILoginDto) {
    return this.http.post<IResponse>(
      'http://localhost:5045/api/account/login',
      loginDto
    );
  }

  sendResume(token: string | null, id: number, resume: IResume) {
    if (token === null) {
      return null;
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.post<IEmailResp>(
      `http://localhost:5045/api/account/sendemail?id=${id}`,
      resume,
      { headers }
    );
  }
}
