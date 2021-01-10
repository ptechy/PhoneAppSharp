Feature: EditExistingEntry

  Background:
  Given I create the test contact with the RestApi
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | Aude      | COLOGNE  | 33          | 112      | 999997      |


@NewEntry
@EditExistingEntry
Scenario: Search for existing contact and edit it with link

Given The test specifications
 | Description                                                                                     | Result                             |
 | Navigate to search page, search for an existing contact and edit the contact with the html link | All the contact values are correct |
When I navigate to homepage
And I fill the search field with the value COLOGNE
And I click on the Go button
Then I check the number of line is equals to 3
When I click on the edit link
Then I check all the test contact values are correct


@NewEntry
@EditExistingEntry
Scenario: Search for existing contact and edit it with the icon

Given The test specifications
 | Description                                                                                 | Result                             |
 | Navigate to search page, search an existing contact and edit the contact with the edit icon | All the contact values are correct |
When I navigate to homepage
And I fill the search field with the value COLOGNE
And I click on the Go button
Then I check the number of line is equals to 1
When I click on the edit icon
Then I check all the test contact values are correct



@NewEntry
@EditExistingEntry
Scenario: Search for existing  contact, edit it and change the first name

Given The test specifications
 | Description                                                                                     | Result                                   |
 | Navigate to search page, search an existing contact, edit the contact and change the first name | The new first name is taken into account |

When I navigate to homepage
And I fill the search field with the value COLOGNE
And I click on the Go button
Then I check the number of line is equals to 1

Given I update the test contact
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | ZZZZZZ    | COLOGNE  | 33          | 112      | 999999      |
When I click on the edit link
And I fill the form with all the test contact values
And I click on the submit button
And I click on the confirm button
Then I check the contact is correctly created


@NewEntry
@EditExistingEntry
Scenario: Search for existing contact and delete it wWith the delete icon

Given The test specifications
 | Description                                                                                | Result                           |
 | Navigate to search page, search for an existing contact and delete it with the delete icon | The contact is correctly deleted |
When I navigate to homepage
And I fill the search field with the value COLOGNE
And I click on the Go button
Then I check the number of line is equals to 1
When I click on the delete icon
When I confirm the deletion
Then I check the number of line is equals to 0


@NewEntry
@EditExistingEntry
Scenario: Search for existing contact and delete it with the delete button

Given The test specifications
 | Description                                                                                  | Result                           |
 | Navigate to search page, search for an existing contact and delete it with the delete button | The contact is correctly deleted |
When I navigate to homepage
And I fill the search field with the value COLOGNE
And I click on the Go button
Then I check the number of line is equals to 1
When I click on the delete button
When I confirm the deletion
Then I check the number of line is equals to 0
