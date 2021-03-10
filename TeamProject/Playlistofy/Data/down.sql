ALTER TABLE [AspNetRoles] DROP CONSTRAINT [PK_AspNetRoles];
ALTER TABLE [AspNetUsers] DROP CONSTRAINT [PK_AspNetUsers];
ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [PK_AspNetRoleClaims];
ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [PK_AspNetUserClaims];
ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [PK_AspNetUserLogins];
ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [PK_AspNetUserRoles];
ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [PK_AspNetUserTokens];
ALTER TABLE [Playlist] DROP CONSTRAINT [PK_Playlist];
ALTER TABLE [Track] DROP CONSTRAINT [PK_Track];


DROP Table [Playlist];
DROP Table [Track];
DROP Table [__EFMigrationsHistory];
DROP Table [AspNetRoles];
DROP Table [AspNetUsers];
DROP Table [AspNetRoleClaims];
DROP Table [AspNetUserClaims];
DROP Table [AspNetUserLogins];
DROP Table [AspNetUserRoles];
DROP Table [AspNetUserTokens];