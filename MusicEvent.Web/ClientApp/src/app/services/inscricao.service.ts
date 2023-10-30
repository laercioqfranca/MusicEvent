import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InscricaoService {

  constructor(private http:HttpClient) { }

  getAllById(id: string): Observable<any> {
    return this.http.get(`v1/Inscricao/GetAllById/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

  create(model: any): Observable<any> {
    return this.http.post('v1/Inscricao/Create', model).pipe(
      map((res: any) => {return res;})
    );
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`v1/Inscricao/Delete/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

}
