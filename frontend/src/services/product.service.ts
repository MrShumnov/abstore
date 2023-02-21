import axios from "axios";
import IProductDto from "../types/dto/IProductDto";
import qs from 'qs'
import { API_URL } from "./host";

class ProductService {
  async getProducts(name?: string, types?:string, cases?:string) {

    let params = "";

    if (name)
      params += `name=${name}`;
    if (types) 
      params += '&type=' + types.split(',').join('&type=');
    if (cases)
      params += '&case=' + cases.split(',').join('&case=');

    return axios.get(API_URL + `products?${params}`);
  }
}

export default new ProductService();
