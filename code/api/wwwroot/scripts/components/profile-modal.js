class ProfileModal extends HTMLElement {
	constructor() {
		super();
		const _session = session.get_sync();
		const root = create_element("div", {}, [
			create_element("h4", {innerText: _session.username, style: {margin: 0}}),
			create_element("p", {innerText: _session.role}),
			create_element("button", {innerText: "Log out", classList: ["do-logout"]}),
			create_element("pre", {innerText: Temporal.Now.instant().toString()})
		]);
		this.style.padding = "5px";
		this.innerHTML = root.innerHTML;
	}
}

customElements.define("profile-modal", ProfileModal);
