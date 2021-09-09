USE inlock_games_tarde;
GO

INSERT INTO TIPOUSUARIO (titulo)
VALUES ('COMUM'),
	   ('ADMINISTRADOR');
GO

INSERT INTO USUARIO (idTipoUsuario,email,senha)
VALUES ('2','admin@admin.com', 'admin'),
	   ('1','cliente@cliente.com', 'cliente');
GO

INSERT INTO ESTUDIO (nomeEstudio)
VALUES ('Blizzard'),
	   ('Rockstar Studios'),
	   ('Square Enix');
GO

INSERT INTO JOGO (nomeJogo,descricao,dataLancamento,valor,idEstudio)
VALUES ('Diablo 3','� um jogo que cont�m bastante a��o e � viciante, seja voc� um novato ou um f�','15/05/2012','99','1'),
	   ('Red Dead Redemption II','jogo eletr�nico de a��o-aventura western','26/10/2018','120','2');
GO

TRUNCATE TABLE USUARIO

