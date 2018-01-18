import { Component, Inject } from '@angular/core';
import { Http, URLSearchParams  } from '@angular/http';
import { LocalDataSource } from 'ng2-smart-table';

@Component({
    selector: 'talkdata',
    templateUrl: './talkdata.component.html'
})


export class TalkComponent {

    source: LocalDataSource;

    dateFromSettings = {
        bigBanner: true,
        timePicker: true,
        format: 'dd-MMM-yyyy hh:mm a',
        defaultOpen: true
    }

    dateToSettings = {
        bigBanner: true,
        timePicker: true,
        format: 'dd-MMM-yyyy hh:mm a',
        defaultOpen: true
    }

    talkSettings = {
        columns: {
            talkDate: {
                title: 'Date'
            },
            topic: {
                title: 'Topic'
            },
            additionalDetail: {
                title: 'Additional Details'
            },
            speakerName: {
                title: 'Speaker Name'
            },
            disciplineName: {
                title: 'Discipline'
            },
            location: {
                title: 'Location'
            }
        }
    };

    possibleRowsPerPage: number[];
    rowsPerPage: number;

    public talkFilterViewModel: TalkFilterViewModel;

    private baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;

        this.possibleRowsPerPage = [20, 50];
        this.rowsPerPage = 20;

        http.get(baseUrl + 'api/Talk/GetFilters').subscribe(result => {
            this.talkFilterViewModel = result.json() as TalkFilterViewModel;
            this.talkFilterViewModel.dateFrom = new Date();
            this.talkFilterViewModel.disciplineName = '';
            this.talkFilterViewModel.locationName = '';
            this.talkFilterViewModel.speakerName = '';

            this.getFilteredTalks();
        });        
    }

    onSelectRowsPerPage(rowsPerPage: number) {
        this.rowsPerPage = rowsPerPage;
        this.source.setPaging(1, this.rowsPerPage, true);
        this.source.refresh();
    }

    onSelectDisciplineFilter(disciplineNameFilter: string) {
        this.talkFilterViewModel.disciplineName = disciplineNameFilter;
    }

    onSelectLocationFilter(locationNameFilter: string) {
        this.talkFilterViewModel.locationName = locationNameFilter;
    }

    onDateFromSelect(dateFrom: Date) {
        this.talkFilterViewModel.dateFrom = dateFrom;
    }

    onDateToSelect(dateTo: Date) {
        this.talkFilterViewModel.dateTo = dateTo;
    }

    onSearch() {
        this.getFilteredTalks();
       

        //var dataType = 'application/json; charset=utf-8';
        //var result = http.post(baseUrl + 'api/Talk/GetFilteredTalks', JSON.stringify(this.talkFilter.disciplineName));
        //this.source = new ServerDataSource(http, { endPoint: this.baseUrl + 'api/Talk/GetFilteredTalks?disciplineName=' + this.talkFilter.disciplineName });
        //$.ajax({
        //    type: 'GET',
        //    url: baseUrl + 'api/Talk/GetFilteredTalks?disciplineName=' + this.talkFilter.disciplineName,
        //    dataType: 'json',
        //    contentType: dataType,
        //    success: function (result) {
        //        console.log('Data received: ');
        //        console.log(result);
        //    }
        //});
    }

    getFilteredTalks()
    {
        let params: URLSearchParams = new URLSearchParams();
        params.set('disciplineName', this.talkFilterViewModel.disciplineName);
        params.set('locationName', this.talkFilterViewModel.locationName);
        params.set('speakerName', this.talkFilterViewModel.speakerName);
        params.set('topic', this.talkFilterViewModel.topic);
        params.set('dateFrom', this.talkFilterViewModel.dateFrom != null ?
            this.talkFilterViewModel.dateFrom.toString() :
            '');
        params.set('dateTo', this.talkFilterViewModel.dateTo != null ?
            this.talkFilterViewModel.dateTo.toString() :
            '');

        this.source = new LocalDataSource(); 
        var talks: Talk[];

        this.http.get(this.baseUrl + 'api/Talk/GetFilteredTalks', { search: params }).subscribe(result => {
            talks = result.json() as Talk[];

            this.source.setPaging(1, this.rowsPerPage, true);
            this.source.load(talks);
            this.source.refresh();
        }); 
         
    }
}

export interface Talk {
    talkDate: Date,
    topic: string,
    additionalDetail: string,
    speakerName: string,
    disciplineName: string,
    location: string
}

export class TalkFilterViewModel {
    public disciplineList: string[];
    public locationList: string[];
    public speakerName: string;
    public dateFrom: Date;
    public dateTo: Date;
    public disciplineName: string;
    public locationName: string;
    public topic: string;
}




