import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegister, IResponse, IResume, IUser } from '../interfaces/appUser';
import { createId } from '@paralleldrive/cuid2';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  constructor(private http: HttpClient) {}

  sendData(profileData: IUser, resumeData: IResume) {
    const bId = createId();
    const combined: IRegister = {
      displayName: profileData.displayName,
      firstName: profileData.firstName,
      lastName: profileData.lastName,
      resume: resumeData,
      password: profileData.password,
      phoneNumber: profileData.phoneNumber,
      email: profileData.email,
      basketId: bId,
    };

    localStorage.setItem('BasketId', bId);

    return this.http.post<IResponse>(
      'http://localhost:5045/api/account/register',
      combined
    );
  }
}
