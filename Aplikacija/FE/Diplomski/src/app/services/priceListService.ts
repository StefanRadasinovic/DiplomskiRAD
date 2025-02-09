import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateMotorDto, Motor, MotorInfo, PaginatedMotorProps, UpdateMotorDto } from "../models/motorDTO";
import { CreatePriceListDto, CustomPriceListInfo, PriceListInfo, PriceListInfo22 } from "../models/priceListDTO";

const baseUrl = 'https://localhost:7213/api/PriceList'


@Injectable({
  providedIn: 'root'
})
export class PriceListService {
    constructor(private http: HttpClient) { }
  
    getPriceListById(id: string): Observable<CustomPriceListInfo> {
      return this.http.get<CustomPriceListInfo>(`${baseUrl}/${id}`);
    }
  
    createPriceList(productId: string, data: CreatePriceListDto): Observable<CustomPriceListInfo> {
      return this.http.post<CustomPriceListInfo>(`${baseUrl}/${productId}`, data);
    }
  
    getCurrentPriceListByMotorId(motorcycleId: string): Observable<PriceListInfo> {
      return this.http.get<PriceListInfo>(`${baseUrl}/MotorCurrentPrice/${motorcycleId}`);
    }
  
    getAllPricesListByMotorId(motorcycleId: string): Observable<PriceListInfo[]> {
      return this.http.get<PriceListInfo[]>(`${baseUrl}/MotorAllPrices/${motorcycleId}`);
    }
  
    getCurrentPriceListByEquipmentId(equipmentId: string): Observable<PriceListInfo22> {
      return this.http.get<PriceListInfo22>(`${baseUrl}/EquipmentCurrentPrice/${equipmentId}`);
    }
  
    getAllPricesListByEquipmentId(equipmentId: string): Observable<PriceListInfo22[]> {
      return this.http.get<PriceListInfo22[]>(`${baseUrl}/EquipmentAllPrices/${equipmentId}`);
    }
  }