const session = {
    _storageKey: "session_data",
    get() {
        return sessionStorage.getItem(session._storageKey);
    },
    set(data) {
        sessionStorage.setItem(session._storageKey, JSON.stringify(data))
    }
}