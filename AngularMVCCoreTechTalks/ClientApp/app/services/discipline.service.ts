import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Discipline } from '../models/discipline';

@Injectable()
export class DisciplineService {

    private baseUrl: string;
    private headers: Headers;
    private options: RequestOptions;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.options = new RequestOptions({ headers: this.headers });
    }

    createDiscipline(discipline: Discipline) {
        return this.http.post(this.baseUrl + 'api/Discipline/CreateDiscipline', discipline, this.options)
            .map(result => result.json() as Discipline);
    }
}
