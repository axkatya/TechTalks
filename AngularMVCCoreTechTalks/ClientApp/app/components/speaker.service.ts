import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Speaker } from './speaker';

@Injectable()
export class SpeakerService {

    private baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getSpeakerById(id: number) {
        return this.http.get(this.baseUrl + 'api/Speaker/GetSpeakerById/'+ id)
            .map((result => result.json() as Speaker));
    }
}
