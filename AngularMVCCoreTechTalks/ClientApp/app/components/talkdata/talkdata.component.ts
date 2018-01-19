import { Component, Inject, OnInit } from '@angular/core';
import { Http, URLSearchParams, Headers } from '@angular/http';
import { LocalDataSource } from 'ng2-smart-table';
import { Talk } from '../talk';
import { TalkFilterViewModel } from '../talkFilterViewModel';
import { TalkFilterViewModelService } from '../talkFilterViewModel.service';

@Component({
    selector: 'talkdata',
    templateUrl: './talkdata.component.html',
    styleUrls: ['./talkdata.component.css']
})

export class TalkComponent {

    private talkFilterViewModel: TalkFilterViewModel;

    private baseUrl: string;

    private source: LocalDataSource;

    private dateFromSettings = {
        bigBanner: true,
        timePicker: true,
        format: 'dd-MMM-yyyy hh:mm a',
        defaultOpen: false
    }

    private dateToSettings = {
        bigBanner: true,
        timePicker: true,
        format: 'dd-MMM-yyyy hh:mm a',
        defaultOpen: false
    }

    private talkSettings = {
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
                title: 'Speaker Name',
                type: 'html'
            },
            disciplineName: {
                title: 'Discipline'
            },
            location: {
                title: 'Location'
            }
        }
    };

    private dateFormatOptions = {
        era: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long',
        timezone: 'UTC',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric'
    };

    private possibleRowsPerPage: number[];
    private rowsPerPage: number;

    public talkFilterViewModelError: Boolean = false;

    constructor(
        private http: Http,
        @Inject('BASE_URL') baseUrl: string,
        private _talkFilterViewModelService: TalkFilterViewModelService
    ) {
        this.baseUrl = baseUrl;
        this.possibleRowsPerPage = [20, 50];
        this.rowsPerPage = 20;
    }

    ngOnInit() {
        this.talkFilterViewModel = new TalkFilterViewModel();
        this.talkFilterViewModel.dateFrom = new Date();
        this.talkFilterViewModel.disciplineName = '';
        this.talkFilterViewModel.locationName = '';
        this.talkFilterViewModel.speakerName = '';

        this._talkFilterViewModelService.getTalkFilterViewModel().subscribe(result => {
            this.talkFilterViewModel.disciplineList = result.disciplineList;
            this.talkFilterViewModel.locationList = result.locationList;
        });

        this.getFilteredTalks(this.talkFilterViewModel);
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
        this.getFilteredTalks(this.talkFilterViewModel);
    }

    getFilteredTalks(talkFilterViewModel: TalkFilterViewModel) {
        this.source = new LocalDataSource();
        var talks: Talk[];
        this.http.post(this.baseUrl + 'api/Talk/GetFilteredTalks', talkFilterViewModel)
            .subscribe(result => {
                talks = result.json() as Talk[];
                this.source.setPaging(1, this.rowsPerPage, true);
                this.source.load(talks);
                this.source.refresh();
            });;
    }
}






