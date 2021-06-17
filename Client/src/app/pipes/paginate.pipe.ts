import { Pipe, PipeTransform } from '@angular/core';
import { Pedido } from '../domain/pedido';

@Pipe({
  name: 'paginate'
})
export class PaginatePipe implements PipeTransform {

  transform(array: Pedido[], pagSize: number | string, pagNumber: number, search: string): any[] {
    if(!array.length) return []
    if(pagSize === 'all')
      return array;
    pagSize = pagSize || 5;
    pagNumber = pagNumber || 1;
    --pagNumber;
    if(search.length) {
      // @ts-ignore
      return array.filter( x => x.cliente.includes(search) || x.descripcion.includes(search)).slice(pagNumber * pagSize, (pagNumber + 1) * pagSize)
    }
    else {
      // @ts-ignore
    return array.slice(pagNumber * pagSize, (pagNumber + 1) * pagSize)

    }
  }
}
