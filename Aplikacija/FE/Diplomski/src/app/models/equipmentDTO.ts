export interface Equipment
{
    id : string,
    name : string,
    slika? : string,
    equipmentState : string,
    amount : number,
}

export interface EquipmentInfo
{
    id : string,
    name : string,
    slika? : string,
}

export interface PaginatedEquipmentProps { //Za Paginaciju
    data: Array<EquipmentInfo>;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalRecord: number;
}

export interface CreateEquipmentDto
{
    name : string,
    slika? : string,
    equipmentState : string,
    amount : number,
}

export interface UpdateEquipmentDto
{
    name : string,
    slika? : string,
    equipmentState : string,
    amount : number,
}


