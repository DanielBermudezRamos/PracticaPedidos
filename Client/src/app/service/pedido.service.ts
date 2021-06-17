import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pedido } from '../domain/pedido';
import { environment } from 'src/environments/environment';
import { AdminService } from './admin.service';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  token: string = `Bearer ${this.authService.getToken()}`;
  //headers: HttpHeaders;

  myURL = environment.myURL;//'https://localhost:44359/';
  myAPI = 'api/Pedidos';

  constructor(public httpClient: HttpClient, private authService: AdminService) {
    //this.myUrl = './assets/Pedidos_DATA.json';
    /*this.headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': this.token!
    });*/

   }

  public getAll(_headers: HttpHeaders): Observable<any> {
    return this.httpClient.get(`${this.myURL+this.myAPI}/`, {headers: _headers});
  }
  public getById(id: number, _headers: HttpHeaders): Observable<any>{
    return this.httpClient.get(`${this.myURL + this.myAPI}/${id}`, {headers: _headers});
  }
  public postPedido(item: Pedido, _headers: HttpHeaders): Observable<any> {
    return this.httpClient.post(this.myURL + this.myAPI, item, {headers: _headers})
  }

  public putPedido(id: number, item: Pedido, _headers: HttpHeaders): Observable<any> {
    return this.httpClient.put(this.myURL + this.myAPI, item, {headers: _headers});
  }

  public delPedido(id: number, _headers: HttpHeaders): Observable<any>{
    return this.httpClient.delete(this.myURL + this.myAPI + id, {headers: _headers});
  }
}
