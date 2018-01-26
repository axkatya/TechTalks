import { TestBed, inject } from '@angular/core/testing';

import { TalkFilterViewModelService } from './talkFilterViewModel.service';

describe('TalkFilterViewModelService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [TalkFilterViewModelService]
        });
    });

    it('should be created', inject([TalkFilterViewModelService], (service: TalkFilterViewModelService) => {
        expect(service).toBeTruthy();
    }));
});
