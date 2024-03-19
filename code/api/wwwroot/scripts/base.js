const LOCALE_STORAGE_KEY = "locale";
const AVAILABLE_LOCALES = ["en", "nb"];

class WebStorageBackedCache {
	#key;
	#staleAfter;
	#dataRetriever;
	#storage;

	constructor(key, staleAfter, dataRetriever, storage = window.sessionStorage) {
		this.#key = key;
		this.#staleAfter = staleAfter;
		this.#dataRetriever = dataRetriever;
		this.#storage = storage;
		if (this.#shouldUpdate()) {
			(async () => {
				this.set(await this.#dataRetriever());
			})();
		}
	}

	#shouldUpdate(set_at) {
		if (!set_at) {
			return true;
		}
		if (this.#staleAfter === -1) {
			return false;
		}
		return (set_at + this.#staleAfter) < Temporal.Now.instant().epochSeconds;
	}

	async get() {
		const stringVal = this.#storage.getItem(this.#key);
		if (!stringVal) {
			return undefined;
		}
		const data = JSON.parse(stringVal);
		if (this.#shouldUpdate(data.set_at)) {
			const newData = await this.#dataRetriever();
			this.set(newData);
			return newData;
		}
		return data.value;
	}

	get_sync() {
		const stringVal = this.#storage.getItem(this.#key);
		if (!stringVal) {
			return undefined;
		}
		return JSON.parse(stringVal).value;
	}

	set(data) {
		this.#storage.setItem(this.#key, JSON.stringify({
			set_at: Temporal.Now.instant().epochSeconds,
			value: data
		}));
	}

	clear() {
		this.#storage.removeItem(session._storageKey);
	}
}

const api = {
	async _fetch(input, init = undefined) {
		const r = new Request(input);
		r.credentials = "include";
		r.headers.append("x-tz", Temporal.Now.timeZoneId());
		if (init?.method) {
			r.method = init.method;
		}
		if (init?.body) {
			r.body = init.body;
		}
		if (init?.headers) {
			for (const [key, value] of Object.entries(init.headers)) {
				r.headers.set(key, value);
			}
		}
		const response = await fetch(r);
		if (response.status === 401) {
			session.clear();
			if (!location.pathname.startsWith("/login")) {
				location.href = "/login?reason=401";
			}
		}
		return response;
	},
	get_session_async() {
		return api._fetch("/session");
	},
	get_translation_async(locale) {
		if (!locale) {
			return;
		}
		return fetch("/translations/" + locale + ".json");
	},
	account: {
		login_async(payload) {
			return fetch("/account/login", {
				method: "post",
				body: JSON.stringify(payload),
				headers: {
					"Content-Type": "application/json;charset=utf-8"
				}
			});
		},
		logout_async() {
			return api._fetch("/account/logout");
		},
		delete_account_async() {
			return api._fetch("/account/delete", {
				method: "delete",
			});
		}
	}
};

async function logout_and_exit() {
	session.clear();
	await api.account.logout_async();
	location.href = "/login?reason=logged_out";
}

function set_locale_iso(val) {
	if (!AVAILABLE_LOCALES.includes(val)) {
		console.error(val + " is not a valid locale");
		return;
	}
	localStorage.setItem(LOCALE_STORAGE_KEY, val);
}

function current_locale_iso() {
	const localeStorageValue = localStorage.getItem(LOCALE_STORAGE_KEY) ?? undefined;
	if (localeStorageValue && AVAILABLE_LOCALES.includes(localeStorageValue)) {
		return localeStorageValue;
	}
	return "en";
}

const session = new WebStorageBackedCache("session_data", 3600, async () => {
	const response = await api.get_session_async();
	if (!response.ok) {
		return {};
	}
	return json_or_default_async(response, {});
});

const translationData = new WebStorageBackedCache("translation_" + current_locale_iso(), -1, async () => {
	return json_or_default_async(await api.get_translation_async(current_locale_iso()), {});
});

function deepFind(obj, path, keySeparator = ".") {
	if (!obj) {
		return undefined;
	}
	if (obj[path]) {
		return obj[path];
	}
	const tokens = path.split(keySeparator);
	let current = obj;
	for (let i = 0; i < tokens.length;) {
		if (!current || typeof current !== "object") {
			return undefined;
		}
		let next;
		let nextPath = "";
		for (let j = i; j < tokens.length; ++j) {
			if (j !== i) {
				nextPath += keySeparator;
			}
			nextPath += tokens[j];
			next = current[nextPath];
			if (next !== undefined) {
				if (["string", "number", "boolean"].indexOf(typeof next) > -1 && j < tokens.length - 1) {
					continue;
				}
				i += j - i + 1;
				break;
			}
		}
		current = next;
	}
	return current;
}

function t(key, args = undefined) {
	if (!key) {
		return undefined;
	}
	const res = deepFind(translationData.get_sync(), key);
	if (!args || typeof args !== "object") {
		return res;
	}
	return res.replace(
		/{(\w+)}/g,
		(placeholderWithDelimiters, placeholderWithoutDelimiters) =>
			args.hasOwnProperty(placeholderWithoutDelimiters)
				? args[placeholderWithoutDelimiters]
				: placeholderWithDelimiters
	);
}
