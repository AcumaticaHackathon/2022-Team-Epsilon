use hackathon2022

DROP TABLE ExternalFile;
DROP TABLE FileEntityMapping;
DROP TABLE FileServicePreferences;

CREATE TABLE ExternalFile (
    CompanyId int NOT NULL,
    RefNoteId uniqueidentifier NOT NULL,
    FileName nvarchar(256) NOT NULL,
    Path nvarchar(256) NOT NULL,

);

CREATE TABLE FileEntityMapping (
    CompanyId int NOT NULL,
    Active bit NOT NULL DEFAULT(0),
    Entity varchar(4) NOT NULL,
    Mapping nvarchar(512) NULL,
);

CREATE TABLE FileServicePreferences (
        CompanyId int NOT NULL,
        PluginType nvarchar(516) NULL
);