import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Talk } from './talk';
import { TalkFilterViewModel } from './talkFilterViewModel';

@Injectable()
export class TalkService {

    private baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getFilteredTalks(talkFilterViewModel: TalkFilterViewModel) {
        return this.http.post(this.baseUrl + 'api/Talk/GetFilteredTalks', talkFilterViewModel)
            .map(result => result.json() as Talk[]);
    }
}
