import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICompany } from '../interfaces/vacancy';

@Injectable({
  providedIn: 'root',
})
export class AboutCompanyService {
  url = 'http://localhost:5045/api/company/company/';

  constructor(private http: HttpClient) {}

  getCompany(id: number) {
    return this.http.get<ICompany>(this.url + id);
  }
}
