/*
-- Habilitar extensão para UUID
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Criar tabela musculos
CREATE TABLE muscles (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    nome VARCHAR(100) NOT NULL UNIQUE
);

-- Criar tabela equipamentos
CREATE TABLE equipments (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    nome VARCHAR(100) NOT NULL,
    musculo_id UUID NOT NULL,
    FOREIGN KEY (musculo_id) REFERENCES muscles(id)
);

-- Criar tabela exercicios
CREATE TABLE exercises (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    nome VARCHAR(100) NOT NULL,
    musculo_principal_id UUID NOT NULL,
    equipamento_id UUID NOT NULL,
    FOREIGN KEY (musculo_principal_id) REFERENCES muscles(id),
    FOREIGN KEY (equipamento_id) REFERENCES equipamentos(id)
);
*/
-- Inserir dados iniciais na tabela musculos
INSERT INTO muscles (nome)
SELECT DISTINCT Muscle_Group
FROM (
    SELECT DISTINCT Muscle_Group
    FROM '/tmp/gym_exercises.csv'
    DELIMITER ','
    CSV HEADER
) AS temp
WHERE Muscle_Group IS NOT NULL
ON CONFLICT (nome) DO NOTHING;

-- Inserir dados na tabela equipamentos
INSERT INTO equipamentos (nome, musculo_id)
SELECT DISTINCT Equipment, m.id
FROM (
    SELECT DISTINCT Equipment
    FROM '/tmp/gym_exercises.csv'
    DELIMITER ','
    CSV HEADER
) AS temp
JOIN muscles m ON m.nome = (
    SELECT Muscle_Group
    FROM '/tmp/gym_exercises.csv'
    DELIMITER ','
    CSV HEADER
    WHERE Equipment = temp.Equipment
    LIMIT 1
)
WHERE Equipment IS NOT NULL
ON CONFLICT (nome) DO NOTHING;

-- Inserir dados na tabela exercicios
INSERT INTO Exercises (Name, musculo_principal_id, equipamento_id)
SELECT 
    t.Exercise_Name,
    m.id AS musculo_principal_id,
    e.id AS equipamento_id
FROM '/tmp/gym_exercises.csv' t
DELIMITER ','
CSV HEADER
JOIN muscles m ON m.nome = t.Muscle_Group
JOIN equipamentos e ON e.nome = t.Equipment
WHERE t.Exercise_Name IS NOT NULL;