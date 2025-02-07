export interface Producer
{
    id : string,
    name : string,
    description? : string
}

export interface ProducerInfo
{
    name : string,
    description? : string
}

export interface CreateProducerDto
{
    name : string,
    description? : string
}


export interface UpdateProducerDto
{
    name : string,
    description? : string
}

