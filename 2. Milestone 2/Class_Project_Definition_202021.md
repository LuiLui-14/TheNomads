Project Inception Template
=====================================

## Summary of Our Approach to Software Development

    What processes are we following? ==> Disciplined Agile Delivery, Scrum, ...  What are we choosing to do and at what level of detail or extent?

## Initial Vision Discussion with Stakeholders
Summarize what was discussed.  What do they want?

Here's the idea for this year.  It's a familiar one so you'll have a head start.  These **Hypothetical** ideas came from an informal discussion with the primary stakeholder:

[The Himalayan Database](https://www.himalayandatabase.com/) wishes to upgrade their website and make it a real web application where expeditions can submit their data far more easily.  In addition they wish to add features that leverage their data.  There's so much information in there that people could make use of that is now only possible for people with database or programming skills.  For example:
1. The public could browse or search for expeditions, climbers, dates, mountains, heights, etc. to learn more about them in a way that is far more user friendly than the existing features at /online.html.  
    - School children could gather data for class projects about the number of times Everest has been climbed within the last 25 years.
    - News organizations or book authors could gather information for their writing, such as for the deadly 1996 climbing season that prompted Jon Krakauer to write *Into Thin Air*.
    - Climbers could find unclimbed mountains, or peaks that haven't been climbed during Winter
    - and so much more
2. Expedition providers could much more easily enter their climbs and members into the database.  They might also be able to submit requests to update or correct previous entries.
3. Employees of The Himalayan Database could administer the site.
4. The site could be much more user friendly, showing dynamic information on the front page.
5. It would be great if it could serve as a hub for people to report on recent climbs or news.  To make it more trustworthy than other sources, they can utilize their relationships with famous climbers and outfitters.  i.e. this can be a trusted resource.
6. And lots more!

The data we used for the final last term came from: [rfordatascience/tidytuesday](https://github.com/rfordatascience/tidytuesday/blob/master/data/2020/2020-09-22/readme.md)

### List of Stakeholders and their Positions
    Who are they? Why are they a stakeholder?

## Initial Requirements Elaboration and Elicitation

### Elicitation Questions
#### Elicitation
    1. Is the goal or outcome well defined?  Does it make sense?
    2. What is not clear from the given description?
    3. How about scope?  Is it clear what is included and what isn't?
    4. What do you not understand?
        * Technical domain knowledge
        * Business domain knowledge
    5. Is there something missing?
    6. Get answers to these questions.
#### Analysis
Go through all the information gathered during the previous round of elicitation.  

    1. For each attribute, term, entity, relationship, activity ... precisely determine its bounds, limitations, types and constraints in both form and function.  Write them down.
    2. Do they work together or are there some conflicting requirements, specifications or behaviors?
    3. Have you discovered if something is missing?  
    4. Return to Elicitation activities if unanswered questions remain.

### Elicitation Interviews
    Transcript or summary of what was learned

### Other Elicitation Activities?
    As needed

## List of Needs and Features
	1. Browse and search for expeditions, climbers, dates, etc. including the ability to refine searches.
	2. Expedition providers could much more easily enter, or update climbs and members into the database.
	3. Employees can administer the site.
	4. Frontpage could include dynamic information, for example, the last three expeditions.
	5. Climbers could have personal accounts to show which mountains they have climbed and when.
	6. Users could report on climbing related news.
	7. Expedition providers could include a schedule of open expeditions they have upcoming for climbers to see.
	8. Filters and features climbers based on their location, sex, mountains they climbed, etc. This can allow specific climbers to meet up and discuss their adventures.
	9.Classify and categorize agencies with their hikers to seek who has more hikers. Who has climbed the highest peaks a certain number of times. This can create competitiveness within agencies.
	10.Just like number 9, we can classify and categorize nations with their hikers. This can help us identify who has climbed the highest peaks a certain number of times. This can create competitiveness within nations to see who has the best of the best hikers.
	11.Show injuries by mountain, climber, nation, etc.


## Initial Modeling
    https://github.com/ApneaMan29/TheNomads/blob/main/2.%20Milestone%202/Diagrams/Himalayan%20Database.pdf

### Use Case Diagrams
    https://github.com/ApneaMan29/TheNomads/blob/main/2.%20Milestone%202/Use_Case_Diagram.png

### Sequence Diagrams

### Other Modeling
    https://github.com/ApneaMan29/TheNomads/blob/main/2.%20Milestone%202/Diagrams/visual%20deployment%20diagram.png

## Identify Non-Functional Requirements
1. English is the default language, but we must support visitors and data in other character sets
2.
3.

## Identify Functional Requirements (In User Story Format)

E: Epic  
U: User Story  
T: Task  
1. [U] As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.
   1. [T] Create starter ASP dot NET Core MVC Web Application with Individual User Accounts and no unit test project
   2. [T] Choose CSS library (Bootstrap 4, or ?) and use it for all pages
   3. [T] Create nice bare homepage: write initial welcome content, customize navbar, hide links to login/register
   4. [T] Create SQL Server database on Azure and configure web app to use it. Hide credentials.
   5. [T] Deploy it on Azure
2. [U] As someone who wishes to submit new information on an expedition I would like to be able to register an account so I will be able to use your services (once they're built)
   1. [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP, DOWN scripts
   2. [T] Configure web app to use our db with Identity tables in it
   3. [T] Create a user table and customize user pages to display additional data
   4. [T] Re-enable login/register links
   5. [T] Manually test register and login; user should easily be able to see that they are logged in
3. [U] As a user on this webpage I want to be able to search through the information for expeditions, climbers, mountains, or other information and be able to refine my searches.
   1. [T] Add search bar to homepage with selection list for type of information to search
   2. [T] Add search result page that displays information in easy to read manner
   3. [T] Include pull down menus and other refining means to limit the returned results at users input
4. [U] As a user on this webpage I want to be able to report relevant news.
   1. [T] Add new page that displays news that has been reported by users in easy to read manner
   2. [T] Add new page where users can fill out form to report news to the website
   3. [T] Link these pages to home page and each other in easy to use manner
5. [U] As a hiker using this website I want to be able to have a personal account to show which expeditions I have been on and which mountains I have climbed.
   1. [T] Enable login feature that shows if a user is logged in or not
   2. [T] Add new personal page that shows that users information including statistics on expeditions.
6. [U] As a hiker using this website I want to be able to search for hikers who have qualities that I am seeking, such as location, gender, or which mountains they have climbed, so that I can contact them.
   1. [T] Create search page for hikers who are logged in to search for other hikers.
   2. [T] Create the approriate form to allow search by location, gender, mountain, or trekking agency.
   3. [T] Create result page to show the results of the search in an easy to read manner.
7. [U] As a Trekking agency I want to be able to create a calender to record dates for future and/or past expeditions.
   1. [T] Create calender that will show registered trekking agencies scheduled expeditions.
   2. [T] Create form for trekking agency to schedule expeditions.
   3. [T] Place calender on home page and trekking agency account pages.
8. [U] As a registered user (hiker or trekking agency) I want to have statistics that classify and/or catergorize me so that I may be able to see my standing amoung other users.
   1. [T] Create statistics area to user pages that shows list of notable statistics, such as, highest peak climbed, number of climbs, etc.
   2. [T] Create button on user page that allows user to share their statistics with other users.
   3. [T] Add ranking list on home page that shows users ranked by statistics.
9. [U] As a user to this webpage I would like to see which mountain has the most injuries.
   1. [T] Create form where registered users can upload known injuries, to include which mountain, what date, how far up the mountain, etc. 
   2. [T] Add area on homepage that shows recent injuries.
   3. [T] Add injuries to search function.
10. [U] As an employee to this webpage I would like the ability to moderate the information coming into the webpage. 
   1. [T] Create employee login account, and page.
   2. [T] Give employee access to moderate users and news articles. 

## Initial Architecture Envisioning
    Diagrams and drawings, lists of components

## Agile Data Modeling
    Diagrams, SQL modeling (dbdiagram.io), UML diagrams

## Timeline and Release Plan
    Schedule: meaningful dates, milestones, sprint cadence, how releases are made (CI/CD, or fixed releases, or ?)

## Vision Statement

   For users who need to search for information regarding hikers, expeditions, the variety of peaks, and the many trekking agencies, The Himalayan Database is an informational system that provides a single point of access to users, climbers, and trekking agencies. This system stores hiker information, nationality, expeditions, peaks, and it displays the many different types of trekking agencies. The purpose of this website is to connect people with necessary information such as for school projects, authors or news agencies in need of information, climbers in search of different peaks, and even to bring about competitiveness amongst agencies and nations to see who has the best climbers and the overall best records. Unlike the typical hiker information websites floating around, our website aims to provide more robust features that connect hikers, users, and agencies together into one platform. This universal foundation makes it incredibly easy to store and keep track of information but at the same time, allows us to create a vast variety of features because of the information available to us.
