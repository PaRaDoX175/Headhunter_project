import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VacancyDetailsService } from './vacancy-details.service';
import { ICompany, IVacancy } from '../interfaces/vacancy';
import { AccountService } from '../services/account.service';
import { IResume } from '../interfaces/appUser';
import { NgForm } from '@angular/forms';
import { Basket, IBasket, IBasketItems } from '../interfaces/basket';
import { FavoritesService } from '../services/favorites.service';

@Component({
  selector: 'app-vacancy-details',
  templateUrl: './vacancy-details.component.html',
  styleUrls: ['./vacancy-details.component.scss'],
})
export class VacancyDetailsComponent implements OnInit {
  vacancy!: IVacancy;
  company?: ICompany;
  sameVacancy?: IVacancy[];
  isHovered = false;
  isClicked = false;
  resume?: IResume;
  token: string | null = localStorage.getItem('TOKEN');
  @ViewChild('resumeForm') resumeForm!: NgForm;
  id!: number;
  basketItem!: IBasket;
  isFavorite!: boolean;

  constructor(
    private activatedRoute: ActivatedRoute,
    private detailsService: VacancyDetailsService,
    private accountService: AccountService,
    public favoritesService: FavoritesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.id = +params['id'];
      this.getVacancyAndCompany(this.id);
    });
  }

  getVacancyAndCompany(id: number) {
    if (id) {
      this.detailsService.getVacancy(Number(id)).subscribe({
        next: (vac) => {
          this.vacancy = vac;
          this.initializeBasketItem();
          this.isInFavorites(vac);
          this.detailsService.getCompany(vac.companyId).subscribe({
            next: (data) => {
              this.company = data;
            },
          });
          this.detailsService.getSameVacancy(Number(id)).subscribe({
            next: (data) => {
              this.sameVacancy = data;
              console.log(`Same vacancy: ${this.sameVacancy}`);
            },
          });
        },
      });
    }
  }

  initializeBasketItem() {
    this.basketItem = {
      id: localStorage.getItem('BasketId'),
      items: [
        {
          id: this.vacancy.id,
          position: this.vacancy.position,
          salary: this.vacancy.salary,
          companyName: this.vacancy.companyName,
          location: this.vacancy.location,
        },
      ],
    };
  }

  isInFavorites(vacancy: IVacancy) {
    this.favoritesService.getFavorites().subscribe({
      next: (basket) => {
        console.log(basket);
        const res = basket.items.some(
          (item) =>
            item.id === vacancy.id &&
            item.position === vacancy.position &&
            item.salary === vacancy.salary &&
            item.companyName === vacancy.companyName &&
            item.location === vacancy.location
        );
        console.log(res);
        if (res) this.isFavorite = false;
        else this.isFavorite = true;
      },
    });
  }

  IsClicked() {
    if (this.token === null) {
      this.router.navigate(['login']).then(() => window.location.reload());
    }
    this.getResume();
    this.isClicked = true;
  }

  closeModal() {
    this.isClicked = false;
  }

  getResume() {
    this.accountService.getResume().subscribe({
      next: (data) => {
        this.resume = data;
      },
    });
  }

  sendResume(resume: IResume) {
    if (this.resumeForm.dirty) {
      this.accountService.updateResume(this.token, resume)?.subscribe({
        next: (data) => {
          console.log(data);
          this.resume = data;
          window.location.reload();
        },
      });
    }

    this.accountService.sendResume(this.token, this.id, resume)?.subscribe({
      next: (data) => {
        console.log(data);
        this.isClicked = false;
      },
    });
  }

  deleteBasketItem(itemId: number) {
    console.log(itemId);
    this.favoritesService.deleteFavorites(itemId).subscribe({
      next: (x) => console.log(x),
    });
  }

  changeIcon() {
    if (this.isFavorite) this.isFavorite = false;
    else this.isFavorite = true;
  }

  splitIntoSentences(text: string) {
    return text.split(/(?<=[.;])\s+/);
  }
}
