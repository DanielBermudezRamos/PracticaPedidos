import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pedido } from 'src/app/domain/pedido';
import { AdminService } from 'src/app/service/admin.service';
import { PedidoService } from 'src/app/service/pedido.service';

@Component({
  selector: 'app-crear-pedido',
  templateUrl: './crear-pedido.component.html',
  styleUrls: ['./crear-pedido.component.css']
})
export class CrearPedidoComponent implements OnInit {
  public item!: Pedido;
  id: number = 0;
  form!: FormGroup;
  accion: string = "";

  constructor(private formBuilder: FormBuilder,
              private toastr: ToastrService,
              public pedido: PedidoService,
              private ruta: Router,
              private authService: AdminService) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      cliente: ['', Validators.required],
      descripcion: ['', Validators.required]
    });
  }

  public save() {
    const item: Pedido = {
      id: 0,
      cliente: this.form.get('cliente')?.value,
      descripcion: this.form.get('descripcion')?.value
    }
    if(this.id == 0) {  // Agregar
      this.pedido.postPedido(item, new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.authService.getToken()}`
      })).subscribe(() => {
          this.toastr.success('Pedido Agregado','AGREGAR');
          this.form.reset();
        }, (resp: any) => {
          const errCliente = resp.error.errors.Cliente == undefined ? '' : resp.error.errors.Cliente[0];
          const errDesc = resp.error.errors.Descripcion == undefined ? '' : resp.error.errors.Descripcion[0];
          this.toastr.error('Ha ocurrido un error al tratar de Agregar. ' + errCliente + ' ' + errDesc,
          'Error en AGREGAR');
          console.log('ERROR:',errCliente, errDesc );

        });
    }
    else {  // Editar
      item.id = this.id;
      this.pedido.putPedido(this.id, this.item, new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.authService.getToken()}`
      })).subscribe(() => {
          this.form.reset();
          this.ruta.navigate(['/list-pedido']);
          this.toastr.info('La Tarjeta fue editada exitosamente.', 'Editar Tarjeta');
        }, error => {
          this.toastr.error('Ha ocurrido un error al tratar de Editar','Error en EDITAR');
          console.log(error);
        });
    }

  }
  editarTarjeta( tarjeta: any) {
    console.log(tarjeta);
    this.id = tarjeta.id;
    this.accion = 'Editar';

    this.form.patchValue({
      titular: tarjeta.titular,
      numero: tarjeta.numeroTarjeta,
      expiracion: tarjeta.fechaExpiracion,
      cvv: tarjeta.cvv,
    });
  }

}
