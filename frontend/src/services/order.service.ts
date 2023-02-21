import axios from "axios";
import authHeader from "./auth-header";
import { API_URL } from "./host";

import { IOrderRequestDto } from "../types/dto/request/IOrderRequestDto";

class OrderService {
  async createOrder(userId: number, itemsIds: number[]) {
    console.log(authHeader());
    return axios
      .post(API_URL + "orders", {userId, itemsIds} as IOrderRequestDto, { headers: authHeader() })
      .then(response => {
        return response.data;
      });
  }
}

export default new OrderService();
