document.addEventListener("DOMContentLoaded", () => {

    const loginModal = document.getElementById("loginModal");
    const registerModal = document.getElementById("registerModal");

    const openLoginBtns = [
        document.getElementById("openLogin")
    ];

    const openRegisterBtns = [
        document.getElementById("openRegister"),
        document.getElementById("openRegister2")
    ];

    const closeLogin = document.getElementById("closeLogin");
    const closeRegister = document.getElementById("closeRegister");

    function openModal(modal) {
        modal.classList.remove("opacity-0", "pointer-events-none");
        modal.querySelector(".modal-box").classList.remove("scale-95");
        modal.querySelector(".modal-box").classList.add("scale-100");
    }

    function closeModal(modal) {
        modal.classList.add("opacity-0", "pointer-events-none");
        modal.querySelector(".modal-box").classList.add("scale-95");
        modal.querySelector(".modal-box").classList.remove("scale-100");
    }

    // BOTONES
    openLoginBtns.forEach(btn => {
        btn?.addEventListener("click", () => {
            closeModal(registerModal);
            openModal(loginModal);
        });
    });

    openRegisterBtns.forEach(btn => {
        btn?.addEventListener("click", () => {
            closeModal(loginModal);
            openModal(registerModal);
        });
    });

    closeLogin?.addEventListener("click", () => closeModal(loginModal));
    closeRegister?.addEventListener("click", () => closeModal(registerModal));

    // CLIC FUERA
    window.addEventListener("click", (e) => {
        if (e.target === loginModal) closeModal(loginModal);
        if (e.target === registerModal) closeModal(registerModal);
    });

    // AUTO OPEN POR ERROR
    if (document.getElementById("openLoginModalAuto")) {
        openModal(loginModal);
    }

    if (document.getElementById("openRegisterModalAuto")) {
        openModal(registerModal);
    }

    // TOGGLE PASSWORD (UN SOLO SISTEMA)
    document.querySelectorAll(".toggle-password").forEach(button => {
        button.addEventListener("click", () => {
            const input = button.previousElementSibling;
            if (!input || input.tagName !== "INPUT") return;

            if (input.type === "password") {
                input.type = "text";
                button.textContent = "🙉";
            } else {
                input.type = "password";
                button.textContent = "🙈";
            }
        });
    });

    // ================= OCULTAR ERRORES =================
    const loginError = document.getElementById("loginError");
    if (loginError) {
        setTimeout(() => {
            loginError.classList.add("opacity-0");
            setTimeout(() => loginError.remove(), 300);
        }, 3700);
    }

    const registerError = document.getElementById("registerError");
    if (registerError) {
        setTimeout(() => {
            registerError.classList.add("opacity-0");
            setTimeout(() => registerError.remove(), 300);
        }, 3700);
    }

    // ================= VALIDACIÓN REGISTRO =================
    const registerForm = document.querySelector("#registerModal form");
    const pass = document.getElementById("regPassword");
    const confirm = document.getElementById("regConfirmPassword");
    const passwordError = document.getElementById("regPasswordError");

    function updateRule(id, ok, text) {
        const el = document.getElementById(id);
        if (!el) return;

        el.textContent = (ok ? "✔️ " : "❌ ") + text;
        el.classList.remove("text-gray-400");
        el.classList.toggle("text-green-600", ok);
        el.classList.toggle("text-red-600", !ok);
    }

    if (pass && confirm) {
        pass.addEventListener("input", () => {
            updateRule("rule-length", pass.value.length >= 6, "Mínimo 6 caracteres");
            updateRule("rule-match", pass.value === confirm.value, "Las contraseñas coinciden");
        });

        confirm.addEventListener("input", () => {
            updateRule("rule-match", pass.value === confirm.value, "Las contraseñas coinciden");
        });
    }

    if (registerForm) {
        registerForm.addEventListener("submit", (e) => {
            if (pass.value !== confirm.value) {
                e.preventDefault(); // 🚫 NO ENVÍA
                passwordError.classList.remove("hidden");
                confirm.focus();
            } else {
                passwordError.classList.add("hidden");
            }
        });
    }
});
