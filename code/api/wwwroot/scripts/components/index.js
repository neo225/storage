function load_components() {
    document.body.appendChild(script_tag("/scripts/components/profile-modal.js", 1));
}

function get_script_version_hash(key, salt) {
    const currentHash = localStorage.getItem(key) ?? "";
    const hash = get_md5_hash(key + salt);
    if (currentHash === hash) return currentHash;
    localStorage.setItem(key, hash);
    return hash;
}

function script_tag(scriptSrc, version) {
    return create_element("script", {
        src: scriptSrc + "?v=" + get_script_version_hash(scriptSrc, version)
    })
}

document.addEventListener("DOMContentLoaded", load_components)