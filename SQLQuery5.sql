drop table if exists m2m_books_authors;
drop table if exists m2m_books_genres;
drop table if exists subscriptions;
drop table if exists subscribers;
drop table if exists autohor;
drop table if exists books;
drop table if exists genres;


create table genres(
g_id integer  IDENTITY(1,1) PRIMARY KEY,
g_name varchar(255) not null
);
create table books(
b_id integer  IDENTITY(1,1) PRIMARY KEY,
b_name varchar(255)
)

create table autohor (
a_id integer  IDENTITY(1,1) PRIMARY KEY,
firstname varchar(255) not null,
secondname varchar(255) not null
);
create table subscribers(
s_id integer  IDENTITY(1,1) PRIMARY KEY,
firstname varchar(255) not null,
secondname varchar(255) not null,
s_book int,
s_last_visit date )
;
create table subscriptions(
sb_id integer  IDENTITY(1,1) PRIMARY KEY,
sb_subscriber integer,
sb_book integer,
sb_start date ,
sb_is_active int,
foreign key (sb_book)  references books(b_id),
foreign key (sb_subscriber)  references subscribers(s_id)
);
create table m2m_books_authors(
m2id integer  IDENTITY(1,1) PRIMARY KEY,
b_id integer,
a_id integer
foreign key (a_id)  references autohor(a_id) on delete  set null,
foreign key (b_id)  references books(b_id)
)
create table  m2m_books_genres(
m2mid integer  IDENTITY(1,1) PRIMARY KEY,
b_id integer,
g_id integer,
foreign key (b_id)  references books(b_id),
foreign key (g_id)  references genres(g_id) on delete  set null
)

INSERT INTO  genres VALUES('Romance');
INSERT INTO  genres VALUES('Animation');
INSERT INTO  genres VALUES('Thriller');
INSERT INTO  genres VALUES('Drama');
INSERT INTO  genres VALUES('Sci-Fi');
INSERT INTO  genres VALUES('Horror');
INSERT INTO  genres VALUES('Adventure');
INSERT INTO  genres VALUES('Musical');

INSERT INTO books(b_name) VALUES('The Sergey Letter');
INSERT INTO books VALUES('Lord of the Flies');
INSERT INTO books VALUES('Lolita');
INSERT INTO books VALUES('The Canterbury Tales');
INSERT INTO books VALUES('The Lord of The Rings');
INSERT INTO books VALUES('The Scarlet Letter');
INSERT INTO books VALUES('Lord of the Flies2');
INSERT INTO books VALUES('Emma');
INSERT INTO books VALUES('Les Misérables');
INSERT INTO books VALUES('Anna Karenina');
INSERT INTO books VALUES('Hamlet');
INSERT INTO books VALUES('In Search of Lost Time');
INSERT INTO books VALUES('The Good Soldier');
INSERT INTO books VALUES('Lolita');
INSERT INTO books VALUES(' Oedipus at Colonus');

INSERT INTO autohor VALUES('Felicity','Clarke');
INSERT INTO autohor VALUES('Cherish','Miller');
INSERT INTO autohor VALUES('Esmeralda','Gunn');
INSERT INTO autohor VALUES('Shelby','Booth');
INSERT INTO autohor VALUES('George','Price');
INSERT INTO autohor VALUES('Maggie','Slater');
INSERT INTO autohor VALUES('Rae','Price');
INSERT INTO autohor VALUES('Kimberly','Addison');
INSERT INTO autohor VALUES('Kurt','Lane');
INSERT INTO autohor VALUES('Jack','Smith');
INSERT INTO autohor VALUES('Javier','Webster');
INSERT INTO autohor VALUES('Juliet','Nielson');
INSERT INTO autohor VALUES('Rosalee','Phillips');
INSERT INTO autohor VALUES('Sofie','Garner');
INSERT INTO autohor VALUES('Maia','Ebbs');
INSERT INTO autohor VALUES('Kurt','Lucas');
INSERT INTO autohor VALUES('Barney','Tate');
INSERT INTO autohor VALUES('Johnny','Norton');
INSERT INTO autohor VALUES('Carmella','Jobson');
INSERT INTO autohor VALUES('Taylor','Gilmore');

