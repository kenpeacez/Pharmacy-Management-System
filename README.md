Pharmacy Management System

Developed using WinForms as the frontend and VB.NET as the backend in Visual Studio 2022.
Optimized for use in Malaysia Country.
This project is under active development as of May 2024, and will receive updates and bug fixes until September 2024.

This project is developed to showcase my skills in software development. It is now being used live in clinic to manage patient data, print labels without requiring internet connection to function. Only available in Windows PC.

Main Features
- Ability to save patients basic information such as Name, Identification (IC No)
- Ability to store data in MYSQL database (MYSQL from XAMPP)
- Validations check for IC formatting No to prevent errors
- Patient medications entry and store
- Patient history medications view with overwrite function
- Add, view, seach, update and store medications infomation in database from application
- Ability to print medications labels based on selected medications, optimized for Label Size 80mm x 50mm (height x width) and customizable

Pictures of Application
![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/0bdbd3b0-8f62-4e03-8dc9-a51d5f4e54dd)
![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/14041a7e-8be2-4abb-854a-e87e13a9cdd6)
![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/36c55beb-55b4-4670-a9eb-86cbbc8517d5)
![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/da965863-4676-42ba-a3c3-f98a468c573d)
![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/457d4e1e-7ba2-4524-80c9-99f4b1e0b59e)

Print Labels 


![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/a0b166f5-1a73-4f97-ba9e-edfa4b3ca124)
![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/42ac4c0d-0bda-40e6-aada-8d7ffc2eac04)


MYSQL Database Installation Guide

1. Download XAMMP 8.2.12 installation file from https://sourceforge.net/projects/xampp/files/XAMPP%20Windows/8.2.12/xampp-windows-x64-8.2.12-0-VS16-installer.exe
2. Install the XAMMP, include phpMyAdmin & MYSQL modules in the installation packages
3. Make sure to disable Windows UAC (User Account Control) to get XAMMP Control Panel working properly.
4. Run the XAMMP Control Panel, press the RED X on the Apache Module and MySQL Modules to install the services, after it should look like this
   ![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/cba23cbd-d40c-4c3e-9323-98cc8af7c17c)
5. Start the Apache Module service, wait 3 seconds for it to finish. Then start the MySQL service
6. Press on the Admin on Apache Module, this will open your web browser to phpMyAdmin.
7. Find the Import Menu as shown in the picture, browse for the database_pharmacy.sql file which can be downloaded from this Github page
   ![image](https://github.com/kenpeacez/Pharmacy-Management-System/assets/28534332/4f9684a5-13f2-4bdb-b362-6e8a674c19a8)
8. Import the .sql file, it will display some error and that is normal. Just ignore it.
9. Open the Pharmacy Management System application to start using it.


