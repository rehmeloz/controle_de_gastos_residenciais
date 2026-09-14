import axios from "axios";

// Definindo Url base da api
export const api = axios.create({
    baseURL: "http://localhost:7134/api"
});
