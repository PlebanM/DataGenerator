import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OptionsService {

  private optionsUrl = "https://localhost:44361/api/generator/options";

  constructor(private http: HttpClient) { }

  options(): Observable<any> {
    return this.http.get(this.optionsUrl);
  }
}
