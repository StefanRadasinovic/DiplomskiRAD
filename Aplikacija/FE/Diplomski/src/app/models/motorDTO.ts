export interface Motor
{
    Id : string,
    Name : string,
    Slika? : string
    Kilometraza : number,
    YearOfProduction : number,
    MotorcycleState: string,
    Amount : number,
    MotorcycleType : string,
}

export interface MotorInfo
{
    id : string,
    name : string,
    slika? : string
    yearOfProduction : number,
    motorcycleType : string,
}

export interface PaginatedMotorProps { //Za Paginaciju
    data: Array<MotorInfo>;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalRecord: number;
}


export interface CreateMotorDto
{
    id : string,
    name : string,
    slika? : string
    kilometraza : number,
    yearOfProduction : number,
    motorcycleState: string,
    amount : number,
    motorcycleType : string,

}

export interface UpdateMotorDto 
{
    name : string,
    slika? : string
    kilometraza : number,
    yearOfProduction : number,
    motorcycleState: string,
    amount : number,
    motorcycleType : string,

}