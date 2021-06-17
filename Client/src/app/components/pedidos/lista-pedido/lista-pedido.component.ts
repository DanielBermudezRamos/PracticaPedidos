import { Component, OnDestroy, OnInit } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { Pedido } from 'src/app/domain/pedido';
import { PedidoService } from '../../../service/pedido.service'
import { Subscription } from 'rxjs';
import { AdminService } from 'src/app/service/admin.service';
import { HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-lista-pedido',
  templateUrl: './lista-pedido.component.html',
  styleUrls: ['./lista-pedido.component.css']
})
export class ListaPedidoComponent implements OnInit, OnDestroy {

  public pedido: Pedido[] = [];
  public subPedido!: Subscription;

  search: string = '';
  pagSize: number = 5;
  pagNumber: number = 1;
  pagSizeOptions = [5, 10, 25, 50];

  constructor(public servicio: PedidoService,
              private toastr: ToastrService,
              private authService: AdminService) { }

  ngOnInit(): void {
    this.getAll();
  }

  ngOnDestroy(): void {
    if(this.subPedido)
      this.subPedido.unsubscribe();
  }

  getAll(){
    this.subPedido = this.servicio.getAll(new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.getToken()}`
    }))
      .subscribe(data => {
        this.pedido = data;
      })
  }

  eliminarTarjeta(id: number) {
    this.servicio.delPedido(id, new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.getToken()}`
    }))
    .subscribe(() => {
      this.toastr.error("La Tarjeta fue eliminada con Éxito", 'Tarjeta Eliminada');
      this.getAll();
    },
      (error: any) => {
      console.log(error)
    })
  }
  // Paginación
  nextPag(): void {
    this.pagNumber += this.pagSize;
  }

  prevPag(): void{
    if(this.pagNumber > 0) {
      if(this.pagNumber - this.pagSize < 0)
        this.pagNumber = 0;
      else
        this.pagNumber -= this.pagSize;
    }
  }

  onSearch(search: string): void {
    this.pagNumber = 0;
    this.search = search;
  }

}
