import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateWorkerDtO, DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto, UpdateClientDto, UpdateDirectorDto, UpdateWorkerDto, UserInfo } from "../models/userDTO";
import { AuthorisationService } from "./authorisationService";


const baseUrl = 'https://localhost:7213/api/User'


@Injectable({
  providedIn: 'root'
})
export class UserService {
  
constructor(private http: HttpClient, private authService : AuthorisationService) { }

getAllUsers(): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>(`${baseUrl}`, {
      headers: { Authorization: `Bearer ${this.authService.getToken()}` }
    });
  }

getUserById(id: string): Observable<DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto> {
    return this.http.get<DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto>(`${baseUrl}/${id}`, {
      headers: { Authorization: `Bearer ${this.authService.getToken()}` }
    });
}

createWorker(data: CreateWorkerDtO): Observable<DisplayWorkerDto> {
    return this.http.post<DisplayWorkerDto>(baseUrl, data);
}

updateUser(id: string, data: UpdateWorkerDto | UpdateClientDto | UpdateDirectorDto): Observable<void> {
    return this.http.put<void>(`${baseUrl}/${id}`, data);
}

deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${baseUrl}/${id}`);
}

getAllWorkers(): Observable<UserInfo[]> {
  return this.http.get<UserInfo[]>(`${baseUrl}/workers`, {
    headers: { Authorization: `Bearer ${this.authService.getToken()}` }
  });
}

getAllFreeUsersForTask(taskId: string): Observable<UserInfo[]> {
  return this.http.get<UserInfo[]>(`${baseUrl}/free-workers/${taskId}`);
}


}