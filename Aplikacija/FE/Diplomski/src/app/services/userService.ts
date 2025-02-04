import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateWorkerDtO, DisplayClientDto, DisplayDirectorDto, DisplayWorkerDto, UpdateClientDto, UpdateDirectorDto, UpdateWorkerDto, UserInfo } from "../models/userDTO";


const baseUrl = 'https://localhost:7213/api/User'


@Injectable({
  providedIn: 'root'
})
export class UserService {
  
constructor(private http: HttpClient) { }

getAllUsers(): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>(`${baseUrl}`);
}

getUserById(id: string): Observable<DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto> {
    return this.http.get<DisplayWorkerDto | DisplayClientDto | DisplayDirectorDto>(`${baseUrl}/${id}`);
}

createWorker(data: CreateWorkerDtO): Observable<DisplayWorkerDto> {
    return this.http.post<DisplayWorkerDto>(baseUrl, data);
}

updateUser(id: string, data: UpdateWorkerDto | UpdateClientDto | UpdateDirectorDto): Observable<void> {
    return this.http.patch<void>(`${baseUrl}/${id}`, data);
}

deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${baseUrl}/${id}`);
}

}