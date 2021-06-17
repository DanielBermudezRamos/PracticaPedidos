import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { from } from 'rxjs';
import { LoginComponent } from './components/admin/login/login.component';
import { RegisterComponent } from './components/admin/register/register.component';
import { CrearPedidoComponent } from './components/pedidos/crear-pedido/crear-pedido.component';
import { ListaPedidoComponent } from './components/pedidos/lista-pedido/lista-pedido.component';
import { CrearProductoComponent } from './components/producto/crear-producto/crear-producto.component';
import { ListaProductoComponent } from './components/producto/lista-producto/lista-producto.component';

const routes: Routes = [
  {path:'', component: ListaPedidoComponent},
  {path:'admin/login', component: LoginComponent},
  {path:'admin/register', component: RegisterComponent},

  {path: 'listapedido', component: ListaPedidoComponent },
  {path: 'crearpedido', component: CrearPedidoComponent },
  {path: 'editarpedido/:id', component: CrearPedidoComponent },

  {path: 'listaproducto', component: ListaProductoComponent },
  {path: 'crearproducto', component: CrearProductoComponent },
  {path: 'editarproducto/:id', component: CrearProductoComponent },

  { path: '**', redirectTo: 'admin/login', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
