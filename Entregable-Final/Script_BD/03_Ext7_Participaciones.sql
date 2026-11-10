-- EXT4/EXT7 participation schema used by baseNueva Brandon.sql.
-- Run only when starting from 01_PublicaSA_CreacionBD.sql and the EXT4 tables
-- are not already present. The EXT4 database dump already contains these tables.

CREATE TABLE IF NOT EXISTS proveedor (
    id_proveedor INT NOT NULL AUTO_INCREMENT,
    cedula_juridica VARCHAR(15) NOT NULL,
    nombre VARCHAR(150) NOT NULL,
    nombre_representante VARCHAR(150) NOT NULL,
    telefono VARCHAR(30) NOT NULL,
    correo VARCHAR(150) NOT NULL,
    fecha_creacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id_proveedor),
    UNIQUE KEY uq_proveedor_cedula (cedula_juridica)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS participacion (
    id_participacion INT NOT NULL AUTO_INCREMENT,
    id_solicitud INT NOT NULL,
    id_proveedor INT NOT NULL,
    fecha_participacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    aprobada TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (id_participacion),
    UNIQUE KEY uq_participacion_solicitud_proveedor (id_solicitud, id_proveedor),
    KEY ix_participacion_proveedor (id_proveedor),
    CONSTRAINT fk_participacion_proveedor FOREIGN KEY (id_proveedor)
        REFERENCES proveedor (id_proveedor),
    CONSTRAINT fk_participacion_solicitud FOREIGN KEY (id_solicitud)
        REFERENCES solicitud (id_solicitud)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS participaciondesglose (
    id_participacion INT NOT NULL,
    mes INT NOT NULL,
    anio INT NOT NULL,
    horas DECIMAL(5,2) NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    iva DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    porcentaje_cobro DECIMAL(5,2) NOT NULL,
    observaciones VARCHAR(500) NOT NULL,
    PRIMARY KEY (id_participacion, mes, anio),
    CONSTRAINT fk_participaciondesglose_participacion FOREIGN KEY (id_participacion)
        REFERENCES participacion (id_participacion),
    CONSTRAINT chk_partdesglose_horas CHECK (horas > 0),
    CONSTRAINT chk_partdesglose_mes CHECK (mes BETWEEN 1 AND 12),
    CONSTRAINT chk_partdesglose_monto CHECK (monto >= 0),
    CONSTRAINT chk_partdesglose_porcentaje CHECK (porcentaje_cobro BETWEEN 0 AND 100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
