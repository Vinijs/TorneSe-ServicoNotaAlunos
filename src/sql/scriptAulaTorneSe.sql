DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'servnota') THEN
        CREATE SCHEMA servnota;
    END IF;
END $EF$;

CREATE TABLE servnota.usuarios (
    id integer GENERATED ALWAYS AS IDENTITY,
    nome VARCHAR(100) NOT NULL,
    documento VARCHAR(11) NOT NULL,
    data_nascimento DATE NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT (TRUE),
    email VARCHAR(255) NOT NULL,
    usuario_adm BOOLEAN NOT NULL DEFAULT (FALSE),
    CONSTRAINT "PK_usuarios" PRIMARY KEY (id)
);


CREATE TABLE servnota.alunos (
    id integer NOT NULL,
    nome_abreviado VARCHAR(100) NOT NULL,
    email_interno VARCHAR(200) NOT NULL,
    data_cadastro TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT "PK_alunos" PRIMARY KEY (id),
    CONSTRAINT "FK_alunos_usuarios_id" FOREIGN KEY (id) REFERENCES servnota.usuarios (id) ON DELETE CASCADE
);


CREATE TABLE servnota.professores (
    id integer NOT NULL,
    nome_abreviado VARCHAR(50) NOT NULL,
    email_interno VARCHAR(100) NOT NULL,
    professor_titular BOOLEAN NOT NULL DEFAULT (TRUE),
    professor_suplente BOOLEAN NOT NULL DEFAULT (FALSE),
    data_cadastro TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    diciplina_id INT NOT NULL,
    CONSTRAINT "PK_professores" PRIMARY KEY (id),
    CONSTRAINT "FK_professores_usuarios_id" FOREIGN KEY (id) REFERENCES servnota.usuarios (id) ON DELETE CASCADE
);


CREATE TABLE servnota.disciplinas (
    id integer GENERATED ALWAYS AS IDENTITY,
    nome VARCHAR(100) NOT NULL,
    descricao VARCHAR(100) NOT NULL,
    data_inicio TIMESTAMP(6) NOT NULL,
    data_fim TIMESTAMP(6) NOT NULL,
    tipo_disciplina text NOT NULL,
    data_cadastro TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    professor_id INT NOT NULL,
    CONSTRAINT "PK_disciplinas" PRIMARY KEY (id),
    CONSTRAINT "FK_disciplinas_professores_professor_id" FOREIGN KEY (professor_id) REFERENCES servnota.professores (id)
);


CREATE TABLE servnota.conteudos (
    id integer GENERATED ALWAYS AS IDENTITY,
    nome VARCHAR(100) NOT NULL,
    descricao VARCHAR(100) NOT NULL,
    data_inicio TIMESTAMP(6) NOT NULL,
    data_termino TIMESTAMP(6) NOT NULL,
    data_cadastro TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    disciplina_id INT NOT NULL,
    CONSTRAINT "PK_conteudos" PRIMARY KEY (id),
    CONSTRAINT "FK_conteudos_disciplinas_disciplina_id" FOREIGN KEY (disciplina_id) REFERENCES servnota.disciplinas (id)
);


CREATE TABLE servnota.turmas (
    id integer GENERATED ALWAYS AS IDENTITY,
    nome VARCHAR(50) NOT NULL,
    periodo text NOT NULL,
    data_inicio TIMESTAMP(6) NOT NULL,
    data_final TIMESTAMP(6) NOT NULL,
    disciplina_id INT NOT NULL,
    data_cadastro TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT "PK_turmas" PRIMARY KEY (id),
    CONSTRAINT "FK_turmas_disciplinas_disciplina_id" FOREIGN KEY (disciplina_id) REFERENCES servnota.disciplinas (id)
);


CREATE TABLE servnota.atividades (
    id integer GENERATED ALWAYS AS IDENTITY,
    descricao VARCHAR(255) NOT NULL,
    tipo_atividade integer NOT NULL,
    data_atividade TIMESTAMP(6) NOT NULL,
    data_cadastro TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    possui_retentativa BOOLEAN NOT NULL DEFAULT (FALSE),
    conteudo_id INT NOT NULL,
    CONSTRAINT "PK_atividades" PRIMARY KEY (id),
    CONSTRAINT "FK_atividades_conteudos_conteudo_id" FOREIGN KEY (conteudo_id) REFERENCES servnota.conteudos (id)
);


CREATE TABLE servnota.alunos_turmas (
    aluno_id INT NOT NULL,
    turma_id INT NOT NULL,
    data_cadastro TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT "PK_alunos_turmas" PRIMARY KEY (aluno_id, turma_id),
    CONSTRAINT "FK_alunos_turmas_alunos_aluno_id" FOREIGN KEY (aluno_id) REFERENCES servnota.alunos (id) ON DELETE CASCADE,
    CONSTRAINT "FK_alunos_turmas_turmas_turma_id" FOREIGN KEY (turma_id) REFERENCES servnota.turmas (id) ON DELETE CASCADE
);


