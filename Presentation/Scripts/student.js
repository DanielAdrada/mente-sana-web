// ===== Tema claro / oscuro  =====
const themeToggle = document.getElementById('themeToggle');
const root = document.documentElement;

const savedTheme = localStorage.getItem('ms_theme');
if (savedTheme === 'dark') {
    root.classList.add('dark');
}

if (themeToggle) {
    themeToggle.addEventListener('click', () => {
        root.classList.toggle('dark');

        const theme = root.classList.contains('dark') ? 'dark' : 'light';
        localStorage.setItem('ms_theme', theme);
    });
}

// ===== Nombre de usuario / saludo =====
const userNameEls = [
    document.getElementById('userName'),
    document.getElementById('profileName')
].filter(el => el !== null);

const avatar = document.getElementById('avatar');

function applyName(name) {
    if (!userNameEls.length && !avatar) return;

    const finalName = name || 'Mente Sana';

    userNameEls.forEach(el => el.textContent = finalName);

    if (avatar) {
        const initials = finalName
            .split(' ')
            .map(x => x[0])
            .slice(0, 2)
            .join('')
            .toUpperCase();

        avatar.textContent = initials || 'MS';
    }
}

const savedName = localStorage.getItem('ms_name');
applyName(savedName);

const editNameBtn = document.getElementById('editName');
if (editNameBtn) {
    editNameBtn.addEventListener('click', () => {
        const v = prompt('¿Cómo te gustaría que te llamemos?', savedName || '');
        if (v && v.trim().length) {
            localStorage.setItem('ms_name', v.trim());
            applyName(v.trim());
        }
    });
}

// ===== Estado / mood =====
const statusEl = document.getElementById('status');
const streakEl = document.getElementById('streak');
const streakFill = document.getElementById('streakFill');

function incStreak() {
    if (!streakEl || !streakFill) return;

    const today = new Date().toDateString();
    const last = localStorage.getItem('ms_last_seen');
    let streak = parseInt(localStorage.getItem('ms_streak') || '0', 10);

    if (last !== today) {
        streak++;
        localStorage.setItem('ms_streak', String(streak));
        localStorage.setItem('ms_last_seen', today);
    }

    streakEl.textContent = streak;
    streakFill.style.width = Math.min(100, Math.max(10, streak * 10)) + '%';
}
incStreak();

document.querySelectorAll('[data-mood]').forEach(btn => {
    if (!statusEl) return;

    btn.addEventListener('click', () => {
        const mood = btn.getAttribute('data-mood');
        statusEl.textContent = mood + ' hoy';
        localStorage.setItem('ms_status', statusEl.textContent);
        award('Primer check-in');
    });
});

if (statusEl) {
    const savedStatus = localStorage.getItem('ms_status');
    if (savedStatus) statusEl.textContent = savedStatus;
}

// ===== Frases motivacionales =====
const quoteEl = document.getElementById('quote');
if (quoteEl) {
    const quotes = [
        'Respira profundo: estás haciendo lo mejor que puedes.',
        'Pequeños pasos también son progreso.',
        'Pedir ayuda es un acto de valentía.',
        'Tu mente es única y poderosa.',
        'Hoy es un buen día para empezar de nuevo.'
    ];

    let qi = Math.floor(Math.random() * quotes.length);
    quoteEl.textContent = quotes[qi];

    setInterval(() => {
        qi = (qi + 1) % quotes.length;
        quoteEl.textContent = quotes[qi];
    }, 8000);
}

// ===== Badges =====
const badgesEl = document.getElementById('badges');

function award(name) {
    if (!badgesEl) return;

    const set = JSON.parse(localStorage.getItem('ms_badges') || '[]');
    if (!set.includes(name)) {
        set.push(name);
        localStorage.setItem('ms_badges', JSON.stringify(set));
        renderBadges();
    }
}

function renderBadges() {
    if (!badgesEl) return;

    const set = JSON.parse(localStorage.getItem('ms_badges') || '[]');
    badgesEl.innerHTML = '';
    set.forEach(b => {
        const span = document.createElement('span');
        span.className = 'badge';
        span.textContent = '🏅 ' + b;
        badgesEl.appendChild(span);
    });
}
renderBadges();

// ===== Comunidad =====
const shareBtn = document.getElementById('shareBtn');
if (shareBtn) {
    shareBtn.addEventListener('click', () => {
        const text = prompt('Comparte algo positivo que te haya pasado hoy ✨');
        if (!text || !text.trim()) return;

        const community = document.querySelector('.community');
        if (!community) return;

        const box = document.createElement('div');
        box.className = 'post';
        box.innerHTML = `<strong>@Tú</strong><p>${text.trim()}</p>`;
        community.insertBefore(box, shareBtn);

        award('Primer post');
    });
}

// ===== Salir =====
const logoutBtn = document.getElementById('logoutBtn');
if (logoutBtn) {
    logoutBtn.addEventListener('click', () => {
        if (confirm('¿Deseas salir de MenteSana?')) {
            window.location.href = '/Login/Logout';
        }
    });
}
