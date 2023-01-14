# Introduction

School group project number 3: A maintenance program for devices in factory/enterprise premises. <br />
Made with Asp.Net Core MVC(.Net 6), C# and mySQL(database in azure cloud) <br />
<br />
What did I do?
I set the appsettings.json, launchSettings.json and program.cs files so that the program would work as required. <br />
Also installed needed packages and handled devops side(sprints, tasks, progress etc) <br />
I coded at least 50% of the login part of the program and also implemented session and guiding infos  <br />
Did 100% of the user control part of the program for both normal user and admin user. <br />
Did 100% of the inspections part of the program. <br />
Did 100% of the file upload and download. <br />
Did 100% of the bootstrap partial views <br />

# User guide
<br />

Admin login: admin_1 password: 1Admin1 <br />
User login: TwitterFan password: 1User3 <br />
<br />

The meaning of the program is that you can perform either scheduled periodic maintenances to a service object or perform a quick random inspection. <br />
Whenever you perform either one the above mentioned, the state of the service object targeted for the inspection/maintenance will change according to the outcome. <br />
You can also of course make your own periodic maintenance forms where you can add tests and define which tests should be passed in order to grant 'In Order' state. <br />
Obviously new service objects can also be made. <br />
It's possible to attach for example an image of the inspected object's problem when creating an inspection and when editing one. <br />
The image can be downloaded when clicking on the details button of the certain inspection. Image will load to temp folder if on Windows and to desktop if on Mac. <br />
<br />
It is highly recommended to move your mouse cursor on top of the info icons found on the site to get more guidance about the program.