CREATE TABLE servnota.notas (
    id integer GENERATED ALWAYS AS IDENTITY,
    aluno_id INT NOT NULL,
    atividade_id INT NOT NULL,
    valor_nota double precision NOT NULL,
    data_lancamento TIMESTAMP(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    usuario_id integer NOT NULL,
    cancelada_por_retentativa BOOLEAN NOT NULL,
    CONSTRAINT "PK_notas" PRIMARY KEY (id),
    CONSTRAINT "FK_notas_alunos_aluno_id" FOREIGN KEY (aluno_id) REFERENCES servnota.alunos (id),
    CONSTRAINT "FK_notas_atividades_atividade_id" FOREIGN KEY (atividade_id) REFERENCES servnota.atividades (id)
);

INSERT INTO servnota.usuarios (id, ativo, data_nascimento, documento, email, nome)
OVERRIDING SYSTEM VALUE
VALUES (1234, TRUE, DATE '1990-03-10', '87628929919', 'raphael.s@email.com', 'Raphael Silvestre');
INSERT INTO servnota.usuarios (id, ativo, data_nascimento, documento, email, nome)
OVERRIDING SYSTEM VALUE
VALUES (1282727, TRUE, DATE '1983-01-01', '30292919821', 'danilo.aparecido@email.com', 'Danilo Aparecido');

INSERT INTO servnota.alunos (id, data_cadastro, email_interno, nome_abreviado)
OVERRIDING SYSTEM VALUE
VALUES (1234, TIMESTAMP '2022-03-06 18:26:18.73941', 'raphael.s@email.com', 'Raphael');

INSERT INTO servnota.professores (id, data_cadastro, diciplina_id, email_interno, nome_abreviado, professor_titular)
OVERRIDING SYSTEM VALUE
VALUES (1282727, TIMESTAMP '2022-03-06 18:26:18.741695', 1341567, 'danilo.s@email.com', 'Danilo', TRUE);

INSERT INTO servnota.disciplinas (id, data_cadastro, data_fim, data_inicio, descricao, nome, professor_id, tipo_disciplina)
OVERRIDING SYSTEM VALUE
VALUES (1341567, TIMESTAMP '2021-09-12 00:00:00', TIMESTAMP '2022-02-12 00:00:00', TIMESTAMP '2021-10-12 00:00:00', 'Matemática base ensino médio', 'Matemática', 1282727, 'Teorica');

INSERT INTO servnota.conteudos (id, data_cadastro, data_inicio, data_termino, descricao, disciplina_id, nome)
OVERRIDING SYSTEM VALUE
VALUES (1, TIMESTAMP '2021-10-15 00:00:00', TIMESTAMP '2021-10-18 00:00:00', TIMESTAMP '2021-11-18 00:00:00', 'Aprendizado de equações de segundo grau', 1341567, 'Equações segundo grau');

INSERT INTO servnota.turmas (id, data_cadastro, data_final, data_inicio, disciplina_id, nome, periodo)
OVERRIDING SYSTEM VALUE
VALUES (1, TIMESTAMP '2022-03-06 18:26:18.742094', TIMESTAMP '2021-12-01 00:00:00', TIMESTAMP '2021-06-01 00:00:00', 1341567, 'Grupo Matemática I', 'Noturno');

INSERT INTO servnota.atividades (id, conteudo_id, data_atividade, data_cadastro, descricao, tipo_atividade)
OVERRIDING SYSTEM VALUE
VALUES (1, 1, TIMESTAMP '2021-11-10 00:00:00', TIMESTAMP '2021-11-01 00:00:00', 'Atividade avaliativa equações', 1);

INSERT INTO servnota.alunos_turmas (aluno_id, turma_id, data_cadastro)
OVERRIDING SYSTEM VALUE
VALUES (1234, 1, CURRENT_TIMESTAMP);

CREATE INDEX "IX_alunos_turmas_turma_id" ON servnota.alunos_turmas (turma_id);


CREATE INDEX "IX_atividades_conteudo_id" ON servnota.atividades (conteudo_id);


CREATE INDEX "IX_conteudos_disciplina_id" ON servnota.conteudos (disciplina_id);


CREATE UNIQUE INDEX "IX_disciplinas_professor_id" ON servnota.disciplinas (professor_id);


CREATE INDEX "IX_notas_aluno_id" ON servnota.notas (aluno_id);


CREATE INDEX "IX_notas_atividade_id" ON servnota.notas (atividade_id);


CREATE INDEX "IX_turmas_disciplina_id" ON servnota.turmas (disciplina_id);


