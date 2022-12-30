class ProfileModal extends HTMLElement {
    constructor() {
        super();
        retry(session.get, res => (res?.username?.length > 0 ?? false), 0).then(sessionData => {
            const root = create_element("div", {
                style: {
                    padding: "5px",
                    display: ""
                }
            }, [
                create_element("h4", {innerText: sessionData.username, style: {margin: 0}}),
                create_element("p", {innerText: sessionData.role}),
                create_element("button", {innerText: "Log out", classList: ["do-logout"]})
            ]);
            this.innerHTML = root.innerHTML;
        });
    }
}

customElements.define('profile-modal', ProfileModal);
