import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MainService {
  constructor(private client: HttpClient) {}

  url = 'http://localhost:5045/api/';

  getVacancyCount() {
    return this.client.get<number>(this.url + 'company/all_vacancy');
  }

  getResumeCount() {
    return this.client.get<number>(this.url + 'resume/all_resume');
  }

  getCompanyCount() {
    return this.client.get<number>(this.url + 'company/all_company');
  }
}
