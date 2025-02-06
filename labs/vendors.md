# Vendors

Do it just like yesterday. Go to https://services.hypertheory.com/lab-1-100

Click on "home", then hit the back button in your browser.

Problem: This is already in the repo.


## Solution 1:

Solution 1: Use the technique I showed you this morning, create a new set up tests for "Vendor2", and write your tests there instead.
Everywhere the `/vendors` resource is show, change it to `/v2/vendors`. 

So you will `POST` to `/v2/vendors`, and `GET` from `V2/Vendors`

If you create an entity class for the database, call it `Vendor2Entity`


## Solution 2:

Create a whole new Web API project. Put it in `/src/`, and recreate the ENTIRE thing from scratch.

We are practicing "extracting a microservice".

You can decide if you want to use controllers, or minimal API, or whatever.

Set it in the project properties to run at `http://localhost:1338".

You can even copy the tests from the first project into the new project, if you like.


