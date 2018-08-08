Create table Stadium (
	ID number(10) primary key,
	NAME varchar2(300),
	CAPACITY number(10),
	CITY varchar2(300),
	COUNTRY varchar2(300),
	DESCRIPTION varchar2(300)
);

create sequence Stadium_SEQ;