const form = document.getElementById("login-form");

const error = {
	element: form.querySelector(".error"),
	show(title, subtitle) {
		if (!title && !subtitle) {
			return;
		}
		error.element.querySelector(".title").innerText = title ?? "";
		error.element.querySelector(".subtitle").innerHTML = subtitle ?? "";
		error.element.classList.add("show");
	},
	hide() {
		error.element.querySelector(".title").innerText = "";
		error.element.querySelector(".subtitle").innerHTML = "";
		error.element.classList.remove("show");
	}
};

async function submit_login_form(event) {
	error.hide();
	event.preventDefault();
	event.stopPropagation();
	const loginResponse = await api.account.login_async({
		username: document.querySelector("input[name=username]").value,
		password: document.querySelector("input[name=password]").value
	});
	if (!loginResponse.ok) {
		const errorObj = await json_or_default_async(loginResponse);
		if (errorObj) {
			error.show(errorObj.title, errorObj.subtitle);
		} else {
			error.show(t("general.anErrorOccured"), t("general.tryAgainSoon"));
		}
		return;
	}
	location.href = "/home";
}

document.addEventListener("DOMContentLoaded", () => {
	form.addEventListener("submit", submit_login_form);
});
