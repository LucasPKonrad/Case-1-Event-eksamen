DROP TABLE IF EXISTS [Registration];
DROP TABLE IF EXISTS [Event];
DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [NewsletterSubscriber];

-- USERS
CREATE TABLE [User] (
    [Id]           INT            NOT NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [Email]        NVARCHAR (200) NOT NULL,
    [PasswordHash] NVARCHAR (500) NOT NULL,
    [Role]         NVARCHAR (50)  NOT NULL,   -- 'Student' eller 'Admin'
      PRIMARY KEY CLUSTERED ([Id] ASC),
    );

    -- EVENTS
    CREATE TABLE [Event] (
    [Id]              INT            NOT NULL,
    [Title]           NVARCHAR (200) NOT NULL,
    [Description]     NVARCHAR (MAX) NOT NULL,  -- 'Fodbold', 'PC-spil', 'Brætspil'
    [StartTime]       DATETIME       NOT NULL,
    [EndTime]         DATETIME       NOT NULL,
    [MaxParticipants] INT            NOT NULL, 
     PRIMARY KEY CLUSTERED ([Id] ASC),
     );

     -- EVENTREGISTRATIONS
    CREATE TABLE [Registration] (
    [Id]           INT      NOT NULL,
    [EventId]      INT      NOT NULL,
    [UserId]       INT      NOT NULL,
    [RegisteredAt] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Registration_Event] FOREIGN KEY ([EventId]) REFERENCES [Event] ([Id]),
    CONSTRAINT [FK_Registration_User]  FOREIGN KEY ([UserId])  REFERENCES [User]  ([Id]),
    );

    -- NEWSLETTER SUBSCRIBERS
    CREATE TABLE [NewsletterSubscriber] (
    [Id]    INT            NOT NULL,
    [Email] NVARCHAR (200) NOT NULL,
    [SubscribedAt] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    );