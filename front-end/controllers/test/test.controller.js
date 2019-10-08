import {
    RouterController
} from '/route/router-controller.js';

export class TestController extends RouterController {
    async init(args, context) {
        this.context = context;
        this.bindEvent();
    }

    async enter(args) {
        super.enter(args);
    }

    async render() {
        super.render();
    }

    bindEvent() {}

}