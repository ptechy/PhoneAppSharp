
# PhoneAppSharp

This CSharp solution was built for a POC on a web site that is no longer available
The goal was to perform an automating testing of a phone application (and to find bugs) with any automation tools


Workspace
============
The choosen automated tools are Selenium, Nunit and Specflow

There are 3 projects inside the solution :
* PhoneApp.Core: project that contains the common libraries
* PhoneApp.Tests: it is the automation project with Selenium and Nunit
* PhoneApp.Tests.Specs: it is the automation project with Selenium and Specflow

The Report folder contains the html test result of the launched campaign



How to build
============
* Use Visual Studio or Visual Code 
* Install Dotnet 5, it is mandatory to run this solution
* Launch in a command line: dotnet restore


Limitations
============
* By design the test are not runnable in parrallel
* The tests are only available for Google Chrome


Specifications
============

Phonebook application helps the user manage his/her contacts
The application handles a set of entries, that contain a first name, last
name, and a telephone number like the example:

First name | Last name | Phone number
------------ | ------------- | -------------
Teddy   | Bear  | +39 02 1234567 


* A contact can have more than one contact phone, but a contact phone
can only belong to one contact.
* The combination of first and last name is unique (i.e. we cannot have two
contacts with the same last name)
* The combination of country code, area code and phone number is unique.
* The entries should be validated, so that it's not possible to enter an empty
last name or phone number. The first name is optional.
* Both the first name and last name fields cannot accept any symbols 1
except for the dash (-) and the space.
* The length of the first name should not exceed 20 characters and that of
the last name should not exceed 40 characters.
* The phone number should be of the form: +39 02 1234567. That is a "+"
followed by a nonempty group of digits, a space, a nonempty group of
digits, a space, a group of digits with at least 6 digits.

The application consists of the following pages:

**Home page**
The page contains a text field that allows to search through all the entries by
name (first name or last name) or number and a button that submits the search
query. The results are displayed in a table form below the search text field. Each
time I submit my search query, the table with the results is updated containing
all the entries that match the text I entered.
The page contains a link to the "Add new entry" page. When an entry is
displayed, it contains a button to the “Edit an entry” page and a Delete button

**Add new entry**
This page contains an empty form where the user can insert the data of a new entry. 
The user can:
1. add a new contact and his/her phone number 
2. add a new phone number for an existing contact.
The form will be composed of the following elements:
* Input text field for the first name
* Input text field for the last name
* Input text field for the country code 
* Input text field for area code
* Input text field for phone number
* A "Clear All" button and a “Submit” button


**Edit new entry**
This page contains a form pre-filled with the contact entry details. The user can modify the data and re-submit the changes.
The form will be composed of the following elements:
* Input text field for the first name
* Input text field for the last name
* Input text field for the country code ( 32, 33)
* Input text field for area code
* Input text field for phone number
* A "Revert all changes" button and a "Submit" button

(c) 2021 Philippe TECHY