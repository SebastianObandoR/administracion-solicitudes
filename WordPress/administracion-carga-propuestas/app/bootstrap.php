<?php

declare(strict_types=1);

spl_autoload_register(static function (string $clase): void {
    $prefijo = 'PublicaSA\\CargaPropuestas\\';
    if (!str_starts_with($clase, $prefijo)) {
        return;
    }

    $nombre = substr($clase, strlen($prefijo));
    if (!preg_match('/^[A-Za-z][A-Za-z0-9\\\\]*$/D', $nombre)) {
        return;
    }

    $ruta = __DIR__ . '/' . str_replace('\\', '/', $nombre) . '.php';
    if (is_file($ruta)) {
        require_once $ruta;
    }
});
