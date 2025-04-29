/*

La DB esta comentada toda porque el .sql de aqui no reconoce algunos statements de postgreSQL

--Les voy dejando comentarios para documentar las cosas. Att: Alejo
-- Los OMMENT ON TABLE/COLUMN son comentarios que van en forma de metadatos osea no se le muestran al usuario pero sirven para documentar
CREATE TABLE Persona (
    id SERIAL PRIMARY KEY,
    cedula VARCHAR(20) UNIQUE NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    direccion TEXT,
    telefono VARCHAR(20),
    email VARCHAR(100) UNIQUE NOT NULL
);
COMMENT ON TABLE Persona IS 'Tabla padre que contiene información común a todos los usuarios del sistema';
COMMENT ON COLUMN Persona.cedula IS 'Documento de identificación único para cada persona';

-- En SQL las "listas" que hay en el diagrama que realizamos se implementan como relaciones desde la tabla contenedora.

-- Las citas de un paciente están en la tabla Cita (paciente_id)
-- Las notificaciones de un paciente están en Notificacion (paciente_id)
-- La agenda de un médico son las citas donde es médico (medico_id)
-- Las citas pendientes de secretaria son citas con estado 'Pendiente' y su secretaria_id

CREATE TABLE Paciente (
    PRIMARY KEY (id)
) INHERITS (Persona);

COMMENT ON TABLE Paciente IS 'Almacena información específica de pacientes, hereda de Persona';

CREATE TABLE Recomendacion (
    idRecomendacion SERIAL PRIMARY KEY,
    tipo VARCHAR(50) NOT NULL,
    descripcion TEXT,
    fechaEmision DATE NOT NULL DEFAULT CURRENT_DATE,
    paciente_id INTEGER NOT NULL REFERENCES Paciente(id) ON DELETE CASCADE
);
COMMENT ON TABLE Recomendacion IS 'Asociación muchos-a-uno con Paciente';

CREATE TABLE Medico (
    especialidad VARCHAR(100) NOT NULL,
    PRIMARY KEY (id)
) INHERITS (Persona);

COMMENT ON TABLE Medico IS 'Almacena información de médicos, incluyendo especialidad';

CREATE TABLE Secretaria (
    PRIMARY KEY (id)
) INHERITS (Persona);

COMMENT ON TABLE Secretaria IS 'Personal administrativo que gestiona citas';

CREATE TABLE Historia_Clinica (
    idHistoria SERIAL PRIMARY KEY,
    paciente_id INTEGER NOT NULL UNIQUE REFERENCES Paciente(id) ON DELETE CASCADE,
    email VARCHAR(100),
    fecha_creacion DATE NOT NULL DEFAULT CURRENT_DATE
);
COMMENT ON TABLE Historia_Clinica IS 'Relación de composición con Paciente - se elimina automáticamente al borrar el paciente';

CREATE TABLE Formula (
    idFormula SERIAL PRIMARY KEY,
    fechaCreacion DATE NOT NULL DEFAULT CURRENT_DATE,
    paciente_id INTEGER NOT NULL REFERENCES Paciente(id),
    historia_id INTEGER NOT NULL REFERENCES Historia_Clinica(idHistoria) ON DELETE CASCADE
);
COMMENT ON TABLE Formula IS 'Tiene agregación con Historia_Clinica';

CREATE TABLE Medicamento (
    idMedicamento SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    dosis VARCHAR(50) NOT NULL,
    frecuencia VARCHAR(100) NOT NULL,
    formula_id INTEGER NOT NULL REFERENCES Formula(idFormula) ON DELETE CASCADE
);
COMMENT ON TABLE Medicamento IS 'Tiene agregación con Formula';

CREATE TABLE Tratamiento (
    idTratamiento SERIAL PRIMARY KEY,
    descripcion TEXT NOT NULL,
    fecha_inicio DATE NOT NULL DEFAULT CURRENT_DATE,
    fecha_fin DATE,
    historia_id INTEGER NOT NULL REFERENCES Historia_Clinica(idHistoria) ON DELETE CASCADE
);

COMMENT ON TABLE Tratamiento IS 'Pertenece a Historia_Clinica';

CREATE TYPE Estado_Cita AS ENUM ('Asignada', 'Cancelada', 'Reasignada', 'Completada', 'Pendiente');

CREATE TABLE Cita (
    idCita SERIAL PRIMARY KEY,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    estado Estado_Cita NOT NULL DEFAULT 'Pendiente',
    motivo TEXT,
    paciente_id INTEGER NOT NULL REFERENCES Paciente(id) ON DELETE CASCADE,
    medico_id INTEGER NOT NULL REFERENCES Medico(id),
    secretaria_id INTEGER REFERENCES Secretaria(id),
    historia_id INTEGER REFERENCES Historia_Clinica(idHistoria),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    CONSTRAINT cita_unica UNIQUE (fecha, hora, medico_id)
);
COMMENT ON TABLE Cita IS 'Registro de citas médicas con relaciones a Paciente, Médico y Secretaria';

CREATE TABLE Notificacion (
    idNotificacion SERIAL PRIMARY KEY,
    tipo VARCHAR(50) NOT NULL,
    mensaje TEXT NOT NULL,
    fechaEnvio TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    estado VARCHAR(20) NOT NULL CHECK (estado IN ('Enviada', 'Leída', 'Pendiente')),
    paciente_id INTEGER NOT NULL REFERENCES Paciente(id) ON DELETE CASCADE,
    cita_id INTEGER REFERENCES Cita(idCita)
);

COMMENT ON TABLE Notificacion IS 'Notificaciones enviadas a pacientes, se eliminan con el paciente';


/*
RELACIONES IMPLEMENTADAAS:

COMPOSICION:
Historia_Clinica → Paciente (ON DELETE CASCADE)
Cita → Paciente (ON DELETE CASCADE)
Notificacion → Paciente (ON DELETE CASCADE)
Tratamiento → Historia_Clinica (ON DELETE CASCADE)
Diagnostico → Historia_Clinica (ON DELETE CASCADE)

AGREGACION:
Formula → Historia_Clinica
Medicamento → Formula
Diagnostico → Medico

ASOCIACION:
Recomendacion → Paciente
Cita → Medico
Cita → Secretaria

ENUMERACION:
Estado_Cita como tipo ENUM
*/


*/