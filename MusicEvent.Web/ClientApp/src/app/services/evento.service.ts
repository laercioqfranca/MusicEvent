import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  constructor(private http:HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get('v1/Evento/GetAll').pipe(
      map((response: any) => ({
        data: response.data,
        success: response.success,
      })
    ));
  }

  getById(id: string): Observable<any> {
    return this.http.get(`v1/Evento/GetById/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

  create(model: any): Observable<any> {
    return this.http.post('v1/Evento/Create', model).pipe(
      map((res: any) => {return res;})
    );
  }

  update(model: any): Observable<any> {
    return this.http.put('v1/Evento/Update', model).pipe(
      map((res: any) => {return res;})
    );
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`v1/Evento/Delete/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

}