INSERT INTO subscribers(firstname,secondname) VALUES('Felicity','Clarke');
INSERT INTO subscribers (firstname,secondname)VALUES('Cherish','Miller');
INSERT INTO subscribers(firstname,secondname) VALUES('Esmeralda','Gunn');
INSERT INTO subscribers(firstname,secondname) VALUES('Shelby','Booth');
INSERT INTO subscribers(firstname,secondname) VALUES('George','Price');
INSERT INTO subscribers(firstname,secondname) VALUES('Maggie','Slater');
INSERT INTO subscribers(firstname,secondname) VALUES('Rae','Price');
INSERT INTO subscribers(firstname,secondname) VALUES('Kimberly','Addison');
INSERT INTO subscribers(firstname,secondname) VALUES('Kurt','Lane');
INSERT INTO subscribers(firstname,secondname) VALUES('Jack','Smith');
INSERT INTO subscribers(firstname,secondname) VALUES('Javier','Webster');
INSERT INTO subscribers(firstname,secondname) VALUES('Juliet','Nielson');
INSERT INTO subscribers(firstname,secondname) VALUES('Rosalee','Phillips');
INSERT INTO subscribers(firstname,secondname) VALUES('Sofie','Garner');
INSERT INTO subscribers(firstname,secondname) VALUES('Maia','Ebbs');
INSERT INTO subscribers(firstname,secondname) VALUES('Kurt','Lucas');
INSERT INTO subscribers(firstname,secondname) VALUES('Barney','Tate');
INSERT INTO subscribers(firstname,secondname) VALUES('Johnny','Norton');
INSERT INTO subscribers(firstname,secondname) VALUES('Carmella','Jobson');
INSERT INTO subscribers (firstname,secondname)VALUES('Taylor','Gilmore');

insert into subscriptions(sb_subscriber,sb_book,sb_is_active) values(1,5,1),
(2,6,0),
(3,7,0);
/*(4,8,1),
(5,9,1),
(6,10,0),
(1,11,0),
(12,13,1),
(15,14,1),
(1,14,1);*/

insert into m2m_books_authors(b_id,a_id) 
values(3,4);
/*(5,6),
(7,8),
(9,10),
(11,12),
(13,14),
(15,16),
(1,15),
(5,8),
(6,2),
(9,1);*/

insert into m2m_books_genres(b_id,g_id) values(1,2),
(3,4),
(5,6),
(7,8),
(9,1),
(11,2),
(13,4),
(15,6);
select b_name,firstname from m2m_books_authors
join books
on m2m_books_authors.b_id=books.b_id
join autohor
on m2m_books_authors.a_id=autohor.a_id

select b_name,g_name from m2m_books_genres
join books
on m2m_books_genres.b_id=books.b_id
join genres
on m2m_books_genres.g_id=genres.g_id


select firstname,secondname,b_name from subscriptions
join subscribers
on subscriptions.sb_subscriber=subscribers.s_id
join books
on subscriptions.sb_book=books.b_id
drop PROCEDURE if exists [S]
go
create procedure [dbo].[S]
@b_name varchar(50),
@firstname varchar(50),
@secondname varchar(50)
as
declare @b_id int
set @b_id=(
select b_id from books
where b_name=@b_name)
declare @s_id int
set @s_id=(
select s_id from subscribers
where firstname=@firstname and secondname=@secondname)
select @b_id;
select @s_id;
delete from subscriptions
where subscriptions.sb_book=@b_id and subscriptions.sb_subscriber=@s_id
go
EXEC [dbo].[S] 'Lord of the Flies','Maia','Ebbs'

--delete from autohor

drop PROCEDURE if exists [G]
go
CREATE PROCEDURE [dbo].[G]
@g_name varchar(50)
as

select g_id from genres
where g_name=@g_name
go
EXEC [dbo].[G] 'Romance'

