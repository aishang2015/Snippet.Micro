import axios from "axios";

class AxiosInstance{

    instance = axios.create({
        baseURL: 'https://localhost:20000/',
        timeout: 1000,
        headers: {'X-Custom-Header': 'foobar'}
    });


    



}

var axisoInstance = new AxiosInstance().instance;

export {axisoInstance};