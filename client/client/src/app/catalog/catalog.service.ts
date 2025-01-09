import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShopParams } from '../interfaces/shopParams';
import { IPagination } from '../interfaces/pagination';
import { IVacancy } from '../interfaces/vacancy';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  constructor(private client: HttpClient) {}

  url = 'http://localhost:5045/api/company/vacancy';

  getVacancies(shopParams: ShopParams) {
    let params = new HttpParams();

    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    return this.client.get<IPagination<IVacancy[]>>(this.url, { params });
  }
}
