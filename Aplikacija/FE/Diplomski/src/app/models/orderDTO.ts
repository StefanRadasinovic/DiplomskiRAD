import { Equipment, EquipmentInfo } from "./equipmentDTO";
import { Motor, MotorInfo } from "./motorDTO";
import { User, UserInfo } from "./userDTO";

export interface Order
{
    id: string;
    orderAmount: number;
    totalPrice: string;
    orderStatus: string;
    equipments?: Equipment[];
    motorcycles?: Motor[];
    userId: string;
    users?: User;
}

export interface OrderInfo
{
    id: string;
    orderAmount: number;
    totalPrice: string;
    orderStatus: string;
    equipmentInfo?: EquipmentInfo[];
    motorcycleInfo?: MotorInfo[];
    userId: string;
    userInfo?: UserInfo;
}


export interface CreateOrderDto
{
    orderAmount: number;
}