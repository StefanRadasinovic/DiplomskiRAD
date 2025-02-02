import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateMotorDto, MotorInfo, PaginatedMotorProps, UpdateMotorDto } from "../models/motorDTO";

const baseUrl = 'https://localhost:7213/api/Motorcycle'


@Injectable({
  providedIn: 'root'
})
export class MotorService {
  
constructor(private http: HttpClient) { }

//Paginacija
getAllWithPagination(pageNumber: number, pageSize: number): Observable<PaginatedMotorProps> {
  return this.http.get<PaginatedMotorProps>(`${baseUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
}

  getMotorById(id: string): Observable<MotorInfo> {
    return this.http.get<MotorInfo>(`${baseUrl}/${id}`);
  }

  createMotor(data: CreateMotorDto): Observable<MotorInfo> {
    return this.http.post<MotorInfo>(baseUrl, data);
  }

  updateMotor(id: string, data: UpdateMotorDto): Observable<void> {
    return this.http.patch<void>(`${baseUrl}/${id}`, data);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${baseUrl}/${id}`);
  }
}