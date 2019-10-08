import {
    RouterController
} from '/route/router-controller.js';

import {
    UsersDataService
} from '/dataservices/users.dataservice.js';

export class TestController extends RouterController {
    async init(args, context) {
        this.context = context;
        this.bindEvent();
    }

    async enter(args) {
        super.enter(args);
    }

    async render() {
        let resp = await UsersDataService.get({
            limit: 2,
            page: 1
        });
        super.render();
    }

    bindEvent() {}

}