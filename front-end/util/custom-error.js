export const CUSTOM_ERROR_TYPE = {
    ERROR: 'ERROR',
    SUCCESS: 'SUCCESS',
    WARN: 'WARN',
    INFO: 'INFO'
};

export class CustomError extends Error {
    constructor(type, reason) {
        super(reason);
        this.type = type;
        this.reason = reason;
    }
}