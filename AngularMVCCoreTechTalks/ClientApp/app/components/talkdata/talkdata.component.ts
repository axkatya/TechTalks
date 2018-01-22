import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { Talk } from '../talk';
import { TalkFilterViewModel } from '../talkFilterViewModel';
import { TalkFilterViewModelService } from '../talkFilterViewModel.service';
import { TalkService } from '../talk.service';
import { Router } from '@angular/router';
import { SpeakerButtonRenderComponent } from '../button-render/speaker.button-render.component';

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
        format: 'dd-MM-yyyy'
    }

    private dateToSettings = {
        autoclose: true,
        todayBtn: 'linked',
        todayHighlight: true,
        assumeNearbyYear: true,
        format: 'dd-MM-yyyy'
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
                type: 'custom',
                renderComponent: SpeakerButtonRenderComponent
            },
            disciplineName: {
                title: 'Discipline'
            },
            location: {
                title: 'Location'
            }
        },
        delete: {
            confirmDelete: true
        },
        actions: {
            add: false,
            edit: false,
            custom: [{
                name: 'edit',
                title: 'Edit'
            }
            ]
        }       
    };

    public talkFilterViewModelError: Boolean = false;

    constructor(
        private _router: Router,
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

    onAddNewTalk() {
        this._router.navigate(['/upsert-talk/']);
    }

    onCustom(event: any) {
        this._router.navigate(['/upsert-talk/' + event.data.talkId]);
    }

    deleteRecord(event: any) {
        var data: Talk = event.newData as Talk;

        this._talkService.deleteTalk(event.data.talkId).subscribe(
            result => {
                event.confirm.resolve(event.source.data);
            }
        );
    }
}






