import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICompany, IVacancy } from '../interfaces/vacancy';

@Injectable({
  providedIn: 'root',
})
export class VacancyDetailsService {
  constructor(private http: HttpClient) {}
  url = 'http://localhost:5045/api/company/';

  getVacancy(id: number) {
    return this.http.get<IVacancy>(this.url + 'vacancy/' + id);
  }

  getCompany(companyId: number) {
    return this.http.get<ICompany>(this.url + 'company/' + companyId);
  }

  getSameVacancy(id: number) {
    return this.http.get<IVacancy[]>(
      this.url + 'vacancy/' + id + '/same_vacancy'
    );
  }
}
