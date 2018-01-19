import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { TalkFilterViewModel } from './talkFilterViewModel';

@Injectable()
export class TalkFilterViewModelService {

    private baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getTalkFilterViewModel() {
        return this.http.get(this.baseUrl + 'api/Talk/GetFilters').map((result => result.json() as TalkFilterViewModel));
    }
}
