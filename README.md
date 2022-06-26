# Sprout.Exam.WebApp

THINGS TO IMPROVE
Tech Debt in React:
1. Convert to typescript. Typescript is an OOP language. It also supports interfacing.
2. Centralize grid and form component:
   - Now, we create <form/>, <td>, <tr> every page
   - We could create column definitions that is passed to grid or form component.
   - This definitions will have properties like FieldName, IsRequired, DataType.
   - Then form component will create the input field based on the column definition. Same with grid component
   - We could also pass GET, POST, PUT requests as a prop to form/grid component.
   - Then move checking required fields to form component. It will ust the column defintion property IsRequired.
   - With this, when adding new page, devs would only be doing is create new page, import the form/grid component and create the column definition for their page.

Tech Debt in C#:
1. Now in class that calculates salary, we can add other types of employees and their salary computation.
  But the only values that I pass are the rate and days (absent days/worked days). 
  Then the other variable like tax in RegularSalary class are hard coded. What if we want the tax to be inputted by user? We'd have to change CalculateSalaryCreator.
  Maybe we could add new creator for salary properties like Tax, Rate, etc..
  
 Architectural design 
 1. We could create a Sql Server database project in vs studio for continuous integration and continuous deployment
