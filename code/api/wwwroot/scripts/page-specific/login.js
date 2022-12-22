const form = document.getElementById("login-form");

function show_error(title, subtitle) {
    const errorEl = form.querySelector(".error");
    errorEl.classList.remove("show");
    if (!title && !subtitle) {
        console.error("Parameter title and subtitle is empty");
        return;
    }
    if (!errorEl) {
        console.error("#" + form.id + " does not have an .error element")
        return;
    }
    errorEl.classList.add("show");
    errorEl.querySelector(".title").innerText = title ?? "";
    errorEl.querySelector(".subtitle").innerHTML = subtitle ?? "";
}

async function submit_login_form(event) {
    show_error();
    event.preventDefault();
    event.stopPropagation();
    const loginResponse = await api.account.login_async({
        username: document.querySelector("input[name=username]").value,
        password: document.querySelector("input[name=password]").value
    });
    if (!loginResponse.ok) {
        const errorObj = await json_or_default_async(loginResponse);
        if (errorObj) show_error(errorObj.title, errorObj.subtitle);
        else show_error(strings.anErrorOccured.v(), strings.tryAgainSoon.v());
        return;
    }
    location.href = "/home";
}

function init() {
    form.addEventListener("submit", submit_login_form)
}

document.addEventListener("DOMContentLoaded", init)