-- ============================================================
-- Sistema Cotizacion Prestamos
-- ============================================================

-- ============================================================
-- Creación Base de Datos
-- ============================================================
CREATE DATABASE DBSistCotPrestamos;
GO

USE DBSistCotPrestamos;
GO

-- ============================================================
-- TABLA: Roles
-- ============================================================
CREATE TABLE Roles (
    id_rol               INT            IDENTITY(1,1)  NOT NULL,
    nombre_rol           VARCHAR(50)    NOT NULL,
    descripcion          VARCHAR(200)   NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Roles PRIMARY KEY (id_rol),
CONSTRAINT UQ_Roles_nombre_rol UNIQUE (nombre_rol),
CONSTRAINT CHK_Roles_nombre_rol CHECK (LEN(LTRIM(RTRIM(nombre_rol))) > 0)
);
GO

-- ============================================================
-- TABLA: Usuarios
-- ============================================================
CREATE TABLE Usuarios (
    id_usuario           INT            IDENTITY(1,1)  NOT NULL,
    nombre_usuario       VARCHAR(100)   NOT NULL,
    hash_password        VARCHAR(256)   NOT NULL,
    email                VARCHAR(150)   NOT NULL,
    nombre               VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Usuarios PRIMARY KEY (id_usuario),
CONSTRAINT UQ_Usuarios_nombre_usuario UNIQUE (nombre_usuario),
CONSTRAINT UQ_Usuarios_email UNIQUE (email),
CONSTRAINT CHK_Usuarios_email CHECK (email LIKE '%@%.%'),
CONSTRAINT CHK_Usuarios_nombre_usuario CHECK (LEN(LTRIM(RTRIM(nombre_usuario))) >= 4)
);
GO

