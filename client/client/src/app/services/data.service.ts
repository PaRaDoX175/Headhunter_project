import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IVacancy } from '../interfaces/vacancy';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  private dataSubject = new BehaviorSubject<any>(null);
  data$: Observable<any> = this.dataSubject.asObservable();

  updateData(data: IVacancy[]) {
    this.dataSubject.next(data);
  }
}
