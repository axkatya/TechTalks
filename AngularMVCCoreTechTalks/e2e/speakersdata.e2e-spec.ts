import { browser, by, element } from 'protractor';

describe('Tech Talks App', () => {

    var addNewSpeakerButton = element(by.id('btnAddNewSpeaker'));

    beforeEach(() => {
        browser.get("/speakers-data");
    });

    it('should display Speakers header', () => {
        expect<any>(element(by.css("h2")).getText()).toEqual('Speakers');
    });

    //it('should add speaker', function () {
    //    addNewSpeakerButton.click();
    //    expect<any>(element(by.css("h2")).getText()).toEqual('Update Speaker');
    //});
});
