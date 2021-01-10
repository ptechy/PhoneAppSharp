Feature: NewSearch

  Background:
  Given I create the test contact with the RestApi
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | Aude      | COLOGNE  | 33          | 112      | 999997      |
   | Aude      | COLOGNE  | 33          | 112      | 999999      |
   | Aude      | COLOGNE  | 34          | 113      | 999998      |


@Search
@NewSearch
Scenario: Search for existing contact with last name
Given The test specifications
 | Description                                                | Result               |
 | Navigate to search page and search for an existing contact | The contact is found |
When I navigate to homepage
And I fill the search field with the value COLOGNE
And I click on the Go button
Then I check the number of line is equals to 3
And Check the Search field is filled with COLOGNE
And Check the values of all contact found


@Search
@NewSearch
Scenario: Search with no result
Given The test specifications
 | Description                                                        | Result                       |
 | Navigate to search page and launch a search that returns no result | The search returns no result |
When I navigate to homepage
And I fill the search field with the value ZZZZZZZ
And I click on the Go button
Then I check the number of line is equals to 0


@Search
@NewSearch
Scenario: Search by phone number an existing contact 
Given The test specifications
 | Description                                                          | Result                                                                     |
 | Navigate to search page and search by phone an existing phone number | The phone number and all others assocaited with the same contact are found |
When I navigate to homepage
And I fill the search field with the value 33 112 999997
And I click on the Go button
Then I check the number of line is equals to 1
And Check the Search field is filled with 33 112 999997



