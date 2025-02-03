import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateEquipmentDto, Equipment, EquipmentInfo, PaginatedEquipmentProps, UpdateEquipmentDto } from "../models/equipmentDTO";

const baseUrl = 'https://localhost:7213/api/Equipment'


@Injectable({
  providedIn: 'root'
})
export class EquipmentService {
  
constructor(private http: HttpClient) { }

getAllWithPagination(pageNumber: number, pageSize: number): Observable<PaginatedEquipmentProps> {
  return this.http.get<PaginatedEquipmentProps>(`${baseUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
}

getEquipmentById(id: string): Observable<Equipment> {
    return this.http.get<Equipment>(`${baseUrl}/${id}`);
}

createEquipment(data: CreateEquipmentDto): Observable<EquipmentInfo> {
    return this.http.post<EquipmentInfo>(baseUrl, data);
}

updateEquipment(id: string, data: UpdateEquipmentDto): Observable<void> {
    return this.http.patch<void>(`${baseUrl}/${id}`, data);
}

delete(id: string): Observable<void> {
    return this.http.delete<void>(`${baseUrl}/${id}`);
  }
}