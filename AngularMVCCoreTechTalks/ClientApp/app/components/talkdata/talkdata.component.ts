import { Component, Inject, OnInit } from '@angular/core';
import { Http, URLSearchParams, Headers } from '@angular/http';
import { LocalDataSource } from 'ng2-smart-table';
import { Talk } from '../talk';
import { TalkFilterViewModel } from '../talkFilterViewModel';
import { TalkFilterViewModelService } from '../talkFilterViewModel.service';
import { TalkService } from '../talk.service';

@Component({
    selector: 'talkdata',
    templateUrl: './talkdata.component.html',
    styleUrls: ['./talkdata.component.css']
})

export class TalkComponent {

    private talkFilterViewModel: TalkFilterViewModel;

    private possibleRowsPerPage: number[];

    private rowsPerPage: number;

    private source: LocalDataSource;

    private dateFromSettings = {
        autoclose: true,
        todayBtn: 'linked',
        todayHighlight: true,
        assumeNearbyYear: true,
        format: 'dd-MMM-yyyy'
    }

    private dateToSettings = {
        autoclose: true,
        todayBtn: 'linked',
        todayHighlight: true,
        assumeNearbyYear: true,
        format: 'dd-MMM-yyyy'
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

    public talkFilterViewModelError: Boolean = false;

    constructor(
        private _talkFilterViewModelService: TalkFilterViewModelService,
        private _talkService: TalkService
    ) {
        this.possibleRowsPerPage = [20, 50];
        this.rowsPerPage = 20;
    }

    ngOnInit() {
        this.talkFilterViewModel = new TalkFilterViewModel();
        this.talkFilterViewModel.dateFrom = new Date();
        this.talkFilterViewModel.disciplineName = '';
        this.talkFilterViewModel.location = '';
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
        this.talkFilterViewModel.location = locationNameFilter;
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

        this._talkService.getFilteredTalks(talkFilterViewModel).subscribe(result => {
            this.source.setPaging(1, this.rowsPerPage, true);
            this.source.load(result);
            this.source.refresh();
        });
    }
}






