import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateOrderDto, OrderInfo } from "../models/orderDTO";

const baseUrl = 'https://localhost:7213/api/Order'


@Injectable({
  providedIn: 'root'
})
export class OrderService {
    constructor(private http: HttpClient) { }
  
    getAllPendingOrders(): Observable<OrderInfo[]> {
      return this.http.get<OrderInfo[]>(`${baseUrl}/pending`);
    }

    getOrderById(id: string): Observable<OrderInfo> {
          return this.http.get<OrderInfo>(`${baseUrl}/${id}`);
    }

    getAllOrdersForUser(id: string): Observable<OrderInfo[]> {
        return this.http.get<OrderInfo[]>(`${baseUrl}/user/${id}`);
    }
  
    createOrder(userId: string, itemId: string, data: CreateOrderDto): Observable<OrderInfo> {
        return this.http.post<OrderInfo>(`${baseUrl}?userId=${userId}&itemId=${itemId}`, data);
    }

    acceptOrder(orderId: string): Observable<void> {
        return this.http.put<void>(`${baseUrl}/accept/${orderId}`, {});
    }
      
      declineOrder(orderId: string): Observable<void> {
        return this.http.put<void>(`${baseUrl}/decline/${orderId}`, {});
    }

}
