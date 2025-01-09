import { Component, OnInit } from '@angular/core';
import { FavoritesService } from '../services/favorites.service';
import { Basket, IBasket } from '../interfaces/basket';
import { Router } from '@angular/router';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss'],
})
export class FavoritesComponent implements OnInit {
  favorites: IBasket = new Basket();

  constructor(
    private favoritesService: FavoritesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getFavorites();
    console.log(this.favorites);
  }

  get items() {
    return this.favorites.items || [];
  }

  getFavorites() {
    this.favoritesService.getFavorites().subscribe({
      next: (fav) => {
        this.favorites = fav || new Basket();
        console.log(this.favorites);
      },
    });
  }

  deleteFavorites(itemId: number) {
    this.favoritesService.deleteFavorites(itemId).subscribe({
      next: (basket) => (this.favorites = basket),
    });
  }

  getDetails(id: number) {
    this.router.navigate(['/catalog/', id]);
  }
}
