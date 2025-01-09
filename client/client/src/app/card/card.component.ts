import { Component, Input } from '@angular/core';
import { IVacancy } from '../interfaces/vacancy';
import { Router } from '@angular/router';
import { FavoritesService } from '../services/favorites.service';
import { IBasket } from '../interfaces/basket';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent {
  @Input() vacancy!: IVacancy;
  @Input() isInFavorites!: boolean;

  constructor(
    private router: Router,
    public favoritesService: FavoritesService
  ) {}

  redirection(id: number) {
    this.router.navigate(['/catalog/', id]);
  }

  updateBasket(item: IVacancy) {
    const basket: IBasket = {
      id: localStorage.getItem('BasketId'),
      items: [
        {
          id: item.id,
          position: item.position,
          salary: item.salary,
          companyName: item.companyName,
          location: item.location,
        },
      ],
    };

    this.favoritesService.setFavorites(basket);
  }

  deleteBasketItem(itemId: number) {
    this.favoritesService.deleteFavorites(itemId).subscribe({
      next: (x) => console.log(x),
    });
  }

  changeIcon() {
    if (this.isInFavorites) this.isInFavorites = false;
    else this.isInFavorites = true;
  }
}
