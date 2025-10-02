import { SparePart, SparePartInfo } from "./sparePartsDTO"
import { User, UserInfo } from "./userDTO"

export interface TaskService
{
    id : string,
    taskDescription : string,
    endDateTask? : string,
    status : string,
    razlogOdbijanja? : string,
    serviceId : string,
    users : User,
    spareParts: SparePart[]
   
}

export interface TaskServiceInfo
{
    id : string,
    taskDescription : string,
    endDateTask? : string,
    status : string,
    razlogOdbijanja? : string,
    sparePartInfo: SparePartInfo[]
    workerInfo : UserInfo,
    serviceId : string,
}

export interface CreateTaskDto
{
    taskDescription : string,
}

export interface RejectTaskDto
{
    razlogOdbijanja? : string,
}