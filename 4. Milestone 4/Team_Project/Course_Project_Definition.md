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
    1.Not in any particular format
    2.
    3.

## Initial Modeling
    Diagrams

### Use Case Diagrams
    Diagrams

### Sequence Diagrams

### Other Modeling
    Diagrams, UI wireframes, page flows, ...

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
3. [E] 
    1. [U]
        a. [T]
        b. [T]
    2. [U]
        a. [T]

## Initial Architecture Envisioning
    Diagrams and drawings, lists of components

## Agile Data Modeling
    Diagrams, SQL modeling (dbdiagram.io), UML diagrams

## Timeline and Release Plan
    Schedule: meaningful dates, milestones, sprint cadence, how releases are made (CI/CD, or fixed releases, or ?)