import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producto } from '../domain/producto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PpService {

  myURL = environment.myURL;//'https://localhost:44359/';
  myAPI = 'api/Pedidos/';

  constructor(public httpClient: HttpClient) {}
  
}
