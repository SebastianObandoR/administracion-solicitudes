(function () {
    'use strict';

    function escapar(texto) {
        const div = document.createElement("div");
        div.textContent = texto == null ? "" : String(texto);
        return div.innerHTML;
    }

    function mostrarFallback(mensajes) {
        // Previene que se muestre un alert vacío 
        if (mensajes.length > 0) {
            alert("Errores:\n\n- " + mensajes.join("\n- "));
        }
    }

    function mostrarErrores() {
        const erroresCampo = Array.isArray(window.__erroresValidacion)
            ? window.__erroresValidacion
            : [];
        const erroresGenerales = Array.isArray(window.__erroresGenerales)
            ? window.__erroresGenerales
            : [];

        if (erroresCampo.length === 0 && erroresGenerales.length === 0) {
            return;
        }

        if (typeof Swal === "undefined") {
            mostrarFallback(erroresCampo.concat(erroresGenerales));
            return;
        }

        if (erroresCampo.length > 0) {
            const items = erroresCampo
                .map(function (e) { return "<li>" + escapar(e) + "</li>"; })
                .join("");

            Swal.fire({
                title: "Campos obligatorios",
                html:
                    '<p style="text-align:left; margin-bottom:8px;">Debe completar los siguientes campos:</p>' +
                    '<ul style="text-align:left; margin:0; padding-left:20px;">' + items + "</ul>",
                icon: "warning",
                confirmButtonText: "Aceptar",
                confirmButtonColor: "#2d7a78"
            });
            return;
        }

        if (erroresGenerales.length > 0) {
            Swal.fire({
                text: erroresGenerales.join(" "),
                icon: "error",
                confirmButtonText: "Aceptar",
                confirmButtonColor: "#2d7a78"
            });
        }
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", mostrarErrores);
    } else {
        mostrarErrores();
    }
})();