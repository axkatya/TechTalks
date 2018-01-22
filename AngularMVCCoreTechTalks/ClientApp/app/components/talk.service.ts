﻿import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Talk } from './talk';
import { TalkFilterViewModel } from './talkFilterViewModel';

@Injectable()
export class TalkService {

    private baseUrl: string;
     
    headers : Headers;
    options: RequestOptions;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.options = new RequestOptions({ headers: this.headers });
    }

    getFilteredTalks(talkFilterViewModel: TalkFilterViewModel) {
        return this.http.post(this.baseUrl + 'api/Talk/GetFilteredTalks', talkFilterViewModel)
            .map(result => result.json() as Talk[]);
    }

    upsert(talk: Talk) {
        if (talk.talkId > 0) {
            this.updateTalk(talk);
        }
        else {
            this.createTalk(talk);
        }
    }

    getTalkById(talkId: number) {
        return this.http.get(this.baseUrl + 'api/Talk/GetTalkById/' + talkId)
            .map(result => result.json() as Talk);
    }

    createTalk(talk: Talk) {
        return this.http.put(this.baseUrl + 'api/Talk/CreateTalk/', talk, this.options)
            .map(result => result as any);
    }

    updateTalk(talk: Talk) {
        return this.http.put(this.baseUrl + 'api/Talk/UpdateTalk/' + talk.talkId, talk, this.options)
            .map(result => result as any);
    }

    deleteTalk(talkId: number) {
        return this.http.delete(this.baseUrl + 'api/Talk/DeleteTalk/' + talkId, this.options)
            .map(result => result as any);
    }
}
