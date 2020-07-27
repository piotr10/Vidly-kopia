namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'610778dc-295f-4eb5-a9e5-5280685388da', N'guest@gmail.com', 0, N'ABKDHMjfI7dvKUQ++UvGCdU+Ywv5ULHII88dhhwBVc8pxze2ql4/2oi+FKEiBbwSYw==', N'49f6595d-27ea-4382-a2ec-327ac02f3ca7', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a8f95cfb-87d6-4b16-8747-04bd65921678', N'admin@gmail.com', 0, N'ALSr+9I7cjBOVSoiD1pVQ/YkD79974wo3EGAced+fDTEb47dcVCuczUhis3l2zg1LQ==', N'c2cf036d-f7c3-43bf-943f-2dc11e6ad5f9', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'276042e8-c11b-48c5-b1be-b58788cc7055', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a8f95cfb-87d6-4b16-8747-04bd65921678', N'276042e8-c11b-48c5-b1be-b58788cc7055')


");
        }
        
        public override void Down()
        {
        }
    }
}
