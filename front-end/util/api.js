import {
    RESPONSE_STATUS
} from '/constants.js';

if (!window['AppApiCache']) {
    window['AppApiCache'] = {};
}

export const ApiHelper = {
    sendRequest: (api, fetchOption, withoutCache) => {
        return new Promise(async (resolve) => { // eslint-disable-line
            let result;
            const newOption = {
                ...fetchOption
            };
            delete newOption.headers;
            if (
                fetchOption.method === 'GET' &&
                window.AppApiCache[api] !== undefined &&
                window.AppApiCache[api][JSON.stringify(newOption)] !== undefined &&
                withoutCache !== true
            ) {
                result = window.AppApiCache[api][JSON.stringify(newOption)];
                resolve(result);
                return;
            }

            if (
                fetchOption.method !== 'GET'
            ) {
                // refactor 現在的做法是只要發生 http method 不是 get 就把整個 cache 清掉，未來的作法應該是 API Response 會對應到 client 端 DB
                window.AppApiCache = {};
            }
            let resp;
            try {
                resp = await ApiHelper.fetch(api, fetchOption, withoutCache);
            } catch (error) {
                result = {
                    status: RESPONSE_STATUS.FAILED,
                    data: {
                        errorMsg: error
                    }
                };
                resolve(result);
                return;
            }
            if (resp.status === 200) {
                const jsonData = await resp.json();
                result = {
                    status: RESPONSE_STATUS.OK,
                    httpStatus: resp.status,
                    data: jsonData
                };
                if (fetchOption.method === 'GET') {
                    window.AppApiCache[api] = window.AppApiCache[api] || {}; // eslint-disable-line
                    const newOption = {
                        ...fetchOption
                    };
                    delete newOption.headers;
                    window.AppApiCache[api][JSON.stringify(newOption)] = result; // eslint-disable-line
                }
            } else {
                const jsonData = await resp.json();
                result = {
                    status: ApiHelper.RESPONSE_STATUS.FAILED,
                    httpStatus: resp.status,
                    data: {
                        errorMsg: jsonData.data.message
                    }
                };
            }
            resolve(result);
        });
    },
    fetch: (url, option) => {
        if (option.cache) {
            console.warn('Cound not declate cache in option params');
        }
        const newOption = {
            ...option,
            headers: {
                ...option.headers,
                'Content-Type': 'application/json'
            }
        };
        return fetch(url, newOption);
    }
};