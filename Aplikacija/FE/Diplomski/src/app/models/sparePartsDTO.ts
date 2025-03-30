export interface SparePart
{
    id : string,
    name : string,
    amount : number,
    isPartUsed : string,
    razlogOdbijanja : string,
    taskServiceId : string,
}


export interface SparePartInfo
{
    id : string,
    name : string,
    amount?: number | null;  
    isPartUsed : string
}

export interface UsedSparePartDto
{
    name : string,
    amount : number,
}