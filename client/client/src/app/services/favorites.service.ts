import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Basket, IBasket } from '../interfaces/basket';

@Injectable({
  providedIn: 'root',
})
export class FavoritesService {
  url = 'http://localhost:5045/api/basket';
  basketId = localStorage.getItem('BasketId');

  constructor(private http: HttpClient) {}

  getFavorites() {
    return this.http.get<IBasket>(this.url + '?id=' + this.basketId);
  }

  setFavorites(basket: IBasket) {
    this.http.post<IBasket>(this.url, basket).subscribe({
      next: (x) => console.log(x),
    });
  }

  deleteFavorites(itemId: number) {
    return this.http.delete<IBasket>(
      `${this.url}/item?basketId=${this.basketId}&itemId=${itemId}`
    );
  }
}
