import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-vacancy-list',
  templateUrl: './vacancy-list.component.html',
  styleUrls: ['./vacancy-list.component.scss'],
})
export class VacancyListComponent implements OnInit {
  search: string = '';

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params) => {
      this.search = params['query'] || '';
    });
  }

  remove() {
    this.search = '';
    this.router.navigate(['catalog'], {
      queryParams: null,
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
