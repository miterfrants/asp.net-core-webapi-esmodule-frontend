import {
    Route
} from '/route/route.js';

import {
    CustomError
} from '/util/custom-error.js';

import {
    extendStringProtoType,
    extendHTMLElementProtoType
} from '/util/extended-prototype.js';
extendStringProtoType();
extendHTMLElementProtoType();

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
        Route.init(APP);
    }
};