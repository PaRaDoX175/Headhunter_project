import { Component, Input, OnInit } from '@angular/core';
import { ShopParams } from '../interfaces/shopParams';
import { CatalogService } from './catalog.service';
import { IVacancy } from '../interfaces/vacancy';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../services/data.service';
import { FavoritesService } from '../services/favorites.service';
import { IBasket, IBasketItems } from '../interfaces/basket';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss'],
})
export class CatalogComponent implements OnInit {
  vacancies: IVacancy[] = [];
  shopParams = new ShopParams();
  totalCount = 0;
  search: string | null = '';
  basket!: IBasket;

  constructor(
    private catalogService: CatalogService,
    private activatedRoute: ActivatedRoute,
    private dataService: DataService,
    private favoritesService: FavoritesService
  ) {}

  ngOnInit() {
    this.dataService.data$.subscribe({
      next: (data) => (this.vacancies = data),
    });

    this.activatedRoute.queryParamMap.subscribe((params) => {
      const s = params.get('query');
      if (s !== null) this.getVacancies(s);
      else if (s === null) this.getVacancies();
    });

    this.favoritesService.getFavorites().subscribe({
      next: (data) => (this.basket = data),
    });
  }

  isInFavorites(vacancy: IVacancy) {
    return this.basket.items.some(
      (item) =>
        item.id === vacancy.id &&
        item.position === vacancy.position &&
        item.salary === vacancy.salary &&
        item.companyName === vacancy.companyName &&
        item.location === vacancy.location
    );
  }

  getVacancies(search?: string) {
    if (search) {
      this.shopParams.search = search;
    } else {
      this.shopParams.search = null;
    }
    this.catalogService.getVacancies(this.shopParams).subscribe({
      next: (response) => {
        this.vacancies = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
    });
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getVacancies();
    }
  }
}
