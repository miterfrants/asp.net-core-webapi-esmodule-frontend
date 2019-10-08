'use strict';
const http = require('https');
const httpProxy = require('http-proxy');
const proxy = httpProxy.createProxyServer({});
const staticAlias = require('node-static-alias');
const fs = require('fs');

var fileServer = new staticAlias.Server('./', {
    alias: [{
        match: /\/config.js$/,
        serve: process.env.NODE_ENV === 'prod' ? 'config.js' : 'config.dev.js'
    }, {
        match: /\/([^/]+\/)*$/,
        serve: 'index.html'
    }, {
        match: /\/([^/]+\/)*([^/]+)\.(js|css|png|woff2|woff|ttf|html|gif|svg|json|jpg)$/,
        serve: function (params) {
            return params.reqPath;
        },
    }]
});

const options = {
    key: fs.readFileSync('ssl/key.pem'),
    cert: fs.readFileSync('ssl/server.crt')
};

http.createServer(options, function (request, response) {
    request.addListener('end', function () {
        let regexp = new RegExp(/\/([a-z|A-Z|\-|_|0-9]+\/){0,}(\?.*)?$/, 'gi');
        if (process.env.SERVER_SIDE_RENDER === 'true' && regexp.test(request.url) || request.url === '/sitemap.xml') {
            return proxy.web(request, response, {
                // render server
                target: 'http://127.0.0.1:8080'
            });
        } else {
            // static front-end server
            fileServer.serve(request, response);
        }
    }).resume();
}).listen(443);
console.log('Sever Launch');