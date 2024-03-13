import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { InscricaoModel } from '../models/InscricaoModel';

@Injectable({
  providedIn: 'root'
})
export class InscricaoService {

  constructor(private http:HttpClient) { }

  getAllById(id: string): Observable<any> {
    return this.http.get(`v1/Subscription/GetAllById/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

  create(model: InscricaoModel): Observable<any> {
    return this.http.post('v1/Subscription/CreateSubscription', model).pipe(
      map((res: any) => {return res;})
    );
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`v1/Subscription/DeleteSubscription/${id}`).pipe(
      map((res: any) => { return res; })
    );
  }

}
