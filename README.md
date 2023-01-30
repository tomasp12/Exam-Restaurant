# Exam-Restaurant
The program is designed to seat customers at a table in a restaurant and take their order.
The number of customers is described in an array that is read in a circle.
1. menu item, showing how many customers came, then showing all the tables, their status, numbers, places.
The program suggests which table to choose, but the operator can decide for himself.
After selecting a table, an initial order is created, which records the date, table number, number of customers.
2. menu item allowed to cancel the table reservation if the customer changed his mind.
3. menu item shows the operator the available menu, which consists of two parts: food and drinks. The menu can be expanded after creating new classes, all of their control is done through the interface.
4. the order is entered in the menu item, a table is selected and the selected items are entered in the order. If you want to add dishes or drinks to your order, you can go to menu 4 and choose what you want. As the selection ends, the entire order is displayed.
After completing the order, the total amount of the order is calculated and saved
5. payment of the order, we select the table, the amount to be paid is shown, and we are asked if we want to send an email. Then, a check is created from the order, which is written in a text file, if an email address was entered, an email containing the content of the check is sent to that address.
The database is implemented in SQLite database, where 5 tables are created.
