import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

// Componentes (rutas)
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CrearPedidoComponent } from './components/pedidos/crear-pedido/crear-pedido.component';
import { ListaPedidoComponent } from './components/pedidos/lista-pedido/lista-pedido.component';

// Servicios
import {PedidoService} from './service/pedido.service';
import { CrearProductoComponent } from './components/producto/crear-producto/crear-producto.component';
import { ListaProductoComponent } from './components/producto/lista-producto/lista-producto.component';
import { LoginComponent } from './components/admin/login/login.component';
import { RegisterComponent } from './components/admin/register/register.component';
import { NavigatorComponent } from './components/navigator/navigator.component';
import { PaginatePipe } from './pipes/paginate.pipe';

@NgModule({
  declarations: [
    AppComponent,
    CrearPedidoComponent,
    ListaPedidoComponent,
    CrearProductoComponent,
    ListaProductoComponent,
    LoginComponent,
    RegisterComponent,
    NavigatorComponent,
    PaginatePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot() // ToastrModule added
  ],
  providers: [
    PedidoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