drop PROCEDURE if exists [sp_CreateUser]
go
CREATE PROCEDURE [dbo].[sp_CreateUser]
    @name varchar(50),
    @secondname varchar(50)
   
AS
    INSERT INTO autohor(firstname, secondname)
    VALUES (@name, @secondname)
  
   
GO
EXEC [dbo].[sp_CreateUser] 'aaaaaa','bbbb'

select * from autohor
select * from books
select * from m2m_books_authors

--9
/*
 select count(sb_subscriber),b_name from subscriptions
 join books
 on books.b_id=subscriptions.sb_book
 group by b_name

 SELECT [autohor].[a_id],
 [autohor].[firstname],
 COUNT([sb_book]) AS [books]
FROM [autohor]
 JOIN [m2m_books_authors]
 ON [autohor].[a_id] = [m2m_books_authors].[a_id]
 LEFT OUTER JOIN [subscriptions]
 ON [m2m_books_authors].[b_id] = [sb_book]
GROUP BY [autohor].[a_id],
 [autohor].[firstname]
ORDER BY COUNT([sb_book]) DESC

SELECT [a_id],
 [secondname],
 MAX([genres_count]) AS [genres_count]
FROM (SELECT [autohor].[a_id],
 [autohor].[secondname],
 COUNT([m2m_books_genres].[g_id]) AS [genres_count]
 FROM [autohor]
 JOIN [m2m_books_authors]
 ON [autohor].[a_id] = [m2m_books_authors].[a_id]
 JOIN [m2m_books_genres]
 ON [m2m_books_authors].[b_id] = [m2m_books_genres].[b_id]
 GROUP BY [autohor].[a_id],
 [secondname],
 [m2m_books_authors].[b_id]
 HAVING COUNT([m2m_books_genres].[g_id]) >= 1) AS [prepared_data]
GROUP BY [a_id],
 [secondname]  

 SELECT [prepared_data].[a_id],
 [firstname],
 COUNT([g_id]) AS [genres_count]
FROM (SELECT DISTINCT [m2m_books_authors].[a_id],
 [m2m_books_genres].[g_id]
 FROM [m2m_books_genres]
 JOIN [m2m_books_authors]
 ON [m2m_books_genres].[b_id] = [m2m_books_authors].[b_id])
AS
 [prepared_data]
 JOIN [autohor]
 ON [prepared_data].[a_id] = [autohor].[a_id]
GROUP BY [prepared_data].[a_id],
 [firstname]
HAVING COUNT([g_id]) <= 1

SELECT AVG(CAST([books] AS FLOAT)) AS [avg_reading]
FROM (SELECT COUNT([sb_book]) AS [books]
 FROM [autohor]
 JOIN [m2m_books_authors]
 ON [autohor].[a_id] = [m2m_books_authors].[a_id]
 LEFT OUTER JOIN [subscriptions]
 ON [m2m_books_authors].[b_id] = [sb_book]
 GROUP BY [autohor].[a_id]) AS [prepared_data]

 select (case when sb_is_active = 1
 then 'no'
 else 'return'
 end) as a,count(*) from subscriptions
 group by (case when sb_is_active = 1
 then 'no'
 else 'return'
 end)

 select * from subscribers
 join subscriptions
 on subscribers.s_id=subscriptions.sb_subscriber
 join books
 on subscriptions.sb_book=books.b_id
 join m2m_books_genres
 on m2m_books_genres.b_id=books.b_id
 where g_id=1 

 select * from 
 (select count(sb_is_active) as active, subscribers.firstname from subscribers
 join subscriptions
 on subscriptions.sb_subscriber=subscribers.s_id
 where sb_is_active=1
 group by subscribers.firstname)as t1
 full join (select count(sb_is_active) as notactive, subscribers.firstname from subscribers
 join subscriptions
 on subscriptions.sb_subscriber=subscribers.s_id
 where sb_is_active=0
 group by subscribers.firstname)as t2
 on t1.firstname=t2.firstname
 


 select (case when sb_is_active = 1
 then 'no'
 else 'return'
 end) as a, count(distinct sb_book) from subscriptions
 group by sb_is_active

 select min(sb_is_active),secondname from subscribers
join subscriptions
on  subscribers.s_id=subscriptions.sb_subscriber
group by secondname

go
CREATE TRIGGER [upd_avgs_on_subscribers_ins_del]
ON [subscribers]
AFTER INSERT, DELETE
AS
 UPDATE [averages]
 SET [sb_is_active] = [active_count] / [subscribers_count]
  FROM (SELECT CAST(COUNT([s_id]) AS DOUBLE PRECISION)
 AS [subscribers_count]
 FROM [subscribers]) AS [tmp_subscribers_count],
 (SELECT CAST(COUNT([sb_id]) AS DOUBLE PRECISION)
 AS [active_count]
 FROM [subscriptions]
 WHERE [sb_is_active] = 'Y') AS [tmp_active_count],
 (SELECT CAST(COUNT([sb_id]) AS DOUBLE PRECISION)
 AS [inactive_count]
 FROM [subscriptions]
 WHERE [sb_is_active] = 'N') AS [tmp_inactive_count],
 (SELECT CAST(SUM(DATEDIFF( dd, [sb_start],[sb_is_active]))
 AS DOUBLE PRECISION) AS [days_sum]
 FROM [subscriptions]
 WHERE [sb_is_active] = 'N') AS [tmp_days_sum];  go
 CREATE TRIGGER [s_has_books_on_subscriptions_ins]
ON [subscriptions]
AFTER INSERT
AS
UPDATE [subscribers]
SET [s_book] = [s_book] + [s_new_books]
FROM [subscribers]
 JOIN (SELECT [sb_subscriber],
 COUNT([sb_id]) AS [s_new_books]
 FROM [inserted]
 WHERE [sb_is_active] = 'Y'
 GROUP BY [sb_subscriber]) AS [prepared_data]
 ON [s_id] = [sb_subscriber];
 go

 SELECT CAST(SUM(DATEDIFF( dd, [sb_start],[sb_is_active]))
 AS DOUBLE PRECISION) AS [days_sum]
 FROM [subscriptions]
 WHERE [sb_is_active] = 'N' 
 select * from subscriptions




 select distinct b_name from books
 join subscriptions
 on subscriptions.sb_book=books.b_id
 where sb_is_active=1;

 with [p_data](b_id,b_name,secondname) as(
 select  [books].[b_id],
 [b_name],
 autohor.secondname
 FROM [books]
 JOIN [m2m_books_authors]
 ON [books].[b_id] = [m2m_books_authors].[b_id]
 JOIN autohor
 ON [m2m_books_authors].[a_id] = autohor.a_id)
 select distinct [outer].b_name,
 STUFF(
 (SELECT ', ' +  [inner].[secondname]
 FROM [p_data] AS [inner] 
 WHERE [outer].[b_id] = [inner].[b_id]
 FOR XML PATH(''), TYPE)
 .value('.', 'nvarchar(max)'),1,1,'!')
  from [p_data] as [outer]
select * from books
for xml path('content'),type
 select * from subscribers
 join subscriptions
 on subscribers.s_id=subscriptions.sb_subscriber
 select * from subscribers
 where s_id in(
 select sb_subscriber from subscriptions)

 select * from subscribers
left join subscriptions
 on subscribers.s_id=subscriptions.sb_subscriber
 where sb_id is null
 select * from subscribers
 where s_id not in(
 select sb_subscriber from subscriptions)

 select * from subscribers
 join subscriptions
 on subscribers.s_id=subscriptions.sb_subscriber
 where sb_is_active=0

 select * from subscribers
 where s_id  in(
 select sb_subscriber from subscriptions
 where sb_is_active=0)

 select b_name from books
 where b_id in(
select b_id from m2m_books_genres
where g_id in(
 select g_id from genres
 where g_name in('Romance','Horror')))

 select * from subscriptions
 where  sb_is_active=0*/    