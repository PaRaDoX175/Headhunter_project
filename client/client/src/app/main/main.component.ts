import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MainService } from './main.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  search: string = '';
  vacancyCount?: number;
  resumeCount?: number;
  companyCount?: number;

  constructor(private main: MainService, private router: Router) {}

  ngOnInit(): void {
    this.getVacancyCount();
    this.getResumeCount();
    this.getCompanyCount();
  }

  getVacancyCount() {
    this.main.getVacancyCount().subscribe((data) => {
      this.vacancyCount = data;
    });
  }

  getResumeCount() {
    this.main.getResumeCount().subscribe((data) => {
      this.resumeCount = data;
    });
  }

  getCompanyCount() {
    this.main.getCompanyCount().subscribe((data) => {
      this.companyCount = data;
    });
  }

  onSearch() {
    if (this.search) {
      this.router.navigate(['catalog'], {
        queryParams: { query: this.search },
      });
    }
  }
}
