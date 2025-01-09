import { Component, OnInit } from '@angular/core';
import { AboutCompanyService } from './about-company.service';
import { ICompany } from '../interfaces/vacancy';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-about-company',
  templateUrl: './about-company.component.html',
  styleUrls: ['./about-company.component.scss'],
})
export class AboutCompanyComponent implements OnInit {
  company?: ICompany;
  isHovered: boolean = false;

  constructor(
    private companyService: AboutCompanyService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getCompany();
  }

  getCompany() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    console.log(id);
    if (id) {
      this.companyService.getCompany(+id).subscribe({
        next: (data) => {
          this.company = data;
          console.log(this.company);
        },
      });
    }
  }
}
