import { CartItem } from "./CartItem.model";

export interface Cart {
  cartId: string; 
  createdDate: Date;
  items: CartItem[];
}