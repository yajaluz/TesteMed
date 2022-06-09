create table contato(
id uniqueidentifier,
nome varchar(100) not null,
dataNascimento datetime not null,
sexo char(1),
idade int not null,
primary key(id)
);

--alter table contato 
--add primary key (id)
--alter column id uniqueidentifier not null
--alter column nome varchar(100) not null
--alter column idade int not null
--alter column dataNascimento datetime not null


ALTER TABLE dbo.contato   
   ADD CONSTRAINT CHK_idade_
   CHECK (idade >=18);  
GO  

ALTER TABLE dbo.contato   
   ADD CONSTRAINT CHK_dataNasc_
   CHECK (dataNascimento < getdate());  
GO  