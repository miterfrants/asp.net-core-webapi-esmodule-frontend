if (!window['AppStylesheet']) {
    window['AppStylesheet'] = [];
}
export const Swissknife = {
    appendStylesheetToHead: (elStylesheet) => {
        if (elStylesheet.href.indexOf('http') === -1) {
            elStylesheet.href = location.origin + elStylesheet.href;
        }
        if (window.AppStylesheet.indexOf(elStylesheet.href) === -1) {
            document.head.appendChild(elStylesheet);
            window.AppStylesheet.push(elStylesheet.href);
        }
    },
    getQueryString: (key) => {
        if (!location.search || location.search.substring(1).length === 0) {
            return '';
        }
        const queryStrings = location.search.substring(1).split('&');
        const result = queryStrings.find((qs) => {
            if (qs.indexOf(`${key}=`) === 0) {
                return true;
            } else {
                return false;
            }
        });
        if (result) {
            return decodeURIComponent(result.split('=')[1]);
        }
        return '';
    },
    copyText: (text) => {
        const tempElement = document.createElement('textarea');
        tempElement.value = text;
        tempElement.style.opacity = 0;
        tempElement.style.position = 'fixed';
        tempElement.style.top = 0;
        document.body.appendChild(tempElement);
        tempElement.select();
        document.execCommand('copy');
        document.body.removeChild(tempElement);
    }
};