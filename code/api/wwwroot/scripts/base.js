const session = {
    _storageKey: "session_data",
    get() {
        const stringVal = sessionStorage.getItem(session._storageKey);
        if (!stringVal) return undefined;
        return JSON.parse(stringVal).value;
    },
    set(data) {
        sessionStorage.setItem(session._storageKey, JSON.stringify({
            set_at: Temporal.Now.instant().epochSeconds,
            value: data
        }))
    },
    clear() {
        sessionStorage.removeItem(session._storageKey)
    }
}

const api = {
    async get_session_async() {
        const res = await fetch("/session", {
            credentials: "include"
        })
        if (!res.ok) act_if_401(res);
        return res;
    },
    account: {
        login_async(payload) {
            return fetch("/account/login", {
                method: "post",
                body: JSON.stringify(payload),
                headers: {
                    "Content-Type": "application/json;charset=utf-8"
                }
            })
        },
        async logout_async() {
            const res = await fetch("/account/logout", {
                credentials: "include"
            })
            if (!res.ok) act_if_401(res);
            return res;
        },
        async delete_account_async() {
            const res = await fetch("/account/delete", {
                method: "delete",
                credentials: "include"
            });
            if (!res.ok) act_if_401(res);
            return res;
        }
    }
}

function act_if_401(response) {
    if (response.status === 401) {
        session.clear();
        location.href = "/login?reason=401";
    }
}

async function logout_and_exit() {
    session.clear();
    await api.account.logout_async();
    location.href = "/login?reason=logged_out";
}

function json_or_default_async(response, defaultValue = undefined) {
    try {
        return response.json();
    } catch {
        return new Promise(resolve => resolve(defaultValue));
    }
}

const LOCALE_STORAGE_KEY = "locale";
const AVAILABLE_LOCALES = ["en", "nb"];

function set_locale_iso(val) {
    if (!AVAILABLE_LOCALES.includes(val)) {
        console.error(val + " is not a valid locale");
        return;
    }
    localStorage.setItem(LOCALE_STORAGE_KEY, val);
}

function current_locale_iso() {
    const localeStorageValue = localStorage.getItem(LOCALE_STORAGE_KEY) ?? undefined;
    if (localeStorageValue && AVAILABLE_LOCALES.includes(localeStorageValue)) return localeStorageValue;
    return "en";
}

const strings = {
    anErrorOccured: {
        nb: "En feil oppstod",
        en: "An error occured",
        v() {
            return strings.anErrorOccured[current_locale_iso()]
        }
    },
    tryAgainSoon: {
        nb: "Prøv igjen snart",
        en: "Try again soon",
        v() {
            return strings.tryAgainSoon[current_locale_iso()]
        }
    },
}