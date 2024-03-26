import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  constructor(private http:HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get('/Events/GetAll').pipe(
      map((response: any) => ({
        data: response.data,
        success: response.success,
      })
    ));
  }

  getById(id: string): Observable<any> {
    return this.http.get(`/Events/GetById/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

  create(model: any): Observable<any> {
    return this.http.post('/Events/Create', model).pipe(
      map((res: any) => {return res;})
    );
  }

  update(model: any): Observable<any> {
    return this.http.put('/Events/Update', model).pipe(
      map((res: any) => {return res;})
    );
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`/Events/Delete/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

}
