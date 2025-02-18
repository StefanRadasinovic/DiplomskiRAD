import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateOrderDto, OrderInfo } from "../models/orderDTO";
import { CreateServiceDto, DeclineServiceDto, DirektorServiceInfo, Service, ServiceInfo, UserServiceInfo } from "../models/serviceDTO";

const baseUrl = 'https://localhost:7213/api/Service'


@Injectable({
  providedIn: 'root'
})
export class ServiceService {
    constructor(private http: HttpClient) { }
  
    getAllPendingAndInprogressServices(): Observable<DirektorServiceInfo[]> {
      return this.http.get<DirektorServiceInfo[]>(`${baseUrl}/pending-inProgress`);
    }
    
    getServiceById(id: string): Observable<ServiceInfo> {
          return this.http.get<ServiceInfo>(`${baseUrl}/${id}`);
    }

    getAllServicesForUser(id: string): Observable<UserServiceInfo[]> {
        return this.http.get<UserServiceInfo[]>(`${baseUrl}/user/${id}`);
    }

    createService(userId: string, data: CreateServiceDto): Observable<Service> {
        return this.http.post<Service>(`${baseUrl}/${userId}`, data);
    }
    
    
    acceptService(orderId: string): Observable<void> {
        return this.http.put<void>(`${baseUrl}/accept/${orderId}`, {});
    }
      
    declineService(orderId: string, data: DeclineServiceDto): Observable<void> {
      return this.http.put<void>(`${baseUrl}/decline/${orderId}`, data);
  }
  
    deleteService(serviceId: string): Observable<void> {
      return this.http.delete<void>(`${baseUrl}/${serviceId}`);
  }

}
