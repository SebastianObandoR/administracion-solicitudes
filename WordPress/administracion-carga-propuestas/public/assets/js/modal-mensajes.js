(function () {
    'use strict';

    function mostrarMensajes() {
        var avisos = Array.from(document.querySelectorAll('[data-publicasa-modal]'));

        function mostrarSiguiente() {
            var aviso = avisos.shift();
            if (!aviso) {
                return;
            }

            var mensajes = Array.from(aviso.children, function (elemento) {
                return elemento.textContent;
            });

            if (typeof window.Swal === 'undefined') {
                window.alert(mensajes.join('\n'));
                mostrarSiguiente();
                return;
            }

            var opciones = {
                icon: aviso.dataset.publicasaModal,
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#147d73'
            };

            if (mensajes.length === 1) {
                opciones.text = mensajes[0];
            } else {
                var lista = document.createElement('ul');
                lista.style.textAlign = 'left';
                lista.style.margin = '0';
                lista.style.paddingLeft = '20px';

                mensajes.forEach(function (mensaje) {
                    var elemento = document.createElement('li');
                    elemento.textContent = mensaje;
                    lista.appendChild(elemento);
                });

                opciones.html = lista;
            }

            window.Swal.fire(opciones).then(mostrarSiguiente);
        }

        mostrarSiguiente();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', mostrarMensajes);
    } else {
        mostrarMensajes();
    }
}());

