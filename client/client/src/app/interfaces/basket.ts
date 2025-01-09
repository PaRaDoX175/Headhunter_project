export interface IBasket {
  id: string | null;
  items: IBasketItems[];
}

export interface IBasketItems {
  id: number;
  position: string;
  salary: number;
  companyName: string;
  location: string;
}

export class Basket implements IBasket {
  id = localStorage.getItem('BasketId');
  items: IBasketItems[] = [];
}
