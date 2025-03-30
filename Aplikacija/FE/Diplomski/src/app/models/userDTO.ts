export interface User {
    id: string,
    name: string,
    surname: string,
    username: string,
    password: string,
    role: string,
    numOfPurchases: number,
    salary: number,
    numOfTasks: number
}

export interface UserLoginDto{
    
    username: string,
    password: string,
}

export interface UserRegisterDto{
    name: string,
    surname: string,
    username: string,
    password: string,
}

export interface UserInfo
{
    id: string,
    name: string,
    surname: string,
    username: string,
    role: string,
}

export interface DisplayWorkerDto
{
    id: string,
    name: string,
    surname: string,
    username: string,
    password: string,
    role: string,
    salary?: number,
    numOfTasks: number,
}

export interface DisplayClientDto
{
    id: string,
    name: string,
    surname: string,
    username: string,
    password: string,
    role: string,
    numOfPurchases: number,
}

export interface DisplayDirectorDto
{
    id: string,
    name: string,
    surname: string,
    username: string,
    password: string,
    role: string,
    salary?: number,
}

export interface CreateWorkerDtO
{
   
    name: string,
    surname: string,
    username: string,
    password: string,
    salary: number,
}

export interface UpdateWorkerDto
{
    name: string,
    surname: string,
    username: string,
    password: string,
    salary?: number | null;
    role:string,

}

export interface UpdateClientDto
{
    name: string,
    surname: string,
    username: string,
    password: string,
    role:string,
}

export interface UpdateDirectorDto
{
    name: string,
    surname: string,
    username: string,
    password: string,
    salary?: number | null;
    role:string,
}