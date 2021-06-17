import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Producto } from 'src/app/domain/producto';
import { ProductoService } from 'src/app/service/producto.service';

@Component({
  selector: 'app-crear-producto',
  templateUrl: './crear-producto.component.html',
  styleUrls: ['./crear-producto.component.css']
})
export class CrearProductoComponent implements OnInit {
  public item!: Producto;
  id: number = 0;
  form!: FormGroup;
  accion: string = "";
  constructor(private formBuilder: FormBuilder,
              private toastr: ToastrService,
              public producto: ProductoService,
              private ruta: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      cliente: ['', Validators.required],
      descripcion: ['', Validators.required]
    });
  }

  public save() {
    const item: Producto = {
      id: 0,
      nombre: this.form.get('cliente')?.value,
      descripcion: this.form.get('descripcion')?.value,
      precio: this.form.get('precio')?.value
    }
    if(this.id == 0) {  // Agregar
      this.producto.post(item)
        .subscribe(() => {
          this.toastr.success('Producto Agregado','AGREGAR');
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
      this.producto.put(this.id, this.item)
        .subscribe(() => {
          this.form.reset();
          this.ruta.navigate(['/lista-producto']);
          this.toastr.info('El Producto fue editado exitosamente.', 'Editar Producto');
        }, (error: any) => {
          this.toastr.error('Ha ocurrido un error al tratar de Editar este Producto','Error en EDITAR');
          console.log(error);
        });
    }

  }
  editarProducto( producto: any) {
    console.log(producto);
    this.id = producto.id;
    this.accion = 'Editar';

    this.form.patchValue({
      nombre: producto.nombre,
      descripcion: producto.descripcion,
      precio: producto.precio
    });
  }
}
