import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { Talk } from '../../models/talk';
import { TalkFilterViewModel } from '../../models/talkFilterViewModel';
import { SelectedTalkFilterViewModel } from '../../models/selectedTalkFilterViewModel';
import { TalkFilterViewModelService } from '../../services/talkFilterViewModel.service';
import { TalkService } from '../../services/talk.service';
import { Router } from '@angular/router';
import { SpeakerButtonRenderComponent } from '../button-render/speaker.button-render.component';

@Component({
    selector: 'talksdata',
    templateUrl: './talksdata.component.html',
    styleUrls: ['../app/app.component.less']
})

export class TalksDataComponent {

    private talkFilterViewModel: TalkFilterViewModel;
    private selectedTalkFilterViewModel: SelectedTalkFilterViewModel;

    private possibleRowsPerPage: number[];

    private rowsPerPage: number;

    private source: LocalDataSource;
    private disciplineFilterItems: any[];
    private locationFilterItems: any[];

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
                name: 'lnkEditTalk',
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

        this.selectedTalkFilterViewModel = new SelectedTalkFilterViewModel();
        this.selectedTalkFilterViewModel.dateFrom = new Date();
        this.selectedTalkFilterViewModel.disciplineName = '';
        this.selectedTalkFilterViewModel.location = '';
        this.selectedTalkFilterViewModel.speakerName = '';
        this.selectedTalkFilterViewModel.topic = '';
    }

    ngOnInit() {
        this._talkFilterViewModelService.getPossibleLists().subscribe(result => {

            this.talkFilterViewModel = result as TalkFilterViewModel;

            this.locationFilterItems = new Array();
            for (let i = 0; i < this.talkFilterViewModel.locationList.length; i++) {
                this.locationFilterItems[i] = {
                    id: this.talkFilterViewModel.locationList[i],
                    text: this.talkFilterViewModel.locationList[i]
                };
            }

            this.disciplineFilterItems = new Array();
            for (let i = 0; i < this.talkFilterViewModel.disciplineList.length; i++) {
                this.disciplineFilterItems[i] = {
                    id: this.talkFilterViewModel.disciplineList[i].disciplineId,
                    text: this.talkFilterViewModel.disciplineList[i].disciplineName
                };
            }
        });

        this.getFilteredTalks(this.selectedTalkFilterViewModel);
    }

    onSelectRowsPerPage(rowsPerPage: number) {
        this.rowsPerPage = rowsPerPage;
        this.source.setPaging(1, this.rowsPerPage, true);
        this.source.refresh();
    }

    onSelectDisciplineFilter(disciplineFilter: any) {
        this.selectedTalkFilterViewModel.disciplineName = disciplineFilter.text;
    }

    onSelectLocationFilter(locationFilter: any) {
        this.selectedTalkFilterViewModel.location = locationFilter.text;
    }

    onDateFromSelect(dateFrom: Date) {
        this.selectedTalkFilterViewModel.dateFrom = dateFrom;
    }

    onDateToSelect(dateTo: Date) {
        this.selectedTalkFilterViewModel.dateTo = dateTo;
    }

    onSearch() {
        this.getFilteredTalks(this.selectedTalkFilterViewModel);
    }

    getFilteredTalks(selectedTalkFilterViewModel: SelectedTalkFilterViewModel) {
        this.source = new LocalDataSource();

        this._talkService.getFilteredTalks(selectedTalkFilterViewModel).subscribe(
            result => {
                this.source.setPaging(1, this.rowsPerPage, true);
                this.source.load(result);
                this.source.refresh();
            });
    }

    onAddNewTalk() {
        this._router.navigate(['/upsert-talk/']);
    }

    onCustomEditTalk(event: any) {
        this._router.navigate(['/upsert-talk/' + event.data.talkId]);
    }

    onDeleteTalk(event: any) {
        var data: Talk = event.newData as Talk;

        this._talkService.deleteTalk(event.data.talkId).subscribe(
            result => {
                event.confirm.resolve(event.source.data);
            }
        );
    }
}






