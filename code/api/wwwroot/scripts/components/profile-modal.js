class ProfileModal extends HTMLElement {
    constructor() {
        super();
        const sessionData = session.get();
        const root = create_element("div", {
            style: {
                padding: "5px",
                display: ""
            }
        }, [
            create_element("h4", {innerText: sessionData.username, style: {margin:0}}),
            create_element("p", {innerText: sessionData.role})
        ]);
        this.innerHTML = root.innerHTML;
    }
}

customElements.define('profile-modal', ProfileModal);
