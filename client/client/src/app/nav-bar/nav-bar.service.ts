import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IVacancy } from '../interfaces/vacancy';

@Injectable({
  providedIn: 'root',
})
export class NavBarService {
  constructor(private http: HttpClient) {}

  getVacancies() {
    return this.http.get<IVacancy[]>(
      'http://localhost:5045/api/company/vacancy'
    );
  }
}
