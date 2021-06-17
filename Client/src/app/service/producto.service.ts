import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producto } from '../domain/producto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  myURL = environment.myURL;//'https://localhost:44359/';
  myAPI = 'api/Producto/';

  constructor(public httpClient: HttpClient) {
    //this.myUrl = './assets/Pedidos_DATA.json';
   }

  public getAll(): Observable<any> {
    return this.httpClient.get(this.myURL + this.myAPI);
  }
  public getById(id: number): Observable<any>{
    return this.httpClient.get(this.myURL + this.myAPI + id);
  }
  public post(item: Producto): Observable<any> {
    return this.httpClient.post(this.myURL + this.myAPI, item)
  }

  public put(id: number, item: Producto): Observable<any> {
    return this.httpClient.put(this.myURL + this.myAPI, item);
  }

  public del(id: number): Observable<any>{
    return this.httpClient.delete(this.myURL + this.myAPI + id);
  }
}
