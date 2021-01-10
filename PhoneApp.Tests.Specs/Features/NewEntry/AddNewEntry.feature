Feature: AddNewEntry


# we use the same contact with 2 different mobile phones
  Background:
  Given I use the following test contact
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | Aude      | COLOGNE  | 33          | 112      | 999999      |
   | Aude      | COLOGNE  | 34          | 113      | 999998      |

@NewEntry
@AddNewEntry
Scenario: Create a new entry
Given The test specifications
 | Description                                           | Result                           |
 | Navigate to AddNewEntry page and create a new contact | The contact is correctly created |
And I remove the test contact inside the RestApi 
When I navigate to homepage
And I click on te AddNewEntry button
And I fill the form with all the test contact values
And I click on the submit button
Then I check the contact is correctly created


@NewEntry
@AddNewEntry
Scenario: Create an existing entry
Given The test specifications
 | Description                                                        | Result                                                                               |
 | Navigate to AddNewEntry page and try to create an existing contact | The contact is not created because the contact is already present insie the database |

And I create a test contact inside the RestApi 
When I navigate to homepage
And I click on te AddNewEntry button
And I fill the form with all the test contact values
And I click on the submit button
Then I check the contact is not created with an error message


@NewEntry
@AddNewEntry
Scenario: Create  a new entry with empty first name
Given The test specifications
 | Description                                                                    | Result                                                |
 | Navigate to AddNewEntry page and create a new contact with an empty first name | The contact is created because first name is optional |

And I remove the test contact inside the RestApi 
And I update the test contact
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   |           | COLOGNE  | 33          | 112      | 999999      |
   |           | COLOGNE  | 34          | 113      | 999998      |

When I navigate to homepage
And I click on te AddNewEntry button
And I fill the form with all the test contact values
And I click on the submit button
Then I check the contact is correctly created




@NewEntry
@AddNewEntry
Scenario: Create a new entry with empty last name
Given The test specifications
 | Description                                                                          | Result                     |
 | Navigate to AddNewEntry page and try to create a new contact with an empty last name | The contact is not created because last name is mandatory |

And I remove the test contact inside the RestApi 
And I update the test contact
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | Aude      |          | 33          | 112      | 999999      |
   | Aude      |          | 34          | 113      | 999998      |

When I navigate to homepage
And I click on te AddNewEntry button
And I fill the form with all the test contact values
And I click on the submit button
Then I check the contact is not created with LastName field error





@NewEntry
@AddNewEntry
Scenario: Create a new entry with empty country code
Given The test specifications
 | Description                                                                             | Result                                                       |
 | Navigate to AddNewEntry page and try to create a new contact with an empty country code | The contact is not created because country code is mandatory |

And I remove the test contact inside the RestApi 
And I update the test contact
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | Aude      | COLOGNE  |             | 112      | 999999      |

When I navigate to homepage
And I click on te AddNewEntry button
And I fill the form with all the test contact values
And I click on the submit button
Then I check the contact is not created with CountryCode field error




@NewEntry
@AddNewEntry
Scenario: CreateNewEntryWithEmptyAreaCode
Given The test specifications
 | Description                                                                          | Result                                                    |
 | Navigate to AddNewEntry page and try to create a new contact with an empty area code | The contact is not created because area code is mandatory |

And I remove the test contact inside the RestApi 
And I update the test contact
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | Aude      | COLOGNE  | 33          |          | 999999      |

When I navigate to homepage
And I click on te AddNewEntry button
And I fill the form with all the test contact values
And I click on the submit button
Then I check the contact is not created with AreaCode field error



@NewEntry
@AddNewEntry
Scenario: CreateNewEntryWithEmptyPhoneNumber
Given The test specifications
 | Description                                                                             | Result                                                       |
 | Navigate to AddNewEntry page and try to create a new contact with an empty phone number | The contact is not created because phone number is mandatory |

And I remove the test contact inside the RestApi 
And I update the test contact
   | FirstName | LastName | CountryCode | AreaCode | PhoneNumber |
   | Aude      | COLOGNE  | 33          | 112      |             |

When I navigate to homepage
And I click on te AddNewEntry button
And I fill the form with all the test contact values
And I click on the submit button
Then I check the contact is not created with PhoneNumber field error