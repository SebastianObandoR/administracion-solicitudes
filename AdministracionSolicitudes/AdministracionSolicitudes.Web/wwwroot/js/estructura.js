document.addEventListener("DOMContentLoaded", () => {
    const espacioTrabajo = document.getElementById("espacioTrabajo");
    const botonMenu = document.getElementById("botonMenu");

    if (!espacioTrabajo || !botonMenu) {
        return;
    }

    botonMenu.addEventListener("click", () => {
        if (window.innerWidth <= 900) {
            espacioTrabajo.classList.toggle("menu-movil-abierto");
            return;
        }

        espacioTrabajo.classList.toggle("esta-colapsado");
    });

    window.addEventListener("resize", () => {
        if (window.innerWidth > 900) {
            espacioTrabajo.classList.remove("menu-movil-abierto");
        }
    });
});