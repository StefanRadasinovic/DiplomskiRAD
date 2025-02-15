import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateTaskDto, RejectTaskDto, TaskServiceInfo } from "../models/taskDTO";
import { Observable } from "rxjs";
import { UsedSparePartDto } from "../models/sparePartsDTO";

const baseUrl = 'https://localhost:7213/api/Task'


@Injectable({
  providedIn: 'root'
})
export class TaskService {
    constructor(private http: HttpClient) {}
  
    createTask(serviceId: string, userId: string, data: CreateTaskDto): Observable<TaskServiceInfo> {
      return this.http.post<TaskServiceInfo>(`${baseUrl}/create/${serviceId}/${userId}`, data);
    }
  
    getTaskById(taskId: string): Observable<TaskServiceInfo> {
      return this.http.get<TaskServiceInfo>(`${baseUrl}/${taskId}`);
    }
  
    getAllTasksForServiceId(serviceId: string): Observable<TaskServiceInfo[]> {
      return this.http.get<TaskServiceInfo[]>(`${baseUrl}/all-service-tasks/${serviceId}`);
    }

    getTasksForUser(workerId: string): Observable<TaskServiceInfo[]> {
      return this.http.get<TaskServiceInfo[]>(`${baseUrl}/tasks-for-worker/${workerId}`);
    }
  
    getAllInProgressDeclinedTasks(): Observable<TaskServiceInfo[]> {
      return this.http.get<TaskServiceInfo[]>(`${baseUrl}/InProgress-declined-tasks`);
    }

    acceptTask(taskId: string, userId: string): Observable<void> {
        return this.http.put<void>(`${baseUrl}/accept/${taskId}/${userId}`, {});
    }
    
    declineTask(taskId: string, data: RejectTaskDto): Observable<void> {
        return this.http.put<void>(`${baseUrl}/decline/${taskId}`, data);
    }
    
    finishTask(taskId: string, data: UsedSparePartDto): Observable<void> {
        return this.http.put<void>(`${baseUrl}/finish/${taskId}`, data);
    }
    
  }
  