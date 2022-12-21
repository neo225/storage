const form = document.getElementById("login-form");


function get_login_payload() {
    return {
        username: document.querySelector("input[name=username]").value,
        password: document.querySelector("input[name=password]").value
    }
}

function show_error(title, subtitle) {
    if (!title || !subtitle) {
        console.error("parameter title or subtitle is empty");
        return;
    }
    const errorEl = form.querySelector(".error");
    if (!errorEl) {
        console.error("#" + form.id + " does not have an .error element")
        return;
    }
    errorEl.querySelector(".title").innerText = title;
    errorEl.querySelector(".subtitle").innerHTML = subtitle;
}

async function submit_login_form(event) {
    event.preventDefault();
    event.stopPropagation();
    const response = await fetch("/account/login", {
        method: "post",
        body: JSON.stringify(get_login_payload()),
        headers: {
            "Content-Type": "application/json;charset=utf-8"
        }
    })

    if (response.ok) {
        const sessionResponse = await fetch("/session");
        session.set(await sessionResponse.json());
        location.href = "/home";
        return;
    }
}

function init() {
    form.addEventListener("submit", submit_login_form)
}

document.addEventListener("DOMContentLoaded", init)