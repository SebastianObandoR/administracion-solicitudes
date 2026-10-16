-- CREATE DATABASE IF NOT EXISTS PublicaSA
--    CHARACTER SET utf8mb4
--    COLLATE utf8mb4_unicode_ci;

-- USE PublicaSA;

-- ===============
-- Tabla: usuarios 
-- ===============
CREATE TABLE usuarios
(
    id_usuario INT NOT NULL AUTO_INCREMENT,
    nombre_usuario VARCHAR(100) NOT NULL,
    nombre_completo VARCHAR(200) NOT NULL,
    correo VARCHAR(200) NOT NULL,
    contrasena_cifrada TEXT NOT NULL,
    nonce_contrasena VARCHAR(100) NOT NULL,
    tag_contrasena VARCHAR(100) NOT NULL,
    estado INT NOT NULL,
    intentos_fallidos INT NOT NULL DEFAULT 0,
    fecha_creacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_modificacion DATETIME NULL,
    CONSTRAINT pk_usuarios PRIMARY KEY (id_usuario),
    CONSTRAINT uq_usuarios_nombre_usuario UNIQUE (nombre_usuario),
    CONSTRAINT uq_usuarios_correo UNIQUE (correo)
);

-- ======================
-- Tabla: EstadoSolicitud
-- ======================
CREATE TABLE EstadoSolicitud (
    id_estado INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
);

-- ====================
-- Tabla: Representante
-- ====================
CREATE TABLE Representante (
    id_representante INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    correo VARCHAR(150) NOT NULL UNIQUE
);

-- ================
-- Tabla: Solicitud
-- ================
CREATE TABLE Solicitud (
    id_solicitud INT AUTO_INCREMENT PRIMARY KEY,
    consecutivo_oficio VARCHAR(50) NOT NULL UNIQUE,
    titulo VARCHAR(300) NOT NULL,
    descripcion VARCHAR(1000) NULL,
    observaciones VARCHAR(1000) NULL,
    documento_inicio VARCHAR(500) NULL,
    documento_respuesta VARCHAR(500) NULL,
    id_representante INT NOT NULL,
    id_estado INT NOT NULL,
    fecha_ingreso DATE NOT NULL,
    fecha_respuesta DATE NULL,
    fecha_inicio DATE NULL,
    CONSTRAINT fk_solicitud_representante FOREIGN KEY (id_representante)
        REFERENCES Representante (id_representante) ON DELETE RESTRICT,
    CONSTRAINT fk_solicitud_estado FOREIGN KEY (id_estado)
        REFERENCES EstadoSolicitud (id_estado) ON DELETE RESTRICT,
    CONSTRAINT chk_solicitud_fecha_respuesta CHECK (fecha_respuesta IS NULL OR fecha_respuesta >= fecha_ingreso),
    CONSTRAINT chk_solicitud_fecha_inicio CHECK (fecha_inicio IS NULL OR fecha_inicio >= fecha_ingreso)
);

-- ========================
-- Tabla: DesgloseSolicitud
-- ========================
CREATE TABLE DesgloseSolicitud (
    id_solicitud INT NOT NULL,
    mes INT NOT NULL,
    anio INT NOT NULL,
    horas DECIMAL(5,2) NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    iva DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    porcentaje_cobro DECIMAL(5,2) NOT NULL,
    observaciones VARCHAR(500) NULL,
    PRIMARY KEY (id_solicitud, mes, anio),
    CONSTRAINT fk_desglose_solicitud FOREIGN KEY (id_solicitud)
        REFERENCES Solicitud (id_solicitud) ON DELETE RESTRICT,
    CONSTRAINT chk_desglose_mes CHECK (mes BETWEEN 1 AND 12),
    CONSTRAINT chk_desglose_horas CHECK (horas > 0),
    CONSTRAINT chk_desglose_monto CHECK (monto >= 0),
    CONSTRAINT chk_desglose_porcentaje_cobro CHECK (porcentaje_cobro BETWEEN 0 AND 100)
);

-- ============
-- Tabla: Tarea
-- ============
CREATE TABLE Tarea (
    id_tarea INT AUTO_INCREMENT PRIMARY KEY,
    id_solicitud INT NOT NULL,
    id_usuario INT NOT NULL,
    descripcion VARCHAR(500) NOT NULL,
    fecha DATE NOT NULL,
    horas_invertidas DECIMAL(5,2) NOT NULL,
    CONSTRAINT fk_tarea_solicitud FOREIGN KEY (id_solicitud)
        REFERENCES Solicitud (id_solicitud) ON DELETE RESTRICT,
    CONSTRAINT fk_tarea_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario) ON DELETE RESTRICT,
    CONSTRAINT chk_tarea_horas_invertidas CHECK (horas_invertidas > 0)
);

-- ===============================
-- Tabla: HistorialEstadoSolicitud
-- ===============================
CREATE TABLE HistorialEstadoSolicitud (
    id_historial INT AUTO_INCREMENT PRIMARY KEY,
    id_solicitud INT NOT NULL,
    id_estado_anterior INT NULL,
    id_estado_nuevo INT NOT NULL,
    id_usuario INT NULL,
    fecha_cambio DATETIME NOT NULL,
    CONSTRAINT fk_historial_solicitud FOREIGN KEY (id_solicitud)
        REFERENCES Solicitud (id_solicitud) ON DELETE RESTRICT,
    CONSTRAINT fk_historial_estado_anterior FOREIGN KEY (id_estado_anterior)
        REFERENCES EstadoSolicitud (id_estado) ON DELETE RESTRICT,
    CONSTRAINT fk_historial_estado_nuevo FOREIGN KEY (id_estado_nuevo)
        REFERENCES EstadoSolicitud (id_estado) ON DELETE RESTRICT,
    CONSTRAINT fk_historial_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario) ON DELETE RESTRICT
);

-- ===============
-- Tabla: Bitacora
-- ===============
CREATE TABLE Bitacora (
    id_bitacora INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NULL,
    modulo VARCHAR(50) NOT NULL,
    accion ENUM('CREATE', 'UPDATE', 'DELETE', 'CONSULT') NOT NULL,
    descripcion TEXT NOT NULL,
    fecha DATETIME NOT NULL,
    CONSTRAINT fk_bitacora_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario) ON DELETE RESTRICT
);

-- ===============
-- Tabla: ErrorLog
-- ===============
CREATE TABLE ErrorLog (
    id_error INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NULL,
    modulo VARCHAR(50) NULL,
    mensaje_error TEXT NOT NULL,
    fecha DATETIME NOT NULL,
    CONSTRAINT fk_errorlog_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario) ON DELETE RESTRICT
);

-- =================
-- Tabla: DiaFeriado
-- =================
CREATE TABLE DiaFeriado (
    id_feriado INT AUTO_INCREMENT PRIMARY KEY,
    fecha DATE NOT NULL UNIQUE,
    descripcion VARCHAR(100) NOT NULL
);
