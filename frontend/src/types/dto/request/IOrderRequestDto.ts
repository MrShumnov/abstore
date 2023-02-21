import IProductDto from "../IProductDto";

export interface IOrderRequestDto {
    userId: number,
    itemsIds: number[]
}