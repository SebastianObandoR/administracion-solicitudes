-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost:3306
-- Tiempo de generación: 10-11-2026 a las 01:09:26
-- Versión del servidor: 10.5.29-MariaDB
-- Versión de PHP: 8.4.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `tiusr21pl_publicasa`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `bitacora`
--

CREATE TABLE `bitacora` (
  `id_bitacora` int(11) NOT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `modulo` varchar(50) NOT NULL,
  `accion` enum('CREATE','UPDATE','DELETE','CONSULT') NOT NULL,
  `descripcion` text NOT NULL,
  `fecha` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `bitacora`
--

INSERT INTO `bitacora` (`id_bitacora`, `id_usuario`, `modulo`, `accion`, `descripcion`, `fecha`) VALUES
(1, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-09 21:19:35'),
(2, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-09 21:21:04'),
(3, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-09 21:21:15'),
(4, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-09 21:21:19'),
(5, 6, 'Representantes', 'CREATE', 'Se crea representante: {\"IdRepresentante\":1,\"Nombre\":\"julian\",\"Correo\":\"juliangmail.com\"}', '2026-10-09 21:22:22'),
(6, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-09 21:22:22'),
(7, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 19:44:10'),
(8, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 19:45:10'),
(9, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 19:48:54'),
(10, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 19:57:46'),
(11, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:03:10'),
(12, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":1,\"idSolicitud\":1,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Realizar calculo de notas finales\",\"fecha\":\"2026-10-10T00:00:00\",\"horasInvertidas\":2.01}', '2026-10-10 20:03:55'),
(13, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:03:55'),
(14, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:05:18'),
(15, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:05:26'),
(16, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:10:34'),
(17, 6, 'Tareas', 'UPDATE', 'ACTUALIZAR {\"anterior\":{\"idTarea\":1,\"idSolicitud\":1,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"Realizar calculo de notas finales\",\"fecha\":\"2026-10-10T00:00:00\",\"horasInvertidas\":2.01},\"actual\":{\"idTarea\":1,\"idSolicitud\":1,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Realizar c\\u00E1lculo de notas finales\",\"fecha\":\"2026-10-10T00:00:00\",\"horasInvertidas\":1.76}}', '2026-10-10 20:10:49'),
(18, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:10:49'),
(19, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:37:42'),
(20, 6, 'Tareas', 'DELETE', 'ELIMINAR {\"idTarea\":1,\"idSolicitud\":1,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"Realizar c\\u00E1lculo de notas finales\",\"fecha\":\"2026-10-10T00:00:00\",\"horasInvertidas\":1.76}', '2026-10-10 20:37:55'),
(21, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-10 20:37:55'),
(22, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 12:53:55'),
(23, 6, 'Representantes', 'CREATE', 'Se crea representante: {\"IdRepresentante\":3,\"Nombre\":\"jul\",\"Correo\":\"jul@123\"}', '2026-10-11 12:54:35'),
(24, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 12:54:35'),
(25, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 12:54:47'),
(26, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 12:54:51'),
(27, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 13:18:08'),
(28, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 15:10:11'),
(29, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 16:06:44'),
(30, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 16:18:29'),
(31, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 16:43:35'),
(32, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 16:53:08'),
(33, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:08:19'),
(34, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:08:22'),
(35, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:08:36'),
(36, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":2,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Darle un mantenimiento al sitio del Cuc por un error\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":2.01}', '2026-10-11 17:10:00'),
(37, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:10:00'),
(38, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:10:10'),
(39, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:10:19'),
(40, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":3,\"idSolicitud\":3,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Hacer el cierre de mes de contabilidad de todas las ventas generadas\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":1.76}', '2026-10-11 17:13:55'),
(41, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:13:56'),
(42, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:14:36'),
(43, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:21:30'),
(44, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:21:35'),
(45, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":4,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Revisar la base de datos\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":2.51}', '2026-10-11 17:30:26'),
(46, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:30:26'),
(47, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:30:54'),
(48, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:30:56'),
(49, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:31:04'),
(50, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 17:36:56'),
(51, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":5,\"idSolicitud\":3,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Ver que se perdio en el mes\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":1.01}', '2026-10-11 17:37:53'),
(52, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:37:53'),
(53, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:38:35'),
(54, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 17:41:11'),
(55, 6, 'Tareas', 'UPDATE', 'ACTUALIZAR {\"anterior\":{\"idTarea\":3,\"idSolicitud\":3,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"Hacer el cierre de mes de contabilidad de todas las ventas generadas\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":1.76},\"actual\":{\"idTarea\":3,\"idSolicitud\":3,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Hacer el cierre de mes de contabilidad de todas las ventas generadas en la tienda\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":1.76}}', '2026-10-11 17:41:29'),
(56, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:41:29'),
(57, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 17:42:33'),
(58, 6, 'Tareas', 'DELETE', 'ELIMINAR {\"idTarea\":3,\"idSolicitud\":3,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"Hacer el cierre de mes de contabilidad de todas las ventas generadas en la tienda\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":1.76}', '2026-10-11 17:43:02'),
(59, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:43:03'),
(60, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":8,\"nombreUsuario\":\"Sebas\",\"nombreCompleto\":\"Sebastian\",\"correo\":\"sebastian079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-11 17:43:13'),
(61, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 17:43:13'),
(62, 6, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-11 17:43:45'),
(63, 6, 'Usuarios', 'UPDATE', 'UPDATE {\"anterior\":{\"idUsuario\":8,\"nombreUsuario\":\"Sebas\",\"nombreCompleto\":\"Sebastian\",\"correo\":\"sebastian079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-11T17:43:13\",\"fechaModificacion\":null},\"actual\":{\"idUsuario\":8,\"nombreUsuario\":\"Sebas\",\"nombreCompleto\":\"Sebastian\",\"correo\":\"sebastian079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-11T17:43:13\",\"fechaModificacion\":null}}', '2026-10-11 17:43:51'),
(64, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 17:43:51'),
(65, 6, 'Usuarios', 'DELETE', 'DELETE {\"idUsuario\":7,\"nombreUsuario\":\"admin3\",\"nombreCompleto\":\"Usuario Administrador 3\",\"correo\":\"admin3@publica.local\",\"estado\":1,\"intentosFallidos\":1,\"fechaCreacion\":\"2026-10-11T16:59:52\",\"fechaModificacion\":\"2026-10-11T17:12:14\"}', '2026-10-11 17:44:21'),
(66, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 17:44:21'),
(67, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:44:32'),
(68, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:44:37'),
(69, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":6,\"idSolicitud\":4,\"idUsuario\":8,\"nombreUsuario\":\"\",\"descripcion\":\"Pasar a la base mas reciente 1234\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":1.01}', '2026-10-11 17:47:39'),
(70, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:47:39'),
(71, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 17:48:40'),
(72, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:00:24'),
(73, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:00:27'),
(74, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:00:31'),
(75, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:00:35'),
(76, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":7,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"szrtgxdygthgdthgdtr\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.76}', '2026-10-11 18:03:38'),
(77, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:03:39'),
(78, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":8,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"yrtdytryuturyrtdutrurty5rde5\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.01}', '2026-10-11 18:03:49'),
(79, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:03:49'),
(80, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":9,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"drtutyutyfdurt6ydr\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.01}', '2026-10-11 18:04:00'),
(81, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:04:02'),
(82, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":10,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"drtyutduiyiftutyur6u6ryur5dy\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.01}', '2026-10-11 18:04:17'),
(83, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:04:18'),
(84, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":11,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"uftutyufr5y7dr5yr5\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.01}', '2026-10-11 18:04:26'),
(85, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:04:26'),
(86, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":12,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"yotfijhyiotfiohyfth\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.01}', '2026-10-11 18:04:40'),
(87, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:04:41'),
(88, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":13,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"ytuitryudrlyjertdodyry5\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.01}', '2026-10-11 18:04:52'),
(89, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:04:52'),
(90, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":14,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"hrtdyrte5ytr5ylirj5tyoireite\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.51}', '2026-10-11 18:05:05'),
(91, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:05:06'),
(92, 6, 'Tareas', 'CREATE', 'CREAR {\"idTarea\":15,\"idSolicitud\":2,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"rdy5eysetjoiroitw\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":0.01}', '2026-10-11 18:05:21'),
(93, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:05:21'),
(94, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:05:26'),
(95, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:05:30'),
(96, 8, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 18:05:58'),
(97, 8, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 18:06:03'),
(98, 8, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 18:06:06'),
(99, 8, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-11 18:06:21'),
(100, 8, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 18:06:25'),
(101, 8, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 18:06:42'),
(102, 8, 'Usuarios', 'DELETE', 'DELETE {\"idUsuario\":9,\"nombreUsuario\":\"prueba01\",\"nombreCompleto\":\"Usuario Prueba 01\",\"correo\":\"prueba01@publica.local\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-11T18:05:00\",\"fechaModificacion\":null}', '2026-10-11 18:07:42'),
(103, 8, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 18:07:42'),
(104, 8, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 18:08:40'),
(105, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:15:05'),
(106, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:15:09'),
(107, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:15:24'),
(108, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 18:15:28'),
(109, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 18:26:33'),
(110, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 18:26:38'),
(111, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 18:27:11'),
(112, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 18:27:14'),
(113, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 18:27:19'),
(114, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 18:32:35'),
(115, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 18:33:37'),
(116, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 18:38:45'),
(117, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 18:39:46'),
(118, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 18:39:49'),
(119, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 18:39:53'),
(120, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 18:39:54'),
(121, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 18:39:55'),
(122, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 18:40:06'),
(123, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":2,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":5,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-11 21:33:26'),
(124, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 21:33:56'),
(125, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 21:33:57'),
(126, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:33:58'),
(127, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:34:04'),
(128, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 21:34:27'),
(129, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:34:41'),
(130, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:34:48'),
(131, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:34:54'),
(132, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:35:03'),
(133, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:35:55'),
(134, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:39:08'),
(135, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:39:12'),
(136, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:39:13'),
(137, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:39:14'),
(138, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":19,\"nombreUsuario\":\"TonyRico\",\"nombreCompleto\":\"Anthony Gamboa Elizondo\",\"correo\":\"sebastianobando079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-11 21:39:49'),
(139, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:39:49'),
(140, 6, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-11 21:39:57'),
(141, 6, 'Usuarios', 'UPDATE', 'UPDATE {\"anterior\":{\"idUsuario\":19,\"nombreUsuario\":\"TonyRico\",\"nombreCompleto\":\"Anthony Gamboa Elizondo\",\"correo\":\"sebastianobando079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-11T21:39:49\",\"fechaModificacion\":null},\"actual\":{\"idUsuario\":19,\"nombreUsuario\":\"TonyGuapo\",\"nombreCompleto\":\"Anthony Gamboa Elizondo\",\"correo\":\"sebastianobando079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-11T21:39:49\",\"fechaModificacion\":null}}', '2026-10-11 21:40:10'),
(142, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:40:10'),
(143, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:40:31'),
(144, 6, 'Solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electrico\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"}', '2026-10-11 21:42:41'),
(145, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:42:41'),
(146, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:42:49'),
(147, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-11 21:42:49'),
(148, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:42:52'),
(149, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:42:59'),
(150, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electrico\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"},\"actual\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electricooooo\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"}}', '2026-10-11 21:43:13'),
(151, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:43:16'),
(152, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:43:53'),
(153, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:45:02'),
(154, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:45:05'),
(155, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:46:42'),
(156, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:46:48'),
(157, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:46:52'),
(158, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:46:55'),
(159, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:46:58'),
(160, 6, 'Solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"sgsgsfgs\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-22T00:00:00\"}', '2026-10-11 21:47:18'),
(161, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:47:18'),
(162, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:47:24'),
(163, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:49:22'),
(164, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:49:25'),
(165, 6, 'Tareas', 'CREATE', 'CREATE {\"idTarea\":16,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Hola como vas\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":13.01}', '2026-10-11 21:50:24'),
(166, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:50:24'),
(167, 6, 'Tareas', 'UPDATE', 'UPDATE {\"anterior\":{\"idTarea\":16,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"Hola como vas\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":13.01},\"actual\":{\"idTarea\":16,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Hola como vas hace tiempo no se de ti\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":13.01}}', '2026-10-11 21:50:42'),
(168, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:50:43'),
(169, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:50:48'),
(170, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:50:49'),
(171, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:50:51'),
(172, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 21:50:53'),
(173, 6, 'Representantes', 'CREATE', 'Se crea representante: {\"IdRepresentante\":6,\"Nombre\":\"Sebastian Obando Ramirez\",\"Correo\":\"sebastianobando079@gmail.com\"}', '2026-10-11 21:51:37'),
(174, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 21:51:37'),
(175, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:52:08'),
(176, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:52:11'),
(177, 6, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-11 21:52:14'),
(178, 6, 'Usuarios', 'UPDATE', 'UPDATE {\"anterior\":{\"idUsuario\":19,\"nombreUsuario\":\"TonyGuapo\",\"nombreCompleto\":\"Anthony Gamboa Elizondo\",\"correo\":\"sebastianobando079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-11T21:39:49\",\"fechaModificacion\":\"2026-10-11T21:40:10\"},\"actual\":{\"idUsuario\":19,\"nombreUsuario\":\"TonyGuapo\",\"nombreCompleto\":\"Anthony Gamboa Elizondo\",\"correo\":\"sebastianobando079@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-11T21:39:49\",\"fechaModificacion\":null}}', '2026-10-11 21:52:34'),
(179, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:52:34'),
(180, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:52:51'),
(181, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:52:59'),
(182, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:53:03'),
(183, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:53:10'),
(184, 6, 'Tareas', 'CREATE', 'CREATE {\"idTarea\":17,\"idSolicitud\":5,\"idUsuario\":19,\"nombreUsuario\":\"\",\"descripcion\":\"sdfgsg\",\"fecha\":\"2026-10-11T00:00:00\",\"horasInvertidas\":5.51}', '2026-10-11 21:53:24'),
(185, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-11 21:53:24'),
(186, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:53:28'),
(187, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:53:34'),
(188, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:53:53'),
(189, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:54:05'),
(190, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:54:07'),
(191, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-11 21:54:08'),
(192, 6, 'Desgloses de solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":8,\"mes\":10,\"anio\":2026,\"horas\":1.11,\"monto\":10092190,\"iva\":1311984.70,\"total\":11404174.70,\"observaciones\":\"sgsgsgf\",\"porcentajeCobro\":12}', '2026-10-11 21:54:36'),
(193, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:54:36'),
(194, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-11 21:54:36'),
(195, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:54:43'),
(196, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:54:46'),
(197, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-11 21:54:46'),
(198, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 21:54:58'),
(199, 6, 'Estados de solicitudes', 'CREATE', 'Se crea estado: {\"IdEstado\":6,\"Nombre\":\"Pare Por Favor\"}', '2026-10-11 21:55:14'),
(200, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 21:55:14'),
(201, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:55:26'),
(202, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 21:55:31'),
(203, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 21:55:38'),
(204, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 21:55:40'),
(205, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 21:56:38'),
(206, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 21:57:06'),
(207, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 21:57:14'),
(208, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:57:26'),
(209, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:57:29'),
(210, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:57:31'),
(211, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 21:57:34'),
(212, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 21:57:43'),
(213, 6, 'Representantes', 'UPDATE', 'Se actualiza representante: {\"anterior\":{\"IdRepresentante\":5,\"Nombre\":\"Carlos Jimenez\",\"Correo\":\"carlos.jimenez@publica.com\"},\"actual\":{\"IdRepresentante\":5,\"Nombre\":\"Carlos Jimenezz\",\"Correo\":\"carlos.jimenez@publica.com\"}}', '2026-10-11 21:57:49'),
(214, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 21:57:49'),
(215, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 21:58:02'),
(216, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:58:13'),
(217, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-11 21:58:29'),
(218, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-11 21:58:43'),
(219, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:58:45'),
(220, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:58:51'),
(221, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:59:03'),
(222, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:59:04'),
(223, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:59:15'),
(224, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 21:59:34'),
(225, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 21:59:38'),
(226, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:00:26'),
(227, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:00:29'),
(228, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 22:00:45'),
(229, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":4,\"fechaIngreso\":\"2026-09-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":4,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-02T00:00:00\"}}', '2026-10-11 22:01:24'),
(230, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:01:25'),
(231, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:01:43'),
(232, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-11 22:01:48'),
(233, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":3,\"consecutivoOficio\":\"OF-003-2026\",\"titulo\":\"[PRUEBA] Reporte mensual de ventas\",\"descripcion\":\"Solicitud de prueba 3\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":3,\"fechaIngreso\":\"2026-10-08T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":3,\"consecutivoOficio\":\"OF-003-2026\",\"titulo\":\"[PRUEBA] Reporte mensual de ventas\",\"descripcion\":\"Solicitud de prueba 3\",\"observaciones\":\"asas\",\"documentoInicio\":\"saasa\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":3,\"fechaIngreso\":\"2026-10-02T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-03T00:00:00\"}}', '2026-10-11 22:02:20'),
(234, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:02:20'),
(235, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:02:26'),
(236, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:02:38'),
(237, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:02:42'),
(238, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:02:43'),
(239, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:02:45'),
(240, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 22:02:47'),
(241, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":3,\"consecutivoOficio\":\"OF-003-2026\",\"titulo\":\"[PRUEBA] Reporte mensual de ventas\",\"descripcion\":\"Solicitud de prueba 3\",\"observaciones\":\"asas\",\"documentoInicio\":\"saasa\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":3,\"fechaIngreso\":\"2026-10-02T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-03T00:00:00\"},\"actual\":{\"idSolicitud\":3,\"consecutivoOficio\":\"OF-003-2026\",\"titulo\":\"[PRUEBA] Reporte mensual de ventas\",\"descripcion\":\"Solicitud de prueba 3\",\"observaciones\":\"asas\",\"documentoInicio\":\"saasa\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-02T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-03T00:00:00\"}}', '2026-10-11 22:03:02'),
(242, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:03:16'),
(243, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-11 22:03:19'),
(244, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 22:03:33'),
(245, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-11 22:03:35'),
(246, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-11 22:03:36'),
(247, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-11 22:03:37'),
(248, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 00:57:06'),
(249, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 00:57:07'),
(250, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:57:08'),
(251, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:57:12'),
(252, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:57:13'),
(253, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:57:16'),
(254, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:57:19'),
(255, 21, 'Solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-09-28T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}', '2026-10-12 00:58:36'),
(256, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:58:36'),
(257, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:58:41'),
(258, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:58:41'),
(259, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 00:58:45'),
(260, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-09-28T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-09-29T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 00:58:49'),
(261, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:58:49'),
(262, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:58:51'),
(263, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:58:52'),
(264, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:58:59'),
(265, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:00'),
(266, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 00:59:07'),
(267, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-09-29T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-09-30T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 00:59:14'),
(268, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:14'),
(269, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 00:59:19'),
(270, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-09-30T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 00:59:22'),
(271, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:22'),
(272, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 00:59:25'),
(273, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-02T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 00:59:35'),
(274, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:35'),
(275, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:38'),
(276, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:39'),
(277, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 00:59:40'),
(278, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-02T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-03T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 00:59:47'),
(279, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:47'),
(280, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 00:59:49'),
(281, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-03T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-04T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 00:59:53'),
(282, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:53'),
(283, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:55'),
(284, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 00:59:56'),
(285, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 00:59:57'),
(286, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-04T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 01:00:02'),
(287, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:00:02'),
(288, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:00:23'),
(289, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:00:31'),
(290, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 01:00:35'),
(291, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":5,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":5,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 01:00:42'),
(292, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:00:42'),
(293, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:00:45'),
(294, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 01:00:50'),
(295, 21, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":5,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":1,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 01:00:53'),
(296, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:00:54'),
(297, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:00:56'),
(298, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:05:14'),
(299, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 01:05:17'),
(300, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 01:05:20'),
(301, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:05:26'),
(302, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 01:05:30'),
(303, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:05:31'),
(304, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 01:05:32'),
(305, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 01:05:38'),
(306, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 01:05:40'),
(307, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:05:51'),
(308, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:05:59'),
(309, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 01:06:01'),
(310, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:52:59'),
(311, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 13:53:07'),
(312, 6, 'Tareas', 'CREATE', 'CREATE {\"idTarea\":18,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"dzfhzdegdsrfgszdgfsd\",\"fecha\":\"2026-10-12T00:00:00\",\"horasInvertidas\":2.00}', '2026-10-12 13:53:58'),
(313, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 13:53:58'),
(314, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:54:59'),
(315, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:55:01'),
(316, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:55:02'),
(317, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:55:02'),
(318, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:07'),
(319, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:08'),
(320, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:09'),
(321, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:16'),
(322, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:18'),
(323, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:19'),
(324, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:23'),
(325, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:23'),
(326, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:24'),
(327, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:26'),
(328, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:27'),
(329, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:28'),
(330, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:28'),
(331, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:29'),
(332, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:29'),
(333, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:30'),
(334, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:55:31'),
(335, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:55:33'),
(336, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:55:35'),
(337, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:55:44'),
(338, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:55:47'),
(339, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:55:47'),
(340, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:55:48'),
(341, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:56:59'),
(342, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:57:02'),
(343, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:02'),
(344, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:05'),
(345, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:06'),
(346, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:57:08'),
(347, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:09');
INSERT INTO `bitacora` (`id_bitacora`, `id_usuario`, `modulo`, `accion`, `descripcion`, `fecha`) VALUES
(348, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:10'),
(349, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 13:57:12'),
(350, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:13'),
(351, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:13'),
(352, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:57:14'),
(353, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:57:15'),
(354, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:57:16'),
(355, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:57:17'),
(356, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:17'),
(357, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:24'),
(358, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:30'),
(359, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:30'),
(360, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:57:30'),
(361, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:57:31'),
(362, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:57:33'),
(363, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:36'),
(364, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:37'),
(365, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:40'),
(366, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:57:44'),
(367, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:47'),
(368, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:48'),
(369, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:49'),
(370, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:50'),
(371, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:57:51'),
(372, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:52'),
(373, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:53'),
(374, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:55'),
(375, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:56'),
(376, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:57'),
(377, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:57:57'),
(378, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:57:58'),
(379, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:58:02'),
(380, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:58:02'),
(381, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 13:58:04'),
(382, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:58:08'),
(383, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:58:09'),
(384, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:58:09'),
(385, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:58:10'),
(386, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:58:11'),
(387, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:58:12'),
(388, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 13:59:03'),
(389, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 13:59:04'),
(390, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:59:05'),
(391, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:59:43'),
(392, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:59:44'),
(393, 21, 'Estados de solicitudes', 'UPDATE', 'Se actualiza estado: {\"anterior\":{\"IdEstado\":2,\"Nombre\":\"Respondida\"},\"actual\":{\"IdEstado\":2,\"Nombre\":\"Respondida\"}}', '2026-10-12 13:59:46'),
(394, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 13:59:46'),
(395, 21, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-12 14:00:29'),
(396, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:00:31'),
(397, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:00:35'),
(398, 21, 'Estados de solicitudes', 'UPDATE', 'Se actualiza estado: {\"anterior\":{\"IdEstado\":6,\"Nombre\":\"Pare Por Favor\"},\"actual\":{\"IdEstado\":6,\"Nombre\":\"Pare Por Favor\"}}', '2026-10-12 14:00:43'),
(399, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:00:43'),
(400, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 14:00:47'),
(401, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:00:48'),
(402, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:00:49'),
(403, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:00:50'),
(404, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:00:50'),
(405, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:00:52'),
(406, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:00:59'),
(407, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:00:59'),
(408, 21, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:01:06'),
(409, 21, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 14:01:06'),
(410, 21, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:01:07'),
(411, 21, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:01:07'),
(412, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:31:33'),
(413, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:31:34'),
(414, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 14:31:35'),
(415, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:31:35'),
(416, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:31:39'),
(417, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 14:31:40'),
(418, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:31:40'),
(419, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:31:42'),
(420, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:31:45'),
(421, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:31:47'),
(422, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 14:31:48'),
(423, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:32:38'),
(424, 6, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-12 14:32:40'),
(425, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:32:41'),
(426, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 14:32:46'),
(427, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":4,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-02T00:00:00\"},\"actual\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-02T00:00:00\"}}', '2026-10-12 14:32:48'),
(428, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:32:48'),
(429, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:32:57'),
(430, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:32:58'),
(431, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 14:33:03'),
(432, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-02T00:00:00\"},\"actual\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-02T00:00:00\"}}', '2026-10-12 14:33:06'),
(433, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-01T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-02T00:00:00\"},\"actual\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-04T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"}}', '2026-10-12 14:33:20'),
(434, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:20'),
(435, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:23'),
(436, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:23'),
(437, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 14:33:26'),
(438, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-04T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"},\"actual\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-07T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"}}', '2026-10-12 14:33:31'),
(439, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:31'),
(440, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:34'),
(441, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:36'),
(442, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:37'),
(443, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:38'),
(444, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:33:40'),
(445, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 14:33:40'),
(446, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:33:41'),
(447, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:42'),
(448, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 14:33:43'),
(449, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:49'),
(450, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 14:33:53'),
(451, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 14:33:53'),
(452, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 14:33:54'),
(453, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 14:33:55'),
(454, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:26:53'),
(455, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:27:34'),
(456, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":22,\"nombreUsuario\":\"Pablo\",\"nombreCompleto\":\"Pablo Meneses\",\"correo\":\"pablo@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-12 21:28:04'),
(457, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:28:04'),
(458, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:28:34'),
(459, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:28:37'),
(460, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:28:38'),
(461, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:28:41'),
(462, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:28:43'),
(463, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 21:29:24'),
(464, 6, 'Tareas', 'CREATE', 'CREATE {\"idTarea\":19,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"drfgbdshgfbsedikfgisdcvfuds\",\"fecha\":\"2026-10-12T00:00:00\",\"horasInvertidas\":2.00}', '2026-10-12 21:29:47'),
(465, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 21:29:47'),
(466, 6, 'Tareas', 'UPDATE', 'UPDATE {\"anterior\":{\"idTarea\":18,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"dzfhzdegdsrfgszdgfsd\",\"fecha\":\"2026-10-12T00:00:00\",\"horasInvertidas\":2.00},\"actual\":{\"idTarea\":18,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Revison de algo\",\"fecha\":\"2026-10-12T00:00:00\",\"horasInvertidas\":2.00}}', '2026-10-12 21:30:08'),
(467, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 21:30:08'),
(468, 6, 'Tareas', 'DELETE', 'DELETE {\"idTarea\":19,\"idSolicitud\":8,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"drfgbdshgfbsedikfgisdcvfuds\",\"fecha\":\"2026-10-12T00:00:00\",\"horasInvertidas\":2.00}', '2026-10-12 21:30:14'),
(469, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 21:30:14'),
(470, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-12 21:30:22'),
(471, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-12 21:30:26'),
(472, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-12 21:30:29'),
(473, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-12 21:30:31'),
(474, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-12 21:30:39'),
(475, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 21:30:59'),
(476, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-12 21:31:24'),
(477, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:31:33'),
(478, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:31:37'),
(479, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:31:39'),
(480, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:31:43'),
(481, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:31:49'),
(482, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:32:18'),
(483, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:32:22'),
(484, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:33:40'),
(485, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:33:40'),
(486, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:33:41'),
(487, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:33:41'),
(488, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:33:42'),
(489, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:33:42'),
(490, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:33:44'),
(491, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:33:48'),
(492, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:33:50'),
(493, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:33:51'),
(494, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:33:54'),
(495, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:34:05'),
(496, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:34:08'),
(497, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:34:09'),
(498, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:34:11'),
(499, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:34:13'),
(500, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:34:13'),
(501, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:34:20'),
(502, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:34:24'),
(503, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:34:45'),
(504, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:34:47'),
(505, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:34:47'),
(506, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:36:46'),
(507, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:36:47'),
(508, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:36:47'),
(509, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:36:48'),
(510, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:36:49'),
(511, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":23,\"nombreUsuario\":\"kadada\",\"nombreCompleto\":\"adasdadad\",\"correo\":\"adadaeda@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-12 21:37:04'),
(512, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:37:04'),
(513, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:37:06'),
(514, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:37:08'),
(515, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:37:10'),
(516, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:37:10'),
(517, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:37:11'),
(518, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:37:25'),
(519, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:37:26'),
(520, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:37:27'),
(521, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:37:28'),
(522, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:37:29'),
(523, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:37:29'),
(524, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:37:52'),
(525, 6, 'Representantes', 'CREATE', 'Se crea representante: {\"IdRepresentante\":7,\"Nombre\":\"Pablo Motos\",\"Correo\":\"pablos@gmail.com\"}', '2026-10-12 21:38:42'),
(526, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:38:42'),
(527, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:38:46'),
(528, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:38:49'),
(529, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:38:50'),
(530, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:38:52'),
(531, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:39:01'),
(532, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:39:05'),
(533, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:39:06'),
(534, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:39:06'),
(535, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:39:07'),
(536, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:39:09'),
(537, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:39:31'),
(538, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:39:43'),
(539, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:39:52'),
(540, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:39:53'),
(541, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:39:54'),
(542, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:39:54'),
(543, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:39:56'),
(544, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:39:58'),
(545, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:41:28'),
(546, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:41:29'),
(547, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:41:30'),
(548, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:41:30'),
(549, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:43:03'),
(550, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:43:07'),
(551, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:43:09'),
(552, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:43:10'),
(553, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:43:34'),
(554, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:43:35'),
(555, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:43:40'),
(556, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:43:53'),
(557, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:43:55'),
(558, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:43:57'),
(559, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:54:25'),
(560, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:54:36'),
(561, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:55:08'),
(562, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:55:13'),
(563, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:55:27'),
(564, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:55:29'),
(565, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:55:30'),
(566, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:55:31'),
(567, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:55:49'),
(568, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:55:51'),
(569, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:55:53'),
(570, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:55:54'),
(571, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:55:55'),
(572, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:55:56'),
(573, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:55:57'),
(574, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:56:00'),
(575, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:56:01'),
(576, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:56:09'),
(577, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:56:11'),
(578, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:56:24'),
(579, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:56:25'),
(580, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:56:26'),
(581, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:56:33'),
(582, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:56:43'),
(583, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:56:47'),
(584, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:56:49'),
(585, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:56:52'),
(586, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:56:59'),
(587, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:57:00'),
(588, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 21:57:01'),
(589, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:57:03'),
(590, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 21:57:04'),
(591, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:57:14'),
(592, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 21:57:30'),
(593, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 21:57:35'),
(594, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:07:02'),
(595, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:11:58'),
(596, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:12:02'),
(597, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:12:05'),
(598, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:12:06'),
(599, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:12:07'),
(600, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:12:07'),
(601, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:12:08'),
(602, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:13:25'),
(603, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 22:13:47'),
(604, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"sgsgsfgs\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-22T00:00:00\"},\"actual\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-22T00:00:00\"}}', '2026-10-12 22:14:03'),
(605, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:14:03'),
(606, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:15:44'),
(607, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:15:48'),
(608, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 22:15:56'),
(609, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-12 22:15:56'),
(610, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:16:04'),
(611, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 22:16:10'),
(612, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-12 22:16:10'),
(613, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:16:12'),
(614, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:16:17'),
(615, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-12 22:16:22'),
(616, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":1,\"consecutivoOficio\":\"OF-001-2026\",\"titulo\":\"Solicitud de prueba para Tareas\",\"descripcion\":\"Solicitud creada para probar TAR1\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":1,\"fechaIngreso\":\"2026-10-10T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":1,\"consecutivoOficio\":\"OF-001-2026\",\"titulo\":\"Solicitud de prueba para Tareas\",\"descripcion\":\"Solicitud creada para probar TAR1\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":3,\"fechaIngreso\":\"2026-10-10T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-12 22:16:28'),
(617, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:16:28'),
(618, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:16:34'),
(619, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:16:38'),
(620, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:17:58'),
(621, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:18:04'),
(622, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:18:05'),
(623, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:18:06'),
(624, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:07'),
(625, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:08'),
(626, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:10'),
(627, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:18:13'),
(628, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:18:13'),
(629, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:18:14'),
(630, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:18:14'),
(631, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:18:14'),
(632, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:22'),
(633, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:25'),
(634, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:18:26'),
(635, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:27'),
(636, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:32'),
(637, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:33'),
(638, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:18:34'),
(639, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:25:37'),
(640, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:26:08'),
(641, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:26:11'),
(642, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:26:15'),
(643, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:26:48'),
(644, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:26:52'),
(645, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:26:54'),
(646, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:26:57'),
(647, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:26:59'),
(648, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:00'),
(649, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:01'),
(650, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:02'),
(651, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:03'),
(652, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:03'),
(653, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:04'),
(654, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:04'),
(655, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:05'),
(656, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:06'),
(657, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:06'),
(658, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:09'),
(659, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:10'),
(660, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:11'),
(661, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:14'),
(662, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:15'),
(663, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:16'),
(664, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:16'),
(665, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:16'),
(666, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:16'),
(667, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:17'),
(668, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:17'),
(669, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:18'),
(670, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:18'),
(671, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:18'),
(672, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:19'),
(673, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:22'),
(674, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:31'),
(675, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:32'),
(676, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:32'),
(677, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:33'),
(678, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:33'),
(679, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:34'),
(680, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:34'),
(681, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:34'),
(682, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:35'),
(683, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:35'),
(684, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:39'),
(685, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:40'),
(686, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:40'),
(687, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:43'),
(688, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:43'),
(689, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:44'),
(690, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:44'),
(691, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:27:47'),
(692, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:48'),
(693, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:27:49'),
(694, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:27:49'),
(695, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:12'),
(696, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:12'),
(697, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:28:13'),
(698, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:13'),
(699, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:14'),
(700, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:14'),
(701, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:14'),
(702, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:15'),
(703, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:16'),
(704, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:16'),
(705, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:17'),
(706, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:17'),
(707, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:21'),
(708, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:29'),
(709, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:30'),
(710, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:28:37'),
(711, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:38'),
(712, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:28:41'),
(713, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:28:48'),
(714, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:29:26'),
(715, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:29:29'),
(716, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-12 22:29:30'),
(717, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:29:31'),
(718, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:29:32'),
(719, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-12 22:56:00'),
(720, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-12 22:56:01'),
(721, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-12 22:56:02'),
(722, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":1,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":4,\"consecutivoOficio\":\"OF-004-2026\",\"titulo\":\"[PRUEBA] Migracion de base de datos\",\"descripcion\":\"Solicitud de prueba 4\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":5,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-13 09:13:24'),
(723, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-13 09:13:24'),
(724, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 09:14:01'),
(725, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 09:14:04'),
(726, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 09:14:07'),
(727, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 09:14:10'),
(728, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 09:14:17'),
(729, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 09:14:20'),
(730, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 09:14:23'),
(731, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 09:14:25'),
(732, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:34:56'),
(733, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:34:58'),
(734, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:34:59'),
(735, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:35:08'),
(736, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-05T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-13 10:35:12'),
(737, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:35:12'),
(738, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:35:15'),
(739, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":3,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-13 10:35:25'),
(740, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:35:25'),
(741, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 10:35:30'),
(742, 6, 'Estados de solicitudes', 'DELETE', 'Se elimina estado: {\"IdEstado\":6,\"Nombre\":\"Pare Por Favor\"}', '2026-10-13 10:35:36'),
(743, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 10:35:36'),
(744, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 10:35:49'),
(745, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:38:44'),
(746, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:38:45'),
(747, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:38:46'),
(748, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:39:24'),
(749, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-22T00:00:00\"},\"actual\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-11T00:00:00\"}}', '2026-10-13 10:39:48'),
(750, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:39:48'),
(751, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:40:01'),
(752, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electricooooo\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"},\"actual\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electricooooo\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"}}', '2026-10-13 10:40:09'),
(753, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:40:09'),
(754, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:40:16'),
(755, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:40:21'),
(756, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:40:23'),
(757, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-11T00:00:00\"},\"actual\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":\"2026-10-13T00:00:00\",\"fechaInicio\":\"2026-10-11T00:00:00\"}}', '2026-10-13 10:40:35'),
(758, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:40:36'),
(759, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:40:40');
INSERT INTO `bitacora` (`id_bitacora`, `id_usuario`, `modulo`, `accion`, `descripcion`, `fecha`) VALUES
(760, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":\"2026-10-13T00:00:00\",\"fechaInicio\":\"2026-10-11T00:00:00\"},\"actual\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":2,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":\"2026-10-13T00:00:00\",\"fechaInicio\":\"2026-10-11T00:00:00\"}}', '2026-10-13 10:40:44'),
(761, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:40:44'),
(762, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:40:52'),
(763, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:41:00'),
(764, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:41:10'),
(765, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:41:13'),
(766, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:41:14'),
(767, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electricooooo\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"},\"actual\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electricooooo\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":6,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"}}', '2026-10-13 10:41:20'),
(768, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:41:20'),
(769, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:41:27'),
(770, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-13 10:41:27'),
(771, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:41:36'),
(772, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:56:25'),
(773, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:56:29'),
(774, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":2,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":\"2026-10-13T00:00:00\",\"fechaInicio\":\"2026-10-11T00:00:00\"},\"actual\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-11T00:00:00\"}}', '2026-10-13 10:56:38'),
(775, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:56:38'),
(776, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:56:49'),
(777, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":6,\"consecutivoOficio\":\"OF-006-2026\",\"titulo\":\"[PRUEBA] Soporte tecnico vencido\",\"descripcion\":\"Solicitud de prueba 6\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":5,\"fechaIngreso\":\"2026-09-21T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":6,\"consecutivoOficio\":\"OF-006-2026\",\"titulo\":\"[PRUEBA] Soporte tecnico vencido\",\"descripcion\":\"Solicitud de prueba 6\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":5,\"fechaIngreso\":\"2026-09-30T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-13 10:57:03'),
(778, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:03'),
(779, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:05'),
(780, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:57:08'),
(781, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":1,\"consecutivoOficio\":\"OF-001-2026\",\"titulo\":\"Solicitud de prueba para Tareas\",\"descripcion\":\"Solicitud creada para probar TAR1\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":3,\"fechaIngreso\":\"2026-10-10T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":1,\"consecutivoOficio\":\"OF-001-2026\",\"titulo\":\"Solicitud de prueba para Tareas\",\"descripcion\":\"Solicitud creada para probar TAR1\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":1,\"fechaIngreso\":\"2026-10-10T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-13 10:57:14'),
(782, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:14'),
(783, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:57:21'),
(784, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":3,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":3,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"}}', '2026-10-13 10:57:26'),
(785, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:26'),
(786, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 10:57:29'),
(787, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:34'),
(788, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:35'),
(789, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:51'),
(790, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:58'),
(791, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:57:59'),
(792, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:58:00'),
(793, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 10:58:09'),
(794, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 13:35:39'),
(795, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 13:35:45'),
(796, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":24,\"nombreUsuario\":\"Pedro\",\"nombreCompleto\":\"Pedro Sanchez Lopez\",\"correo\":\"pedro12@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-13 13:36:30'),
(797, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 13:36:30'),
(798, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 13:36:41'),
(799, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 13:36:44'),
(800, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 13:36:50'),
(801, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 13:36:52'),
(802, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 13:36:54'),
(803, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 13:36:55'),
(804, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 13:36:56'),
(805, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 13:36:57'),
(806, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:20:52'),
(807, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:20:53'),
(808, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:20:54'),
(809, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:20:56'),
(810, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:20:57'),
(811, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:21:01'),
(812, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:21:02'),
(813, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-13 15:21:05'),
(814, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-13 15:21:09'),
(815, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-13 15:21:11'),
(816, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-13 15:21:26'),
(817, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-13 15:21:29'),
(818, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-13 15:21:30'),
(819, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 15:21:33'),
(820, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 15:21:38'),
(821, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 15:21:41'),
(822, 6, 'Reporte de facturacion mensual', 'CONSULT', 'El usuario consulta reporte de facturacion mensual', '2026-10-13 15:21:58'),
(823, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-13 15:22:13'),
(824, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-13 15:22:16'),
(825, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-13 15:22:20'),
(826, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-13 15:22:27'),
(827, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:22:31'),
(828, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:22:32'),
(829, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 15:22:33'),
(830, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 16:59:54'),
(831, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 16:59:55'),
(832, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 17:00:01'),
(833, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 17:00:03'),
(834, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 17:30:10'),
(835, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-13 17:31:46'),
(836, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 17:31:50'),
(837, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 17:31:52'),
(838, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 17:31:55'),
(839, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 17:32:00'),
(840, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 17:32:02'),
(841, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 17:32:11'),
(842, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 17:32:24'),
(843, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 17:32:28'),
(844, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 17:32:30'),
(845, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:16:46'),
(846, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:16:57'),
(847, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:17:01'),
(848, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:17:13'),
(849, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:17:42'),
(850, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:17:44'),
(851, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:17:45'),
(852, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:17:46'),
(853, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:19:24'),
(854, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:19:45'),
(855, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:45:56'),
(856, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:46:21'),
(857, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:46:35'),
(858, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:46:43'),
(859, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:46:45'),
(860, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:46:50'),
(861, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:46:52'),
(862, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:46:55'),
(863, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:46:58'),
(864, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:47:25'),
(865, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:47:30'),
(866, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:47:31'),
(867, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:47:32'),
(868, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:47:33'),
(869, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:47:36'),
(870, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:47:39'),
(871, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:47:41'),
(872, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:47:41'),
(873, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:47:43'),
(874, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:47:59'),
(875, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:49:01'),
(876, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:49:05'),
(877, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:49:17'),
(878, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:49:18'),
(879, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:49:23'),
(880, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:49:31'),
(881, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 18:50:08'),
(882, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:50:11'),
(883, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:51:00'),
(884, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:51:08'),
(885, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 18:51:09'),
(886, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:51:12'),
(887, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 18:51:21'),
(888, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:51:29'),
(889, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 18:51:38'),
(890, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 19:26:45'),
(891, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 19:26:50'),
(892, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 19:27:02'),
(893, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 19:27:04'),
(894, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 19:27:15'),
(895, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 19:27:56'),
(896, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 19:28:00'),
(897, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 20:10:00'),
(898, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 20:10:21'),
(899, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:10:27'),
(900, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 20:10:32'),
(901, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:10:42'),
(902, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 20:10:47'),
(903, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:11:12'),
(904, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 20:11:18'),
(905, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:11:26'),
(906, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 20:11:28'),
(907, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:11:30'),
(908, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 20:11:31'),
(909, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:11:55'),
(910, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 20:12:02'),
(911, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:12:19'),
(912, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-13 20:12:36'),
(913, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-13 20:12:40'),
(914, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-13 20:13:12'),
(915, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-13 20:15:31'),
(916, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":3,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"}}', '2026-10-14 04:37:01'),
(917, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-14 20:20:57'),
(918, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-14 20:20:59'),
(919, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-14 20:21:09'),
(920, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-14 20:21:14'),
(921, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-14 20:21:21'),
(922, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-14 20:21:23'),
(923, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-14 20:21:25'),
(924, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-14 20:21:31'),
(925, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-07T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"},\"actual\":{\"idSolicitud\":5,\"consecutivoOficio\":\"OF-005-2026\",\"titulo\":\"[PRUEBA] Capacitacion de usuarios\",\"descripcion\":\"Solicitud de prueba 5\",\"observaciones\":\"ADADA\",\"documentoInicio\":\"ADADS\",\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-07T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"}}', '2026-10-15 12:25:58'),
(926, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":1,\"consecutivoOficio\":\"OF-001-2026\",\"titulo\":\"Solicitud de prueba para Tareas\",\"descripcion\":\"Solicitud creada para probar TAR1\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":1,\"fechaIngreso\":\"2026-10-10T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":1,\"consecutivoOficio\":\"OF-001-2026\",\"titulo\":\"Solicitud de prueba para Tareas\",\"descripcion\":\"Solicitud creada para probar TAR1\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":2,\"idEstado\":5,\"fechaIngreso\":\"2026-10-10T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-17 13:46:22'),
(927, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":2,\"consecutivoOficio\":\"OF-002-2026\",\"titulo\":\"[PRUEBA] Mantenimiento del sitio web\",\"descripcion\":\"Solicitud de prueba 2\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":2,\"consecutivoOficio\":\"OF-002-2026\",\"titulo\":\"[PRUEBA] Mantenimiento del sitio web\",\"descripcion\":\"Solicitud de prueba 2\",\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":4,\"idEstado\":5,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-17 13:46:22'),
(928, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electricooooo\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":6,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"},\"actual\":{\"idSolicitud\":7,\"consecutivoOficio\":\"OF-90\",\"titulo\":\"Cambio de Cableado Electrico\",\"descripcion\":\"Cambio de cableado electricooooo\",\"observaciones\":\"Mal cableado\",\"documentoInicio\":\"Cedula\",\"documentoRespuesta\":null,\"idRepresentante\":6,\"idEstado\":5,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-14T00:00:00\"}}', '2026-10-17 13:46:22'),
(929, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-11T00:00:00\"},\"actual\":{\"idSolicitud\":8,\"consecutivoOficio\":\"123\",\"titulo\":\"Incentivos\",\"descripcion\":\"sfdsf\",\"observaciones\":\"sfdsfs\",\"documentoInicio\":\"sfsdf\",\"documentoRespuesta\":\"sfsd\",\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-11T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-11T00:00:00\"}}', '2026-10-17 13:46:22'),
(930, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-17 13:46:30'),
(931, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-17 13:46:31'),
(932, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-17 13:46:33'),
(933, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-17 13:46:44'),
(934, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-17 13:46:46'),
(935, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-17 13:46:48'),
(936, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-17 13:46:51'),
(937, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-17 13:46:53'),
(938, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-17 13:55:35'),
(939, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-17 13:55:35'),
(940, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-17 13:55:36'),
(941, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-18 18:55:26'),
(942, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-18 18:55:29'),
(943, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 18:55:30'),
(944, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:11:25'),
(945, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:13:52'),
(946, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:13:53'),
(947, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:13:54'),
(948, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:13:55'),
(949, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:13:59'),
(950, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:14:00'),
(951, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-18 19:14:04'),
(952, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-18 19:14:05'),
(953, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-18 19:14:06'),
(954, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:14:09'),
(955, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:14:12'),
(956, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-18 19:14:14'),
(957, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-07T00:00:00\"},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-18T00:00:00\"}}', '2026-10-18 19:14:24'),
(958, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:14:24'),
(959, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:14:26'),
(960, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:14:27'),
(961, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-18 19:14:30'),
(962, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-18T00:00:00\"},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-18T00:00:00\"}}', '2026-10-18 19:14:33'),
(963, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:14:34'),
(964, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:18:43'),
(965, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 19:18:48'),
(966, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 21:22:52'),
(967, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 21:23:19'),
(968, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-18 21:23:20'),
(969, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-18 21:23:32'),
(970, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-18 21:23:40'),
(971, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-18 21:23:41'),
(972, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-18 21:23:42'),
(973, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-18 21:23:42'),
(974, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-18 21:23:43'),
(975, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-18 21:23:43'),
(976, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-18 21:23:48'),
(977, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:47:29'),
(978, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:47:30'),
(979, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:47:32'),
(980, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:47:33'),
(981, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:47:34'),
(982, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:47:35'),
(983, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:47:57'),
(984, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:48:00'),
(985, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:48:02'),
(986, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:48:03'),
(987, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:48:04'),
(988, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:48:05'),
(989, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:48:06'),
(990, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:48:06'),
(991, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:48:07'),
(992, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:48:11'),
(993, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:48:12'),
(994, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:48:12'),
(995, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":25,\"nombreUsuario\":\"hglhk\",\"nombreCompleto\":\"kjhkjhv\",\"correo\":\"mvjhvjhvjhv@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-19 14:48:37'),
(996, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:48:37'),
(997, 6, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-19 14:48:51'),
(998, 6, 'Usuarios', 'UPDATE', 'UPDATE {\"anterior\":{\"idUsuario\":25,\"nombreUsuario\":\"hglhk\",\"nombreCompleto\":\"kjhkjhv\",\"correo\":\"mvjhvjhvjhv@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-19T14:48:37\",\"fechaModificacion\":null},\"actual\":{\"idUsuario\":25,\"nombreUsuario\":\"jjjjjjjjjjjjjjjjjjjjjj\",\"nombreCompleto\":\"kjhkjhv\",\"correo\":\"mvjhvjhvjhv@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-19T14:48:37\",\"fechaModificacion\":null}}', '2026-10-19 14:49:04'),
(999, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:49:04'),
(1000, 6, 'Usuarios', 'DELETE', 'DELETE {\"idUsuario\":25,\"nombreUsuario\":\"jjjjjjjjjjjjjjjjjjjjjj\",\"nombreCompleto\":\"kjhkjhv\",\"correo\":\"mvjhvjhvjhv@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-19T14:48:37\",\"fechaModificacion\":\"2026-10-19T14:49:04\"}', '2026-10-19 14:49:09'),
(1001, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:49:09'),
(1002, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:49:18'),
(1003, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:49:18'),
(1004, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:49:19'),
(1005, 6, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-19 14:49:20'),
(1006, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 14:49:20'),
(1007, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:49:20'),
(1008, 6, 'Solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}', '2026-10-19 14:50:09'),
(1009, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:09'),
(1010, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:50:12'),
(1011, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 14:50:12'),
(1012, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-06T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-07T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:50:16'),
(1013, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:16'),
(1014, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 14:50:20'),
(1015, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:21'),
(1016, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-07T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-13T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:50:29'),
(1017, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:29'),
(1018, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-19 14:50:29'),
(1019, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 14:50:31'),
(1020, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-13T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-14T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:50:34'),
(1021, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:34'),
(1022, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 14:50:36'),
(1023, 6, 'Tareas', 'CREATE', 'CREATE {\"idTarea\":20,\"idSolicitud\":9,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"kjashgfjusfjgyjusedfa\",\"fecha\":\"2026-10-19T00:00:00\",\"horasInvertidas\":2.00}', '2026-10-19 14:50:39'),
(1024, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-19 14:50:39'),
(1025, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-14T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-15T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:50:39'),
(1026, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:39'),
(1027, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 14:50:42'),
(1028, 6, 'Tareas', 'UPDATE', 'UPDATE {\"anterior\":{\"idTarea\":20,\"idSolicitud\":9,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"kjashgfjusfjgyjusedfa\",\"fecha\":\"2026-10-19T00:00:00\",\"horasInvertidas\":2.00},\"actual\":{\"idTarea\":20,\"idSolicitud\":9,\"idUsuario\":6,\"nombreUsuario\":\"\",\"descripcion\":\"Editar tarea\",\"fecha\":\"2026-10-19T00:00:00\",\"horasInvertidas\":2.00}}', '2026-10-19 14:50:47'),
(1029, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-19 14:50:47'),
(1030, 6, 'Tareas', 'DELETE', 'DELETE {\"idTarea\":20,\"idSolicitud\":9,\"idUsuario\":6,\"nombreUsuario\":\"Usuario Administrador\",\"descripcion\":\"Editar tarea\",\"fecha\":\"2026-10-19T00:00:00\",\"horasInvertidas\":2.00}', '2026-10-19 14:50:51'),
(1031, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-19 14:50:51'),
(1032, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-15T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-15T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:50:52'),
(1033, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:52'),
(1034, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 14:50:55'),
(1035, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-15T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-13T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:50:59'),
(1036, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:50:59'),
(1037, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 14:51:01'),
(1038, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:03'),
(1039, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-19 14:51:05'),
(1040, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-13T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-14T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-19 14:51:06'),
(1041, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:06'),
(1042, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:07'),
(1043, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:08'),
(1044, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:12'),
(1045, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:51:14'),
(1046, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:51:15'),
(1047, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:51:18'),
(1048, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:26'),
(1049, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:30'),
(1050, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:51:37'),
(1051, 6, 'reporte de tareas por usuario', 'CONSULT', 'El usuario consulta reporte de tareas por usuario', '2026-10-19 14:51:47'),
(1052, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-19 14:51:54'),
(1053, 6, 'reporte de tareas por solicitud', 'CONSULT', 'El usuario consulta reporte de tareas por solicitud', '2026-10-19 14:52:00'),
(1054, 6, 'Solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":11,\"consecutivoOficio\":\"PRUEBA-003\",\"titulo\":\"Prueba Solicitudes 3\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":7,\"idEstado\":1,\"fechaIngreso\":\"2026-10-12T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}', '2026-10-19 14:52:19'),
(1055, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:52:19'),
(1056, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 14:53:39'),
(1057, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:53:40'),
(1058, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:54:35'),
(1059, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-19 14:54:41'),
(1060, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 14:58:08'),
(1061, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 14:58:40'),
(1062, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:00:59'),
(1063, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:00'),
(1064, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:00'),
(1065, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:03'),
(1066, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:04'),
(1067, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:05'),
(1068, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:05'),
(1069, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:06'),
(1070, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:06'),
(1071, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:07'),
(1072, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:07'),
(1073, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:08'),
(1074, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:08'),
(1075, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:09'),
(1076, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:10'),
(1077, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:10'),
(1078, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:11'),
(1079, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:12'),
(1080, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:12'),
(1081, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:13'),
(1082, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:13'),
(1083, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:14'),
(1084, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:14'),
(1085, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 15:01:16'),
(1086, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:19'),
(1087, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-19 15:01:22'),
(1088, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:27'),
(1089, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:01:29'),
(1090, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-19 15:01:29'),
(1091, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:01:39'),
(1092, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-19 15:01:39'),
(1093, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:40'),
(1094, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:01:43'),
(1095, 6, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-10-19 15:01:43'),
(1096, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:48'),
(1097, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:50'),
(1098, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:50'),
(1099, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:51'),
(1100, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:51'),
(1101, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:58'),
(1102, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:01:59'),
(1103, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:02:00'),
(1104, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 15:02:06'),
(1105, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 15:10:32'),
(1106, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 15:10:33'),
(1107, 22, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":26,\"nombreUsuario\":\"Tony\",\"nombreCompleto\":\"Anthony Gamboa Elizondo\",\"correo\":\"elsa96150@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-19 15:11:07');
INSERT INTO `bitacora` (`id_bitacora`, `id_usuario`, `modulo`, `accion`, `descripcion`, `fecha`) VALUES
(1108, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 15:11:07'),
(1109, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 15:11:12'),
(1110, 22, 'Representantes', 'CREATE', 'Se crea representante: {\"IdRepresentante\":8,\"Nombre\":\"Jordi Calvo\",\"Correo\":\"brekennoobcr@gmail.com\"}', '2026-10-19 15:12:04'),
(1111, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 15:12:04'),
(1112, 22, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-19 15:12:07'),
(1113, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:13:04'),
(1114, 22, 'Solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":1,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}', '2026-10-19 15:14:18'),
(1115, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:14:18'),
(1116, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:14:24'),
(1117, 22, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":1,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"},\"actual\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":4,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}}', '2026-10-19 15:14:35'),
(1118, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:14:35'),
(1119, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:17:46'),
(1120, 22, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":4,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"},\"actual\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":3,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}}', '2026-10-19 15:17:55'),
(1121, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:17:55'),
(1122, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 15:24:24'),
(1123, 22, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 15:24:25'),
(1124, 22, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 15:24:34'),
(1125, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:24:36'),
(1126, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:24:38'),
(1127, 22, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":3,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"},\"actual\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":2,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}}', '2026-10-19 15:24:44'),
(1128, 22, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:24:44'),
(1129, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:25:41'),
(1130, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:25:43'),
(1131, 26, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":2,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"},\"actual\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":3,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}}', '2026-10-19 15:25:49'),
(1132, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:25:49'),
(1133, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:29:27'),
(1134, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:29:30'),
(1135, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:29:33'),
(1136, 26, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":3,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"},\"actual\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":1,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}}', '2026-10-19 15:29:37'),
(1137, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:29:37'),
(1138, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:30:38'),
(1139, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:31:08'),
(1140, 26, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-19 15:38:43'),
(1141, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:38:44'),
(1142, 26, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-19 15:38:46'),
(1143, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:38:54'),
(1144, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:38:55'),
(1145, 26, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":1,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"},\"actual\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":5,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}}', '2026-10-19 15:39:02'),
(1146, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:39:05'),
(1147, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-10-19 15:39:53'),
(1148, 26, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":5,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"},\"actual\":{\"idSolicitud\":12,\"consecutivoOficio\":\"TI\",\"titulo\":\"Tecnologias\",\"descripcion\":\"Si\",\"observaciones\":\"Si\",\"documentoInicio\":\"Si\",\"documentoRespuesta\":\"No\",\"idRepresentante\":8,\"idEstado\":4,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-19T00:00:00\"}}', '2026-10-19 15:40:01'),
(1149, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:40:03'),
(1150, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:40:52'),
(1151, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:40:54'),
(1152, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-19 15:40:58'),
(1153, 26, 'Reporte de facturacion mensual', 'CONSULT', 'El usuario consulta reporte de facturacion mensual', '2026-10-19 15:41:34'),
(1154, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":11,\"consecutivoOficio\":\"PRUEBA-003\",\"titulo\":\"Prueba Solicitudes 3\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":7,\"idEstado\":1,\"fechaIngreso\":\"2026-10-12T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":11,\"consecutivoOficio\":\"PRUEBA-003\",\"titulo\":\"Prueba Solicitudes 3\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":7,\"idEstado\":5,\"fechaIngreso\":\"2026-10-12T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-20 00:06:28'),
(1155, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-18T00:00:00\"},\"actual\":{\"idSolicitud\":9,\"consecutivoOficio\":\"OF-001\",\"titulo\":\"Reestructuraci\\u00F3n de la Bodega\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-18T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-10-18T00:00:00\"}}', '2026-10-24 18:40:47'),
(1156, NULL, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":1,\"fechaIngreso\":\"2026-10-14T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null},\"actual\":{\"idSolicitud\":10,\"consecutivoOficio\":\"PRUEBA-002\",\"titulo\":\"Prueba Solicitudes\",\"descripcion\":null,\"observaciones\":null,\"documentoInicio\":null,\"documentoRespuesta\":null,\"idRepresentante\":5,\"idEstado\":5,\"fechaIngreso\":\"2026-10-14T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":null}}', '2026-10-24 18:40:48'),
(1157, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-27 11:24:17'),
(1158, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:24:20'),
(1159, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":27,\"nombreUsuario\":\"Stella\",\"nombreCompleto\":\"Stella Obando Ramirez\",\"correo\":\"stellaooo602@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-27 11:25:21'),
(1160, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:25:21'),
(1161, 6, 'usuario', 'CONSULT', 'El usuario consulta usuario', '2026-10-27 11:25:41'),
(1162, 6, 'Usuarios', 'UPDATE', 'UPDATE {\"anterior\":{\"idUsuario\":27,\"nombreUsuario\":\"Stella\",\"nombreCompleto\":\"Stella Obando Ramirez\",\"correo\":\"stellaooo602@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-27T11:25:21\",\"fechaModificacion\":null},\"actual\":{\"idUsuario\":27,\"nombreUsuario\":\"Stellaa\",\"nombreCompleto\":\"Stella Obando Ramirez\",\"correo\":\"stellaooo602@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"2026-10-27T11:25:21\",\"fechaModificacion\":null}}', '2026-10-27 11:25:49'),
(1163, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:25:49'),
(1164, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 11:25:56'),
(1165, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-27 11:25:58'),
(1166, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 11:25:59'),
(1167, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:26:00'),
(1168, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 11:26:00'),
(1169, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-27 11:26:00'),
(1170, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 11:26:01'),
(1171, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:26:01'),
(1172, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-27 11:26:02'),
(1173, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:26:03'),
(1174, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 11:26:03'),
(1175, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-27 11:26:04'),
(1176, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 11:26:05'),
(1177, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:26:05'),
(1178, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-27 11:26:05'),
(1179, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 11:26:06'),
(1180, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-27 11:26:07'),
(1181, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 11:26:09'),
(1182, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-27 11:26:10'),
(1183, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-27 15:22:33'),
(1184, 6, 'Tareas', 'CONSULT', 'El usuario consulta Tareas', '2026-10-27 15:22:35'),
(1185, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-10-27 15:22:40'),
(1186, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-27 15:22:41'),
(1187, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-10-27 15:22:43'),
(1188, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-10-27 15:22:44'),
(1189, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-28 17:55:39'),
(1190, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":28,\"nombreUsuario\":\"dndsnsd\",\"nombreCompleto\":\"Stellaaaa\",\"correo\":\"sbsbsbsbs@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-10-28 17:55:56'),
(1191, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-10-28 17:55:56'),
(1192, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-07 09:38:36'),
(1193, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-07 09:38:37'),
(1194, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-07 09:39:44'),
(1195, 6, 'Usuarios', 'CREATE', 'CREATE {\"idUsuario\":31,\"nombreUsuario\":\"Kas\",\"nombreCompleto\":\"Kassandra Campos\",\"correo\":\"kasss9090@gmail.com\",\"estado\":1,\"intentosFallidos\":0,\"fechaCreacion\":\"0001-01-01T00:00:00\",\"fechaModificacion\":null}', '2026-11-07 09:40:14'),
(1196, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-07 09:40:14'),
(1197, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-07 09:40:19'),
(1198, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-07 09:40:20'),
(1199, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-11-07 09:40:20'),
(1200, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-11-07 09:40:21'),
(1201, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-11-07 09:40:22'),
(1202, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-11-07 09:40:23'),
(1203, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-07 09:40:23'),
(1204, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-07 09:40:24'),
(1205, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:49:39'),
(1206, 26, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-08 15:49:44'),
(1207, 26, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-11-08 15:49:47'),
(1208, 26, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-11-08 15:49:50'),
(1209, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:50:05'),
(1210, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:50:24'),
(1211, 26, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-08 15:50:38'),
(1212, 26, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-11-08 15:50:40'),
(1213, 26, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-11-08 15:50:43'),
(1214, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:50:54'),
(1215, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:50:56'),
(1216, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:50:56'),
(1217, 26, 'Desgloses de solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":12,\"mes\":11,\"anio\":2026,\"horas\":7,\"monto\":1500,\"iva\":195.00,\"total\":1695.00,\"observaciones\":null,\"porcentajeCobro\":0}', '2026-11-08 15:51:18'),
(1218, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:51:18'),
(1219, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:51:18'),
(1220, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desglose de la solicitud', '2026-11-08 15:51:21'),
(1221, 26, 'Desgloses de solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"mes\":11,\"anio\":2026,\"horas\":7.00,\"monto\":1500.00,\"iva\":195.00,\"total\":1695.00,\"observaciones\":null,\"porcentajeCobro\":0.00},\"actual\":{\"idSolicitud\":12,\"mes\":11,\"anio\":2026,\"horas\":7.00,\"monto\":1500.00,\"iva\":195.00,\"total\":1695.00,\"observaciones\":\"Si\",\"porcentajeCobro\":5.00}}', '2026-11-08 15:51:32'),
(1222, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:51:32'),
(1223, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:51:32'),
(1224, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:51:49'),
(1225, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:52:28'),
(1226, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:52:29'),
(1227, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:52:31'),
(1228, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:52:33'),
(1229, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:52:33'),
(1230, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:52:34'),
(1231, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:52:35'),
(1232, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:52:35'),
(1233, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:52:37'),
(1234, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:52:38'),
(1235, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:52:38'),
(1236, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:52:40'),
(1237, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:52:42'),
(1238, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:52:43'),
(1239, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:52:43'),
(1240, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:52:44'),
(1241, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:52:49'),
(1242, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:52:49'),
(1243, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:53:18'),
(1244, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:54:39'),
(1245, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:54:39'),
(1246, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:54:43'),
(1247, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:54:50'),
(1248, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:54:50'),
(1249, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:54:56'),
(1250, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:57:04'),
(1251, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:57:04'),
(1252, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:57:11'),
(1253, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:57:23'),
(1254, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:57:23'),
(1255, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 15:57:26'),
(1256, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-08 15:57:39'),
(1257, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-08 15:57:39'),
(1258, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-08 16:19:44'),
(1259, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 11:54:26'),
(1260, 6, 'usuarios', 'CONSULT', 'El usuario consulta usuarios', '2026-11-09 11:54:29'),
(1261, 6, 'Representantes', 'CONSULT', 'El usuario consulta representantes', '2026-11-09 11:54:30'),
(1262, 6, 'Estados de solicitudes', 'CONSULT', 'El usuario consulta estados', '2026-11-09 11:54:31'),
(1263, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 11:54:34'),
(1264, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:00:13'),
(1265, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:00:56'),
(1266, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 12:01:22'),
(1267, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 12:01:22'),
(1268, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:01:25'),
(1269, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 12:01:39'),
(1270, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-09 12:01:39'),
(1271, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:01:41'),
(1272, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 12:01:42'),
(1273, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 12:01:42'),
(1274, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:01:44'),
(1275, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 12:01:46'),
(1276, 26, 'Desgloses de solicitudes', 'CONSULT', 'El usuario consulta desgloses de la solicitud', '2026-11-09 12:01:46'),
(1277, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:01:47'),
(1278, 6, 'Solicitudes', 'CREATE', 'CREATE {\"idSolicitud\":13,\"consecutivoOficio\":\"87878\",\"titulo\":\"Compras\",\"descripcion\":\"COMPRAS \",\"observaciones\":\"COMPRAS BUENAS\",\"documentoInicio\":\"N/A\",\"documentoRespuesta\":\"N/A\",\"idRepresentante\":7,\"idEstado\":1,\"fechaIngreso\":\"2026-11-09T00:00:00\",\"fechaRespuesta\":null,\"fechaInicio\":\"2026-11-18T00:00:00\"}', '2026-11-09 12:01:59'),
(1279, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:01:59'),
(1280, 6, 'Solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":13,\"publicada\":false,\"fechaVencimientoPublicacion\":null},\"actual\":{\"idSolicitud\":13,\"publicada\":true,\"fechaVencimientoPublicacion\":\"2026-11-19T00:00:00\"}}', '2026-11-09 12:02:08'),
(1281, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:02:08'),
(1282, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 12:02:30'),
(1283, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 12:02:30'),
(1284, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:02:57'),
(1285, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 12:05:15'),
(1286, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 12:05:15'),
(1287, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:05:16'),
(1288, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:22:24'),
(1289, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 12:22:27'),
(1290, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 12:22:27'),
(1291, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 12:22:28'),
(1292, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 21:05:59'),
(1293, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 21:16:41'),
(1294, NULL, 'Participaciones', 'CREATE', 'Participación registrada: {\"idParticipacion\":1,\"idSolicitud\":12,\"proveedorNuevo\":true,\"proveedor\":{\"idProveedor\":1,\"cedulaJuridica\":\"3-102-123456\",\"nombre\":\"Stella\"},\"desgloses\":[{\"mes\":9,\"anio\":2026,\"horas\":2.00,\"monto\":1000.00,\"iva\":130.00,\"total\":1130.00,\"porcentajeCobro\":13.00,\"observaciones\":\"no\"}]}', '2026-11-09 21:16:59'),
(1295, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 21:17:17'),
(1296, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 21:17:19'),
(1297, 6, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 21:17:19'),
(1298, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 21:17:22'),
(1299, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 21:17:24'),
(1300, 6, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 21:17:24'),
(1301, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 21:17:26'),
(1302, 6, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta detalle de propuesta del proveedor', '2026-11-09 21:17:26'),
(1303, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-09 21:17:29'),
(1304, 6, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-09 21:17:29'),
(1305, 6, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-09 21:17:30'),
(1306, NULL, 'Participaciones', 'CREATE', 'Participación registrada: {\"idParticipacion\":2,\"idSolicitud\":12,\"proveedorNuevo\":true,\"proveedor\":{\"idProveedor\":2,\"cedulaJuridica\":\"3-056-900978\",\"nombre\":\"Jonathan Guido Ruiz\"},\"desgloses\":[{\"mes\":10,\"anio\":2026,\"horas\":1.00,\"monto\":1000.00,\"iva\":130.00,\"total\":1130.00,\"porcentajeCobro\":1.00,\"observaciones\":\"dssdvfhdfki\"},{\"mes\":9,\"anio\":2026,\"horas\":2.00,\"monto\":1200.00,\"iva\":156.00,\"total\":1356.00,\"porcentajeCobro\":1.99,\"observaciones\":\"fthjfhftgh\"}]}', '2026-11-09 23:43:10'),
(1307, NULL, 'Participaciones', 'CREATE', 'Participación registrada: {\"idParticipacion\":3,\"idSolicitud\":13,\"proveedorNuevo\":true,\"proveedor\":{\"idProveedor\":3,\"cedulaJuridica\":\"3-101-123456\",\"nombre\":\"Big Tech\"},\"desgloses\":[{\"mes\":11,\"anio\":2026,\"horas\":20.00,\"monto\":100000.00,\"iva\":13000.00,\"total\":113000.00,\"porcentajeCobro\":40.00,\"observaciones\":\"Análisis y respaldo de la base de datos\"},{\"mes\":12,\"anio\":2026,\"horas\":30.00,\"monto\":150000.00,\"iva\":19500.00,\"total\":169500.00,\"porcentajeCobro\":40.00,\"observaciones\":\"Migración y validación de datos\"},{\"mes\":1,\"anio\":2027,\"horas\":10.00,\"monto\":50000.00,\"iva\":6500.00,\"total\":56500.00,\"porcentajeCobro\":20.00,\"observaciones\":\"Pruebas y documentación de la migración\"}]}', '2026-11-09 23:51:41'),
(1308, NULL, 'Participaciones en solicitudes', 'CONSULT', 'El usuario consulta participaciones en solicitudes', '2026-11-09 23:51:55'),
(1309, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-10 00:49:12'),
(1310, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:49:15'),
(1311, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-10 00:49:15'),
(1312, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:50:55'),
(1313, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-10 00:50:55'),
(1314, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:53:24'),
(1315, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta detalle de propuesta del proveedor', '2026-11-10 00:53:24'),
(1316, 26, 'Participaciones de solicitudes', 'UPDATE', 'UPDATE {\"anterior\":{\"idSolicitud\":12,\"idParticipacion\":2,\"aprobada\":false},\"actual\":{\"idSolicitud\":12,\"idParticipacion\":2,\"aprobada\":true}}', '2026-11-10 00:55:46'),
(1317, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:55:46'),
(1318, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta detalle de propuesta del proveedor', '2026-11-10 00:55:46'),
(1319, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:55:49'),
(1320, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-10 00:55:49'),
(1321, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-10 00:57:47'),
(1322, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:57:48'),
(1323, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-10 00:57:48'),
(1324, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitudes', '2026-11-10 00:57:50'),
(1325, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:57:51'),
(1326, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-10 00:57:51'),
(1327, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:58:22'),
(1328, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta detalle de propuesta del proveedor', '2026-11-10 00:58:22'),
(1329, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:58:25'),
(1330, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-10 00:58:25'),
(1331, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:58:28'),
(1332, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta detalle de propuesta del proveedor', '2026-11-10 00:58:28'),
(1333, 26, 'Solicitudes', 'CONSULT', 'El usuario consulta solicitud', '2026-11-10 00:58:31'),
(1334, 26, 'Participaciones de solicitudes', 'CONSULT', 'El usuario consulta participaciones de la solicitud', '2026-11-10 00:58:31');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosesolicitud`
--

CREATE TABLE `desglosesolicitud` (
  `id_solicitud` int(11) NOT NULL,
  `mes` int(11) NOT NULL,
  `anio` int(11) NOT NULL,
  `horas` decimal(5,2) NOT NULL,
  `monto` decimal(10,2) NOT NULL,
  `iva` decimal(10,2) NOT NULL,
  `total` decimal(10,2) NOT NULL,
  `porcentaje_cobro` decimal(5,2) NOT NULL,
  `observaciones` varchar(500) DEFAULT NULL
) ;

--
-- Volcado de datos para la tabla `desglosesolicitud`
--

INSERT INTO `desglosesolicitud` (`id_solicitud`, `mes`, `anio`, `horas`, `monto`, `iva`, `total`, `porcentaje_cobro`, `observaciones`) VALUES
(8, 10, 2026, 1.11, 10092190.00, 1311984.70, 11404174.70, 12.00, 'sgsgsgf'),
(12, 9, 2026, 2.00, 1200.00, 156.00, 1356.00, 1.99, 'fthjfhftgh'),
(12, 10, 2026, 1.00, 1000.00, 130.00, 1130.00, 1.00, 'dssdvfhdfki');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `diaferiado`
--

CREATE TABLE `diaferiado` (
  `id_feriado` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `descripcion` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `diaferiado`
--

INSERT INTO `diaferiado` (`id_feriado`, `fecha`, `descripcion`) VALUES
(1, '2026-01-01', 'Año Nuevo'),
(2, '2026-04-02', 'Jueves Santo'),
(3, '2026-04-03', 'Viernes Santo'),
(4, '2026-04-11', 'Día de Juan Santamaría'),
(5, '2026-05-01', 'Día del Trabajo'),
(6, '2026-07-25', 'Anexión del Partido de Nicoya a Costa Rica'),
(7, '2026-08-02', 'Día de la Virgen de los Ángeles'),
(8, '2026-08-15', 'Día de la Madre'),
(9, '2026-08-31', 'Día de la Persona Negra y la Cultura Afrocostarricense'),
(10, '2026-09-15', 'Día de la Independencia'),
(11, '2026-12-01', 'Día de la Abolición del Ejército'),
(12, '2026-12-25', 'Navidad');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `errorlog`
--

CREATE TABLE `errorlog` (
  `id_error` int(11) NOT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `modulo` varchar(50) DEFAULT NULL,
  `mensaje_error` text NOT NULL,
  `fecha` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `errorlog`
--

INSERT INTO `errorlog` (`id_error`, `id_usuario`, `modulo`, `mensaje_error`, `fecha`) VALUES
(1, 6, 'Tareas', 'Table \'publicasa.usuario\' doesn\'t exist', '2026-10-10 20:03:58'),
(2, 6, 'Tareas', 'Table \'publicasa.usuario\' doesn\'t exist', '2026-10-10 20:05:24'),
(3, 6, 'Usuarios', 'Data truncated for column \'accion\' at row 1', '2026-10-11 17:31:23'),
(4, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-11 21:33:26'),
(5, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-12 01:00:54'),
(6, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-12 14:32:48'),
(7, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-12 14:33:06'),
(8, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-12 14:33:20'),
(9, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-12 22:16:28'),
(10, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-13 09:13:24'),
(11, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-13 09:13:24'),
(12, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-13 10:35:25'),
(13, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-13 10:40:09'),
(14, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-13 10:40:44'),
(15, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-13 10:56:38'),
(16, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-13 10:57:14'),
(17, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-14 04:37:01'),
(18, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-15 12:25:58'),
(19, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-17 13:46:22'),
(20, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-17 13:46:22'),
(21, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-17 13:46:22'),
(22, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-17 13:46:22'),
(23, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-18 19:14:34'),
(24, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-19 14:50:12'),
(25, 6, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-19 14:50:52'),
(26, 22, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-19 15:14:35'),
(27, 22, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-19 15:17:55'),
(28, 22, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-19 15:24:44'),
(29, 26, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-19 15:25:49'),
(30, 26, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-19 15:29:37'),
(31, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-20 00:06:28'),
(32, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-24 18:40:47'),
(33, NULL, 'Notificaciones de solicitudes', 'La configuracion de correo (EmailSettings) esta incompleta.', '2026-10-24 18:40:48'),
(34, 6, 'Usuarios', 'Duplicate entry \'pablo@gmail.com\' for key \'uq_usuarios_correo\'', '2026-11-07 09:39:20'),
(35, 6, 'Usuarios', 'Duplicate entry \'pablo@gmail.com\' for key \'uq_usuarios_correo\'', '2026-11-07 09:39:28');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `estadosolicitud`
--

CREATE TABLE `estadosolicitud` (
  `id_estado` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `estadosolicitud`
--

INSERT INTO `estadosolicitud` (`id_estado`, `nombre`) VALUES
(4, 'Finalizada'),
(3, 'Iniciada'),
(1, 'Nueva'),
(2, 'Respondida'),
(5, 'Vencida');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `historialestadosolicitud`
--

CREATE TABLE `historialestadosolicitud` (
  `id_historial` int(11) NOT NULL,
  `id_solicitud` int(11) NOT NULL,
  `id_estado_anterior` int(11) DEFAULT NULL,
  `id_estado_nuevo` int(11) NOT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha_cambio` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `historialestadosolicitud`
--

INSERT INTO `historialestadosolicitud` (`id_historial`, `id_solicitud`, `id_estado_anterior`, `id_estado_nuevo`, `id_usuario`, `fecha_cambio`) VALUES
(1, 4, 2, 5, NULL, '2026-10-11 21:33:25'),
(2, 3, 3, 5, NULL, '2026-10-11 22:03:02'),
(3, 5, 1, 5, NULL, '2026-10-12 14:33:06'),
(4, 4, 1, 5, NULL, '2026-10-13 09:13:24'),
(5, 9, 1, 5, NULL, '2026-10-13 09:13:24'),
(6, 9, 3, 5, NULL, '2026-10-14 04:37:01'),
(7, 5, 1, 5, NULL, '2026-10-15 12:25:58'),
(8, 1, 1, 5, NULL, '2026-10-17 13:46:22'),
(9, 2, 1, 5, NULL, '2026-10-17 13:46:22'),
(10, 7, 1, 5, NULL, '2026-10-17 13:46:22'),
(11, 8, 1, 5, NULL, '2026-10-17 13:46:22'),
(12, 10, 1, 5, NULL, '2026-10-19 14:50:12'),
(13, 11, 1, 5, NULL, '2026-10-20 00:06:28'),
(14, 9, 1, 5, NULL, '2026-10-24 18:40:47'),
(15, 10, 1, 5, NULL, '2026-10-24 18:40:48');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `participacion`
--

CREATE TABLE `participacion` (
  `id_participacion` int(11) NOT NULL,
  `id_solicitud` int(11) NOT NULL,
  `id_proveedor` int(11) NOT NULL,
  `fecha_participacion` datetime NOT NULL DEFAULT current_timestamp(),
  `aprobada` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `participacion`
--

INSERT INTO `participacion` (`id_participacion`, `id_solicitud`, `id_proveedor`, `fecha_participacion`, `aprobada`) VALUES
(1, 12, 1, '2026-11-09 21:16:59', 0),
(2, 12, 2, '2026-11-09 23:43:10', 1),
(3, 13, 3, '2026-11-09 23:51:41', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `participaciondesglose`
--

CREATE TABLE `participaciondesglose` (
  `id_participacion` int(11) NOT NULL,
  `mes` int(11) NOT NULL,
  `anio` int(11) NOT NULL,
  `horas` decimal(5,2) NOT NULL,
  `monto` decimal(10,2) NOT NULL,
  `iva` decimal(10,2) NOT NULL,
  `total` decimal(10,2) NOT NULL,
  `porcentaje_cobro` decimal(5,2) NOT NULL,
  `observaciones` varchar(500) NOT NULL
) ;

--
-- Volcado de datos para la tabla `participaciondesglose`
--

INSERT INTO `participaciondesglose` (`id_participacion`, `mes`, `anio`, `horas`, `monto`, `iva`, `total`, `porcentaje_cobro`, `observaciones`) VALUES
(1, 9, 2026, 2.00, 1000.00, 130.00, 1130.00, 13.00, 'no'),
(2, 9, 2026, 2.00, 1200.00, 156.00, 1356.00, 1.99, 'fthjfhftgh'),
(2, 10, 2026, 1.00, 1000.00, 130.00, 1130.00, 1.00, 'dssdvfhdfki'),
(3, 1, 2027, 10.00, 50000.00, 6500.00, 56500.00, 20.00, 'Pruebas y documentación de la migración'),
(3, 11, 2026, 20.00, 100000.00, 13000.00, 113000.00, 40.00, 'Análisis y respaldo de la base de datos'),
(3, 12, 2026, 30.00, 150000.00, 19500.00, 169500.00, 40.00, 'Migración y validación de datos');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedor`
--

CREATE TABLE `proveedor` (
  `id_proveedor` int(11) NOT NULL,
  `cedula_juridica` varchar(15) NOT NULL,
  `nombre` varchar(150) NOT NULL,
  `nombre_representante` varchar(150) NOT NULL,
  `telefono` varchar(30) NOT NULL,
  `correo` varchar(150) NOT NULL,
  `fecha_creacion` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `proveedor`
--

INSERT INTO `proveedor` (`id_proveedor`, `cedula_juridica`, `nombre`, `nombre_representante`, `telefono`, `correo`, `fecha_creacion`) VALUES
(1, '3-102-123456', 'Stella', 'Pablo', '12345678', 'pablo13@gmail.com', '2026-11-09 21:16:59'),
(2, '3-056-900978', 'Jonathan Guido Ruiz', 'Jonathan Guido Ruiz', '12334567', 'Jonata@gmail.com', '2026-11-09 23:43:10'),
(3, '3-101-123456', 'Big Tech', 'Jose Luis Soto', '88664422', 'bigtech@gmail.com', '2026-11-09 23:51:41');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `representante`
--

CREATE TABLE `representante` (
  `id_representante` int(11) NOT NULL,
  `nombre` varchar(150) NOT NULL,
  `correo` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `representante`
--

INSERT INTO `representante` (`id_representante`, `nombre`, `correo`) VALUES
(1, 'julian', 'juliangmail.com'),
(2, 'Representante Prueba', 'rep.prueba@publica.com'),
(3, 'jul', 'jul@123'),
(4, 'Maria Rodriguez', 'maria.rodriguez@publica.com'),
(5, 'Carlos Jimenezz', 'carlos.jimenez@publica.com'),
(6, 'Sebastian Obando Ramirez', 'sebastianobando079@gmail.com'),
(7, 'Pablo Motos', 'pablos@gmail.com'),
(8, 'Jordi Calvo', 'brekennoobcr@gmail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `solicitud`
--

CREATE TABLE `solicitud` (
  `id_solicitud` int(11) NOT NULL,
  `consecutivo_oficio` varchar(50) NOT NULL,
  `titulo` varchar(300) NOT NULL,
  `descripcion` varchar(1000) DEFAULT NULL,
  `observaciones` varchar(1000) DEFAULT NULL,
  `documento_inicio` varchar(500) DEFAULT NULL,
  `documento_respuesta` varchar(500) DEFAULT NULL,
  `id_representante` int(11) NOT NULL,
  `id_estado` int(11) NOT NULL,
  `fecha_ingreso` date NOT NULL,
  `fecha_respuesta` date DEFAULT NULL,
  `fecha_inicio` date DEFAULT NULL,
  `publicada` tinyint(1) NOT NULL DEFAULT 0,
  `fecha_vencimiento_publicacion` date DEFAULT NULL
) ;

--
-- Volcado de datos para la tabla `solicitud`
--

INSERT INTO `solicitud` (`id_solicitud`, `consecutivo_oficio`, `titulo`, `descripcion`, `observaciones`, `documento_inicio`, `documento_respuesta`, `id_representante`, `id_estado`, `fecha_ingreso`, `fecha_respuesta`, `fecha_inicio`, `publicada`, `fecha_vencimiento_publicacion`) VALUES
(1, 'OF-001-2026', 'Solicitud de prueba para Tareas', 'Solicitud creada para probar TAR1', NULL, NULL, NULL, 2, 5, '2026-10-10', NULL, NULL, 0, NULL),
(2, 'OF-002-2026', '[PRUEBA] Mantenimiento del sitio web', 'Solicitud de prueba 2', NULL, NULL, NULL, 4, 5, '2026-10-11', NULL, NULL, 0, NULL),
(3, 'OF-003-2026', '[PRUEBA] Reporte mensual de ventas', 'Solicitud de prueba 3', 'asas', 'saasa', NULL, 5, 5, '2026-10-02', NULL, '2026-10-03', 0, NULL),
(4, 'OF-004-2026', '[PRUEBA] Migracion de base de datos', 'Solicitud de prueba 4', NULL, NULL, NULL, 4, 5, '2026-10-05', NULL, NULL, 0, NULL),
(5, 'OF-005-2026', '[PRUEBA] Capacitacion de usuarios', 'Solicitud de prueba 5', 'ADADA', 'ADADS', NULL, 5, 5, '2026-10-07', NULL, '2026-10-07', 0, NULL),
(6, 'OF-006-2026', '[PRUEBA] Soporte tecnico vencido', 'Solicitud de prueba 6', NULL, NULL, NULL, 2, 5, '2026-09-30', NULL, NULL, 0, NULL),
(7, 'OF-90', 'Cambio de Cableado Electrico', 'Cambio de cableado electricooooo', 'Mal cableado', 'Cedula', NULL, 6, 5, '2026-10-11', NULL, '2026-10-14', 0, NULL),
(8, '123', 'Incentivos', 'sfdsf', 'sfdsfs', 'sfsdf', 'sfsd', 5, 5, '2026-10-11', NULL, '2026-10-11', 0, NULL),
(9, 'OF-001', 'Reestructuración de la Bodega', NULL, NULL, NULL, NULL, 5, 5, '2026-10-18', NULL, '2026-10-18', 0, NULL),
(10, 'PRUEBA-002', 'Prueba Solicitudes', NULL, NULL, NULL, NULL, 5, 5, '2026-10-14', NULL, NULL, 0, NULL),
(11, 'PRUEBA-003', 'Prueba Solicitudes 3', NULL, NULL, NULL, NULL, 7, 5, '2026-10-12', NULL, NULL, 0, NULL),
(12, 'TI', 'Tecnologias', 'Si', 'Si', 'Si', 'No', 8, 4, '2026-10-18', NULL, '2026-10-19', 1, '2026-12-31'),
(13, '87878', 'Compras', 'COMPRAS ', 'COMPRAS BUENAS', 'N/A', 'N/A', 7, 1, '2026-11-09', NULL, '2026-11-18', 1, '2026-11-19');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tarea`
--

CREATE TABLE `tarea` (
  `id_tarea` int(11) NOT NULL,
  `id_solicitud` int(11) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  `descripcion` varchar(500) NOT NULL,
  `fecha` date NOT NULL,
  `horas_invertidas` decimal(5,2) NOT NULL
) ;

--
-- Volcado de datos para la tabla `tarea`
--

INSERT INTO `tarea` (`id_tarea`, `id_solicitud`, `id_usuario`, `descripcion`, `fecha`, `horas_invertidas`) VALUES
(2, 2, 6, 'Darle un mantenimiento al sitio del Cuc por un error', '2026-10-11', 2.01),
(4, 2, 6, 'Revisar la base de datos', '2026-10-11', 2.51),
(5, 3, 6, 'Ver que se perdio en el mes', '2026-10-11', 1.01),
(6, 4, 8, 'Pasar a la base mas reciente 1234', '2026-10-11', 1.01),
(7, 2, 6, 'szrtgxdygthgdthgdtr', '2026-10-11', 0.76),
(8, 2, 6, 'yrtdytryuturyrtdutrurty5rde5', '2026-10-11', 0.01),
(9, 2, 6, 'drtutyutyfdurt6ydr', '2026-10-11', 0.01),
(10, 2, 6, 'drtyutduiyiftutyur6u6ryur5dy', '2026-10-11', 0.01),
(11, 2, 6, 'uftutyufr5y7dr5yr5', '2026-10-11', 0.01),
(12, 2, 6, 'yotfijhyiotfiohyfth', '2026-10-11', 0.01),
(13, 2, 6, 'ytuitryudrlyjertdodyry5', '2026-10-11', 0.01),
(14, 2, 6, 'hrtdyrte5ytr5ylirj5tyoireite', '2026-10-11', 0.51),
(15, 2, 6, 'rdy5eysetjoiroitw', '2026-10-11', 0.01),
(16, 8, 6, 'Hola como vas hace tiempo no se de ti', '2026-10-11', 13.01),
(17, 5, 19, 'sdfgsg', '2026-10-11', 5.51),
(18, 8, 6, 'Revison de algo', '2026-10-12', 2.00);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id_usuario` int(11) NOT NULL,
  `nombre_usuario` varchar(100) NOT NULL,
  `nombre_completo` varchar(200) NOT NULL,
  `correo` varchar(200) NOT NULL,
  `contrasena_cifrada` text NOT NULL,
  `nonce_contrasena` varchar(100) NOT NULL,
  `tag_contrasena` varchar(100) NOT NULL,
  `estado` int(11) NOT NULL,
  `intentos_fallidos` int(11) NOT NULL DEFAULT 0,
  `fecha_creacion` datetime NOT NULL DEFAULT current_timestamp(),
  `fecha_modificacion` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id_usuario`, `nombre_usuario`, `nombre_completo`, `correo`, `contrasena_cifrada`, `nonce_contrasena`, `tag_contrasena`, `estado`, `intentos_fallidos`, `fecha_creacion`, `fecha_modificacion`) VALUES
(1, 'admin', 'Usuario Administrador', 'admin@publica.local', 'kAmzHTGtPJfV', 'uVYLNKemHNgepZWR', 'PFBRFvqVXI+DtYY8cAwhUw==', 3, 3, '2026-10-09 20:58:15', '2026-10-09 21:04:20'),
(6, 'admin2', 'Usuario Administrador', 'admin2@publica.local', 'oGiN0Q==', 'dToWb8hdXzwq7bwd', 'MTRmhF0yBr18Bes2WiwGOA==', 1, 0, '2026-10-09 21:17:07', '2026-11-07 09:38:30'),
(8, 'Sebas', 'Sebastian', 'sebastian079@gmail.com', '8DGORQ==', 'PjqwCf3aklGgZ+9c', 'J9MxYvxVlQ6jc6oH99N+qQ==', 1, 1, '2026-10-11 17:43:13', '2026-10-27 11:23:59'),
(19, 'TonyGuapo', 'Anthony Gamboa Elizondo', 'sebastianobando079@gmail.com', '3fmUlg==', 'A23szgMpb1snJi6E', 'idwkq5CqiKxWR4TN652xXQ==', 1, 0, '2026-10-11 21:39:49', '2026-10-11 21:52:34'),
(21, 'pruebam', 'Administrador de Prueba', 'admin.prueba@example.com', 'KoCEs3kIlfijbNArx0m0eg==', '6zP8hTixNOFJ4vL4', 'ktEl7Ol4TQcxgdzXzmi8Dg==', 1, 1, '2026-10-12 00:53:13', '2026-10-12 14:30:19'),
(22, 'Pablo', 'Pablo Meneses', 'pablo@gmail.com', 'S+YSWQ==', 'oLLeSTOQAVjkf7mt', '9tfSbLGeE57m1m1BJ+NRIw==', 1, 0, '2026-10-12 21:28:04', NULL),
(23, 'kadada', 'adasdadad', 'adadaeda@gmail.com', 'liQy1Q==', '1nKPlpCxOCmHay9I', 'qhX2yh/Pffsk40OLb3wliw==', 1, 0, '2026-10-12 21:37:04', NULL),
(24, 'Pedro', 'Pedro Sanchez Lopez', 'pedro12@gmail.com', 'HsS44g==', 'V7rOa/kVtRZzH0q+', '1YjH9+Rhoka1+s4pY3FlwA==', 1, 0, '2026-10-13 13:36:30', NULL),
(26, 'Tony', 'Anthony Gamboa Elizondo', 'elsa96150@gmail.com', 'gNdXceM=', '0DXnY/z/olzuWdq9', 'YZyDpYKkR8SxhAHPLuCDWg==', 1, 0, '2026-10-19 15:11:07', NULL),
(27, 'Stellaa', 'Stella Obando Ramirez', 'stellaooo602@gmail.com', 'o+/baA==', 'Ku/b2DsAIOmbiCLI', 'apMOFtU/daWstKoK++X4XQ==', 1, 0, '2026-10-27 11:25:21', '2026-10-27 11:25:49'),
(28, 'dndsnsd', 'Stellaaaa', 'sbsbsbsbs@gmail.com', '3gRzQQ==', 'loDnMWaoCpjQ04dP', 'pgbFU7JpyZBrC6iKS8y/VA==', 1, 0, '2026-10-28 17:55:56', NULL),
(31, 'Kas', 'Kassandra Campos', 'kasss9090@gmail.com', 'wptkPA==', 'SpVz6R/63mie+Rwn', 'sDDoKGsJ9kyXQ6Wm3SQyrw==', 1, 0, '2026-11-07 09:40:14', NULL);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `bitacora`
--
ALTER TABLE `bitacora`
  ADD PRIMARY KEY (`id_bitacora`),
  ADD KEY `fk_bitacora_usuario` (`id_usuario`);

--
-- Indices de la tabla `desglosesolicitud`
--
ALTER TABLE `desglosesolicitud`
  ADD PRIMARY KEY (`id_solicitud`,`mes`,`anio`);

--
-- Indices de la tabla `diaferiado`
--
ALTER TABLE `diaferiado`
  ADD PRIMARY KEY (`id_feriado`),
  ADD UNIQUE KEY `fecha` (`fecha`);

--
-- Indices de la tabla `errorlog`
--
ALTER TABLE `errorlog`
  ADD PRIMARY KEY (`id_error`),
  ADD KEY `fk_errorlog_usuario` (`id_usuario`);

--
-- Indices de la tabla `estadosolicitud`
--
ALTER TABLE `estadosolicitud`
  ADD PRIMARY KEY (`id_estado`),
  ADD UNIQUE KEY `nombre` (`nombre`);

--
-- Indices de la tabla `historialestadosolicitud`
--
ALTER TABLE `historialestadosolicitud`
  ADD PRIMARY KEY (`id_historial`),
  ADD KEY `fk_historial_solicitud` (`id_solicitud`),
  ADD KEY `fk_historial_estado_anterior` (`id_estado_anterior`),
  ADD KEY `fk_historial_estado_nuevo` (`id_estado_nuevo`),
  ADD KEY `fk_historial_usuario` (`id_usuario`);

--
-- Indices de la tabla `participacion`
--
ALTER TABLE `participacion`
  ADD PRIMARY KEY (`id_participacion`),
  ADD UNIQUE KEY `uq_participacion_solicitud_proveedor` (`id_solicitud`,`id_proveedor`),
  ADD KEY `ix_participacion_proveedor` (`id_proveedor`);

--
-- Indices de la tabla `participaciondesglose`
--
ALTER TABLE `participaciondesglose`
  ADD PRIMARY KEY (`id_participacion`,`mes`,`anio`);

--
-- Indices de la tabla `proveedor`
--
ALTER TABLE `proveedor`
  ADD PRIMARY KEY (`id_proveedor`),
  ADD UNIQUE KEY `uq_proveedor_cedula` (`cedula_juridica`);

--
-- Indices de la tabla `representante`
--
ALTER TABLE `representante`
  ADD PRIMARY KEY (`id_representante`),
  ADD UNIQUE KEY `correo` (`correo`);

--
-- Indices de la tabla `solicitud`
--
ALTER TABLE `solicitud`
  ADD PRIMARY KEY (`id_solicitud`),
  ADD UNIQUE KEY `consecutivo_oficio` (`consecutivo_oficio`),
  ADD KEY `fk_solicitud_representante` (`id_representante`),
  ADD KEY `fk_solicitud_estado` (`id_estado`);

--
-- Indices de la tabla `tarea`
--
ALTER TABLE `tarea`
  ADD PRIMARY KEY (`id_tarea`),
  ADD KEY `fk_tarea_solicitud` (`id_solicitud`),
  ADD KEY `fk_tarea_usuario` (`id_usuario`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id_usuario`),
  ADD UNIQUE KEY `uq_usuarios_nombre_usuario` (`nombre_usuario`),
  ADD UNIQUE KEY `uq_usuarios_correo` (`correo`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `bitacora`
--
ALTER TABLE `bitacora`
  MODIFY `id_bitacora` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1335;

--
-- AUTO_INCREMENT de la tabla `diaferiado`
--
ALTER TABLE `diaferiado`
  MODIFY `id_feriado` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `errorlog`
--
ALTER TABLE `errorlog`
  MODIFY `id_error` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT de la tabla `estadosolicitud`
--
ALTER TABLE `estadosolicitud`
  MODIFY `id_estado` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `historialestadosolicitud`
--
ALTER TABLE `historialestadosolicitud`
  MODIFY `id_historial` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `participacion`
--
ALTER TABLE `participacion`
  MODIFY `id_participacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `proveedor`
--
ALTER TABLE `proveedor`
  MODIFY `id_proveedor` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `representante`
--
ALTER TABLE `representante`
  MODIFY `id_representante` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `solicitud`
--
ALTER TABLE `solicitud`
  MODIFY `id_solicitud` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tarea`
--
ALTER TABLE `tarea`
  MODIFY `id_tarea` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `bitacora`
--
ALTER TABLE `bitacora`
  ADD CONSTRAINT `fk_bitacora_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id_usuario`) ON DELETE SET NULL;

--
-- Filtros para la tabla `desglosesolicitud`
--
ALTER TABLE `desglosesolicitud`
  ADD CONSTRAINT `fk_desglose_solicitud` FOREIGN KEY (`id_solicitud`) REFERENCES `solicitud` (`id_solicitud`);

--
-- Filtros para la tabla `errorlog`
--
ALTER TABLE `errorlog`
  ADD CONSTRAINT `fk_errorlog_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id_usuario`) ON DELETE SET NULL;

--
-- Filtros para la tabla `historialestadosolicitud`
--
ALTER TABLE `historialestadosolicitud`
  ADD CONSTRAINT `fk_historial_estado_anterior` FOREIGN KEY (`id_estado_anterior`) REFERENCES `estadosolicitud` (`id_estado`),
  ADD CONSTRAINT `fk_historial_estado_nuevo` FOREIGN KEY (`id_estado_nuevo`) REFERENCES `estadosolicitud` (`id_estado`),
  ADD CONSTRAINT `fk_historial_solicitud` FOREIGN KEY (`id_solicitud`) REFERENCES `solicitud` (`id_solicitud`),
  ADD CONSTRAINT `fk_historial_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id_usuario`);

--
-- Filtros para la tabla `participacion`
--
ALTER TABLE `participacion`
  ADD CONSTRAINT `fk_participacion_proveedor` FOREIGN KEY (`id_proveedor`) REFERENCES `proveedor` (`id_proveedor`),
  ADD CONSTRAINT `fk_participacion_solicitud` FOREIGN KEY (`id_solicitud`) REFERENCES `solicitud` (`id_solicitud`);

--
-- Filtros para la tabla `participaciondesglose`
--
ALTER TABLE `participaciondesglose`
  ADD CONSTRAINT `fk_participaciondesglose_participacion` FOREIGN KEY (`id_participacion`) REFERENCES `participacion` (`id_participacion`);

--
-- Filtros para la tabla `solicitud`
--
ALTER TABLE `solicitud`
  ADD CONSTRAINT `fk_solicitud_estado` FOREIGN KEY (`id_estado`) REFERENCES `estadosolicitud` (`id_estado`),
  ADD CONSTRAINT `fk_solicitud_representante` FOREIGN KEY (`id_representante`) REFERENCES `representante` (`id_representante`);

--
-- Filtros para la tabla `tarea`
--
ALTER TABLE `tarea`
  ADD CONSTRAINT `fk_tarea_solicitud` FOREIGN KEY (`id_solicitud`) REFERENCES `solicitud` (`id_solicitud`),
  ADD CONSTRAINT `fk_tarea_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id_usuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
