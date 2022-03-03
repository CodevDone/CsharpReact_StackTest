DROP TABLE todo.dbo.todo

CREATE TABLE todo.dbo.todo  (
	Todo_Id  INT NOT NULL IDENTITY PRIMARY KEY,
	Description varchar(100) not NULL,
	IsComplete bit not NULL
);


INSERT into todo.dbo.todo (Description,IsComplete) values ('NetApi',0)
INSERT into todo.dbo.todo (Description,IsComplete) values ('React',0)
INSERT into todo.dbo.todo (Description,IsComplete) values ('Algorithm',0)
INSERT into todo.dbo.todo (Description,IsComplete) values ('Architecture',1)

SELECT  * 
FROM todo.dbo.todo t 
WHERE  Todo_Id = 4

DELETE todo.dbo.todo where Todo_Id = 4