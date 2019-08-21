import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataFormInput } from '../models/input-representations/data-form-input';

@Injectable({
  providedIn: 'root'
})
export class InputDataSenderService {

  private apiUrl = 'https://localhost:44361/api/generator/';

  constructor(private httpClient: HttpClient) { }

  send(data: DataFormInput) {
    const type = 'application/zip';
    const headers = new HttpHeaders();
    headers.append('Accept', type);
    const options = {
      headers: headers,
      responseType: 'blob' as 'json'
    }
    this.httpClient.post<Blob>(this.apiUrl, data, options).subscribe(
      data => {
        console.log(data);
        let fileUrl = window.URL.createObjectURL(data);
        window.open(fileUrl);
      },
      response => console.log("Post in error", response),
      () => console.log("Post request completed"));
  }
}
