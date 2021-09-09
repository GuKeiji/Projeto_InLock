USE inlock_games_tarde;
GO

SELECT * FROM JOGO
SELECT * FROM USUARIO
SELECT * FROM ESTUDIO

--Listar todos os jogos e seus respectivos estúdios
SELECT J.idJogo, 
	   J.nomeJogo [Nome do Jogo], 
	   J.descricao, 
	   J.dataLancamento [Data de Lançamento], 
	   J.valor, 
	   E.nomeEstudio 
  FROM JOGO J
 INNER JOIN ESTUDIO E
    ON J.idEstudio = E.idEstudio

--Buscar e trazer na lista todos os estúdios com os respectivos jogos. Obs.: Listar
--todos os estúdios mesmo que eles não contenham nenhum jogo de referência
SELECT E.idEstudio,
	   E.nomeEstudio [Nome do Estúdio],
	   J.nomeJogo [Nome do Jogo],
	   J.descricao,
	   J.dataLancamento [Data de Lançamento],
	   J.valor
  FROM ESTUDIO E
  LEFT JOIN JOGO J
    ON E.idEstudio = J.idEstudio

--Buscar um usuário por e-mail e senha (login)
SELECT * 
  FROM USUARIO
 WHERE email = 'admin@admin.com'
   AND senha = 'admin'

--Buscar um jogo por idJogo
SELECT *
  FROM JOGO
 WHERE idJogo = 1

--Buscar um estúdio por idEstudio
SELECT *
  FROM ESTUDIO
 WHERE idEstudio = 2