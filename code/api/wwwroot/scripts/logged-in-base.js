async function load_session() {
    const sessionResponse = await api.get_session_async();
    if (!sessionResponse.ok) {
        show_error(strings.anErrorOccured.v(), strings.tryAgainSoon.v());
        console.error("/session responded unsuccessfully");
    }
    session.set(await json_or_default_async(sessionResponse, {}));
}

async function init() {
    await load_session();
    document.querySelectorAll(".do-logout").forEach(el => {
        el.addEventListener("click", () => logout_and_exit());
    })

}

document.addEventListener("DOMContentLoaded", init);
