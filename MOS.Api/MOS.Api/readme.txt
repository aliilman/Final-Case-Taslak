

1- migration olusturma 
Dbcontext in oldugu projede
    dotnet ef migrations add **** -s ../MOS.Api/ 


2- migration degisikliklerini db ye gecme yansitma guncelle migrate etme
Olusan migrationlarin calistirilmasi 
sln dizininde 
    dotnet ef database update --project "./MOS.Data" --startup-project "./MOS.Api"




--ortak proje yapisinda 
dotnet ef migrations add UniqueMigrationName
dotnet ef database update