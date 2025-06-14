import { OrderItem } from "./OrderItem";

export interface OrderVM {
  orderId: string; 
  userId: string;  
  paymentId: string; 
  receiverName: string;
  receiverPhone: string;
  receiverAddress: string;
  total: number; 
  isPayment: boolean;
  status: string;
  createdDate: Date;
  items: OrderItem[];
}
