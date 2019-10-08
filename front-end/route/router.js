import {
    MainController
} from '/controllers/main.controller.js';

import {
    TestController
} from '/controllers/test/test.controller.js';

export const Router = [{
    path: '/',
    controller: MainController,
    html: '/controllers/main.html',
    Router: [{
        path: 'test/',
        controller: TestController,
        html: '/controllers/test/test.html'
    }]
}];