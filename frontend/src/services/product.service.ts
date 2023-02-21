import axios from "axios";
import IProductDto from "../types/dto/IProductDto";
import qs from 'qs'

const API_URL = "https://localhost:7264/api/";

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
