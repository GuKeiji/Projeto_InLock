USE inlock_games_tarde;
GO

SELECT * FROM JOGO
SELECT * FROM USUARIO
SELECT * FROM ESTUDIO

--Listar todos os jogos e seus respectivos est�dios
SELECT J.idJogo, 
	   J.nomeJogo [Nome do Jogo], 
	   J.descricao, 
	   J.dataLancamento [Data de Lan�amento], 
	   J.valor, 
	   E.nomeEstudio 
  FROM JOGO J
 INNER JOIN ESTUDIO E
    ON J.idEstudio = E.idEstudio

--Buscar e trazer na lista todos os est�dios com os respectivos jogos. Obs.: Listar
--todos os est�dios mesmo que eles n�o contenham nenhum jogo de refer�ncia
SELECT E.idEstudio,
	   E.nomeEstudio [Nome do Est�dio],
	   J.nomeJogo [Nome do Jogo],
	   J.descricao,
	   J.dataLancamento [Data de Lan�amento],
	   J.valor
  FROM ESTUDIO E
  LEFT JOIN JOGO J
    ON E.idEstudio = J.idEstudio

--Buscar um usu�rio por e-mail e senha (login)
SELECT * 
  FROM USUARIO
 WHERE email = 'admin@admin.com'
   AND senha = 'admin'

--Buscar um jogo por idJogo
SELECT *
  FROM JOGO
 WHERE idJogo = 1

--Buscar um est�dio por idEstudio
SELECT *
  FROM ESTUDIO
 WHERE idEstudio = 2