-- ============================================================
-- TABLA: UsuariosRoles
-- ============================================================
CREATE TABLE UsuariosRoles (
    id_usuario_rol       INT            IDENTITY(1,1)  NOT NULL,
    id_usuario           INT            NOT NULL,
    id_rol               INT            NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_UsuariosRoles PRIMARY KEY (id_usuario_rol),
CONSTRAINT UQ_UsuariosRoles_id_usuario_id_rol UNIQUE (id_usuario, id_rol),
CONSTRAINT FK_UsuariosRoles_Usuarios_id_usuario FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_UsuariosRoles_Roles_id_rol FOREIGN KEY (id_rol) REFERENCES Roles(id_rol) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- ============================================================
-- TABLA: Generos
-- ============================================================
CREATE TABLE Generos (
    id_genero            INT            IDENTITY(1,1)  NOT NULL,
    nombre               VARCHAR(50)    NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Generos PRIMARY KEY (id_genero),
CONSTRAINT UQ_Generos_nombre UNIQUE (nombre),
CONSTRAINT CHK_Generos_nombre CHECK (LEN(LTRIM(RTRIM(nombre))) > 0)
);
GO

-- ============================================================
-- TABLA: NivelesEducativos
-- ============================================================
CREATE TABLE NivelesEducativos (
    id_nivel_educativo   INT            IDENTITY(1,1)  NOT NULL,
    nombre               VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_NivelesEducativos PRIMARY KEY (id_nivel_educativo),
CONSTRAINT UQ_NivelesEducativos_nombre UNIQUE (nombre),
CONSTRAINT CHK_NivelesEducativos_nombre CHECK (LEN(LTRIM(RTRIM(nombre))) > 0)
);
GO

-- ============================================================
-- TABLA: RangosIngresos
-- ============================================================
CREATE TABLE RangosIngresos (
    id_rango_ingresos    INT            IDENTITY(1,1)  NOT NULL,
    monto_minimo         DECIMAL(12,2)  NOT NULL,
    monto_maximo         DECIMAL(12,2)  NOT NULL,
    descripcion          VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_RangosIngresos PRIMARY KEY (id_rango_ingresos),
CONSTRAINT CHK_RangosIngresos_rango CHECK (monto_maximo >= monto_minimo),
CONSTRAINT CHK_RangosIngresos_minimo CHECK (monto_minimo >= 0)
);
GO

-- ============================================================
-- TABLA: RangosEdad
-- ============================================================
CREATE TABLE RangosEdad (
    id_rango_edad        INT            IDENTITY(1,1)  NOT NULL,
    edad_minima          INT            NOT NULL,
    edad_maxima          INT            NOT NULL,
    descripcion          VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_RangosEdad PRIMARY KEY (id_rango_edad),
CONSTRAINT CHK_RangosEdad_rango CHECK (edad_maxima >= edad_minima),
CONSTRAINT CHK_RangosEdad_minimo CHECK (edad_minima >= 0)
);
GO

-- ============================================================
-- TABLA: Ocupaciones
-- ============================================================
CREATE TABLE Ocupaciones (
    id_ocupacion         INT            IDENTITY(1,1)  NOT NULL,
    nombre               VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Ocupaciones PRIMARY KEY (id_ocupacion),
CONSTRAINT UQ_Ocupaciones_nombre UNIQUE (nombre),
CONSTRAINT CHK_Ocupaciones_nombre CHECK (LEN(LTRIM(RTRIM(nombre))) > 0)
);
GO

-- ============================================================
-- TABLA: TiposPrestamo
-- ============================================================
CREATE TABLE TiposPrestamo (
    id_tipo_prestamo     INT            IDENTITY(1,1)  NOT NULL,
    nombre               VARCHAR(100)   NOT NULL,
    descripcion          VARCHAR(250)   NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_TiposPrestamo PRIMARY KEY (id_tipo_prestamo),
CONSTRAINT UQ_TiposPrestamo_nombre UNIQUE (nombre),
CONSTRAINT CHK_TiposPrestamo_nombre CHECK (LEN(LTRIM(RTRIM(nombre))) > 0)
);
GO

-- ============================================================
-- TABLA: Plazos
-- ============================================================
CREATE TABLE Plazos (
    id_plazo             INT            IDENTITY(1,1)  NOT NULL,
    meses                INT            NOT NULL,
    descripcion          VARCHAR(100)   NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Plazos PRIMARY KEY (id_plazo),
CONSTRAINT UQ_Plazos_meses UNIQUE (meses),
CONSTRAINT CHK_Plazos_meses CHECK (meses IN (6, 12, 24, 36, 48, 60))
);
GO

-- ============================================================
-- TABLA: MontosRango
-- ============================================================
CREATE TABLE MontosRango (
    id_monto_rango       INT            IDENTITY(1,1)  NOT NULL,
    monto_minimo         DECIMAL(12,2)  NOT NULL,
    monto_maximo         DECIMAL(12,2)  NOT NULL,
    descripcion          VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_MontosRango PRIMARY KEY (id_monto_rango),
CONSTRAINT CHK_MontosRango_rango CHECK (monto_maximo >= monto_minimo),
CONSTRAINT CHK_MontosRango_minimo CHECK (monto_minimo >= 0)
);
GO

-- ============================================================
-- TABLA: TasasInteresRango
-- ============================================================
CREATE TABLE TasasInteresRango (
    id_tasa_rango        INT            IDENTITY(1,1)  NOT NULL,
    tasa_minima          DECIMAL(5,2)   NOT NULL,
    tasa_maxima          DECIMAL(5,2)   NOT NULL,
    descripcion          VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_TasasInteresRango PRIMARY KEY (id_tasa_rango),
CONSTRAINT CHK_TasasInteresRango_rango CHECK (tasa_maxima >= tasa_minima),
CONSTRAINT CHK_TasasInteresRango_minimo CHECK (tasa_minima >= 0)
);
GO

-- ============================================================
-- TABLA: CapacidadesPago
-- ============================================================
CREATE TABLE CapacidadesPago (
    id_capacidad_pago    INT            IDENTITY(1,1)  NOT NULL,
    monto_minimo         DECIMAL(10,2)  NOT NULL,
    monto_maximo         DECIMAL(10,2)  NOT NULL,
    descripcion          VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_CapacidadesPago PRIMARY KEY (id_capacidad_pago),
CONSTRAINT CHK_CapacidadesPago_rango CHECK (monto_maximo >= monto_minimo),
CONSTRAINT CHK_CapacidadesPago_minimo CHECK (monto_minimo >= 0)
);
GO

-- ============================================================
-- TABLA: HistorialesCrediticios
-- ============================================================
CREATE TABLE HistorialesCrediticios (
    id_historial         INT            IDENTITY(1,1)  NOT NULL,
    tiene_prestamos_previos BIT            NOT NULL,
    ha_morado            BIT            NOT NULL,
    descripcion          VARCHAR(200)   NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_HistorialesCrediticios PRIMARY KEY (id_historial)
);
GO

-- ============================================================
-- TABLA: MediosContratacion
-- ============================================================
CREATE TABLE MediosContratacion (
    id_medio             INT            IDENTITY(1,1)  NOT NULL,
    nombre               VARCHAR(100)   NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_MediosContratacion PRIMARY KEY (id_medio),
CONSTRAINT UQ_MediosContratacion_nombre UNIQUE (nombre),
CONSTRAINT CHK_MediosContratacion_nombre CHECK (LEN(LTRIM(RTRIM(nombre))) > 0)
);
GO

-- ============================================================
-- TABLA: CategoriasPregunta
-- ============================================================
CREATE TABLE CategoriasPregunta (
    id_categoria         INT            IDENTITY(1,1)  NOT NULL,
    nombre               VARCHAR(100)   NOT NULL,
    descripcion          VARCHAR(250)   NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_CategoriasPregunta PRIMARY KEY (id_categoria),
CONSTRAINT UQ_CategoriasPregunta_nombre UNIQUE (nombre),
CONSTRAINT CHK_CategoriasPregunta_nombre CHECK (LEN(LTRIM(RTRIM(nombre))) > 0)
);
GO

-- ============================================================
-- TABLA: Clientes
-- ============================================================
CREATE TABLE Clientes (
    id_cliente           INT            IDENTITY(1,1)  NOT NULL,
    nombre               VARCHAR(100)   NOT NULL,
    apellidos            VARCHAR(150)   NOT NULL,
    email                VARCHAR(150)   NULL,
    telefono             VARCHAR(20)    NULL,
    id_genero            INT            NOT NULL,
    id_nivel_educativo   INT            NOT NULL,
    id_rango_ingresos    INT            NOT NULL,
    id_rango_edad        INT            NOT NULL,
    id_ocupacion         INT            NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Clientes PRIMARY KEY (id_cliente),
CONSTRAINT FK_Clientes_Generos_id_genero FOREIGN KEY (id_genero) REFERENCES Generos(id_genero) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Clientes_NivelesEducativos_id_nivel_educativo FOREIGN KEY (id_nivel_educativo) REFERENCES NivelesEducativos(id_nivel_educativo) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Clientes_RangosIngresos_id_rango_ingresos FOREIGN KEY (id_rango_ingresos) REFERENCES RangosIngresos(id_rango_ingresos) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Clientes_RangosEdad_id_rango_edad FOREIGN KEY (id_rango_edad) REFERENCES RangosEdad(id_rango_edad) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Clientes_Ocupaciones_id_ocupacion FOREIGN KEY (id_ocupacion) REFERENCES Ocupaciones(id_ocupacion) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT CHK_Clientes_nombre CHECK (LEN(LTRIM(RTRIM(nombre))) > 0),
CONSTRAINT CHK_Clientes_email CHECK (email IS NULL OR email LIKE '%@%.%')
);
GO

-- ============================================================
-- TABLA: Preguntas
-- ============================================================
CREATE TABLE Preguntas (
    id_pregunta          INT            IDENTITY(1,1)  NOT NULL,
    texto                VARCHAR(500)   NOT NULL,
    tipo_control         VARCHAR(50)    NOT NULL,
    id_categoria         INT            NOT NULL,
    orden                INT            NOT NULL  DEFAULT 0,
    obligatoria          BIT            NOT NULL  DEFAULT 1,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Preguntas PRIMARY KEY (id_pregunta),
CONSTRAINT FK_Preguntas_CategoriasPregunta_id_categoria FOREIGN KEY (id_categoria) REFERENCES CategoriasPregunta(id_categoria) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT CHK_Preguntas_tipo_control CHECK (tipo_control IN ('DropDownList','RadioButtonList','CheckBoxList','TextBox','TextArea')),
CONSTRAINT CHK_Preguntas_texto CHECK (LEN(LTRIM(RTRIM(texto))) > 0)
);
GO

-- ============================================================
-- TABLA: Encuestas
-- ============================================================
CREATE TABLE Encuestas (
    id_encuesta          INT            IDENTITY(1,1)  NOT NULL,
    id_cliente           INT            NOT NULL,
    id_usuario           INT            NULL,
    fecha_registro       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    ip_origen            VARCHAR(50)    NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Encuestas PRIMARY KEY (id_encuesta),
CONSTRAINT FK_Encuestas_Clientes_id_cliente FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Encuestas_Usuarios_id_usuario FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- ============================================================
-- TABLA: Respuestas
-- ============================================================
CREATE TABLE Respuestas (
    id_respuesta         INT            IDENTITY(1,1)  NOT NULL,
    id_encuesta          INT            NOT NULL,
    id_pregunta          INT            NOT NULL,
    valor_texto          VARCHAR(500)   NULL,
    valor_entero         INT            NULL,
    valor_decimal        DECIMAL(12,2)  NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Respuestas PRIMARY KEY (id_respuesta),
CONSTRAINT UQ_Respuestas_id_encuesta_id_pregunta UNIQUE (id_encuesta, id_pregunta),
CONSTRAINT FK_Respuestas_Encuestas_id_encuesta FOREIGN KEY (id_encuesta) REFERENCES Encuestas(id_encuesta) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Respuestas_Preguntas_id_pregunta FOREIGN KEY (id_pregunta) REFERENCES Preguntas(id_pregunta) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- ============================================================
-- TABLA: Auditorias
-- (tabla de bitácora; no lleva los campos de auditoría genéricos
--  porque ES el registro de auditoría del sistema)
-- ============================================================
CREATE TABLE Auditorias (
    id_auditoria          INT            IDENTITY(1,1)  NOT NULL,
    id_usuario            INT            NULL,
    accion                VARCHAR(50)    NOT NULL,
    tabla_afectada        VARCHAR(100)   NOT NULL,
    id_registro_afectado  INT            NULL,
    detalle               VARCHAR(500)   NULL,
    fecha_accion          DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    ip_origen             VARCHAR(50)    NULL,
    CONSTRAINT PK_Auditorias PRIMARY KEY (id_auditoria),
    CONSTRAINT FK_Auditorias_Usuarios FOREIGN KEY (id_usuario)
        REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CHK_Auditorias_Accion CHECK (accion IN ('INSERT','UPDATE','DELETE_LOGICO','RESTORE'))
);
GO

CREATE INDEX idx_auditorias_id_usuario     ON Auditorias (id_usuario);
CREATE INDEX idx_auditorias_fecha_accion   ON Auditorias (fecha_accion);
CREATE INDEX idx_auditorias_tabla_afectada ON Auditorias (tabla_afectada);
GO

-- ============================================================
-- Llaves Foráneas de Auditoría (usuario_creacion / usuario_modificacion)
-- ============================================================
ALTER TABLE Roles ADD CONSTRAINT FK_Roles_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Roles ADD CONSTRAINT FK_Roles_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Usuarios ADD CONSTRAINT FK_Usuarios_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Usuarios ADD CONSTRAINT FK_Usuarios_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE UsuariosRoles ADD CONSTRAINT FK_UsuariosRoles_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE UsuariosRoles ADD CONSTRAINT FK_UsuariosRoles_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Generos ADD CONSTRAINT FK_Generos_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Generos ADD CONSTRAINT FK_Generos_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE NivelesEducativos ADD CONSTRAINT FK_NivelesEducativos_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE NivelesEducativos ADD CONSTRAINT FK_NivelesEducativos_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE RangosIngresos ADD CONSTRAINT FK_RangosIngresos_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE RangosIngresos ADD CONSTRAINT FK_RangosIngresos_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE RangosEdad ADD CONSTRAINT FK_RangosEdad_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE RangosEdad ADD CONSTRAINT FK_RangosEdad_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Ocupaciones ADD CONSTRAINT FK_Ocupaciones_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Ocupaciones ADD CONSTRAINT FK_Ocupaciones_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE TiposPrestamo ADD CONSTRAINT FK_TiposPrestamo_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE TiposPrestamo ADD CONSTRAINT FK_TiposPrestamo_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Plazos ADD CONSTRAINT FK_Plazos_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Plazos ADD CONSTRAINT FK_Plazos_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE MontosRango ADD CONSTRAINT FK_MontosRango_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE MontosRango ADD CONSTRAINT FK_MontosRango_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE TasasInteresRango ADD CONSTRAINT FK_TasasInteresRango_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE TasasInteresRango ADD CONSTRAINT FK_TasasInteresRango_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE CapacidadesPago ADD CONSTRAINT FK_CapacidadesPago_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE CapacidadesPago ADD CONSTRAINT FK_CapacidadesPago_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE HistorialesCrediticios ADD CONSTRAINT FK_HistorialesCrediticios_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE HistorialesCrediticios ADD CONSTRAINT FK_HistorialesCrediticios_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE MediosContratacion ADD CONSTRAINT FK_MediosContratacion_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE MediosContratacion ADD CONSTRAINT FK_MediosContratacion_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE CategoriasPregunta ADD CONSTRAINT FK_CategoriasPregunta_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE CategoriasPregunta ADD CONSTRAINT FK_CategoriasPregunta_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Clientes ADD CONSTRAINT FK_Clientes_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Clientes ADD CONSTRAINT FK_Clientes_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Preguntas ADD CONSTRAINT FK_Preguntas_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Preguntas ADD CONSTRAINT FK_Preguntas_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Encuestas ADD CONSTRAINT FK_Encuestas_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Encuestas ADD CONSTRAINT FK_Encuestas_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Respuestas ADD CONSTRAINT FK_Respuestas_UsuarioCreacion FOREIGN KEY (usuario_creacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Respuestas ADD CONSTRAINT FK_Respuestas_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- ============================================================
-- Índices (prefijo idx_ según estándar) — uno por cada llave foránea
-- ============================================================
CREATE INDEX idx_roles_usuario_creacion ON Roles (usuario_creacion);
CREATE INDEX idx_roles_usuario_modificacion ON Roles (usuario_modificacion);
CREATE INDEX idx_usuarios_usuario_creacion ON Usuarios (usuario_creacion);
CREATE INDEX idx_usuarios_usuario_modificacion ON Usuarios (usuario_modificacion);
CREATE INDEX idx_usuariosroles_id_usuario ON UsuariosRoles (id_usuario);
CREATE INDEX idx_usuariosroles_id_rol ON UsuariosRoles (id_rol);
CREATE INDEX idx_usuariosroles_usuario_creacion ON UsuariosRoles (usuario_creacion);
CREATE INDEX idx_usuariosroles_usuario_modificacion ON UsuariosRoles (usuario_modificacion);
CREATE INDEX idx_generos_usuario_creacion ON Generos (usuario_creacion);
CREATE INDEX idx_generos_usuario_modificacion ON Generos (usuario_modificacion);
CREATE INDEX idx_niveleseducativos_usuario_creacion ON NivelesEducativos (usuario_creacion);
CREATE INDEX idx_niveleseducativos_usuario_modificacion ON NivelesEducativos (usuario_modificacion);
CREATE INDEX idx_rangosingresos_usuario_creacion ON RangosIngresos (usuario_creacion);
CREATE INDEX idx_rangosingresos_usuario_modificacion ON RangosIngresos (usuario_modificacion);
CREATE INDEX idx_rangosedad_usuario_creacion ON RangosEdad (usuario_creacion);
CREATE INDEX idx_rangosedad_usuario_modificacion ON RangosEdad (usuario_modificacion);
CREATE INDEX idx_ocupaciones_usuario_creacion ON Ocupaciones (usuario_creacion);
CREATE INDEX idx_ocupaciones_usuario_modificacion ON Ocupaciones (usuario_modificacion);
CREATE INDEX idx_tiposprestamo_usuario_creacion ON TiposPrestamo (usuario_creacion);
CREATE INDEX idx_tiposprestamo_usuario_modificacion ON TiposPrestamo (usuario_modificacion);
CREATE INDEX idx_plazos_usuario_creacion ON Plazos (usuario_creacion);
CREATE INDEX idx_plazos_usuario_modificacion ON Plazos (usuario_modificacion);
CREATE INDEX idx_montosrango_usuario_creacion ON MontosRango (usuario_creacion);
CREATE INDEX idx_montosrango_usuario_modificacion ON MontosRango (usuario_modificacion);
CREATE INDEX idx_tasasinteresrango_usuario_creacion ON TasasInteresRango (usuario_creacion);
CREATE INDEX idx_tasasinteresrango_usuario_modificacion ON TasasInteresRango (usuario_modificacion);
CREATE INDEX idx_capacidadespago_usuario_creacion ON CapacidadesPago (usuario_creacion);
CREATE INDEX idx_capacidadespago_usuario_modificacion ON CapacidadesPago (usuario_modificacion);
CREATE INDEX idx_historialescrediticios_usuario_creacion ON HistorialesCrediticios (usuario_creacion);
CREATE INDEX idx_historialescrediticios_usuario_modificacion ON HistorialesCrediticios (usuario_modificacion);
CREATE INDEX idx_medioscontratacion_usuario_creacion ON MediosContratacion (usuario_creacion);
CREATE INDEX idx_medioscontratacion_usuario_modificacion ON MediosContratacion (usuario_modificacion);
CREATE INDEX idx_categoriaspregunta_usuario_creacion ON CategoriasPregunta (usuario_creacion);
CREATE INDEX idx_categoriaspregunta_usuario_modificacion ON CategoriasPregunta (usuario_modificacion);
CREATE INDEX idx_clientes_id_genero ON Clientes (id_genero);
CREATE INDEX idx_clientes_id_nivel_educativo ON Clientes (id_nivel_educativo);
CREATE INDEX idx_clientes_id_rango_ingresos ON Clientes (id_rango_ingresos);
CREATE INDEX idx_clientes_id_rango_edad ON Clientes (id_rango_edad);
CREATE INDEX idx_clientes_id_ocupacion ON Clientes (id_ocupacion);
CREATE INDEX idx_clientes_usuario_creacion ON Clientes (usuario_creacion);
CREATE INDEX idx_clientes_usuario_modificacion ON Clientes (usuario_modificacion);
CREATE INDEX idx_preguntas_id_categoria ON Preguntas (id_categoria);
CREATE INDEX idx_preguntas_usuario_creacion ON Preguntas (usuario_creacion);
CREATE INDEX idx_preguntas_usuario_modificacion ON Preguntas (usuario_modificacion);
CREATE INDEX idx_encuestas_id_cliente ON Encuestas (id_cliente);
CREATE INDEX idx_encuestas_id_usuario ON Encuestas (id_usuario);
CREATE INDEX idx_encuestas_usuario_creacion ON Encuestas (usuario_creacion);
CREATE INDEX idx_encuestas_usuario_modificacion ON Encuestas (usuario_modificacion);
CREATE INDEX idx_respuestas_id_encuesta ON Respuestas (id_encuesta);
CREATE INDEX idx_respuestas_id_pregunta ON Respuestas (id_pregunta);
CREATE INDEX idx_respuestas_usuario_creacion ON Respuestas (usuario_creacion);
CREATE INDEX idx_respuestas_usuario_modificacion ON Respuestas (usuario_modificacion);
CREATE INDEX idx_encuestas_fecha_registro ON Encuestas (fecha_registro);
GO

-- ============================================================
-- Datos Insertados
-- ============================================================

-- Roles
INSERT INTO Roles (nombre_rol, descripcion) VALUES
    ('Administrador', 'Acceso total al sistema: CRUD, usuarios, catálogos y auditoría'),
    ('Encuestador',   'Puede registrar y consultar sus propias encuestas'),
    ('Consultor',     'Solo lectura: reportes y gráficos');
GO

-- Usuario Administrador por defecto
-- Contraseña: Admin2026! (SHA-256)
INSERT INTO Usuarios (nombre_usuario, hash_password, email, nombre) VALUES
    ('admin',
     'A2F4C8D1E3B5F7A9C1D2E4F6A8B0C2D4E6F8A0B2C4D6E8F0A2B4C6D8E0F2A4B6',
     'admin@sistcotprestamos.cr',
     'Administrador del Sistema');
GO

INSERT INTO UsuariosRoles (id_usuario, id_rol)
    SELECT id_usuario, id_rol FROM Usuarios, Roles
    WHERE nombre_usuario = 'admin' AND nombre_rol = 'Administrador';
GO

-- Género
INSERT INTO Generos (nombre) VALUES ('Masculino'),('Femenino'),('Otro');
GO

-- Nivel Educativo
INSERT INTO NivelesEducativos (nombre) VALUES
    ('Primaria'),('Secundaria'),('Técnico'),
    ('Universitario'),('Posgrado');
GO

-- Rango de Ingresos (USD mensuales)
INSERT INTO RangosIngresos (monto_minimo, monto_maximo, descripcion) VALUES
    (0,      499.99,  'Menos de $500'),
    (500,    999.99,  '$500 – $999'),
    (1000,   1999.99, '$1,000 – $1,999'),
    (2000,   3999.99, '$2,000 – $3,999'),
    (4000,   99999,   '$4,000 o más');
GO

-- Rango de Edad
INSERT INTO RangosEdad (edad_minima, edad_maxima, descripcion) VALUES
    (18, 25, '18 – 25 años'),
    (26, 35, '26 – 35 años'),
    (36, 45, '36 – 45 años'),
    (46, 55, '46 – 55 años'),
    (56, 99, '56 años o más');
GO

-- Ocupación
INSERT INTO Ocupaciones (nombre) VALUES
    ('Empleado público'),('Empleado privado'),
    ('Trabajador independiente'),('Empresario'),
    ('Estudiante'),('Pensionado');
GO

-- Tipo de Préstamo
INSERT INTO TiposPrestamo (nombre, descripcion) VALUES
    ('Terreno',       'Adquisición de lotes o terrenos'),
    ('Vehículo',      'Compra de vehículo nuevo o usado'),
    ('Estudios',      'Financiamiento de estudios universitarios o técnicos'),
    ('Personal',      'Préstamo personal de libre inversión'),
    ('Remodelación',  'Mejoras y remodelación del hogar');
GO

-- Plazo
INSERT INTO Plazos (meses, descripcion) VALUES
    (6,  '6 meses'),
    (12, '12 meses (1 año)'),
    (24, '24 meses (2 años)'),
    (36, '36 meses (3 años)'),
    (48, '48 meses (4 años)'),
    (60, '60 meses (5 años)');
GO

-- Monto Rango
INSERT INTO MontosRango (monto_minimo, monto_maximo, descripcion) VALUES
    (0,      4999.99,  'Menos de $5,000'),
    (5000,   10000,    '$5,000 – $10,000'),
    (10001,  15000,    '$10,001 – $15,000'),
    (15001,  9999999,  'Más de $15,000');
GO

-- Tasa Interés Rango
INSERT INTO TasasInteresRango (tasa_minima, tasa_maxima, descripcion) VALUES
    (0,    4.99,  'Menos del 5%'),
    (5,    9.99,  '5% – 9.99%'),
    (10,   14.99, '10% – 14.99%'),
    (15,   100,   '15% o más');
GO

-- Capacidad de Pago
INSERT INTO CapacidadesPago (monto_minimo, monto_maximo, descripcion) VALUES
    (0,   99.99,  'Menos de $100 mensuales'),
    (100, 300,    '$100 – $300 mensuales'),
    (301, 500,    '$301 – $500 mensuales'),
    (501, 999999, 'Más de $500 mensuales');
GO

-- Historial Crediticio
INSERT INTO HistorialesCrediticios (tiene_prestamos_previos, ha_morado, descripcion) VALUES
    (0, 0, 'Sin préstamos previos, sin morosidad'),
    (1, 0, 'Con préstamos previos, sin morosidad'),
    (1, 1, 'Con préstamos previos y con morosidad'),
    (0, 1, 'Sin préstamos previos pero con morosidad registrada');
GO

-- Medio de Contratación
INSERT INTO MediosContratacion (nombre) VALUES
    ('Aplicación móvil'),('Sitio web'),('Sucursal presencial');
GO

-- Categorías de Preguntas
INSERT INTO CategoriasPregunta (nombre, descripcion) VALUES
    ('Datos Generales',            'Información demográfica e identidad del encuestado'),
    ('Situación Financiera',       'Ingresos, capacidad de pago y ocupación'),
    ('Tipo de Préstamo',           'Finalidad y monto del préstamo deseado'),
    ('Historial Crediticio',       'Comportamiento crediticio previo del encuestado'),
    ('Preferencias de Contratación','Canal y plazo preferidos para contratar el préstamo');
GO

-- Preguntas (20 funcionales)
INSERT INTO Preguntas (texto, tipo_control, id_categoria, orden, obligatoria) VALUES
-- Categoría 1: Datos Generales (id_categoria = 1)
('¿Cuál es su género?',                              'RadioButtonList', 1, 1,  1),
('¿A qué rango de edad pertenece?',                  'DropDownList',    1, 2,  1),
('¿Cuál es su nivel educativo más alto alcanzado?',  'DropDownList',    1, 3,  1),
('¿Cuál es su nombre completo?',                     'TextBox',         1, 4,  1),
('¿Cuál es su correo electrónico?',                  'TextBox',         1, 5,  0),
-- Categoría 2: Situación Financiera (id_categoria = 2)
('¿Cuál es su ocupación actual?',                    'DropDownList',    2, 6,  1),
('¿Cuál es su rango de ingresos mensuales?',         'DropDownList',    2, 7,  1),
('¿Cuánto podría pagar mensualmente por un préstamo?','DropDownList',   2, 8,  1),
('¿Tiene otros compromisos financieros activos?',    'RadioButtonList', 2, 9,  1),
('¿Cuántos dependientes económicos tiene?',          'DropDownList',    2, 10, 1),
-- Categoría 3: Tipo de Préstamo (id_categoria = 3)
('¿Para qué tipo de préstamo está cotizando?',       'DropDownList',    3, 11, 1),
('¿Cuál es el monto que desea solicitar?',           'DropDownList',    3, 12, 1),
('¿Qué tasa de interés anual estaría dispuesto a aceptar?','DropDownList',3,13,1),
('¿En cuántos meses desea pagar el préstamo?',       'DropDownList',    3, 14, 1),
('¿Ha calculado el monto de la cuota que podría asumir?','RadioButtonList',3,15,1),
-- Categoría 4: Historial Crediticio (id_categoria = 4)
('¿Ha tenido préstamos en entidades financieras anteriormente?','RadioButtonList',4,16,1),
('¿Ha presentado morosidad en algún préstamo previo?','RadioButtonList',4,17,1),
('¿Está actualmente reportado en un buró de crédito?','RadioButtonList',4,18,1),
-- Categoría 5: Preferencias de Contratación (id_categoria = 5)
('¿Por cuál medio preferiría contratar el préstamo?','CheckBoxList',    5, 19, 1),
('¿Le gustaría recibir asesoría personalizada antes de contratar?','RadioButtonList',5,20,1);
GO

-- ============================================================
-- Procedimientos Almacenados CRUD (estándar sp_Accion+Entidad)
-- ============================================================

-- ===== Roles =====
-- ============================================================
-- sp_InsertarRol
-- ============================================================
CREATE PROCEDURE sp_InsertarRol
    @NombreRol VARCHAR(50),
    @Descripcion VARCHAR(200) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM Roles WHERE nombre_rol = @NombreRol AND activo = 1)
                RAISERROR('Ya existe un registro de Roles con ese valor.', 16, 1);

            INSERT INTO Roles (nombre_rol, descripcion, usuario_creacion)
            VALUES (@NombreRol, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarRol
-- ============================================================
CREATE PROCEDURE sp_ActualizarRol
    @RolID INT,
    @NombreRol VARCHAR(50),
    @Descripcion VARCHAR(200) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Roles WHERE id_rol = @RolID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Roles
            SET nombre_rol = @NombreRol,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_rol = @RolID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerRolPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerRolPorId
    @RolID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Roles
        WHERE  id_rol = @RolID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarRoles
-- ============================================================
CREATE PROCEDURE sp_ListarRoles
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Roles
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre_rol;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoRol
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoRol
    @RolID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Roles WHERE id_rol = @RolID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Roles
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_rol = @RolID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarRol
-- ============================================================
CREATE PROCEDURE sp_RestaurarRol
    @RolID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Roles WHERE id_rol = @RolID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Roles
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_rol = @RolID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Usuarios =====
-- ============================================================
-- sp_InsertarUsuario
-- ============================================================
CREATE PROCEDURE sp_InsertarUsuario
    @NombreUsuario VARCHAR(100),
    @HashPassword VARCHAR(256),
    @Email VARCHAR(150),
    @Nombre VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM Usuarios WHERE nombre_usuario = @NombreUsuario AND activo = 1)
                RAISERROR('Ya existe un registro de Usuarios con ese valor.', 16, 1);

            INSERT INTO Usuarios (nombre_usuario, hash_password, email, nombre, usuario_creacion)
            VALUES (@NombreUsuario, @HashPassword, @Email, @Nombre, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarUsuario
-- ============================================================
CREATE PROCEDURE sp_ActualizarUsuario
    @UsuarioID INT,
    @NombreUsuario VARCHAR(100),
    @HashPassword VARCHAR(256),
    @Email VARCHAR(150),
    @Nombre VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE id_usuario = @UsuarioID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Usuarios
            SET nombre_usuario = @NombreUsuario,
            hash_password = @HashPassword,
            email = @Email,
            nombre = @Nombre,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_usuario = @UsuarioID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerUsuarioPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerUsuarioPorId
    @UsuarioID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Usuarios
        WHERE  id_usuario = @UsuarioID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarUsuarios
-- ============================================================
CREATE PROCEDURE sp_ListarUsuarios
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Usuarios
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre_usuario;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoUsuario
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoUsuario
    @UsuarioID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE id_usuario = @UsuarioID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Usuarios
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_usuario = @UsuarioID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarUsuario
-- ============================================================
CREATE PROCEDURE sp_RestaurarUsuario
    @UsuarioID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE id_usuario = @UsuarioID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Usuarios
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_usuario = @UsuarioID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== UsuariosRoles =====
-- ============================================================
-- sp_InsertarUsuarioRol
-- ============================================================
CREATE PROCEDURE sp_InsertarUsuarioRol
    @UsuarioID INT,
    @RolID INT,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO UsuariosRoles (id_usuario, id_rol, usuario_creacion)
            VALUES (@UsuarioID, @RolID, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarUsuarioRol
-- ============================================================
CREATE PROCEDURE sp_ActualizarUsuarioRol
    @UsuarioRolID INT,
    @UsuarioID INT,
    @RolID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM UsuariosRoles WHERE id_usuario_rol = @UsuarioRolID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE UsuariosRoles
            SET id_usuario = @UsuarioID,
            id_rol = @RolID,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_usuario_rol = @UsuarioRolID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerUsuarioRolPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerUsuarioRolPorId
    @UsuarioRolID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   UsuariosRoles
        WHERE  id_usuario_rol = @UsuarioRolID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarUsuariosRoles
-- ============================================================
CREATE PROCEDURE sp_ListarUsuariosRoles
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   UsuariosRoles
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY id_usuario;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoUsuarioRol
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoUsuarioRol
    @UsuarioRolID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM UsuariosRoles WHERE id_usuario_rol = @UsuarioRolID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE UsuariosRoles
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_usuario_rol = @UsuarioRolID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarUsuarioRol
-- ============================================================
CREATE PROCEDURE sp_RestaurarUsuarioRol
    @UsuarioRolID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM UsuariosRoles WHERE id_usuario_rol = @UsuarioRolID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE UsuariosRoles
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_usuario_rol = @UsuarioRolID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Generos =====
-- ============================================================
-- sp_InsertarGenero
-- ============================================================
CREATE PROCEDURE sp_InsertarGenero
    @Nombre VARCHAR(50),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM Generos WHERE nombre = @Nombre AND activo = 1)
                RAISERROR('Ya existe un registro de Generos con ese valor.', 16, 1);

            INSERT INTO Generos (nombre, usuario_creacion)
            VALUES (@Nombre, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarGenero
-- ============================================================
CREATE PROCEDURE sp_ActualizarGenero
    @GeneroID INT,
    @Nombre VARCHAR(50),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Generos WHERE id_genero = @GeneroID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Generos
            SET nombre = @Nombre,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_genero = @GeneroID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerGeneroPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerGeneroPorId
    @GeneroID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Generos
        WHERE  id_genero = @GeneroID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarGeneros
-- ============================================================
CREATE PROCEDURE sp_ListarGeneros
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Generos
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoGenero
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoGenero
    @GeneroID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Generos WHERE id_genero = @GeneroID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Generos
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_genero = @GeneroID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarGenero
-- ============================================================
CREATE PROCEDURE sp_RestaurarGenero
    @GeneroID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Generos WHERE id_genero = @GeneroID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Generos
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_genero = @GeneroID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== NivelesEducativos =====
-- ============================================================
-- sp_InsertarNivelEducativo
-- ============================================================
CREATE PROCEDURE sp_InsertarNivelEducativo
    @Nombre VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM NivelesEducativos WHERE nombre = @Nombre AND activo = 1)
                RAISERROR('Ya existe un registro de NivelesEducativos con ese valor.', 16, 1);

            INSERT INTO NivelesEducativos (nombre, usuario_creacion)
            VALUES (@Nombre, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarNivelEducativo
-- ============================================================
CREATE PROCEDURE sp_ActualizarNivelEducativo
    @NivelEducativoID INT,
    @Nombre VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM NivelesEducativos WHERE id_nivel_educativo = @NivelEducativoID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE NivelesEducativos
            SET nombre = @Nombre,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_nivel_educativo = @NivelEducativoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerNivelEducativoPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerNivelEducativoPorId
    @NivelEducativoID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   NivelesEducativos
        WHERE  id_nivel_educativo = @NivelEducativoID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarNivelesEducativos
-- ============================================================
CREATE PROCEDURE sp_ListarNivelesEducativos
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   NivelesEducativos
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoNivelEducativo
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoNivelEducativo
    @NivelEducativoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM NivelesEducativos WHERE id_nivel_educativo = @NivelEducativoID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE NivelesEducativos
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_nivel_educativo = @NivelEducativoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarNivelEducativo
-- ============================================================
CREATE PROCEDURE sp_RestaurarNivelEducativo
    @NivelEducativoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM NivelesEducativos WHERE id_nivel_educativo = @NivelEducativoID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE NivelesEducativos
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_nivel_educativo = @NivelEducativoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== RangosIngresos =====
-- ============================================================
-- sp_InsertarRangoIngresos
-- ============================================================
CREATE PROCEDURE sp_InsertarRangoIngresos
    @MontoMinimo DECIMAL(12,2),
    @MontoMaximo DECIMAL(12,2),
    @Descripcion VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO RangosIngresos (monto_minimo, monto_maximo, descripcion, usuario_creacion)
            VALUES (@MontoMinimo, @MontoMaximo, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarRangoIngresos
-- ============================================================
CREATE PROCEDURE sp_ActualizarRangoIngresos
    @RangoIngresosID INT,
    @MontoMinimo DECIMAL(12,2),
    @MontoMaximo DECIMAL(12,2),
    @Descripcion VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM RangosIngresos WHERE id_rango_ingresos = @RangoIngresosID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE RangosIngresos
            SET monto_minimo = @MontoMinimo,
            monto_maximo = @MontoMaximo,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_rango_ingresos = @RangoIngresosID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerRangoIngresosPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerRangoIngresosPorId
    @RangoIngresosID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   RangosIngresos
        WHERE  id_rango_ingresos = @RangoIngresosID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarRangosIngresos
-- ============================================================
CREATE PROCEDURE sp_ListarRangosIngresos
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   RangosIngresos
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY monto_minimo;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoRangoIngresos
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoRangoIngresos
    @RangoIngresosID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM RangosIngresos WHERE id_rango_ingresos = @RangoIngresosID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE RangosIngresos
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_rango_ingresos = @RangoIngresosID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarRangoIngresos
-- ============================================================
CREATE PROCEDURE sp_RestaurarRangoIngresos
    @RangoIngresosID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM RangosIngresos WHERE id_rango_ingresos = @RangoIngresosID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE RangosIngresos
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_rango_ingresos = @RangoIngresosID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== RangosEdad =====
-- ============================================================
-- sp_InsertarRangoEdad
-- ============================================================
CREATE PROCEDURE sp_InsertarRangoEdad
    @EdadMinima INT,
    @EdadMaxima INT,
    @Descripcion VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO RangosEdad (edad_minima, edad_maxima, descripcion, usuario_creacion)
            VALUES (@EdadMinima, @EdadMaxima, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarRangoEdad
-- ============================================================
CREATE PROCEDURE sp_ActualizarRangoEdad
    @RangoEdadID INT,
    @EdadMinima INT,
    @EdadMaxima INT,
    @Descripcion VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM RangosEdad WHERE id_rango_edad = @RangoEdadID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE RangosEdad
            SET edad_minima = @EdadMinima,
            edad_maxima = @EdadMaxima,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_rango_edad = @RangoEdadID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerRangoEdadPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerRangoEdadPorId
    @RangoEdadID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   RangosEdad
        WHERE  id_rango_edad = @RangoEdadID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarRangosEdad
-- ============================================================
CREATE PROCEDURE sp_ListarRangosEdad
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   RangosEdad
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY edad_minima;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoRangoEdad
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoRangoEdad
    @RangoEdadID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM RangosEdad WHERE id_rango_edad = @RangoEdadID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE RangosEdad
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_rango_edad = @RangoEdadID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarRangoEdad
-- ============================================================
CREATE PROCEDURE sp_RestaurarRangoEdad
    @RangoEdadID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM RangosEdad WHERE id_rango_edad = @RangoEdadID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE RangosEdad
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_rango_edad = @RangoEdadID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Ocupaciones =====
-- ============================================================
-- sp_InsertarOcupacion
-- ============================================================
CREATE PROCEDURE sp_InsertarOcupacion
    @Nombre VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM Ocupaciones WHERE nombre = @Nombre AND activo = 1)
                RAISERROR('Ya existe un registro de Ocupaciones con ese valor.', 16, 1);

            INSERT INTO Ocupaciones (nombre, usuario_creacion)
            VALUES (@Nombre, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarOcupacion
-- ============================================================
CREATE PROCEDURE sp_ActualizarOcupacion
    @OcupacionID INT,
    @Nombre VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Ocupaciones WHERE id_ocupacion = @OcupacionID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Ocupaciones
            SET nombre = @Nombre,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_ocupacion = @OcupacionID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerOcupacionPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerOcupacionPorId
    @OcupacionID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Ocupaciones
        WHERE  id_ocupacion = @OcupacionID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarOcupaciones
-- ============================================================
CREATE PROCEDURE sp_ListarOcupaciones
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Ocupaciones
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoOcupacion
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoOcupacion
    @OcupacionID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Ocupaciones WHERE id_ocupacion = @OcupacionID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Ocupaciones
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_ocupacion = @OcupacionID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarOcupacion
-- ============================================================
CREATE PROCEDURE sp_RestaurarOcupacion
    @OcupacionID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Ocupaciones WHERE id_ocupacion = @OcupacionID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Ocupaciones
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_ocupacion = @OcupacionID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== TiposPrestamo =====
-- ============================================================
-- sp_InsertarTipoPrestamo
-- ============================================================
CREATE PROCEDURE sp_InsertarTipoPrestamo
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(250) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM TiposPrestamo WHERE nombre = @Nombre AND activo = 1)
                RAISERROR('Ya existe un registro de TiposPrestamo con ese valor.', 16, 1);

            INSERT INTO TiposPrestamo (nombre, descripcion, usuario_creacion)
            VALUES (@Nombre, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarTipoPrestamo
-- ============================================================
CREATE PROCEDURE sp_ActualizarTipoPrestamo
    @TipoPrestamoID INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(250) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM TiposPrestamo WHERE id_tipo_prestamo = @TipoPrestamoID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE TiposPrestamo
            SET nombre = @Nombre,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_tipo_prestamo = @TipoPrestamoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerTipoPrestamoPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerTipoPrestamoPorId
    @TipoPrestamoID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   TiposPrestamo
        WHERE  id_tipo_prestamo = @TipoPrestamoID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarTiposPrestamo
-- ============================================================
CREATE PROCEDURE sp_ListarTiposPrestamo
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   TiposPrestamo
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoTipoPrestamo
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoTipoPrestamo
    @TipoPrestamoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM TiposPrestamo WHERE id_tipo_prestamo = @TipoPrestamoID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE TiposPrestamo
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_tipo_prestamo = @TipoPrestamoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarTipoPrestamo
-- ============================================================
CREATE PROCEDURE sp_RestaurarTipoPrestamo
    @TipoPrestamoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM TiposPrestamo WHERE id_tipo_prestamo = @TipoPrestamoID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE TiposPrestamo
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_tipo_prestamo = @TipoPrestamoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Plazos =====
-- ============================================================
-- sp_InsertarPlazo
-- ============================================================
CREATE PROCEDURE sp_InsertarPlazo
    @Meses INT,
    @Descripcion VARCHAR(100) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM Plazos WHERE meses = @Meses AND activo = 1)
                RAISERROR('Ya existe un registro de Plazos con ese valor.', 16, 1);

            INSERT INTO Plazos (meses, descripcion, usuario_creacion)
            VALUES (@Meses, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarPlazo
-- ============================================================
CREATE PROCEDURE sp_ActualizarPlazo
    @PlazoID INT,
    @Meses INT,
    @Descripcion VARCHAR(100) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Plazos WHERE id_plazo = @PlazoID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Plazos
            SET meses = @Meses,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_plazo = @PlazoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerPlazoPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerPlazoPorId
    @PlazoID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Plazos
        WHERE  id_plazo = @PlazoID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarPlazos
-- ============================================================
CREATE PROCEDURE sp_ListarPlazos
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Plazos
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY meses;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoPlazo
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoPlazo
    @PlazoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Plazos WHERE id_plazo = @PlazoID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Plazos
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_plazo = @PlazoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarPlazo
-- ============================================================
CREATE PROCEDURE sp_RestaurarPlazo
    @PlazoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Plazos WHERE id_plazo = @PlazoID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Plazos
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_plazo = @PlazoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== MontosRango =====
-- ============================================================
-- sp_InsertarMontoRango
-- ============================================================
CREATE PROCEDURE sp_InsertarMontoRango
    @MontoMinimo DECIMAL(12,2),
    @MontoMaximo DECIMAL(12,2),
    @Descripcion VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO MontosRango (monto_minimo, monto_maximo, descripcion, usuario_creacion)
            VALUES (@MontoMinimo, @MontoMaximo, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarMontoRango
-- ============================================================
CREATE PROCEDURE sp_ActualizarMontoRango
    @MontoRangoID INT,
    @MontoMinimo DECIMAL(12,2),
    @MontoMaximo DECIMAL(12,2),
    @Descripcion VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM MontosRango WHERE id_monto_rango = @MontoRangoID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE MontosRango
            SET monto_minimo = @MontoMinimo,
            monto_maximo = @MontoMaximo,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_monto_rango = @MontoRangoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerMontoRangoPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerMontoRangoPorId
    @MontoRangoID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   MontosRango
        WHERE  id_monto_rango = @MontoRangoID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarMontosRango
-- ============================================================
CREATE PROCEDURE sp_ListarMontosRango
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   MontosRango
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY monto_minimo;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoMontoRango
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoMontoRango
    @MontoRangoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM MontosRango WHERE id_monto_rango = @MontoRangoID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE MontosRango
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_monto_rango = @MontoRangoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarMontoRango
-- ============================================================
CREATE PROCEDURE sp_RestaurarMontoRango
    @MontoRangoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM MontosRango WHERE id_monto_rango = @MontoRangoID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE MontosRango
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_monto_rango = @MontoRangoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== TasasInteresRango =====
-- ============================================================
-- sp_InsertarTasaInteresRango
-- ============================================================
CREATE PROCEDURE sp_InsertarTasaInteresRango
    @TasaMinima DECIMAL(5,2),
    @TasaMaxima DECIMAL(5,2),
    @Descripcion VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO TasasInteresRango (tasa_minima, tasa_maxima, descripcion, usuario_creacion)
            VALUES (@TasaMinima, @TasaMaxima, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarTasaInteresRango
-- ============================================================
CREATE PROCEDURE sp_ActualizarTasaInteresRango
    @TasaRangoID INT,
    @TasaMinima DECIMAL(5,2),
    @TasaMaxima DECIMAL(5,2),
    @Descripcion VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM TasasInteresRango WHERE id_tasa_rango = @TasaRangoID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE TasasInteresRango
            SET tasa_minima = @TasaMinima,
            tasa_maxima = @TasaMaxima,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_tasa_rango = @TasaRangoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerTasaInteresRangoPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerTasaInteresRangoPorId
    @TasaRangoID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   TasasInteresRango
        WHERE  id_tasa_rango = @TasaRangoID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarTasasInteresRango
-- ============================================================
CREATE PROCEDURE sp_ListarTasasInteresRango
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   TasasInteresRango
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY tasa_minima;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoTasaInteresRango
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoTasaInteresRango
    @TasaRangoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM TasasInteresRango WHERE id_tasa_rango = @TasaRangoID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE TasasInteresRango
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_tasa_rango = @TasaRangoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarTasaInteresRango
-- ============================================================
CREATE PROCEDURE sp_RestaurarTasaInteresRango
    @TasaRangoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM TasasInteresRango WHERE id_tasa_rango = @TasaRangoID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE TasasInteresRango
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_tasa_rango = @TasaRangoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== CapacidadesPago =====
-- ============================================================
-- sp_InsertarCapacidadPago
-- ============================================================
CREATE PROCEDURE sp_InsertarCapacidadPago
    @MontoMinimo DECIMAL(10,2),
    @MontoMaximo DECIMAL(10,2),
    @Descripcion VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO CapacidadesPago (monto_minimo, monto_maximo, descripcion, usuario_creacion)
            VALUES (@MontoMinimo, @MontoMaximo, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarCapacidadPago
-- ============================================================
CREATE PROCEDURE sp_ActualizarCapacidadPago
    @CapacidadPagoID INT,
    @MontoMinimo DECIMAL(10,2),
    @MontoMaximo DECIMAL(10,2),
    @Descripcion VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM CapacidadesPago WHERE id_capacidad_pago = @CapacidadPagoID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE CapacidadesPago
            SET monto_minimo = @MontoMinimo,
            monto_maximo = @MontoMaximo,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_capacidad_pago = @CapacidadPagoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerCapacidadPagoPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerCapacidadPagoPorId
    @CapacidadPagoID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   CapacidadesPago
        WHERE  id_capacidad_pago = @CapacidadPagoID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarCapacidadesPago
-- ============================================================
CREATE PROCEDURE sp_ListarCapacidadesPago
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   CapacidadesPago
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY monto_minimo;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoCapacidadPago
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoCapacidadPago
    @CapacidadPagoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM CapacidadesPago WHERE id_capacidad_pago = @CapacidadPagoID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE CapacidadesPago
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_capacidad_pago = @CapacidadPagoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarCapacidadPago
-- ============================================================
CREATE PROCEDURE sp_RestaurarCapacidadPago
    @CapacidadPagoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM CapacidadesPago WHERE id_capacidad_pago = @CapacidadPagoID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE CapacidadesPago
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_capacidad_pago = @CapacidadPagoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== HistorialesCrediticios =====
-- ============================================================
-- sp_InsertarHistorialCrediticio
-- ============================================================
CREATE PROCEDURE sp_InsertarHistorialCrediticio
    @TienePrestamosPrevios BIT,
    @HaMorado BIT,
    @Descripcion VARCHAR(200) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO HistorialesCrediticios (tiene_prestamos_previos, ha_morado, descripcion, usuario_creacion)
            VALUES (@TienePrestamosPrevios, @HaMorado, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarHistorialCrediticio
-- ============================================================
CREATE PROCEDURE sp_ActualizarHistorialCrediticio
    @HistorialID INT,
    @TienePrestamosPrevios BIT,
    @HaMorado BIT,
    @Descripcion VARCHAR(200) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM HistorialesCrediticios WHERE id_historial = @HistorialID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE HistorialesCrediticios
            SET tiene_prestamos_previos = @TienePrestamosPrevios,
            ha_morado = @HaMorado,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_historial = @HistorialID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerHistorialCrediticioPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerHistorialCrediticioPorId
    @HistorialID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   HistorialesCrediticios
        WHERE  id_historial = @HistorialID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarHistorialesCrediticios
-- ============================================================
CREATE PROCEDURE sp_ListarHistorialesCrediticios
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   HistorialesCrediticios
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY tiene_prestamos_previos;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoHistorialCrediticio
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoHistorialCrediticio
    @HistorialID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM HistorialesCrediticios WHERE id_historial = @HistorialID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE HistorialesCrediticios
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_historial = @HistorialID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarHistorialCrediticio
-- ============================================================
CREATE PROCEDURE sp_RestaurarHistorialCrediticio
    @HistorialID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM HistorialesCrediticios WHERE id_historial = @HistorialID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE HistorialesCrediticios
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_historial = @HistorialID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== MediosContratacion =====
-- ============================================================
-- sp_InsertarMedioContratacion
-- ============================================================
CREATE PROCEDURE sp_InsertarMedioContratacion
    @Nombre VARCHAR(100),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM MediosContratacion WHERE nombre = @Nombre AND activo = 1)
                RAISERROR('Ya existe un registro de MediosContratacion con ese valor.', 16, 1);

            INSERT INTO MediosContratacion (nombre, usuario_creacion)
            VALUES (@Nombre, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarMedioContratacion
-- ============================================================
CREATE PROCEDURE sp_ActualizarMedioContratacion
    @MedioID INT,
    @Nombre VARCHAR(100),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM MediosContratacion WHERE id_medio = @MedioID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE MediosContratacion
            SET nombre = @Nombre,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_medio = @MedioID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerMedioContratacionPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerMedioContratacionPorId
    @MedioID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   MediosContratacion
        WHERE  id_medio = @MedioID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarMediosContratacion
-- ============================================================
CREATE PROCEDURE sp_ListarMediosContratacion
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   MediosContratacion
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoMedioContratacion
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoMedioContratacion
    @MedioID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM MediosContratacion WHERE id_medio = @MedioID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE MediosContratacion
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_medio = @MedioID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarMedioContratacion
-- ============================================================
CREATE PROCEDURE sp_RestaurarMedioContratacion
    @MedioID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM MediosContratacion WHERE id_medio = @MedioID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE MediosContratacion
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_medio = @MedioID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== CategoriasPregunta =====
-- ============================================================
-- sp_InsertarCategoriaPregunta
-- ============================================================
CREATE PROCEDURE sp_InsertarCategoriaPregunta
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(250) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM CategoriasPregunta WHERE nombre = @Nombre AND activo = 1)
                RAISERROR('Ya existe un registro de CategoriasPregunta con ese valor.', 16, 1);

            INSERT INTO CategoriasPregunta (nombre, descripcion, usuario_creacion)
            VALUES (@Nombre, @Descripcion, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarCategoriaPregunta
-- ============================================================
CREATE PROCEDURE sp_ActualizarCategoriaPregunta
    @CategoriaID INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(250) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM CategoriasPregunta WHERE id_categoria = @CategoriaID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE CategoriasPregunta
            SET nombre = @Nombre,
            descripcion = @Descripcion,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_categoria = @CategoriaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerCategoriaPreguntaPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerCategoriaPreguntaPorId
    @CategoriaID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   CategoriasPregunta
        WHERE  id_categoria = @CategoriaID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarCategoriasPregunta
-- ============================================================
CREATE PROCEDURE sp_ListarCategoriasPregunta
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   CategoriasPregunta
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoCategoriaPregunta
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoCategoriaPregunta
    @CategoriaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM CategoriasPregunta WHERE id_categoria = @CategoriaID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE CategoriasPregunta
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_categoria = @CategoriaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarCategoriaPregunta
-- ============================================================
CREATE PROCEDURE sp_RestaurarCategoriaPregunta
    @CategoriaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM CategoriasPregunta WHERE id_categoria = @CategoriaID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE CategoriasPregunta
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_categoria = @CategoriaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Clientes =====
-- ============================================================
-- sp_InsertarCliente
-- ============================================================
CREATE PROCEDURE sp_InsertarCliente
    @Nombre VARCHAR(100),
    @Apellidos VARCHAR(150),
    @Email VARCHAR(150) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @GeneroID INT,
    @NivelEducativoID INT,
    @RangoIngresosID INT,
    @RangoEdadID INT,
    @OcupacionID INT = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Clientes (nombre, apellidos, email, telefono, id_genero, id_nivel_educativo, id_rango_ingresos, id_rango_edad, id_ocupacion, usuario_creacion)
            VALUES (@Nombre, @Apellidos, @Email, @Telefono, @GeneroID, @NivelEducativoID, @RangoIngresosID, @RangoEdadID, @OcupacionID, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarCliente
-- ============================================================
CREATE PROCEDURE sp_ActualizarCliente
    @ClienteID INT,
    @Nombre VARCHAR(100),
    @Apellidos VARCHAR(150),
    @Email VARCHAR(150) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @GeneroID INT,
    @NivelEducativoID INT,
    @RangoIngresosID INT,
    @RangoEdadID INT,
    @OcupacionID INT = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Clientes WHERE id_cliente = @ClienteID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Clientes
            SET nombre = @Nombre,
            apellidos = @Apellidos,
            email = @Email,
            telefono = @Telefono,
            id_genero = @GeneroID,
            id_nivel_educativo = @NivelEducativoID,
            id_rango_ingresos = @RangoIngresosID,
            id_rango_edad = @RangoEdadID,
            id_ocupacion = @OcupacionID,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_cliente = @ClienteID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerClientePorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerClientePorId
    @ClienteID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Clientes
        WHERE  id_cliente = @ClienteID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarClientes
-- ============================================================
CREATE PROCEDURE sp_ListarClientes
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Clientes
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoCliente
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoCliente
    @ClienteID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Clientes WHERE id_cliente = @ClienteID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Clientes
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_cliente = @ClienteID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarCliente
-- ============================================================
CREATE PROCEDURE sp_RestaurarCliente
    @ClienteID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Clientes WHERE id_cliente = @ClienteID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Clientes
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_cliente = @ClienteID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Preguntas =====
-- ============================================================
-- sp_InsertarPregunta
-- ============================================================
CREATE PROCEDURE sp_InsertarPregunta
    @Texto VARCHAR(500),
    @TipoControl VARCHAR(50),
    @CategoriaID INT,
    @Orden INT,
    @Obligatoria BIT,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Preguntas (texto, tipo_control, id_categoria, orden, obligatoria, usuario_creacion)
            VALUES (@Texto, @TipoControl, @CategoriaID, @Orden, @Obligatoria, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarPregunta
-- ============================================================
CREATE PROCEDURE sp_ActualizarPregunta
    @PreguntaID INT,
    @Texto VARCHAR(500),
    @TipoControl VARCHAR(50),
    @CategoriaID INT,
    @Orden INT,
    @Obligatoria BIT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Preguntas WHERE id_pregunta = @PreguntaID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Preguntas
            SET texto = @Texto,
            tipo_control = @TipoControl,
            id_categoria = @CategoriaID,
            orden = @Orden,
            obligatoria = @Obligatoria,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_pregunta = @PreguntaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerPreguntaPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerPreguntaPorId
    @PreguntaID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Preguntas
        WHERE  id_pregunta = @PreguntaID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarPreguntas
-- ============================================================
CREATE PROCEDURE sp_ListarPreguntas
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Preguntas
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY texto;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoPregunta
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoPregunta
    @PreguntaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Preguntas WHERE id_pregunta = @PreguntaID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Preguntas
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_pregunta = @PreguntaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarPregunta
-- ============================================================
CREATE PROCEDURE sp_RestaurarPregunta
    @PreguntaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Preguntas WHERE id_pregunta = @PreguntaID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Preguntas
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_pregunta = @PreguntaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Encuestas =====
-- ============================================================
-- sp_InsertarEncuesta
-- ============================================================
CREATE PROCEDURE sp_InsertarEncuesta
    @ClienteID INT,
    @UsuarioID INT = NULL,
    @FechaRegistro DATETIME2(3),
    @IpOrigen VARCHAR(50) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Encuestas (id_cliente, id_usuario, fecha_registro, ip_origen, usuario_creacion)
            VALUES (@ClienteID, @UsuarioID, @FechaRegistro, @IpOrigen, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarEncuesta
-- ============================================================
CREATE PROCEDURE sp_ActualizarEncuesta
    @EncuestaID INT,
    @ClienteID INT,
    @UsuarioID INT = NULL,
    @FechaRegistro DATETIME2(3),
    @IpOrigen VARCHAR(50) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Encuestas WHERE id_encuesta = @EncuestaID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Encuestas
            SET id_cliente = @ClienteID,
            id_usuario = @UsuarioID,
            fecha_registro = @FechaRegistro,
            ip_origen = @IpOrigen,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_encuesta = @EncuestaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerEncuestaPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerEncuestaPorId
    @EncuestaID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Encuestas
        WHERE  id_encuesta = @EncuestaID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarEncuestas
-- ============================================================
CREATE PROCEDURE sp_ListarEncuestas
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Encuestas
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY id_cliente;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoEncuesta
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoEncuesta
    @EncuestaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Encuestas WHERE id_encuesta = @EncuestaID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Encuestas
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_encuesta = @EncuestaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarEncuesta
-- ============================================================
CREATE PROCEDURE sp_RestaurarEncuesta
    @EncuestaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Encuestas WHERE id_encuesta = @EncuestaID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Encuestas
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_encuesta = @EncuestaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ===== Respuestas =====
-- ============================================================
-- sp_InsertarRespuesta
-- ============================================================
CREATE PROCEDURE sp_InsertarRespuesta
    @EncuestaID INT,
    @PreguntaID INT,
    @ValorTexto VARCHAR(500) = NULL,
    @ValorEntero INT = NULL,
    @ValorDecimal DECIMAL(12,2) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Respuestas (id_encuesta, id_pregunta, valor_texto, valor_entero, valor_decimal, usuario_creacion)
            VALUES (@EncuestaID, @PreguntaID, @ValorTexto, @ValorEntero, @ValorDecimal, @UsuarioCreacionID);

            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ActualizarRespuesta
-- ============================================================
CREATE PROCEDURE sp_ActualizarRespuesta
    @RespuestaID INT,
    @EncuestaID INT,
    @PreguntaID INT,
    @ValorTexto VARCHAR(500) = NULL,
    @ValorEntero INT = NULL,
    @ValorDecimal DECIMAL(12,2) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Respuestas WHERE id_respuesta = @RespuestaID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);

            UPDATE Respuestas
            SET id_encuesta = @EncuestaID,
            id_pregunta = @PreguntaID,
            valor_texto = @ValorTexto,
            valor_entero = @ValorEntero,
            valor_decimal = @ValorDecimal,
            fecha_modificacion = SYSDATETIME(),
            usuario_modificacion = @UsuarioModificacionID
            WHERE id_respuesta = @RespuestaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ObtenerRespuestaPorId
-- ============================================================
CREATE PROCEDURE sp_ObtenerRespuestaPorId
    @RespuestaID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Respuestas
        WHERE  id_respuesta = @RespuestaID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_ListarRespuestas
-- ============================================================
CREATE PROCEDURE sp_ListarRespuestas
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Respuestas
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY id_encuesta;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_EliminarLogicoRespuesta
-- ============================================================
CREATE PROCEDURE sp_EliminarLogicoRespuesta
    @RespuestaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Respuestas WHERE id_respuesta = @RespuestaID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);

            UPDATE Respuestas
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_respuesta = @RespuestaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- sp_RestaurarRespuesta
-- ============================================================
CREATE PROCEDURE sp_RestaurarRespuesta
    @RespuestaID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Respuestas WHERE id_respuesta = @RespuestaID AND activo = 0)
                RAISERROR('El registro no existe o ya está activo.', 16, 1);

            UPDATE Respuestas
            SET activo = 1,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_respuesta = @RespuestaID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ============================================================
-- Triggers de Auditoría (prefijo trg_ + acción + tabla, según estándar)
-- Cada trigger distingue, fila por fila, entre:
--   INSERT | UPDATE | DELETE_LOGICO (activo 1->0) | RESTORE (activo 0->1)
-- ============================================================

-- ============================================================
-- trg_AfterInsertUpdate_Encuestas
-- ============================================================
CREATE TRIGGER trg_AfterInsertUpdate_Encuestas
ON Encuestas
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            i.id_usuario,
            'INSERT',
            'Encuestas',
            i.id_encuesta,
            'fecha_registro: ' + CONVERT(VARCHAR, i.fecha_registro, 120) +
            ' | id_cliente: ' + CAST(i.id_cliente AS VARCHAR),
            SYSDATETIME()
        FROM inserted i;
    END
    ELSE
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            i.id_usuario,
            CASE
                WHEN d.activo = 1 AND i.activo = 0 THEN 'DELETE_LOGICO'
                WHEN d.activo = 0 AND i.activo = 1 THEN 'RESTORE'
                ELSE 'UPDATE'
            END,
            'Encuestas',
            i.id_encuesta,
            'fecha_registro: ' + CONVERT(VARCHAR, i.fecha_registro, 120) +
            ' | id_cliente: ' + CAST(i.id_cliente AS VARCHAR),
            SYSDATETIME()
        FROM inserted i
        INNER JOIN deleted d ON d.id_encuesta = i.id_encuesta;
    END
END;
GO

-- ============================================================
-- trg_AfterInsertUpdate_Clientes
-- ============================================================
CREATE TRIGGER trg_AfterInsertUpdate_Clientes
ON Clientes
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            i.usuario_creacion,
            'INSERT',
            'Clientes',
            i.id_cliente,
            'nombre: ' + i.nombre + ' ' + i.apellidos,
            SYSDATETIME()
        FROM inserted i;
    END
    ELSE
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            i.usuario_modificacion,
            CASE
                WHEN d.activo = 1 AND i.activo = 0 THEN 'DELETE_LOGICO'
                WHEN d.activo = 0 AND i.activo = 1 THEN 'RESTORE'
                ELSE 'UPDATE'
            END,
            'Clientes',
            i.id_cliente,
            'nombre: ' + i.nombre + ' ' + i.apellidos,
            SYSDATETIME()
        FROM inserted i
        INNER JOIN deleted d ON d.id_cliente = i.id_cliente;
    END
END;
GO

-- ============================================================
-- trg_AfterInsertUpdate_Usuarios
-- ============================================================
CREATE TRIGGER trg_AfterInsertUpdate_Usuarios
ON Usuarios
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            COALESCE(i.usuario_creacion, i.id_usuario),
            'INSERT',
            'Usuarios',
            i.id_usuario,
            'nombre_usuario: ' + i.nombre_usuario + ' | email: ' + i.email,
            SYSDATETIME()
        FROM inserted i;
    END
    ELSE
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            COALESCE(i.usuario_modificacion, i.id_usuario),
            CASE
                WHEN d.activo = 1 AND i.activo = 0 THEN 'DELETE_LOGICO'
                WHEN d.activo = 0 AND i.activo = 1 THEN 'RESTORE'
                ELSE 'UPDATE'
            END,
            'Usuarios',
            i.id_usuario,
            'nombre_usuario: ' + i.nombre_usuario + ' | email: ' + i.email,
            SYSDATETIME()
        FROM inserted i
        INNER JOIN deleted d ON d.id_usuario = i.id_usuario;
    END
END;
GO

-- ============================================================
-- Vistas
-- ============================================================

-- Vista general de encuestas con datos del cliente
CREATE VIEW VW_EncuestasCompletas AS
SELECT
    e.id_encuesta,
    e.fecha_registro,
    e.ip_origen,
    c.nombre + ' ' + c.apellidos   AS nombre_cliente,
    c.email,
    g.nombre                        AS genero,
    re.descripcion                  AS rango_edad,
    ne.nombre                       AS nivel_educativo,
    ri.descripcion                  AS rango_ingresos,
    o.nombre                        AS ocupacion
FROM Encuestas e
INNER JOIN Clientes          c  ON e.id_cliente        = c.id_cliente
INNER JOIN Generos           g  ON c.id_genero          = g.id_genero
INNER JOIN RangosEdad        re ON c.id_rango_edad      = re.id_rango_edad
INNER JOIN NivelesEducativos ne ON c.id_nivel_educativo = ne.id_nivel_educativo
INNER JOIN RangosIngresos    ri ON c.id_rango_ingresos  = ri.id_rango_ingresos
LEFT  JOIN Ocupaciones       o  ON c.id_ocupacion       = o.id_ocupacion
WHERE e.activo = 1 AND c.activo = 1;
GO

-- Vista de respuestas con texto de pregunta y categoría
CREATE VIEW VW_RespuestasDetalle AS
SELECT
    r.id_respuesta,
    r.id_encuesta,
    cat.nombre   AS categoria,
    p.orden,
    p.texto      AS pregunta,
    p.tipo_control,
    r.valor_texto,
    r.valor_entero,
    r.valor_decimal
FROM Respuestas r
INNER JOIN Preguntas          p   ON r.id_pregunta  = p.id_pregunta
INNER JOIN CategoriasPregunta cat ON p.id_categoria = cat.id_categoria
WHERE r.activo = 1 AND p.activo = 1;
GO

DROP PROCEDURE IF EXISTS sp_GuardarEncuestaCompleta;
GO

CREATE PROCEDURE sp_GuardarEncuestaCompleta
(
    @Nombre VARCHAR(100),
    @Apellidos VARCHAR(150),
    @Email VARCHAR(150),
    @Telefono VARCHAR(20),

    @IdGenero INT,
    @IdNivelEducativo INT,
    @IdRangoIngresos INT,
    @IdRangoEdad INT,
    @IdOcupacion INT,

    @IdUsuario INT = NULL,

    @IpOrigen VARCHAR(50) = NULL,

    @RespuestasXml XML
)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        --------------------------------------------------
        -- CLIENTE
        --------------------------------------------------

        INSERT INTO Clientes
        (
            nombre,
            apellidos,
            email,
            telefono,
            id_genero,
            id_nivel_educativo,
            id_rango_ingresos,
            id_rango_edad,
            id_ocupacion,
            usuario_creacion
        )
        VALUES
        (
            @Nombre,
            @Apellidos,
            @Email,
            @Telefono,
            @IdGenero,
            @IdNivelEducativo,
            @IdRangoIngresos,
            @IdRangoEdad,
            @IdOcupacion,
            @IdUsuario
        );

        DECLARE @IdCliente INT = SCOPE_IDENTITY();

        --------------------------------------------------
        -- ENCUESTA
        --------------------------------------------------

        INSERT INTO Encuestas
        (
            id_cliente,
            id_usuario,
            ip_origen,
            usuario_creacion
        )
        VALUES
        (
            @IdCliente,
            @IdUsuario,
            @IpOrigen,
            @IdUsuario
        );

        DECLARE @IdEncuesta INT = SCOPE_IDENTITY();

        --------------------------------------------------
        -- RESPUESTAS
        --------------------------------------------------

        INSERT INTO Respuestas
        (
            id_encuesta,
            id_pregunta,
            valor_texto,
            usuario_creacion
        )
        SELECT
            @IdEncuesta,
            X.N.value('(IdPregunta)[1]', 'INT'),
            NULLIF(X.N.value('(ValorTexto)[1]', 'VARCHAR(500)'), ''),
            @IdUsuario
        FROM @RespuestasXml.nodes('/Respuestas/Respuesta') AS X(N);

        --------------------------------------------------
        -- AUDITORIA
        --------------------------------------------------

        INSERT INTO Auditorias
        (
            id_usuario,
            accion,
            tabla_afectada,
            id_registro_afectado,
            detalle,
            ip_origen
        )
        VALUES
        (
            @IdUsuario,
            'INSERT',
            'Encuestas',
            @IdEncuesta,
            'Encuesta registrada correctamente',
            @IpOrigen
        );

        COMMIT TRANSACTION;

        SELECT
            1 AS Exito,
            'Encuesta almacenada correctamente.' AS Mensaje,
            @IdEncuesta AS IdEncuesta;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT
            0 AS Exito,
            ERROR_MESSAGE() AS Mensaje,
            NULL AS IdEncuesta;

    END CATCH

END
GO

-- ============================================================
-- TABLA: EstadosCotizacion
-- ============================================================
CREATE TABLE EstadosCotizacion (
    id_estado            INT            IDENTITY(1,1)  NOT NULL,
    nombre_estado        VARCHAR(50)    NOT NULL,
    descripcion          VARCHAR(200)   NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_EstadosCotizacion PRIMARY KEY (id_estado),
CONSTRAINT UQ_EstadosCotizacion_nombre_estado UNIQUE (nombre_estado),
CONSTRAINT CHK_EstadosCotizacion_nombre CHECK (LEN(LTRIM(RTRIM(nombre_estado))) > 0)
);
GO
 
-- ============================================================
-- TABLA: Cotizaciones
-- ============================================================
CREATE TABLE Cotizaciones (
    id_cotizacion         INT            IDENTITY(1,1)  NOT NULL,
    id_cliente            INT            NOT NULL,
    id_tipo_prestamo      INT            NOT NULL,
    id_plazo              INT            NOT NULL,
    id_estado             INT            NOT NULL  DEFAULT 1,
    id_usuario            INT            NULL,
    monto_solicitado      DECIMAL(12,2)  NOT NULL,
    tasa_interes_aplicada DECIMAL(5,2)   NOT NULL,
    cuota_mensual         DECIMAL(12,2)  NOT NULL,
    monto_total_intereses DECIMAL(14,2)  NOT NULL,
    monto_total_pagar     DECIMAL(14,2)  NOT NULL,
    fecha_cotizacion      DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    observaciones         VARCHAR(500)   NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_Cotizaciones PRIMARY KEY (id_cotizacion),
CONSTRAINT FK_Cotizaciones_Clientes_id_cliente FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Cotizaciones_TiposPrestamo_id_tipo_prestamo FOREIGN KEY (id_tipo_prestamo) REFERENCES TiposPrestamo(id_tipo_prestamo) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Cotizaciones_Plazos_id_plazo FOREIGN KEY (id_plazo) REFERENCES Plazos(id_plazo) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Cotizaciones_EstadosCotizacion_id_estado FOREIGN KEY (id_estado) REFERENCES EstadosCotizacion(id_estado) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT FK_Cotizaciones_Usuarios_id_usuario FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT CHK_Cotizaciones_monto_solicitado CHECK (monto_solicitado > 0),
CONSTRAINT CHK_Cotizaciones_tasa_interes CHECK (tasa_interes_aplicada >= 0),
CONSTRAINT CHK_Cotizaciones_cuota_mensual CHECK (cuota_mensual > 0),
CONSTRAINT CHK_Cotizaciones_montos_totales CHECK (monto_total_pagar >= monto_solicitado AND monto_total_intereses >= 0)
);
GO
 
CREATE INDEX idx_cotizaciones_id_cliente  ON Cotizaciones (id_cliente);
CREATE INDEX idx_cotizaciones_id_estado   ON Cotizaciones (id_estado);
CREATE INDEX idx_cotizaciones_fecha       ON Cotizaciones (fecha_cotizacion);
GO
 
-- ============================================================
-- TABLA: DetalleAmortizacion
-- ============================================================
CREATE TABLE DetalleAmortizacion (
    id_detalle_amortizacion INT            IDENTITY(1,1)  NOT NULL,
    id_cotizacion           INT            NOT NULL,
    numero_cuota            INT            NOT NULL,
    monto_capital           DECIMAL(12,2)  NOT NULL,
    monto_interes           DECIMAL(12,2)  NOT NULL,
    monto_cuota             DECIMAL(12,2)  NOT NULL,
    saldo_pendiente         DECIMAL(12,2)  NOT NULL,
activo               BIT            NOT NULL  DEFAULT 1,
    fecha_creacion       DATETIME2(3)   NOT NULL  DEFAULT SYSDATETIME(),
    usuario_creacion     INT            NULL,
    fecha_modificacion   DATETIME2(3)   NULL,
    usuario_modificacion INT            NULL,
CONSTRAINT PK_DetalleAmortizacion PRIMARY KEY (id_detalle_amortizacion),
CONSTRAINT UQ_DetalleAmortizacion_id_cotizacion_numero_cuota UNIQUE (id_cotizacion, numero_cuota),
CONSTRAINT FK_DetalleAmortizacion_Cotizaciones_id_cotizacion FOREIGN KEY (id_cotizacion) REFERENCES Cotizaciones(id_cotizacion) ON DELETE NO ACTION ON UPDATE NO ACTION,
CONSTRAINT CHK_DetalleAmortizacion_numero_cuota CHECK (numero_cuota > 0),
CONSTRAINT CHK_DetalleAmortizacion_montos CHECK (monto_capital >= 0 AND monto_interes >= 0 AND monto_cuota > 0 AND saldo_pendiente >= 0)
);
GO
 
CREATE INDEX idx_detalleamortizacion_id_cotizacion ON DetalleAmortizacion (id_cotizacion);
GO
 
-- ============================================================
-- Llaves Foráneas de Auditoría (usuario_creacion / usuario_modificacion)
-- ============================================================
ALTER TABLE EstadosCotizacion   ADD CONSTRAINT FK_EstadosCotizacion_UsuarioCreacion     FOREIGN KEY (usuario_creacion)     REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE EstadosCotizacion   ADD CONSTRAINT FK_EstadosCotizacion_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Cotizaciones        ADD CONSTRAINT FK_Cotizaciones_UsuarioCreacion         FOREIGN KEY (usuario_creacion)     REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Cotizaciones        ADD CONSTRAINT FK_Cotizaciones_UsuarioModificacion     FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE DetalleAmortizacion ADD CONSTRAINT FK_DetalleAmortizacion_UsuarioCreacion     FOREIGN KEY (usuario_creacion)     REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE DetalleAmortizacion ADD CONSTRAINT FK_DetalleAmortizacion_UsuarioModificacion FOREIGN KEY (usuario_modificacion) REFERENCES Usuarios(id_usuario) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
 
-- ============================================================
-- Datos Insertados
-- ============================================================
 
-- Estados de Cotización
INSERT INTO EstadosCotizacion (nombre_estado, descripcion) VALUES
    ('Pendiente', 'Cotización generada, en espera de revisión'),
    ('Aprobada',  'Cotización aprobada por un asesor o administrador'),
    ('Rechazada', 'Cotización rechazada por no cumplir requisitos'),
    ('Cancelada', 'Cotización cancelada por el cliente o el sistema');
GO
 
-- ============================================================
-- Trigger de Auditoría
-- ============================================================
CREATE TRIGGER trg_AfterInsertUpdate_Cotizaciones
ON Cotizaciones
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
 
    IF NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            i.id_usuario,
            'INSERT',
            'Cotizaciones',
            i.id_cotizacion,
            'id_cliente: ' + CAST(i.id_cliente AS VARCHAR) +
            ' | monto_solicitado: ' + CAST(i.monto_solicitado AS VARCHAR) +
            ' | id_estado: ' + CAST(i.id_estado AS VARCHAR),
            SYSDATETIME()
        FROM inserted i;
    END
    ELSE
    BEGIN
        INSERT INTO Auditorias (id_usuario, accion, tabla_afectada, id_registro_afectado, detalle, fecha_accion)
        SELECT
            COALESCE(i.usuario_modificacion, i.id_usuario),
            CASE
                WHEN d.activo = 1 AND i.activo = 0 THEN 'DELETE_LOGICO'
                WHEN d.activo = 0 AND i.activo = 1 THEN 'RESTORE'
                ELSE 'UPDATE'
            END,
            'Cotizaciones',
            i.id_cotizacion,
            'id_cliente: ' + CAST(i.id_cliente AS VARCHAR) +
            ' | monto_solicitado: ' + CAST(i.monto_solicitado AS VARCHAR) +
            ' | id_estado: ' + CAST(i.id_estado AS VARCHAR),
            SYSDATETIME()
        FROM inserted i
        INNER JOIN deleted d ON d.id_cotizacion = i.id_cotizacion;
    END
END;
GO


 
-- ===== EstadosCotizacion =====
 
CREATE PROCEDURE sp_InsertarEstadoCotizacion
    @NombreEstado VARCHAR(50),
    @Descripcion VARCHAR(200) = NULL,
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF EXISTS (SELECT 1 FROM EstadosCotizacion WHERE nombre_estado = @NombreEstado AND activo = 1)
                RAISERROR('Ya existe un registro de EstadosCotizacion con ese valor.', 16, 1);
 
            INSERT INTO EstadosCotizacion (nombre_estado, descripcion, usuario_creacion)
            VALUES (@NombreEstado, @Descripcion, @UsuarioCreacionID);
 
            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ActualizarEstadoCotizacion
    @EstadoID INT,
    @NombreEstado VARCHAR(50),
    @Descripcion VARCHAR(200) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM EstadosCotizacion WHERE id_estado = @EstadoID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);
 
            UPDATE EstadosCotizacion
            SET nombre_estado = @NombreEstado,
                descripcion = @Descripcion,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_estado = @EstadoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ObtenerEstadoCotizacionPorId
    @EstadoID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   EstadosCotizacion
        WHERE  id_estado = @EstadoID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ListarEstadosCotizacion
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   EstadosCotizacion
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY id_estado;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_EliminarLogicoEstadoCotizacion
    @EstadoID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM EstadosCotizacion WHERE id_estado = @EstadoID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);
 
            UPDATE EstadosCotizacion
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_estado = @EstadoID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
-- ===== Cotizaciones =====
 
CREATE PROCEDURE sp_InsertarCotizacion
    @IdCliente INT,
    @IdTipoPrestamo INT,
    @IdPlazo INT,
    @IdEstado INT = 1,
    @IdUsuario INT = NULL,
    @MontoSolicitado DECIMAL(12,2),
    @TasaInteresAplicada DECIMAL(5,2),
    @CuotaMensual DECIMAL(12,2),
    @MontoTotalIntereses DECIMAL(14,2),
    @MontoTotalPagar DECIMAL(14,2),
    @Observaciones VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Cotizaciones
            (
                id_cliente, id_tipo_prestamo, id_plazo, id_estado, id_usuario,
                monto_solicitado, tasa_interes_aplicada, cuota_mensual,
                monto_total_intereses, monto_total_pagar, observaciones,
                usuario_creacion
            )
            VALUES
            (
                @IdCliente, @IdTipoPrestamo, @IdPlazo, @IdEstado, @IdUsuario,
                @MontoSolicitado, @TasaInteresAplicada, @CuotaMensual,
                @MontoTotalIntereses, @MontoTotalPagar, @Observaciones,
                @IdUsuario
            );
 
            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ActualizarCotizacion
    @CotizacionID INT,
    @IdEstado INT,
    @MontoSolicitado DECIMAL(12,2),
    @TasaInteresAplicada DECIMAL(5,2),
    @CuotaMensual DECIMAL(12,2),
    @MontoTotalIntereses DECIMAL(14,2),
    @MontoTotalPagar DECIMAL(14,2),
    @Observaciones VARCHAR(500) = NULL,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Cotizaciones WHERE id_cotizacion = @CotizacionID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);
 
            UPDATE Cotizaciones
            SET id_estado = @IdEstado,
                monto_solicitado = @MontoSolicitado,
                tasa_interes_aplicada = @TasaInteresAplicada,
                cuota_mensual = @CuotaMensual,
                monto_total_intereses = @MontoTotalIntereses,
                monto_total_pagar = @MontoTotalPagar,
                observaciones = @Observaciones,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_cotizacion = @CotizacionID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ObtenerCotizacionPorId
    @CotizacionID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Cotizaciones
        WHERE  id_cotizacion = @CotizacionID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ListarCotizaciones
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   Cotizaciones
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY fecha_cotizacion DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_EliminarLogicoCotizacion
    @CotizacionID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM Cotizaciones WHERE id_cotizacion = @CotizacionID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);
 
            UPDATE Cotizaciones
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_cotizacion = @CotizacionID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
-- ===== DetalleAmortizacion =====
 
CREATE PROCEDURE sp_InsertarDetalleAmortizacion
    @IdCotizacion INT,
    @NumeroCuota INT,
    @MontoCapital DECIMAL(12,2),
    @MontoInteres DECIMAL(12,2),
    @MontoCuota DECIMAL(12,2),
    @SaldoPendiente DECIMAL(12,2),
    @UsuarioCreacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO DetalleAmortizacion
            (
                id_cotizacion, numero_cuota, monto_capital, monto_interes,
                monto_cuota, saldo_pendiente, usuario_creacion
            )
            VALUES
            (
                @IdCotizacion, @NumeroCuota, @MontoCapital, @MontoInteres,
                @MontoCuota, @SaldoPendiente, @UsuarioCreacionID
            );
 
            SELECT SCOPE_IDENTITY() AS IdGenerado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ActualizarDetalleAmortizacion
    @DetalleAmortizacionID INT,
    @MontoCapital DECIMAL(12,2),
    @MontoInteres DECIMAL(12,2),
    @MontoCuota DECIMAL(12,2),
    @SaldoPendiente DECIMAL(12,2),
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM DetalleAmortizacion WHERE id_detalle_amortizacion = @DetalleAmortizacionID AND activo = 1)
                RAISERROR('El registro no existe o está inactivo.', 16, 1);
 
            UPDATE DetalleAmortizacion
            SET monto_capital = @MontoCapital,
                monto_interes = @MontoInteres,
                monto_cuota = @MontoCuota,
                saldo_pendiente = @SaldoPendiente,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_detalle_amortizacion = @DetalleAmortizacionID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ObtenerDetalleAmortizacionPorId
    @DetalleAmortizacionID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   DetalleAmortizacion
        WHERE  id_detalle_amortizacion = @DetalleAmortizacionID AND activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ListarDetalleAmortizacion
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   DetalleAmortizacion
        WHERE  activo = 1 OR @IncluirInactivos = 1
        ORDER BY id_cotizacion, numero_cuota;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_ListarDetalleAmortizacionPorCotizacion
    @IdCotizacion INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM   DetalleAmortizacion
        WHERE  id_cotizacion = @IdCotizacion AND activo = 1
        ORDER BY numero_cuota;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 
CREATE PROCEDURE sp_EliminarLogicoDetalleAmortizacion
    @DetalleAmortizacionID INT,
    @UsuarioModificacionID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            IF NOT EXISTS (SELECT 1 FROM DetalleAmortizacion WHERE id_detalle_amortizacion = @DetalleAmortizacionID AND activo = 1)
                RAISERROR('El registro no existe o ya está inactivo.', 16, 1);
 
            UPDATE DetalleAmortizacion
            SET activo = 0,
                fecha_modificacion = SYSDATETIME(),
                usuario_modificacion = @UsuarioModificacionID
            WHERE id_detalle_amortizacion = @DetalleAmortizacionID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE PROCEDURE sp_Login
    @NombreUsuario VARCHAR(100),
    @HashPassword VARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT TOP 1
            u.id_usuario,
            u.nombre_usuario,
            u.nombre,
            u.email,
            r.id_rol,
            r.nombre_rol
        FROM Usuarios u
        INNER JOIN UsuariosRoles ur ON ur.id_usuario = u.id_usuario AND ur.activo = 1
        INNER JOIN Roles r ON r.id_rol = ur.id_rol AND r.activo = 1
        WHERE u.nombre_usuario = @NombreUsuario
          AND u.hash_password = @HashPassword
          AND u.activo = 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
 