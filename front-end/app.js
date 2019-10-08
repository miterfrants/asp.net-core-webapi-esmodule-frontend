import {
    Route
} from '/route/route.js';

import {
    RESPONSE_STATUS,
    API
} from '/constants.js';

import {
    APP_CONFIG
} from '/config.js';

import {
    Api
} from '/util/api.js';

import {
    CustomError
} from '/util/custom-error.js';

export const APP = {
    run: (isUpdateDOMFirstRunRouting) => {
        window.addEventListener('error', (e) => {
            if (e.error && e.error instanceof CustomError) {
                // show error message;
                e.stopPropagation();
                e.preventDefault();
                return;
            }
        });

        window.addEventListener('unhandledrejection', function (e) {
            if (e.reason && e.reason instanceof CustomError) {
                // show error message;
                e.stopPropagation();
                e.preventDefault();
                return;
            }
        });
        APP.isUpdateDOMFirstRunRouting = !!isUpdateDOMFirstRunRouting;
        Api.init(APP_CONFIG.API_ENDPOINT, API, RESPONSE_STATUS);
        Route.init(APP);
    }
};