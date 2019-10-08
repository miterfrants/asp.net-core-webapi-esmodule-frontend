import {
    ApiHelper
} from '/util/api.js';

import {
    API
} from '/constants.js';

import {
    APP_CONFIG
} from '/config.js';

export const UsersDataService = {
    get: async (data) => {
        let api = APP_CONFIG.API_ENDPOINT + API.AUTHORIZED.USERS;
        api = api.bind(data);
        return ApiHelper.sendRequest(api, {
            method: 'GET'
        });
    }
};