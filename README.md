# innowise

To connect to the database, you need to have MS SQL LocalDB and enter the following commands into the package manager console

Update-Database -Project InnoShop.Users.Infrastructure -StartupProject InnoShop.Users.API -Verbose

Update-Database -Project InnoShop.Products.Infrastructure -StartupProject InnoShop.Products.API -Verbose
