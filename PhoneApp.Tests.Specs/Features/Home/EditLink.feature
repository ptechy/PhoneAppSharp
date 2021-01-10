Feature: EditLink

@Home
Scenario: Check PhoneBookApp link
Given The test specifications
 | Description                                     | Result                           |
 | Navigate to homepage to verify honeBookApp link | The PhoneBookApp link is present |

When I navigate to homepage
And I click on PhoneBookWebApp link
Then I check the PhoneBookApp link is correct


@Home
Scenario: Check Home link
Given The test specifications
 | Description                              | Result                   |
 | Navigate to homepage to verify Home link | The Home link is present |
When I navigate to homepage
And I click on the home link 
Then I check the Home link is correct